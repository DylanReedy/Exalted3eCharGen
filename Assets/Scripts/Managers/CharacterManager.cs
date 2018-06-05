using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterManager: MonoBehaviour {

	Character character;
	public readonly string chars = "SavedCharacters";

	void Awake(){}

	public void LoadCharacter(string name){
		string charText = File.ReadAllText(name);
		character = JsonUtility.FromJson<Character> (charText);
		print (name + " loaded...");
	}

	public void SaveCharacter(){
		//Can probably rewrite this without the SavedCharacters file - just control the directory where saved characters go, and load all files in that directory as characters.
		string savedChars;
		if (File.Exists (chars)) {
			savedChars = File.ReadAllText (chars);
			print ("saved characters loaded...");
		} else {
			savedChars = "";
		}

		SavedCharacters sc = JsonUtility.FromJson<SavedCharacters> (savedChars);

		if (sc == null) {
			sc = new SavedCharacters ();
		}

		string charData = JsonUtility.ToJson (character);
		if (!File.Exists (character.name)) {
			sc.Characters.Add (character.name);
			string characterList = JsonUtility.ToJson (sc);
			File.WriteAllText (chars, characterList);
			print ("new character saved!");
		}
		File.WriteAllText (character.name, charData);
		print ("character saved");
	}

	public List<string> GetSavedCharacters(){
		return JsonUtility.FromJson<SavedCharacters> (chars).Characters;
	}

    public void SetName(string name) { character.name = name; }
    public void SetCaste(Caste caste) { character.caste = caste; }
    public void SetAttribute(Attribute attribute, int value) { character.SetAttribute((int)attribute, value); }
    public void SetAbility(Ability ability, int value) { character.SetAbility((int)ability, value); }
    public void SetSupernalAbility(Ability ability) { character.supernalAbility = ability; }
    public void SetEssence(int essence) { character.essence = essence; }
    public void SetMaxWillpower(int maxWillpower) { character.maxWillpower = maxWillpower; }
    public void SetCurrentWillpower(int tempWillpower) { character.tempWillpower = tempWillpower; }
    public void SetLimitValue(int limitValue) { character.limit = limitValue; }
    public void SetLimitTrigger(string limitTrigger) { character.limitTrigger = limitTrigger; }
    public void SetLimitBreak(string limitBreak) { character.limitBreak = limitBreak; }
    public void SetAnimaIntensity(AnimaIntensity intensity) { character.animaLevel = intensity; }
    public void SetCurrentPersonalMotes(int currentPersonalMotes) { character.personalMotes = currentPersonalMotes; }
    public void SetCurrentPeripheralMotes(int currentPeripheralMotes) { character.peripheralMotes = currentPeripheralMotes; }
    //public void AddWeapon(Weapon weapon) { }
    //public void AddArmor(Armor armor) { }
    //public void AddCharm(Charm charm) { }
    //public void AddMerit(Merit merit) { }

}
