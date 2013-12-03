using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class BotAIFlee : MonoBehaviour {
	
	public float animSpeed = 1.5f; // a public setting for overall animator animation speed
	public float lookSmoother = 3f; // a smoothing setting for camera motion 
	
	public Transform player; //the player
	//public GUIText status; //the red gui text telling the player if they are it
	
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
		PlannedDestination = GetComponent <NavMeshAgent>().destination;//sends the bot somewhere
	}
	
	
	void Update ()
	{
		transform.LookAt(PlannedDestination);// point the bot towards the destination
		distanceFromPlayer = Vector3.Distance(player.position, gameObject.transform.position); //find the distance between bot and player
		if (distanceFromPlayer < 10.0f){
			//if the distance between bot and player is less then 10, face the player
			// and shoot at them
			transform.LookAt(player);
			if (shootTimer <=0){
				shootTimer = 1.0f;
				GameObject deathBullet = (GameObject)Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
				deathBullet.GetComponent<Bullet>().damage = bulletDamage;
				deathBullet.rigidbody.AddForce(bulletSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);
				audio.PlayOneShot(throwing,0.5f);
			}
		}
		chooseDestination();//find the furtherest location and move to it, regardless of player combat

		shootTimer -= Time.deltaTime;//reload the bot
		
		float h = Input.GetAxis("Horizontal");// setup h variable as our horizontal input axis
		float v = 1.0f;	// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); // set our animator's float parameter 'Direction' equal to the horizontal input axis		
		anim.speed = animSpeed;	// set the speed of our animator to the public variable 'animSpeed'
	}
	
	/**
	 * sets the damage the bot does, used for double damage. not implemented 
	 */
	public void setDamage(float damage){
		audio.PlayOneShot(double1, 1.0f);
		DoubleDamageTimer = 10.0f;
		bulletDamage = damage;	
	}
	
	/**
	 * Chooses the furthest location from the player given some predetermined locations
	 * Once the furthest location is chosen, set that as the destination
	 */ 
	void chooseDestination(){
		//locations
		Vector3[] Destination = new Vector3[6];
		Destination[0] = new Vector3 (-30.96512f, 0.1f, 31.82335f);
		Destination[1] = new Vector3 (-36.40741f, 0.1f, -35.84986f);
		Destination[2] = new Vector3 (31.69841f, 0.1f, -35.84986f);
		Destination[3] = new Vector3 (33.04593f, 0.1f, 36.00597f);
		Destination[4] = new Vector3 (22.49821f, 0.1f, 3.456315f); 
		Destination[5] = new Vector3 (-28.9175f, 0.1f, -0.8987858f);
		
		Vector3 Furthest = Destination[0];
		float FurthestDistance = Vector3.Distance(player.position, Destination [0]);
		float Temp;
		//choose the furthest location
		for (int i = 1; i < Destination.Length; i ++){
			Temp = Vector3.Distance(player.position, Destination [i]);	
			if (Temp > FurthestDistance){
				Furthest = Destination[i];	
			}
		}
		//send the bot towards the furthest location
		GetComponent <NavMeshAgent>().destination = Furthest;
	}

}
