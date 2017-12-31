using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;


public static class GameData{

	//	public enum AbilityName {Archery,Athletics,Awareness,Brawl,Bureaucracy,Craft,Dodge,Integrity,Investigation,Larceny,Linguistics,Lore,MartialArts,
	//		Medicine,Melee,Occult,Performance,Presence,Resistance,Ride,Sail,Socialize,Stealth,Survival,Thrown,War};

	public static class CharmLibrary
	{
		static CharmLibrary(){
			List<Charm> Charms = Utils.LoadCharms().Charms;
			foreach(Charm c in Charms){
				switch(c.Ability){
				case "Archery":{
						Archery.Add(c);
						break;
					}
				case "Athletics":{
						Athletics.Add(c);
						break;
					}
				case "Awareness":{
						Awareness.Add(c);
						break;
					}
				default:{
						break;
					}
				}
			}
		}
		public static List<Charm> Archery = new List<Charm>();
		public static List<Charm> Athletics = new List<Charm>();
		public static List<Charm> Awareness = new List<Charm>();
		public static List<Charm> Brawl = new List<Charm>();
		public static List<Charm> Bureaucracy = new List<Charm>();
		//craft needs to be split up into types
		//		public List<Charm> Craft = new List<Charm>();
		public static List<Charm> Dodge = new List<Charm>();
		public static List<Charm> Integrity = new List<Charm>();
		public static List<Charm> Investigation = new List<Charm>();
		public static List<Charm> Larceny = new List<Charm>();
		public static List<Charm> Linguistics = new List<Charm>();
		public static List<Charm> Lore = new List<Charm> ();
		public static List<Charm> Medicine = new List<Charm>();
		public static List<Charm> Melee = new List<Charm>();
		public static List<Charm> Occult = new List<Charm>();
		public static List<Charm> Performance = new List<Charm>();
		public static List<Charm> Presence = new List<Charm>();
		public static List<Charm> Resistance = new List<Charm>();
		public static List<Charm> Ride = new List<Charm>();
		public static List<Charm> Sail = new List<Charm>();
		public static List<Charm> Socialize = new List<Charm>();
		public static List<Charm> Stealth = new List<Charm>();
		public static List<Charm> Survival = new List<Charm>();
		public static List<Charm> Thrown = new List<Charm>();
		public static List<Charm> War = new List<Charm>();
		public static class MartialArts{
			//martial art schools need to be marked in csv
//			public List<Charm>  = new List<Charm>();
//			public List<Charm>  = new List<Charm>();
//			public List<Charm>  = new List<Charm>();
//			public List<Charm>  = new List<Charm>();
//			public List<Charm>  = new List<Charm>();
//			public List<Charm>  = new List<Charm>();
//			public List<Charm>  = new List<Charm>();
//			public List<Charm>  = new List<Charm>();
//			public List<Charm>  = new List<Charm>();
//			public List<Charm>  = new List<Charm>();
		}
	}
}

public static class Caste{
	public const string DAWN = "Dawn";
	public const string ZENITH = "Zenith";
	public const string TWILIGHT = "Twilight";
	public const string NIGHT = "Night";
	public const string ECLIPSE = "Eclipse";
}

public static class Anima{
	public const string DIM = "Dim";
	public const string GLOWING = "Glowing";
	public const string BURNING = "Burning";
	public const string ICONIC = "Bonfire/Iconic";
}

public struct CasteAttributes{
	public static Dictionary<string,int[]> AttributeLookup = new Dictionary<string, int[]>(){
		{Caste.DAWN,new int[]{(int)AbilityName.Archery,(int)AbilityName.Awareness,(int)AbilityName.Brawl,(int)AbilityName.MartialArts,
				(int)AbilityName.Dodge,(int)AbilityName.Melee,(int)AbilityName.Resistance,(int)AbilityName.Thrown,(int)AbilityName.War}},
		{Caste.ZENITH, new int[]{(int)AbilityName.Athletics,(int)AbilityName.Integrity,(int)AbilityName.Performance,(int)AbilityName.Lore,
				(int)AbilityName.Presence,(int)AbilityName.Resistance,(int)AbilityName.Survival,(int)AbilityName.War}},
		{Caste.TWILIGHT, new int[]{(int)AbilityName.Bureaucracy,(int)AbilityName.Craft,(int)AbilityName.Integrity,(int)AbilityName.Investigation,
				(int)AbilityName.Linguistics,(int)AbilityName.Lore,(int)AbilityName.Medicine,(int)AbilityName.Occult}},
		{Caste.NIGHT, new int[]{(int)AbilityName.Athletics,(int)AbilityName.Awareness,(int)AbilityName.Dodge,(int)AbilityName.Investigation,
				(int)AbilityName.Larceny,(int)AbilityName.Ride,(int)AbilityName.Stealth,(int)AbilityName.Socialize}},
		{Caste.ECLIPSE, new int[]{(int)AbilityName.Bureaucracy,(int)AbilityName.Larceny,(int)AbilityName.Linguistics,(int)AbilityName.Occult,
				(int)AbilityName.Presence,(int)AbilityName.Ride,(int)AbilityName.Sail,(int)AbilityName.Socialize}},

	};
}
	
