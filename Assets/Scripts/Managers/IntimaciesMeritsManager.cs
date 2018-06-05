using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;

public class IntimaciesMeritsManager : MonoBehaviour {

	public GameObject IntimacyHolder;
	public GameObject MeritHolder;
	public GameObject IntimacyPrefab;
	public GameObject MeritPrefab;
	public Button AddIntimacy;
	public Button AddMerit;
    [SerializeField] CharacterManager charactermanager;

	void Start(){
		AddIntimacy.onClick.AddListener(delegate{AddNewIntimacy();});
	}

	public void AddNewIntimacy(){
		GameObject newIntimacyUI = Instantiate (IntimacyPrefab);
		newIntimacyUI.transform.SetParent (IntimacyHolder.transform);
		IntimacyUI intUI = newIntimacyUI.GetComponent<IntimacyUI> ();
		Intimacy newIntimacy = new Intimacy ();
		intUI.intimacy = newIntimacy;
		//characterManager.character.AddIntimacy (newIntimacy);
		intUI.AddCallbacks ();
	}
		
}
