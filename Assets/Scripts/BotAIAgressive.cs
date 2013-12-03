using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class BotAIAgressive : MonoBehaviour {
	
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

	void Start ()
	{
		anim = GetComponent<Animator>(); // sets the bots animations					  
	}
	
	
	void Update ()
	{
		//look at player and determine distance between player and bot
		transform.LookAt(player);
		distanceFromPlayer = Vector3.Distance(player.position, gameObject.transform.position);
		
		if (distanceFromPlayer < 20.0f){
			//if dstiance from player is less then 20, start shooting at the player
			if (shootTimer <=0){
				//if reloaded, fire
				shootTimer = 1.0f;
				GameObject deathBullet = (GameObject)Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
				deathBullet.GetComponent<Bullet>().damage = bulletDamage;
				deathBullet.rigidbody.AddForce(bulletSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);
				audio.PlayOneShot(throwing,0.5f);
			}	
		}
		
		if (distanceFromPlayer > 5.0f){
			//chase the player
			GetComponent <NavMeshAgent>().destination = player.position; 
		}
		else {
			//stop moving closer to the player, already close enough
			GetComponent <NavMeshAgent>().destination = gameObject.transform.position;
		}

		shootTimer -= Time.deltaTime;//reload the bot's weapon
		
		float h = Input.GetAxis("Horizontal");// setup h variable as our horizontal input axis
		float v = 1.0f;	// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); // set our animator's float parameter 'Direction' equal to the horizontal input axis		
		anim.speed = animSpeed;	// set the speed of our animator to the public variable 'animSpeed'
	}
	
	/**
	 * controls the bot's damage, used for double damage, not implemented
	 */ 
	public void setDamage(float damage){
		audio.PlayOneShot(double1, 1.0f);
		DoubleDamageTimer = 10.0f;
		bulletDamage = damage;	
	}

}
