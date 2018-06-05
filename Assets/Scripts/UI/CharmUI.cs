using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CharmUI : MonoBehaviour {

	public Text CharmName;
	public Image[] EssenceReqs = new Image[5];
	public Image[] SkillReqs = new Image[5];
	public GameObject ExtraInfo;
	public Image lineImage;
	public Charm charm;
	public Button charmButton;

	public void ExtraInfoButton(){
		if (ExtraInfo.activeSelf) {
			ExtraInfo.SetActive (false);
		} else {
			ExtraInfo.SetActive (true);
		}
	}

	public void Load(Charm charm){
		this.charm = charm;
		LoadReqs ();
		CharmName.text = charm.Name;
	}

	public void LoadReqs(){
		for (int i = 0; i < EssenceReqs.Length; i++) {
			if (i < charm.MinEssence) {
				EssenceReqs [i].sprite = Resources.Load<Sprite> ("Images/yellowcircle");
			} else {
				EssenceReqs [i].sprite = Resources.Load<Sprite> ("Images/emptycircle");
			}
		}
		for (int i = 0; i < SkillReqs.Length; i++) {
			if (i < charm.MinAbility) {
				SkillReqs [i].sprite = Resources.Load<Sprite> ("Images/redcircle");
			} else {
				SkillReqs [i].sprite = Resources.Load<Sprite> ("Images/emptycircle");
			}
		}
	}

	void Awake(){
		LoadReqs ();
		Test ();
		charmButton = gameObject.AddComponent<Button> ();
	}

	void Test(){
	}
		
}
