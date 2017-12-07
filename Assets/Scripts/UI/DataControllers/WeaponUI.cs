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
	public CharacterManager charManager;

	void Awake(){
		weapon = new Weapon ();
	}

	public void UpdateWeapon(){
		print ("update weapon called, weapon ID " + weapon.ID + " weapon hash " + Weapon.WeaponHash);
		weapon.Name = NameField.text;
		weapon.Accuracy = AccField.text;
		weapon.Damage = DmgField.text;
		weapon.Defense = DefField.text;
		weapon.Overwhelming = OvwField.text;
		weapon.Parry = ParryField.text;
		weapon.Tags = TagsField.text;
		if (!charManager.character.GearList.Weapons.Contains (weapon)) {
			print ("weapon added to gear list");
			charManager.character.GearList.Weapons.Add (weapon);
		}
	}

	public void LoadWeapon(){
		NameField.text = weapon.Name;
		AccField.text = weapon.Accuracy;
		DmgField.text = weapon.Damage;
		DefField.text = weapon.Defense;
		OvwField.text = weapon.Overwhelming;
		ParryField.text = weapon.Parry;
		TagsField.text = weapon.Tags;
	}



}
