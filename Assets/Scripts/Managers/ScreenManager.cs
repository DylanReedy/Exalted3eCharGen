using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine.UI;


public class ScreenManager : MonoBehaviour {

	public GameObject StartPanel;
	public GameObject OverviewPanel;
	public GameObject LoadButtonPanel;
	public Canvas GameCanvas;
	public CharacterManager charManager;
	public CharacterBuilder charBuilder;

	// Use this for initialization
	void Start () {
	}


	public void LoadCharacter(){
		LoadButtonPanel.SetActive (true);
		string savedChars = null;
		try{savedChars = File.ReadAllText ("SavedCharacters");}
		catch{}
		if (savedChars != null) {
			SavedCharacters sc = JsonUtility.FromJson<SavedCharacters> (savedChars);	
			foreach (string s in sc.Characters) {
				GameObject CharSelectButton = Resources.Load<GameObject> ("Prefabs/UI/CharacterChoice");
				GameObject instance = Instantiate (CharSelectButton);
				instance.GetComponent<CharacterLoader> ().charName.text = s;
				instance.GetComponent<CharacterLoader> ().charChoice.onClick.AddListener (delegate {
					CloseLoadPanel();
					LoadOverviewPanel(s);
				});
				instance.transform.SetParent (LoadButtonPanel.transform);
			}
		}
	}

	void CloseLoadPanel(){
		LoadButtonPanel.SetActive (false);
	}

	void LoadOverviewPanel(string name){
		StartPanel.SetActive (false);
		OverviewPanel.SetActive (true);
		charManager.LoadCharacter (name);
		charBuilder.LoadCharacter ();
		charBuilder.InitializeInterface ();
	}

	public void NewCharacter(){
		StartPanel.SetActive (false);
		OverviewPanel.SetActive (true);
		charBuilder.InitializeInterface ();
		charBuilder.LoadCharacter ();
		charManager.character = new Character ();
	}


}
