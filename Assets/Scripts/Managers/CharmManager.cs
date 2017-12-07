using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharmManager : MonoBehaviour {

	public static CharmManager Instance;
	public Dropdown CharmDropdown;
	public GameObject CascadeAnchor;
	public Dictionary<int,GameObject> CharmUILookup = new Dictionary<int, GameObject>();

	

	void Awake(){
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (this);
		}
		PopulateCharmMenu ();
	}

	void PopulateCharmMenu(){
		foreach (AbilityName abil in Enum.GetValues(typeof(AbilityName))) {
			CharmDropdown.options.Add (new Dropdown.OptionData (abil.ToString ()));
		}
		CharmDropdown.onValueChanged.AddListener(delegate{LoadCascade(CharmDropdown.value);});
	}

	void LoadCascade(int i){
//		List<Charm> AbilityCharms = GameData.GetCharms (i);
//		GameObject CharmUI = Resources.Load ("Prefabs/UI/CharmUI") as GameObject;
//		foreach (Charm c in AbilityCharms) {
//			GameObject cObject = Instantiate (CharmUI);
//			CharmUI ui = cObject.GetComponent<CharmUI> ();
//			ui.CharmName.text = c.Name;
//			ui.CharmSource.text = c.Source;
//			ui.CharmDescription.text = c.ShortDescription;
//			ui.ExtraInfoText.text = c.FullDescription;
//			cObject.transform.SetParent (CascadeAnchor.transform);
//			//logic to find the proper location for the charm
//		}
		//load a charmUI object. set its anchor to the top left via getcomponent<recttransform>().anchormin/max to 0,1 for both
		//get the y position from the y position of any parent charm.  get the x position as an average of parent x pos
		//store charm positions in tree in the charm explicitly.
		
	}


}
