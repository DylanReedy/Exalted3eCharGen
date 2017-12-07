using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterManager : MonoBehaviour {

	public Character character;
	public List<string> characterSaves;

	void Awake(){
		character = new Character ();
	}

	public void LoadCharacter(string name){
		string testChar = File.ReadAllText(name);
		character = JsonUtility.FromJson<Character> (testChar);
		print (name + " loaded...");
	}

	public void SaveCharacter(){
		string savedChars;
		if (File.Exists ("SavedCharacters")) {
			savedChars = File.ReadAllText ("SavedCharacters");
			print ("saved characters loaded...");
		} else {
			savedChars = "";
		}

		SavedCharacters sc = JsonUtility.FromJson<SavedCharacters> (savedChars);
		if (sc == null) {
			sc = new SavedCharacters ();
		}

		string charData = JsonUtility.ToJson (character);
		if (!File.Exists (character.Name)) {
			sc.Characters.Add (character.Name);
			string characterList = JsonUtility.ToJson (sc);
			File.WriteAllText ("SavedCharacters", characterList);
			print ("new character saved!");
		}
		File.WriteAllText (character.Name, charData);
		print ("character saved");
	}

	public List<string> GetSavedCharacters(){
		return JsonUtility.FromJson<SavedCharacters> ("SavedCharacters").Characters;
	}
		
}
