using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	
	float lifespan = 5.0f; // how long the fire lasts before being destroyed
	float fireDamage = 5.0f; // how much damage the fire does per second
	string source; // who created the fire
	
	public AudioClip explosion; // sound the explosion makes
	public AudioClip fire; // sound the fire makes after the explosion

	void Start () {
		audio.PlayOneShot(explosion, 1.0f); //play explosion sound 
		audio.PlayOneShot(fire, 0.4f); //start playing the fire sound
	}
	
	void Update () {
		lifespan -= Time.deltaTime;
		
		if(lifespan <= 0){
			//destory the object at the end of it's life span
			Destroy(gameObject);
		}
	}
	
	/**
	 * set who create the fire
	 */ 
	public void setSource(string bulletSource){
		source = bulletSource;	
	}
	
	void OnTriggerStay(Collider collision){
		if (collision.gameObject.tag == "Player"){
			try{
				//do fire damage to the player
				health h = collision.gameObject.transform.parent.gameObject.GetComponent<health>();
				if (h != null){
					h.ReceiveDamage(fireDamage * Time.deltaTime, source);
				}
			}
			catch{}
		}
		else if (collision.gameObject.tag == "Enemy"){
			//do fire damage to the bot
			health h = collision.gameObject.GetComponent<health>();
			if (h != null){
				h.ReceiveDamage(fireDamage * Time.deltaTime, source);
			}
		}
	}
}
