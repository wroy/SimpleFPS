using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
public class BotControlScript : MonoBehaviour
{
	[System.NonSerialized]					
	public float animSpeed = 1.5f; // a public setting for overall animator animation speeds
	private Animator anim; // a reference to the animator on the character
	private AnimatorStateInfo currentBaseState; // a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState; // a reference to the current state of the animator, used for layer 2
	void Start ()
	{
		anim = GetComponent<Animator>(); //initalizes the animator for the player				  				
	}
	
	
	void FixedUpdate ()
	{	
		float h = Input.GetAxis("Horizontal");	// setup h variable as our horizontal input axis
		float v = Input.GetAxis("Vertical");// setup v variables as our vertical input axis
		if (h != 0 && v == 0) v = -1f;// maps the the straffing animation, only moving left/right
		anim.SetFloat("Speed", v);// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); // set our animator's float parameter 'Direction' equal to the horizontal input axis		
		anim.speed = animSpeed;	// set the speed of our animator to the public variable 'animSpeed'
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation
		
		if(anim.layerCount ==2)		
			layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);	// set our layer2CurrentState variable to the current state of the second Layer (1) of animation
		
	}
	
}
