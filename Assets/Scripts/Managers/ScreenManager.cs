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
    public GameObject CurrentPanel;
    public GameObject CharmPanel;
    [SerializeField]
    CharacterManager characterManager;

	public void LoadCharacter(){
		LoadButtonPanel.SetActive (true);
		List<string> savedChars = characterManager.GetSavedCharacters ();
		if (savedChars != null) {
			foreach (string s in savedChars) {
				GameObject CharSelectButton = Resources.Load<GameObject> ("Prefabs/UI/CharacterChoice");
				GameObject instance = Instantiate (CharSelectButton);
				instance.GetComponent<CharacterLoader> ().charName.text = s;
				instance.GetComponent<CharacterLoader> ().charChoice.onClick.AddListener (delegate {
					characterManager.LoadCharacter (name);
					LoadOverviewPanel();
				});
				instance.transform.SetParent (LoadButtonPanel.transform);
			}
		}
	}

	public void NewCharacter(){
		LoadOverviewPanel ();
	}
		
	void LoadOverviewPanel(){
		CloseLoadPanel();
		OverviewPanel.SetActive (true);
		StartPanel.SetActive (false);
		//characterManager.InitializeInterface ();
		//CharacterBuilder.Instance.ImportCharacter ();
	}

	void CloseLoadPanel(){
		LoadButtonPanel.SetActive (false);
	}

    public void OpenCharmManager() {
        CurrentPanel.SetActive(false);
        CharmPanel.SetActive(true);
    }
}
