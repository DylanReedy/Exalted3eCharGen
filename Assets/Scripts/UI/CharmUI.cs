using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharmUI : MonoBehaviour {

	public Text CharmName;
	public Text CharmSource;
	public Text CharmDescription;
	public GameObject ExtraInfo;
	public Text ExtraInfoText;
	public Charm charm;

	public void ExtraInfoButton(){
		if (ExtraInfo.activeSelf) {
			ExtraInfo.SetActive (false);
		} else {
			ExtraInfo.SetActive (true);
		}
	}

	public void LoadCharm(Charm charm){
//		this.charm = charm;
//		CharmName.text = charm.Name;
//		CharmSource.text = charm.Source;
//		CharmDescription.text = charm.ShortDescription;
//		ExtraInfoText.text = charm.FullDescription;
	}

}
