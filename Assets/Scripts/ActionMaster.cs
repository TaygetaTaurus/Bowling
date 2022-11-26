using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster{

	public enum Action {Tidy, Reset, EndTurn, EndGame};
	
	private int[] bowls = new int[21];
	private int bowl = 1;
	
	public Action Bowl(int pins){
		if (pins < 0 || pins > 10){throw new UnityException("Invalid pins!");}
		
		if (bowl == 21){
			return Action.EndGame;
		}
		
		bowls [bowl - 1] = pins;
		
		if (bowl == 20 && IsStrikeOn19Bowl() && Bowl21Awarded() && !IsStrikeOn20Bowl()){
			bowl +=1;
			return Action.Tidy;
		}else if (bowl == 20 && TwoStrikesLastFrame() && Bowl21Awarded()){
			bowl +=1;
			return Action.Reset;
		}
		
		if (bowl >= 19 && Bowl21Awarded()){
			bowl += 1;
			return Action.Reset;
		}else if (bowl == 20 && !Bowl21Awarded()){
			return Action.EndGame;
		}
		
		if (bowl % 2 != 0){ // Mid frame (or last frame)
			if (pins == 10){
				bowl += 2;
				return Action.EndTurn;
			}else{
				bowl += 1;
				return Action.Tidy;
			}
		}else if(bowl % 2 == 0){ // End of frame
			bowl += 1;
			return Action.EndTurn;
		}
		
		throw new UnityException("Not sure what action to return!");
	}
	
	private bool Bowl21Awarded(){
		return (bowls[19-1] + bowls[20-1] >= 10);
	}
	
	private bool IsStrikeOn19Bowl(){
		return (bowls[19-1] == 10);
	}
	
	private bool IsStrikeOn20Bowl(){
		return (bowls[20-1] == 10);
	}
	
	private bool TwoStrikesLastFrame(){
		return (bowls[19-1] + bowls[20-1] == 20);
	}
}




