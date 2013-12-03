using UnityEngine;
using System.Collections;

public class Corpse : MonoBehaviour {
	
	float lifespan = 5.0f; //how long the body lasts before disappearing
	
	public AudioClip dead1;//death sound effect

	void Start () {
		audio.PlayOneShot(dead1,1.0f);//play death sound
	}
	
	void Update () {
		lifespan -= Time.deltaTime; //start countdown on deletion
		
		if(lifespan <= 0){
			//object has reached the end of its life, delete it
			Destroy(gameObject);
		}
	}
}
