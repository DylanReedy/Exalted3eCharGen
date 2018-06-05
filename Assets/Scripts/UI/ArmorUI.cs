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
    [SerializeField]
    CharacterManager characterManager;

	void Awake(){
		armor = new Armor ();
	}

	public void UpdateArmor(){
		armor.name = NameField.text;
		armor.soak = SoakField.text;
		armor.hardness= HardnessField.text;
		armor.mobility = MobField.text;
		armor.tags = TagsField.text;
		//if (!characterManager.character.armors.Contains (armor)) {
		//	print ("armor added to gear list");
		//	characterManager.character.armors.Add (armor);
		//} else {
		//	int i = characterManager.character.armors.IndexOf (armor);
		//	characterManager.character.armors [i] = armor;
		//}
	}

	public void LoadArmor(){
		NameField.text = armor.name;
		SoakField.text = armor.soak;
		HardnessField.text = armor.hardness;
		MobField.text = armor.mobility;
		TagsField.text = armor.tags;
	}


}
