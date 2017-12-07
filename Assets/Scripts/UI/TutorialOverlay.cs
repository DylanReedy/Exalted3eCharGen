using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialOverlay : MonoBehaviour {

	public Image GrayOverlay;
	public Image Highlight;

	void Start(){
		GrayOverlay = GetComponent<Image> ();
	}

}
