using UnityEngine;
using System.Collections;

public class IntroMenu : MonoBehaviour {
	
	float rotationAmount = 0.1f;//rotation speed of the camera
	public GUISkin skin;//button gui skin
	
	void Update () { 
		transform.Rotate(new Vector3(0.0f, rotationAmount, 0.0f)); //rotate the camera arround the map
	}
	
	void OnGUI(){
		GUI.skin = skin;
		
		//set demensions of the buttons
		float buttonW = 100;
		float buttonH = 50;
		
		//set the play game button
		float halfScreenW = (Screen.width/2) -50;
		float halfScreenH = Screen.height/2 + 130;
		if (GUI.Button(new Rect(halfScreenW,halfScreenH,buttonW,buttonH),"Play Game")){
			Application.LoadLevel("Room1");
		}
		
		//set the exit button
		halfScreenW = (Screen.width/2) - 50;
		halfScreenH = Screen.height/2 + 190;
		if (GUI.Button(new Rect(halfScreenW,halfScreenH,buttonW,buttonH),"Exit")){
			Application.Quit();
		}
	}
}
