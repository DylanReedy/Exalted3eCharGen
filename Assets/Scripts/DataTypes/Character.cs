using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class Character {

	public string Name;
	public List<int> Attributes = new List<int>();
	public List<int> Abilities= new List<int>();
//	public List<Merit> Merits = new List<Merit>();
//	public List<Intimacy> Intimacies = new List<Intimacy> ();
	public string caste;
	public int SupernalAbility;
	public int Willpower;
    public int Essence;
	public int PersonalMotes, PeripheralMotes, CommittedMotes;
	public int Limit;
	public string LimitBreak;
	public int AnimaLevel;
	public string AnimaBanner;
	public GearList GearList = new GearList();
	public HealthBars Health = new HealthBars();

	public Character(){

		Name = "Char Name";
		caste = Caste.DAWN;
		Willpower = 5;
		Essence = 1;
		foreach (AttributeName attr in Enum.GetValues(typeof(AttributeName))) {
			Attributes.Add(1);
		}
		foreach (AbilityName abil in Enum.GetValues(typeof(AbilityName))) {
			Abilities.Add(0);
		}

		PersonalMotes = Essence * 3 + 10;
		PeripheralMotes = Essence * 7 + 26;
		CommittedMotes = 0;
		Health.Zero = 1;
		Health.One = 2;
		Health.Two = 2;
		Health.Four = 1;
	}

//	public bool canAddMerit(Merit merit){
//		if (Merits.Contains (merit)) {
//			return false;
//		} 
//		return true;
//	}
		
	public void SetAttribute(int attribute, int value){
		Attributes [attribute] = value;
	}

	public void SetAbility(int attribute, int value){
		Abilities [attribute] = value;
	}


}