public enum AttributeName {Strength,Dexterity,Stamina,Charisma,Manipulation,Appearance,Perception,Intelligence,Wits};

public struct Attributes{
	AttributeName name{ get; set; }
	int value{ get; set; }

	public Attributes(AttributeName name, int value){
		this.name = name;
		this.value = value;
	}
}

public enum AbilityName {Archery,Athletics,Awareness,Brawl,Bureaucracy,Craft,Dodge,Integrity,Investigation,Larceny,Linguistics,Lore,MartialArts,
	Medicine,Melee,Occult,Performance,Presence,Resistance,Ride,Sail,Socialize,Stealth,Survival,Thrown,War};

public struct Ability{
	AbilityName name{ get; set; }
	int value{ get; set; }

	public Ability(AbilityName name, int value){
		this.name = name;
		this.value = value;
	}
}

public enum MeritName{MeritA,MeritB,MeritC,Gigantic,Quick,Ugly};

[Serializable]
public struct Merit{
	public String Name;
	public List<int> Cost;
	public int Level;
	public string Text;

	public Merit(String name, String text, List<int> cost){		
		this.Name = name;
		this.Cost = cost;
		this.Text = text;
		Level = 0;
	}

	public static Dictionary<string, Merit> MeritLookup = new Dictionary<string, Merit> ();
}

[Serializable]
public class MeritContainer{
	public List<Merit> Merits;
}

[Serializable]
public class Charm{
	public string Name;
	public string Book;
	public string Page;
	public string Cost;
	public string Ability;
	public int MinAbility;
	public int MinEssence;
	public string Type;
	public string Keywords;
	public string Duration;
	public string Prerequisites;
	public string Description;
	public Node Node;
	public List<Node> Parents = new List<Node>();
}

[Serializable]
public struct Node{
	public int x;
	public int y;

	public Node(int x, int y){
		this.x = x;
		this.y = y;
	}

	public override bool Equals (object obj)
	{
		Node n = (Node)obj;
		return (x == n.x && y == n.y);
	}
}
	
[Serializable]
public struct CharmCascade{
	public List<Charm> Charms;
	public int width;
	public int height;
}

[Serializable]
public class Intimacy{
	public string Description;
	public int Strength;
}

[Serializable]
public class IntimacyContainer{
	public List<Intimacy> Intimacies;
}

[Serializable]
public class SavedCharacters{
	public List<string> Characters = new List<string>();
}

[Serializable]
public class GearList{
	public List<Weapon> Weapons = new List<Weapon>();
	public List<Armor> Armors = new List<Armor>();
}

[Serializable]
public class Weapon{

	public Weapon(){
		Interlocked.Increment (ref WeaponHash);
		ID = Weapon.WeaponHash;
	}

	public string Name;
	public string Accuracy;
	public string Damage;
	public string Defense;
	public string Overwhelming;
	public string Parry;
	public string Tags;
	public string Description;
	public int ID;
	public static int WeaponHash;

	public override bool Equals (object obj)
	{
		Weapon wep = (Weapon)obj;
		return (this.ID == wep.ID);
	}
	//something for evocations, perhaps?
}

[Serializable]
public class Armor{

	public Armor(){
		Interlocked.Increment (ref ArmorHash);
		ID = Weapon.WeaponHash;
	}

	public string Name;
	public string Soak;
	public string Hardness;
	public string Mobility;
	public string Tags;
	public string Description;
	public int ID;
	public static int ArmorHash;

	public override bool Equals (object obj)
	{
		Armor arm = (Armor)obj;
		return (this.ID == arm.ID);
	}
	//something for evocations, perhaps?
}

[Serializable]
public class HealthBars{
	public int Zero;
	public int One;
	public int Two;
	public int Four;
	public readonly int Inc = 1;
}

[Serializable]
public struct CharmGraph{
	
}
