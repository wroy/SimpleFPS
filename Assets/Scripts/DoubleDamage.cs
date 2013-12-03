using UnityEngine;
using System.Collections;

public class DoubleDamage : MonoBehaviour {

	float isVisible = 30.0f; // how long before the powerup responds
	
	// Update is called once per frame
	void Update () {
		isVisible -= Time.deltaTime;//countdown to reappearing
		if (isVisible <= 0.0f){
			//make the power up available again, and make it pretty
			gameObject.transform.FindChild("Cylinder").renderer.enabled = true;
			gameObject.transform.FindChild("Cylinder_001").renderer.enabled = true;
			gameObject.transform.FindChild("Point light").light.enabled = true;
			gameObject.collider.enabled = true;
		}
	}
	
	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.tag == "Enemy") {
			//implementation for bot powerup pick up, not implemented
		}
		else if (collision.gameObject.tag == "Player"){
			Shooting s = collision.gameObject.transform.parent.gameObject.GetComponent<Shooting>();
			if (s != null){
				s.setDamage(100.0f); //double the player's bullet damage
				
				//make the powerup disappear and sets the timer for it to reappear
				gameObject.transform.FindChild("Cylinder").renderer.enabled = false;
				gameObject.transform.FindChild("Cylinder_001").renderer.enabled = false;
				gameObject.transform.FindChild("Point light").light.enabled = false;
				gameObject.collider.enabled = false;
				isVisible = 30.0f;
			}
		}
	}
}
