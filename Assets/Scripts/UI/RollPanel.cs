using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RollPanel : MonoBehaviour {

	public Dropdown AttributeDropdown;
	public Dropdown AbilityDropdown;
	public InputField MiscInput;
	public Text TotalMod;
	public Button RollButton;
	public Text ResultText;

	void Awake(){
		InitializeAbilities ();
		InitializeAttributes ();
		//RollButton.onClick.AddListener (Roll);
	}

	void InitializeAttributes(){
		AttributeDropdown.ClearOptions ();
		foreach (Ability n in Enum.GetValues(typeof(Ability))) {
			AttributeDropdown.options.Add (new Dropdown.OptionData (){ text = n.ToString()});
		}
	}

	void InitializeAbilities(){
		AbilityDropdown.ClearOptions ();
		foreach (Ability n in Enum.GetValues(typeof(Ability))) {
			AbilityDropdown.options.Add (new Dropdown.OptionData (){ text = n.ToString()});

		}
	}

	//public void Roll(){
	//	Character character = PlayManager.character;
	//	int diceToRoll = 0;
	//	print (AbilityDropdown.captionText.text);
	//	diceToRoll += character.abilities [(int)Enum.Parse (typeof(Ability), AbilityDropdown.captionText.text)];
	//	diceToRoll += character.attributes[(int)Enum.Parse (typeof(Ability), AttributeDropdown.captionText.text)];
	//	int miscValue;
	//	if(Int32.TryParse(MiscInput.text, out miscValue)){
	//		diceToRoll += miscValue;
	//	} 
	//	TotalMod.text = diceToRoll.ToString ();
	//	int successes = Utils.RollDice (diceToRoll);
	//	if (successes == -1) {
	//		ResultText.text = "Botch!";
	//	} else {
	//		ResultText.text = successes.ToString ();
	//	}
	//}




}
