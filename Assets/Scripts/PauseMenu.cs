using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GUISkin skin;//skin for buttons and backdrop
	Rect backWindow; //button's backdrop
	bool paused = false;//paused state, true the menu is open, false closed
	
	private void Start(){
		//back drop dimensions
		float backW = 200;
		float backH = 250;
		//center the back drop
		float halfScreenW = (Screen.width/2) - backW/2;
		float halfScreenH = (Screen.height/2) - backH/2;
		
		backWindow = new Rect(halfScreenW, halfScreenH, backW, backH);//build backdrop
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){ 
		//if escape is pressed open/close the menu, 
			if (paused){ 
				//if open, then close the menu
				paused = false;
				//relock cursor and enable FPS elements
				Screen.lockCursor = true;
				GetComponent<FirstPersonController>().enabled = true;
				GetComponent<Shooting>().enabled = true;
			}
			else{
				//if closed, open the menu
				paused = true;
				//unlock cursor and disable FPS elements
				Screen.lockCursor = false;
				GetComponent<FirstPersonController>().enabled = false;
				GetComponent<Shooting>().enabled = false;
			}
		}
	}
	
	void OnGUI(){
		
		if (paused){
			//if paused build the menu
			GUI.skin = skin;
			GUI.Box(backWindow, "Pause Menu");//title the menu
			
			//button dimensions
			float buttonW = 100;
			float buttonH = 50;
			
			// set the resume button and functionality
			float halfScreenW = (Screen.width/2) - buttonW/2;
			float halfScreenH = Screen.height/2 - 80;
			if (GUI.Button(new Rect(halfScreenW,halfScreenH,buttonW,buttonH),"Resume")){
				paused = false;
				Screen.lockCursor = true;
				GetComponent<FirstPersonController>().enabled = true;
				GetComponent<Shooting>().enabled = true;
			}
			
			//set the restart button and functionality
			halfScreenW = (Screen.width/2) - buttonW/2;
			halfScreenH = Screen.height/2 - 20;
			if (GUI.Button(new Rect(halfScreenW,halfScreenH,buttonW,buttonH),"Restart")){
				Application.LoadLevel("Room1");
			}
			
			//set the back to menu button and functionality
			halfScreenW = (Screen.width/2) - buttonW/2;
			halfScreenH = Screen.height/2 + 40;
			if (GUI.Button(new Rect(halfScreenW,halfScreenH,buttonW,buttonH),"Back To Menu")){
				Application.LoadLevel("IntroMenu");
			}
		}
	}
	
	void windowFunc(int id){
		
	}
}
