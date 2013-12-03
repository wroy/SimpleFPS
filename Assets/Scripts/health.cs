using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {
	
	public float hitPoints = 100.0f; // amount of health
	public GameObject corpse; //the corpse that appears when the player/bot dies
	
	public GameObject player; //the player
	
	public AudioClip restore1; //the sound that plays when a health pack is picked up
	
	public GUIText deaths; //the gui text that hold the player death count
	int deathCount = 0;//player death count
	string deathString = "Deaths: ";//beginning part of the gui string
	
	public GUIText kills;//the gui text that holds the player's kill count
	int killCount = 0;//player's kill count
	string killString = "Kills: "; //beginning part of the gui string
	
	void Start(){
		//sets the initial gui texts
		deaths.text = deathString + deathCount;//sets deaths
		health h = player.GetComponent<health>();//sets health
		h.kills.text = h.killString + h.killCount;//sets kills
	}
	
	/**
	 * does damage to the player/bot given the amount and who 
	 * did the damage
	 */ 
	public void ReceiveDamage (float amount, string bulletSource) {
		hitPoints -= amount;
		if(hitPoints <= 0){
			//health <=0, player/bot should die
			Die(bulletSource);	
		}
	}
	
	/**
	 * gives the player health given the ammount of health being given
	 */ 
	public void giveHealth(float amount){
		audio.PlayOneShot(restore1,1.0f);
		hitPoints += amount;	
	}
	
	/**
	 * Handes the player's/bot's death. Increments the player's kill and
	 * death count if appropriate (player kills bot, +1 Kills, bot kills player
	 * +1 death, and player kills themself -1 kills). Spawns a corpse where the 
	 * bot/player was and moves the player/bot to a respawn location.
	 */ 
	void Die(string source){
		deathCount++; // increase death count on parent gameobject
		deaths.text = deathString + deathCount;
		if (gameObject.tag == "Enemy" && source == "BulletPlayer"){
			// player killed a bot, incremenet kill count
			health h = player.GetComponent<health>();
			h.killCount++;
			h.kills.text = h.killString + h.killCount;
		}
		
		if (gameObject.tag == "Player" && source == "BulletPlayer"){
			//player killed themself, decrement kill count
			health h = player.GetComponent<health>();
			h.killCount--;
			h.kills.text = h.killString + h.killCount;
		}
		
		//spawn a corpse
		GameObject body = (GameObject)Instantiate(corpse, gameObject.transform.position, Quaternion.identity);
		body.transform.rotation = gameObject.transform.rotation;
		
		//move the player/bot to a spawn location
		int spawn = Random.Range(0, 6);
		if(spawn == 0) 
			gameObject.transform.localPosition = new Vector3 (-30.96512f, 0.1f, 31.82335f);
		else if(spawn == 1) 
			gameObject.transform.localPosition = new Vector3 (-36.40741f, 0.1f, -35.84986f);
		else if(spawn == 2) 
			gameObject.transform.localPosition = new Vector3 (31.69841f, 0.1f, -35.84986f);
		else if(spawn == 3) 
			gameObject.transform.localPosition = new Vector3 (33.04593f, 0.1f, 36.00597f);
		else if(spawn == 4) 
			gameObject.transform.localPosition = new Vector3 (22.49821f, 0.1f, 3.456315f);
		else  
			gameObject.transform.localPosition = new Vector3 (-28.9175f, 0.1f, -0.8987858f);
		
		//give palyer/bot more health points
		hitPoints = 100.0f;
	}
}
