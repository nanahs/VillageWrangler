using UnityEngine;
using System.Collections;

public class VillagerController : MonoBehaviour {

	public float walkSpeed = 5f;
	public Vector3 walkDirection;
	public float walkDirMagnitude;

	public float groundBounce = 5f;

	Rigidbody rb;

	public bool isHeld = false;
	public bool canMove = true;
	public bool isInAir = false;
	public bool isCaged = false;

	GameObject villageSpawner;

	Animator anim;
	bool isFacingRight = true;

    public ParticleSystem m_DropParticle = null;
    public ParticleSystem m_TrailParticle = null;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
		anim = transform.GetChild(0).GetComponent<Animator>();
		villageSpawner = GameObject.FindGameObjectWithTag("VillageSpawner");
		InvokeRepeating("changeDirection", 0f, 5f);
	
	}
	
	// Update is called once per frame
	void Update () {

		checkDirection();

		walkDirMagnitude = walkDirection.magnitude;


		anim.SetBool("isInAir", isInAir);
		anim.SetBool("isHeld", isHeld);
		anim.SetBool("canMove", canMove);

		if(walkDirMagnitude > 30f){
			anim.SetBool("isWalking", true);
		}else{
			anim.SetBool("isWalking", false);
		}

		if(canMove){
			rb.velocity = walkDirection*Time.deltaTime;
		}
	}

	private void changeDirection(){


		if(!canMove){
			return;
		}


		if(isHeld){
			
		}else{

			float temp = rb.velocity.y;

			walkDirection = Random.insideUnitSphere.normalized * walkSpeed;
			walkDirection.y = temp;
		}

	}

	private void cagedChangeDirection(){
		float temp = rb.velocity.y;

		walkDirection = Random.insideUnitSphere.normalized * walkSpeed;
		walkDirection.y = temp;
	}

	public void thrown(){
		setHoldState(false);
		isInAir = true;
	}

	private void hitGround(){

		isInAir = false;

        if (m_DropParticle)
        m_DropParticle.Play();

        if (m_TrailParticle)
            m_TrailParticle.Play();

		rb.AddForce(Vector3.up * groundBounce);

		Invoke("villagerCanMove", 3f);
		InvokeRepeating("changeDirection", 3f, 5f);
		rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

	}

	public void setHoldState(bool held){
     
		isHeld = held;
		if(isHeld){
			canMove = false;
			walkDirection = Vector3.zero;
			CancelInvoke("changeDirection");
			CancelInvoke("villagerCanMove");
			//Debug.Log("stopped changing direction");

			rb.constraints = RigidbodyConstraints.FreezeRotation;
		}

        //stop trail particle in air
        m_TrailParticle.Stop();
	}

	private void villagerCanMove(){
		canMove = true;
	}

	void OnCollisionEnter(Collision col){

		if(col.collider.tag == "Ground" && isInAir){
			hitGround();
		}

	}

	void OnDestroy(){

		if(villageSpawner){
			villageSpawner.GetComponent<VillagerSpawner>().villagerDied();
		}
			
	}

	void checkDirection(){
		if(walkDirection.x >= 0 && !isFacingRight){
			FlipSpriteX();
		}else if(walkDirection.x < 0 && isFacingRight){
			FlipSpriteX();
		}
	}

	void FlipSpriteX(){
		isFacingRight = !isFacingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;

		transform.localScale = scale;
	}
}
