using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using Mono.Data.Sqlite;
using System.IO;

public enum Attribute {Strength,Dexterity,Stamina,Charisma,Manipulation,Appearance,Perception,Intelligence,Wits};
public enum Ability {Archery,Athletics,Awareness,Brawl,Bureaucracy,Craft,Dodge,Integrity,Investigation,Larceny,Linguistics,Lore,MartialArts,
	Medicine,Melee,Occult,Performance,Presence,Resistance,Ride,Sail,Socialize,Stealth,Survival,Thrown,War};
public enum Caste {Dawn,Zenith,Twilight,Night,Eclipse};
public enum AnimaIntensity{Dim,Glowing,Burning,Bonfire};
public enum MeritType{Innate,Purchased,Story}
public enum HealthStatus{Healthy,Bashing,Lethal,Aggravated};
public enum CharmType{Simple,Supplemental,Reflexive,Permanent};
public enum CharmBook{Core,Arms};
public enum IntimacyIntensity{None, Minor, Major, Defining}

public class Specialty{

	public Attribute attribute;
	public string description;

	public override bool Equals(object s){
		Specialty ss = (Specialty)s;
		return (ss.attribute == attribute && ss.description == description);
	}
}

[Serializable]
public class Merit{
	public String name;
	public String customName;
	public List<int> cost = new List<int>();
	public MeritType type;
	public String description;
	public int level;

	public override bool Equals(object m){
		Merit mm = (Merit)m;
		return (mm.name == name && mm.level == level && mm.description == description);
	}
}

[Serializable]
public class Ratio{
	public short current;
	public short max;
}

[Serializable]
public class Weapon{

	public Weapon(){
		Interlocked.Increment (ref weaponHash);
		ID = Weapon.weaponHash;
	}

	public string name;
	public string accuracy;
	public string damage;
	public string defense;
	public string overwhelming;
	public string parry;
	public string tags;
	public string description;
	public int ID;
	public static int weaponHash;

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
		Interlocked.Increment (ref armorHash);
		ID = Weapon.weaponHash;
	}

	public string name;
	public string soak;
	public string hardness;
	public string mobility;
	public string tags;
	public string description;
	public int ID;
	public static int armorHash;

	public override bool Equals (object obj)
	{
		Armor arm = (Armor)obj;
		return (this.ID == arm.ID);
	}
	//something for evocations, perhaps?
}

[Serializable]
public class Charm{
	public string Name;
//	public CharmType type;
//	public string duration;
//	public string cost;
//	public CharmBook book;
	//public string page;
	//public string description;
	public Ability Ability;
	public int MinAbility;
	public int MinEssence;
	//public string keywords;
	public List<string> Prerequisites;
	public Node Node;
	public List<Node> Parents = new List<Node>();

	public override bool Equals(object c){
		Charm cc = (Charm)c;
		return (Name == cc.Name);
	}
}

[Serializable]
public class HealthBox{
	public int level; // -1 = incap
	public HealthStatus type;

	public HealthStatus Advance(){
		type = (HealthStatus)(((int)type+1) % Enum.GetValues (typeof(HealthStatus)).Length);
		return type;
	}

	public HealthBox(int input){
		level = input;
	}
}

[Serializable]
public class Intimacy{
	public string Name;
	public string Context;
	public IntimacyIntensity Intensity;
}


/// <summary>
/// Unity requires us to wrap lists in serializable classes. See below. 
/// </summary>

[Serializable]
public class MeritContainer{
	public List<Merit> Merits;

}

[Serializable]
public class IntimacyContainer{
	public List<Intimacy> Intimacies;
}

[Serializable]
public struct CharmCascade{
	public List<Charm> Charms;
	public int width;
	public int height;
}

[Serializable]
public class SavedCharacters{
	public List<string> Characters = new List<string>();
}

[Serializable]
public class Node{
	public int x;
	public int y;

	public override bool Equals (object obj)
	{
		Node n = (Node)obj;
		return (x == n.x && y == n.y);
	}
}


