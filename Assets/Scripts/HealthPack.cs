using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour {
	
	float healthAddition = 50.0f; // how much health the health pack gives
	float isVisible = 10.0f; // how long before the health pack respawns
	
	void Update () {
		isVisible -= Time.deltaTime;
		if (isVisible <= 0.0f){
			//health pack can be made visble again, make it reappear
			gameObject.transform.FindChild("Heart_Mesh").renderer.enabled = true;
			gameObject.transform.FindChild("Point light").light.enabled = true;
			gameObject.collider.enabled = true;
		}
	}
	
	void OnTriggerEnter(Collider collision){
		try{
			if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") {
				health h = collision.gameObject.transform.parent.gameObject.GetComponent<health>();
				if (h != null){
					//player/bot picked up the health pack, health them and make the health pack disappear
					h.giveHealth(healthAddition);
					gameObject.transform.FindChild("Heart_Mesh").renderer.enabled = false;
					gameObject.transform.FindChild("Point light").light.enabled = false;
					gameObject.collider.enabled = false;
					isVisible = 10.0f;
				}
				
			}
		}
		catch{}
	}
}
