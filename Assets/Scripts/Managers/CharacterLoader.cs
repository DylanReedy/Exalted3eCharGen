using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLoader : MonoBehaviour {

	public Text charName;
	public Button charChoice;

	void Awake(){
		charChoice = gameObject.GetComponent<Button> ();
		charName = gameObject.GetComponentInChildren<Text> ();
	}

}
