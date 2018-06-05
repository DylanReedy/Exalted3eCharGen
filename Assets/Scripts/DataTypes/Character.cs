using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class Character {

	public string name;
	public Caste caste;
	public string concept;
	public string animaBanner;
	public Ability supernalAbility;
	public AnimaIntensity animaLevel;
	public int maxWillpower;
    public int tempWillpower;
	public int limit;
    public int essence;
	public int personalMotes, peripheralMotes, committedMotes;
	public string limitTrigger;
    public string limitBreak;
	public Ratio experience;
	public Ratio solarExperience;

    public List<int> attributes = new List<int>();
    public List<int> abilities = new List<int>();
    public List<Specialty> specialties = new List<Specialty>();
    public List<Merit> merits = new List<Merit>();
    public List<Weapon> weapons = new List<Weapon>();
    public List<Armor> armors = new List<Armor>();
    public List<Intimacy> intimacies = new List<Intimacy> ();
    public List<Charm> charms = new List<Charm>();
    public List<HealthBox> healthLevels = new List<HealthBox>();

	public int naturalSoak, totalSoak, parry, evasion, rush, resolve, guile, disengage, joinBattle;

	public List<string> charmReference = new List<string> ();

	public Character(){

		name = "Char Name";
		caste = Caste.Dawn;
		maxWillpower = 5;
		essence = 1;
		foreach (Attribute attr in Enum.GetValues(typeof(Attribute))) {
			attributes.Add(1);
		}
		foreach (Ability abil in Enum.GetValues(typeof(Ability))) {
			abilities.Add(0);
		}

		personalMotes = essence * 3 + 10;
		peripheralMotes = essence * 7 + 26;
		committedMotes = 0;
		InitializeHealth (healthLevels);
	}

	public void AddCharm(Charm c){
		charms.Add (c);
		charmReference.Add (c.Name);
	}

	public void RemoveCharm(Charm c){
		charms.Remove (c);
		charmReference.Remove (c.Name);
	}

//	public bool canAddMerit(Merit merit){
//		if (Merits.Contains (merit)) {
//			return false;
//		} 
//		return true;
//	}
		
	public void SetAttribute(int attribute, int value){
		attributes [attribute] = value;
	}

	public void SetAbility(int attribute, int value){
		abilities [attribute] = value;
	}

	public void InitializeHealth(List<HealthBox> h){
		h.Add (new HealthBox (0));
		h.Add (new HealthBox (1));
		h.Add (new HealthBox (1));
		h.Add (new HealthBox (2));
		h.Add (new HealthBox (2));
		h.Add (new HealthBox (4));
		h.Add (new HealthBox (-1));
	}

	public void AddIntimacy(Intimacy i){
		if (!intimacies.Contains (i)) {
			intimacies.Add (i);
		}
		//handle new intimacy
	}




}
