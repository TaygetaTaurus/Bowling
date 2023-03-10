using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
	public Text standingDisplay;
	
	private GameManager gameManager;
	private int lastStandingCount = -1;
	private int lastSettledCount = 10;
	private bool ballOutOfPlay = false;
	private float lastChangeTime;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		standingDisplay.text = CountStanding().ToString();
		
		if (ballOutOfPlay){
			UpdateStandingCountAndSettle();
			standingDisplay.color = Color.red;
		}
	}
	
	public void UpdateStandingCountAndSettle(){
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
	
	public void PinsHaveSettled(){
		int standing = CountStanding();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;
		gameManager.Bowl(pinFall);
		lastStandingCount = -1;
		ballOutOfPlay = false;
		standingDisplay.color = Color.green;
	}
	
	public int CountStanding(){
		int standing = 0;
		
		foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()){
			if (pin.IsStanding()){
				standing++;
			}
		}
		return standing;
	}
	
	public void Reset(){
		lastSettledCount = 10;
	}
	
	void OnTriggerExit(Collider collider){
		if (collider.gameObject.name == "Ball"){
			ballOutOfPlay = true;
		}
	}
	
}







