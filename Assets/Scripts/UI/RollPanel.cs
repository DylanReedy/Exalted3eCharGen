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
		RollButton.onClick.AddListener (Roll);
	}

	void InitializeAttributes(){
		AttributeDropdown.ClearOptions ();
		foreach (AttributeName n in Enum.GetValues(typeof(AttributeName))) {
			AttributeDropdown.options.Add (new Dropdown.OptionData (){ text = n.ToString()});
		}
	}

	void InitializeAbilities(){
		AbilityDropdown.ClearOptions ();
		foreach (AbilityName n in Enum.GetValues(typeof(AbilityName))) {
			AbilityDropdown.options.Add (new Dropdown.OptionData (){ text = n.ToString()});

		}
	}

	public void Roll(){
		Character character = PlayManager.character;
		int diceToRoll = 0;
		print (AbilityDropdown.captionText.text);
		diceToRoll += character.Abilities [(int)Enum.Parse (typeof(AbilityName), AbilityDropdown.captionText.text)];
		diceToRoll += character.Attributes[(int)Enum.Parse (typeof(AttributeName), AttributeDropdown.captionText.text)];
		int miscValue;
		if(Int32.TryParse(MiscInput.text, out miscValue)){
			diceToRoll += miscValue;
		} 
		TotalMod.text = diceToRoll.ToString ();
		int successes = Utils.RollDice (diceToRoll);
		if (successes == -1) {
			ResultText.text = "Botch!";
		} else {
			ResultText.text = successes.ToString ();
		}
	}




}
