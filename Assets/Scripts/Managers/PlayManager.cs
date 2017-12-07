using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml.Serialization;
using System.IO;

public class PlayManager : MonoBehaviour {

	public static Character character = new Character();
	public static PlayManager Instance;
	public GameObject Background;

	public CharmContainer charms;

	void Awake(){
		if (Instance == null) {
			Instance = this;
		} else if(Instance != this) {
			Destroy (gameObject);
		}
	}
				
	public void AddPanel(){
		GameObject panel = Resources.Load ("Prefabs/UI/RollPanel") as GameObject;
		GameObject newPanel = Instantiate (panel);
		newPanel.transform.SetParent (Background.transform);
	}

	public void TestXML(){
	}


}
