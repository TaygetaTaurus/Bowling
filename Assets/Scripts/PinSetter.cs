using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
	public GameObject pinSet;
	
	private Animator animator;
	private PinCounter pinCounter;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		pinCounter = GameObject.FindObjectOfType<PinCounter>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
		
	public void OnTriggerExit(Collider collider){
		GameObject thingLeft = collider.gameObject;

		if(thingLeft.GetComponent<Pin>()){
			Destroy(thingLeft);
		}
	}
	
	public void PerformAction (ActionMasterOld.Action action){
		if(action == ActionMasterOld.Action.Tidy){
			animator.SetTrigger("tidyTrigger");
		}else if (action == ActionMasterOld.Action.EndTurn){
			animator.SetTrigger("resetTrigger");
			pinCounter.Reset();
		}else if (action == ActionMasterOld.Action.Reset){
			animator.SetTrigger("resetTrigger");
			pinCounter.Reset();
		}else if (action == ActionMasterOld.Action.EndGame){
			throw new UnityException("Dont know how to end game xd");
		}
	}
}
