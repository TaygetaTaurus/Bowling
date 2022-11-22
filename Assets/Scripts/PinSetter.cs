using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
	public int lastStandingCount = -1;
	public Text standingDisplay;
	
	private Ball ball;
	private float lastChangeTime;
	private bool ballEnteredBox = false;
	
	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
		standingDisplay.text = CountStanding().ToString();
		
		if (ballEnteredBox){
			CheckStanding();
		}
	}
	
	void CheckStanding(){
		int currentStanding = CountStanding();
		
		if (currentStanding != lastStandingCount){
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
			return;
		}
		
		float settleTime = 3f;
		if((Time.time - lastChangeTime) > settleTime){
			PinsHaveSettled();
		}
	}
	
	void PinsHaveSettled(){
		ball.Reset();
		lastStandingCount = -1;
		ballEnteredBox = false;
		standingDisplay.color = Color.green;
	}
	
	
	int CountStanding(){
		int standing = 0;
		
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()){
			if (pin.IsStanding()){
				standing++;
			}
		}
		return standing;
	}
	
	void OnTriggerEnter(Collider collider){
		if (collider.GetComponent<Ball>()){
			ballEnteredBox = true;
			standingDisplay.color = Color.red;
		}
	}
	
	void OnTriggerExit(Collider collider){
		GameObject thingLeft = collider.gameObject;

		if(thingLeft.GetComponent<Pin>()){
			Destroy(thingLeft);
		}
	}
	
}
