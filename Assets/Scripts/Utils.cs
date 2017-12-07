using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public static class Utils {
//	public static MeritContainer LoadMerits(string MeritsJSON){
//		string jsonText = "";
//		StreamReader reader = new StreamReader (MeritsJSON);
//
//		using (reader) {
//			while (!reader.EndOfStream) {
//				jsonText += reader.ReadLine ();
//			}
//			reader.Close ();
//		}
//		jsonText = " { \"Merits\": " + jsonText + "}";
//		MeritContainer data = JsonUtility.FromJson<MeritContainer>(jsonText);
//		foreach (Merit m in data.Merits) {
//			Merit.MeritLookup.Add (m.Name, m);
//		}
//		return data;
//	}

	public static CharmContainer LoadCharms(){

//			string testChar = File.ReadAllText("erza");
//			character = JsonUtility.FromJson<Character> (testChar);
//			print ("erza loaded...");
		string jsonText = File.ReadAllText("Assets/Resources/Data/Charms.txt");
		jsonText = " { \"Charms\": " + jsonText + "}";
		CharmContainer data = JsonUtility.FromJson<CharmContainer>(jsonText);
		return data;	
	}

	public static int RollDice(int dice){
		return RollDice (dice, 8, 10);
	}

	public static int RollDice(int dice, int targetNumber, int explodeNumber){
		string testString = "";
		int successes = 0;
		bool botch = false;
		for (int i = 0; i < dice; i++) {
			int rollValue = Random.Range (1, 11);
			testString += rollValue + ",";
			if (rollValue >= explodeNumber) {
				successes += 2;
			}else if (rollValue >= targetNumber && rollValue < explodeNumber){
				successes++;
			}
			if (rollValue == 1 && !botch) {
				botch = true;
			}
		}
		Debug.Log ("roll result: " + testString);
		if (successes == 0 && botch) {
			Debug.Log ("botch!");
			return -1;
		} else {
			return successes;
		}
	}
}
