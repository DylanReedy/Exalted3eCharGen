using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public TutorialOverlay AttributeOverlay;
	public TutorialOverlay AbilityOverlay;
	public TutorialOverlay CasteOverlay;
	public TutorialOverlay WillpowerOverlay;
	public GameObject TutorialLayer;
	public GameObject ModalPanel;
	public Text ModalText;
	public Button ModalNextButton;
    public Button ModalCloseButton;
	public Vector3 RootPosition;

	public const string AttributeMessage = "This is the Attribute panel. You have 1 point in each attribute by default. You can assign eight (8) points in one column, six (6) in another column, and four (4) in the " +
	                                       "third column."; 

	public const string CasteMessage = "This is the Caste panel. You can select your Caste with the dropdown menu.  Each Caste has particular abilities that they excel at, which are color coded in the Ability panel.";

	public const string AbilityMessage = "This is the Ability panel. Choose five (5) Caste abilities by clicking the toggle to the left of the name. Pick five (5) more abilities at your leisure - from your Caste" +
		"list or not! Once these ten (10) abilities are chosen, divide twenty-eight (28) dots among all abilities, with at least one in each of the ten (10) chosen abilities.  Abilities cannot be raised above 3 in this way.";

    public const string WillpowerMessage = "This is the Willpower panel.  You can raise your willpower with bonus points. You cannot raise your essence intentionally - that happens as you play!";

	void Start(){
		RootPosition = ModalPanel.transform.position;
		foreach (Transform t in ModalPanel.transform) {
			if (t.name == "ModalText") {
				ModalText = t.gameObject.GetComponent<Text> ();
			}
			if (t.name == "Next") {
				ModalNextButton = t.gameObject.GetComponent<Button> ();
			}
		}

        ModalCloseButton.onClick.AddListener(EndTutorial);
	}

	public void LoadAttributeMessage(){
		ResetOverlays ();
		ModalText.text = AttributeMessage;
		AttributeOverlay.GrayOverlay.enabled = false;
		AttributeOverlay.Highlight.enabled = true;
		ModalNextButton.onClick.RemoveAllListeners ();
		ModalNextButton.onClick.AddListener (LoadCasteMessage);
	}

	public void LoadCasteMessage(){
		ResetOverlays ();
		ModalText.text = CasteMessage;
		CasteOverlay.GrayOverlay.enabled = false;
		CasteOverlay.Highlight.enabled = true;
		ModalNextButton.onClick.RemoveAllListeners ();
		ModalNextButton.onClick.AddListener (LoadAbilityMessage);
	}

	public void LoadAbilityMessage(){
		ResetOverlays ();
		ModalText.text = AbilityMessage;
		AbilityOverlay.GrayOverlay.enabled = false;
		AbilityOverlay.Highlight.enabled = true;
		ModalNextButton.onClick.RemoveAllListeners ();
		ModalNextButton.onClick.AddListener (LoadWillpowerMessage);
	}

    public void LoadWillpowerMessage() {
        ResetOverlays();
        ModalText.text = WillpowerMessage;
        WillpowerOverlay.GrayOverlay.enabled = false;
        WillpowerOverlay.Highlight.enabled = true;
        ModalNextButton.onClick.RemoveAllListeners();
        ModalNextButton.onClick.AddListener(EndTutorial);
    }

	public void EndTutorial(){
		TutorialLayer.SetActive (false);
		ModalPanel.SetActive (false);
	}

	public void ResetOverlays(){
		AttributeOverlay.GrayOverlay.enabled = true;
		AttributeOverlay.Highlight.enabled = false;
		AbilityOverlay.GrayOverlay.enabled = true;
		AbilityOverlay.Highlight.enabled = false;
		CasteOverlay.GrayOverlay.enabled = true;
		CasteOverlay.Highlight.enabled = false;
		WillpowerOverlay.GrayOverlay.enabled = true;
		WillpowerOverlay.Highlight.enabled = false;
	}

	//TODO:  Make a script for the overlays so it's easier to toggle them on/off.  Make a script for the modal panel so its easier to access.  Make a content
	//fitter for the modal panel so the long text doesn't run under the buttons.

}
