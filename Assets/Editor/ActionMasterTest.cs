using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ActionMasterTest {
	
	private List<int> pinFalls;
	private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
	private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
	private ActionMaster.Action reset = ActionMaster.Action.Reset;
	private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
	
	[SetUp]
	public void Setup(){
		pinFalls = new List<int>();
	}
	
	[Test]
	public void T00PassingTest(){
		Assert.AreEqual(1,1);
	}
	
	[Test]
	public void T01OneStrikeReturnsEndTurn(){
		pinFalls.Add(10);
		Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
	}
	
	[Test]
	public void T02Bowl8ReturnsTidy(){
		pinFalls.Add(8);
		Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
	}
	
	[Test]
	public void T03Bowl28ReturnsEndTurn(){
		int[] rolls = {2, 8};
		Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T04CheckResetAtStrikeInLastFrame(){
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10};
		Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T05CheckEndGame20InLastFrame(){
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};
		Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T06CheckResetAtSpareInLastFrame(){
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,9};
		Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T07RollsEndInGame(){
		int[] rolls = {8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2, 9};
		Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T08BowlStrikeAnd5InLastFrame(){
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,5};
		Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T09Bowl2StrikeInARowInLastFrame(){
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,10};
		Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T10BowlStrikeOn19And0At20InLastFrame(){
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,0};
		Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T11Bowl3StrikeInARowInLastFrame(){
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,10,10};
		Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T12Strike19And0After(){
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,0, 0};
		Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
	}
	
	[Test]
	public void T13Bowl0and10and5and1(){
		int[] rolls = {0,10, 5,1};
		Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
	}
}











