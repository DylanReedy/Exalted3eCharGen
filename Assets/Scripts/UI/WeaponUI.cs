using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {

	public InputField NameField;
	public InputField AccField;
	public InputField DmgField;
	public InputField DefField;
	public InputField OvwField;
	public InputField ParryField;
	public InputField TagsField;
	public Weapon weapon = new Weapon ();
    [SerializeField]
    CharacterManager characterManager;

	void Awake(){
		weapon = new Weapon ();
	}

	//public void UpdateWeapon(){
	//	print ("update weapon called, weapon ID " + weapon.ID + " weapon hash " + Weapon.weaponHash);
	//	weapon.name = NameField.text;
	//	weapon.accuracy = AccField.text;
	//	weapon.damage = DmgField.text;
	//	weapon.defense = DefField.text;
	//	weapon.overwhelming = OvwField.text;
	//	weapon.parry = ParryField.text;
	//	weapon.tags = TagsField.text;
	//	if (!characterManager.character.weapons.Contains (weapon)) {
	//		print ("weapon added to gear list");
	//		characterManager.character.weapons.Add (weapon);
	//	} else {
	//		int i = characterManager.character.weapons.IndexOf (weapon);
	//		characterManager.character.weapons [i] = weapon;		
	//	}
	//}

	public void LoadWeapon(){
		NameField.text = weapon.name;
		AccField.text = weapon.accuracy;
		DmgField.text = weapon.damage;
		DefField.text = weapon.defense;
		OvwField.text = weapon.overwhelming;
		ParryField.text = weapon.parry;
		TagsField.text = weapon.tags;
	}



}
