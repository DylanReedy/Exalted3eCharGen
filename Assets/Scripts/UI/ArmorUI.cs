using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorUI : MonoBehaviour {

	public InputField NameField;
	public InputField SoakField;
	public InputField HardnessField;
	public InputField MobField;
	public InputField TagsField;
	public Armor armor = new Armor();
	public CharacterManager charManager;

	void Awake(){
		armor = new Armor ();
	}

	public void UpdateArmor(){
		armor.Name = NameField.text;
		armor.Soak = SoakField.text;
		armor.Hardness= HardnessField.text;
		armor.Mobility = MobField.text;
		armor.Tags = TagsField.text;
		if (!charManager.character.GearList.Armors.Contains (armor)) {
			print ("armor added to gear list");
			charManager.character.GearList.Armors.Add (armor);
		} else {
			int i = charManager.character.GearList.Armors.IndexOf (armor);
			charManager.character.GearList.Armors [i] = armor;
		}
	}

	public void LoadArmor(){
		NameField.text = armor.Name;
		SoakField.text = armor.Soak;
		HardnessField.text = armor.Hardness;
		MobField.text = armor.Mobility;
		TagsField.text = armor.Tags;
	}


}
