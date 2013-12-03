using UnityEngine;
using System.Collections;

public class Bullet: MonoBehaviour {
	
	float lifespan = 3.0f; //how long before bullet explodes
	public GameObject fire; //the fire the explosion leaves behind
	public float damage = 50f; //how much damage the explosion does
	float notHitSelf = 0.05f; //a time to prevent the players and bots from blowing themselves up
	
	void Update () {
		lifespan -= Time.deltaTime;//start timer on bullet
		notHitSelf -= Time.deltaTime;
		
		if (damage >= 100){
			//if double damage, colour the bullet to look different
			gameObject.renderer.material.color = new Color (0.0f,255.0f, 0.0f, 255.0f);
		}
		
		if(lifespan <= 0){
			//if timer is up, explode
			Explode();
		}
	
	}
	
	void OnTriggerEnter(Collider collision){
		if((collision.gameObject.tag == "Enemy" && notHitSelf <= 0.01) || (collision.gameObject.tag == "Player" && notHitSelf <= 0.01)) {
			//if the bullet collides with player or bot, blow up, create fire, and do damage
			health h = collision.gameObject.transform.parent.gameObject.GetComponent<health>();
			if (h != null){
				h.ReceiveDamage(damage, transform.tag);
				Explode();
			}
			
		}
	}
	
	void Explode(){
		
		//some experiment code with explosion physics
		/*float range = 10.0f;
		float force = 100.0f;
			 
		Collider[] objs = Physics.OverlapSphere(transform.position, range);
		foreach(Collider C in objs)
		{
			Debug.Log("" + C.tag);
			var R = C.rigidbody;
			if (R == null)
				continue;
			R.AddExplosionForce(force, transform.position, range, 3.0f);
		}*/
		
		// creates the fire of the explosion
		GameObject FireObj = (GameObject)Instantiate(fire, gameObject.transform.position, Quaternion.identity);
		Fire f = FireObj.GetComponent<Fire>();
		f.setSource(transform.tag);
		
		//destroys the bullet
		Destroy(gameObject);
	}
}
