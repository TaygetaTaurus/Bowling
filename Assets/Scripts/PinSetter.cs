using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
	public Text standingDisplay;
	public GameObject pinSet;
	
	private int lastStandingCount = -1;
	private int lastSettledCount = 10;
	private bool ballOutOfPlay = false;
	private float lastChangeTime;
	
	private Ball ball;
	private Animator animator;
	
	private ActionMaster actionMaster = new ActionMaster();
	
	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		standingDisplay.text = CountStanding().ToString();
		
		if (ballOutOfPlay){
			UpdateStandingCountAndSettle();
			standingDisplay.color = Color.red;
		}
	}
	
	public void SetBallOutOfPlay(){
		ballOutOfPlay = true;
	}
	
	public void RaisePins(){
		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()){	
			pin.RaiseIfStanding();
			}
		}
		
	public void LowerPins(){
		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()){
			pin.Lower();
		}
	}
		
	public void Renew(){
		Instantiate(pinSet, new Vector3(0f, 0f, 1829f), Quaternion.identity);
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
		
		ActionMaster.Action action = actionMaster.Bowl(pinFall);
	
		if(action == ActionMaster.Action.Tidy){
			animator.SetTrigger("tidyTrigger");
		}else if (action == ActionMaster.Action.EndTurn){
			animator.SetTrigger("resetTrigger");
			lastSettledCount = 10;
		}else if (action == ActionMaster.Action.Reset){
			animator.SetTrigger("resetTrigger");
			lastSettledCount = 10;
		}else if (action == ActionMaster.Action.EndGame){
			throw new UnityException("Dont know how to end game xd");
		}
		
		ball.Reset();
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
	
	public void OnTriggerExit(Collider collider){
		GameObject thingLeft = collider.gameObject;

		if(thingLeft.GetComponent<Pin>()){
			Destroy(thingLeft);
		}
	}
}
