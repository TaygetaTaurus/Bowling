using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	public float standingThreshold;
	public float distToRaise = 40f;
	
	private Rigidbody rigidBody;
	
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
	}
	
	public void RaiseIfStanding(){
		if (IsStanding()){
			rigidBody.useGravity = false;
			transform.Translate(new Vector3(0f, distToRaise, 0f), Space.World);
			transform.rotation = Quaternion.Euler(270f, 0, 0);
			rigidBody.velocity = Vector3.zero;
			rigidBody.angularVelocity = Vector3.zero;
		}
	}
	
	public void Lower(){
		transform.Translate(new Vector3(0f, -distToRaise, 0f), Space.World);
		rigidBody.useGravity = true;
	}
	
	public bool IsStanding(){
		Vector3 rotationInEuler = transform.rotation.eulerAngles;
		
		float tiltInX = Mathf.Abs(270 - rotationInEuler.x);
		float tiltInZ = Mathf.Abs(270 - rotationInEuler.z);
	
		if (tiltInX < standingThreshold || tiltInZ < standingThreshold){
			return true;
		}else{
			return false;
		}	
	}
}