public static class GameData{

	public static class CharmLibrary
	{
		static CharmLibrary(){
			List<Charm> Charms = Utils.LoadCharms().Charms;
			foreach(Charm c in Charms){
				switch(c.Ability){
				case Ability.Archery:{
						Archery.Add(c);
						break;
					}
				case Ability.Athletics:{
						Athletics.Add(c);
						break;
					}
				case Ability.Awareness:{
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
	public struct CasteAbilities{
		public static Dictionary<Caste,Ability[]> AbilityLookup = new Dictionary<Caste, Ability[]>(){
			{Caste.Dawn,new Ability[]{Ability.Archery,Ability.Awareness,Ability.Brawl,Ability.MartialArts,
					Ability.Dodge,Ability.Melee,Ability.Resistance,Ability.Thrown,Ability.War}},
			{Caste.Zenith, new Ability[]{Ability.Athletics,Ability.Integrity,Ability.Performance,Ability.Lore,
					Ability.Presence,Ability.Resistance,Ability.Survival,Ability.War}},
			{Caste.Twilight, new Ability[]{Ability.Bureaucracy,Ability.Craft,Ability.Integrity,Ability.Investigation,
					Ability.Linguistics,Ability.Lore,Ability.Medicine,Ability.Occult}},
			{Caste.Night, new Ability[]{Ability.Athletics,Ability.Awareness,Ability.Dodge,Ability.Investigation,
					Ability.Larceny,Ability.Ride,Ability.Stealth,Ability.Socialize}},
			{Caste.Eclipse, new Ability[]{Ability.Bureaucracy,Ability.Larceny,Ability.Linguistics,Ability.Occult,
					Ability.Presence,Ability.Ride,Ability.Sail,Ability.Socialize}},

		};
	}
}

public class BaseDicePool {
    public Attribute attribute;
    public Ability ability;
}





//	public static void LoadDB(){
//		if (!File.Exists ("CharmDatabase.sqlite")) {
//			SqliteConnection.CreateFile ("CharmDatabase.sqlite");		
//		}
//		SqliteConnection connection = new SqliteConnection ("Data Source = CharmDatabase.sqlite;Version=3;");
//		connection.Open ();
//		string sql = "CREATE TABLE charms (Name VARCHAR(50), Book VARCHAR(50), Page VARCHAR(10), Cost VARCHAR(20), Ability VARCHAR(20), " +
//			"MinAbility INT, MinEssence INT, Type VARCHAR(20), Keywords VARCHAR(50), Duration VARCHAR(20), ";
//		//	public string Name;
//		//	public string Book;
//		//	public string Page;
//		//	public string Cost;
//		//	public string Ability;
//		//	public int MinAbility;
//		//	public int MinEssence;
//		//	public string Type;
//		//	public string Keywords;
//		//	public string Duration;
//		//	public List<string> Prereqs;
//		//	public string Description;
//		//	public Node Node;
//		//	public List<Node> Parents = new List<Node>();
//		SqliteCommand command = new SqliteCommand (sql, connection);
//		command.ExecuteNonQuery();
//
//		sql = "INSERT into highscores (name, score) values ('Me', 1712)";
//		command = new SqliteCommand (sql, connection);
//		command.ExecuteNonQuery();
//		sql = "INSERT into highscores (name, score) values ('Myself', 9001)";
//		command = new SqliteCommand (sql, connection);
//		command.ExecuteNonQuery();
//		sql = "INSERT into highscores (name, score) values ('Me', 8888)";
//		command = new SqliteCommand (sql, connection);
//		command.ExecuteNonQuery();
//
//		sql = "SELECT * FROM highscores ORDER BY score DESC";
//		command = new SqliteCommand (sql, connection);
//		SqliteDataReader reader = command.ExecuteReader ();
//		while (reader.Read ())
//			Debug.Log ("Name: " + reader ["name"] + " Score: " + reader ["score"]);
//
//
//		connection.Close ();
//	}








	



