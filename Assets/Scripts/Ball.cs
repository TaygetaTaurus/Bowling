using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	
	public Vector3 launchVelocity;
	public bool inPlay = false;
	
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private Vector3 ballStartPos;
	
	
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.useGravity = false;
		
		ballStartPos = transform.position;
		
	}
	public void Launch (Vector3 velocity)
	{	
		inPlay = true;
		rigidBody.useGravity = true;
		rigidBody.velocity = velocity;
		
		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.Play ();
	}
	
	public void Reset(){
		inPlay = false;
		transform.position = ballStartPos;
		rigidBody.useGravity = false;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
		rigidBody.transform.rotation = Quaternion.identity;
	}
}











