using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	
	public GameObject bullet; //prefab bullet to fire
	float bulletSpeed = 75f; //speed of the bullet
	float bulletDamage = 50.0f; //damage the bullet does
	float DoubleDamageTimer = 0.0f; //how long double damage lasts
	
	public AudioClip throwing; //sound thowing makes
	public AudioClip double1; //sound on double damage pickup
	
	void Update () {
		if (DoubleDamageTimer >= 0){
			//souble damage if time remains
			bulletDamage = 100.0f;
			DoubleDamageTimer -= Time.deltaTime;
		}
		else {
			//double damage over, normal damage
			bulletDamage = 50.0f;
		}
		
		if(Input.GetButtonDown("Fire1")){
			//fire a bullet on left mouse click
			Camera camera = Camera.main;
			
			//build the bullet, set damage, and play sound
			GameObject deathBullet = (GameObject)Instantiate(bullet, camera.transform.position, camera.transform.rotation);
			deathBullet.GetComponent<Bullet>().damage = bulletDamage;
			audio.PlayOneShot(throwing,0.5f);
			
			//to help enable shooting while moving backwards
			if (Input.GetAxis("Vertical") < 0){
				//walking backwards
				float opp = -1 * bulletSpeed;
				deathBullet.rigidbody.AddForce(camera.velocity.normalized * opp, ForceMode.Impulse);
			}
			else {
				//walking forwards
				deathBullet.rigidbody.AddForce(camera.transform.forward * bulletSpeed, ForceMode.Impulse);
			}
		}

	}
	
	/**
	 * sets the damage of the bullet
	 */ 
	public void setDamage(float damage){
		audio.PlayOneShot(double1, 1.0f);
		DoubleDamageTimer = 10.0f;
		bulletDamage = damage;	
	}
}
