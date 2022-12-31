using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private List<int> rolls = new List<int>();
	private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;
	private static bool isBoardsOn = true;
	
	// Use this for initialization
	void Start () {
		pinSetter = FindObjectOfType<PinSetter>();
		ball = FindObjectOfType<Ball>();	
		scoreDisplay = FindObjectOfType<ScoreDisplay>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Bowl(int pinFall){
		try {
			rolls.Add(pinFall);
			ball.Reset();

			pinSetter.PerformAction(ActionMasterOld.NextAction(rolls));
		}catch {
			Debug.LogWarning ("Something went wrong in Bowl()");
		}

		try {
			scoreDisplay.FillRoll (rolls);
			scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(rolls));
		}catch {
			Debug.LogWarning ("FillRollCard failed");
		}
	}

	public void ToggleBoards(){
		if (!ball.inPlay) {
			isBoardsOn = !isBoardsOn;
			
			GameObject[] boards = GameObject.FindGameObjectsWithTag ("Board");
			
			foreach (GameObject board in boards) {
				board.GetComponent<BoxCollider> ().enabled = isBoardsOn;
				board.GetComponent<MeshRenderer> ().enabled = isBoardsOn;
			}
		}
	}
}
