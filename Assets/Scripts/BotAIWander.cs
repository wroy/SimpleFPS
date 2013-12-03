using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class BotAIWander : MonoBehaviour {
	
	public float animSpeed = 1.5f; // a public setting for overall animator animation speed
	public float lookSmoother = 3f; // a smoothing setting for camera motion 
	
	public Transform player; //the player
	
	private Animator anim; // a reference to the animator on the character
	private AnimatorStateInfo currentBaseState; // a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState; // a reference to the current state of the animator, used for layer 2
	
	float distanceFromPlayer; //distance the bot is from the player
	
	public GameObject bullet; //the projectile the bot fires
	float bulletSpeed = 75f; //speed of the projectile
	float bulletDamage = 50.0f; //the amount of damage it does
	float DoubleDamageTimer = 0.0f; //counts how long the bot has double damage for, not implemented
	
	public AudioClip throwing; //audio clip of the projectile being thrown
	public AudioClip double1; //audio clip the bot would play on double damage pick up
	
	public GameObject bulletSpawn; //where the projectile is spawned, avoids getting caught it bot's mesh
	public float shootTimer = 0.0f;//amount of time till bot's next shot
	
	Vector3 PlannedDestination;	//where the bot is planning to go

	void Start ()
	{
		anim = GetComponent<Animator>(); // sets the bots animations
		PlannedDestination = GetComponent <NavMeshAgent>().destination; //gives the bot a plan
	}
	
	
	void Update ()
	{
		//look towards the player and determine distance between bot and player
		transform.LookAt(PlannedDestination);
		distanceFromPlayer = Vector3.Distance(player.position, gameObject.transform.position);
		
		if (distanceFromPlayer < 5.0f){
			//if bot is within 5 units, stop moving closer and shoot at player
			transform.LookAt(player);
			GetComponent <NavMeshAgent>().destination = gameObject.transform.position;
			shoot ();
		}
		else if (distanceFromPlayer < 10.0f){
			// if the bot is between 5 and 10 units, move closer and shoot at player
			shoot ();
			PlannedDestination = player.position;
			GetComponent <NavMeshAgent>().destination = PlannedDestination; // makes the bot follow player
			transform.LookAt(player);
		}
		else{
			//if the bot is greater the 10 units from the player
			//consider a new location
			if (Vector3.Distance(PlannedDestination, gameObject.transform.position) < 5.0){
				//if bot is close to planned location, pick a new location to move to
				int wander = Random.Range(0, 6);
				if(wander == 0) 
					PlannedDestination = new Vector3 (-30.96512f, 0.1f, 31.82335f);
				else if(wander == 1) 
					PlannedDestination = new Vector3 (-36.40741f, 0.1f, -35.84986f);
				else if(wander == 2) 
					PlannedDestination = new Vector3 (31.69841f, 0.1f, -35.84986f);
				else if(wander == 3) 
					PlannedDestination = new Vector3 (33.04593f, 0.1f, 36.00597f);
				else if(wander == 4) 
					PlannedDestination = new Vector3 (22.49821f, 0.1f, 3.456315f);
				else  
					PlannedDestination = new Vector3 (-28.9175f, 0.1f, -0.8987858f);
				GetComponent <NavMeshAgent>().destination = PlannedDestination;
			}	
		}
		
		shootTimer -= Time.deltaTime;//reloading
		
		float h = Input.GetAxis("Horizontal");// setup h variable as our horizontal input axis
		float v = 1.0f;	// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); // set our animator's float parameter 'Direction' equal to the horizontal input axis		
		anim.speed = animSpeed;	// set the speed of our animator to the public variable 'animSpeed'
	}
	
	/**
	 * handles the bot's shooting
	 */
	void shoot(){
		if (shootTimer <=0){
			//if the bot is reloaded then shoot
			shootTimer = 1.0f;
			GameObject deathBullet = (GameObject)Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
			deathBullet.GetComponent<Bullet>().damage = bulletDamage;
			deathBullet.rigidbody.AddForce(bulletSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);
			audio.PlayOneShot(throwing,0.5f);
		}
	}
	
	/**
	 * set the damage the bot can do, not implemented
	 */ 
	public void setDamage(float damage){
		audio.PlayOneShot(double1, 1.0f);
		DoubleDamageTimer = 10.0f;
		bulletDamage = damage;	
	}

}
