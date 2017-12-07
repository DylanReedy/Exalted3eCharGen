using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TogglePanel : MonoBehaviour {

    public Text Title;
    public Button[] Buttons = new Button[5];
    public Image[] ButtonImages = new Image[5];
	public int CurrentValue = 0;

	void Awake(){
		for (int i = 0; i < Buttons.Length; i++) {
			int j = i;
			Buttons[j].onClick.AddListener(delegate { ButtonClicked(j);});
		}	
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonClicked(int i) {
        for (int j = 0; j <= i; j++)
        {
            int k = j;
            ButtonImages[j].gameObject.SetActive(true);
			CurrentValue = i + 1;
        }
        for (int j = i+1; j < ButtonImages.Length; j++)
        {
            int k = j;
            ButtonImages[j].gameObject.SetActive(false);
        }
    }

	public void AddListener(UnityAction input){
		foreach (Button b in Buttons) {
			b.onClick.AddListener (input);
		}
	}

	public void SetPanel(int i){
		ButtonClicked (i-1);
	}

	public void SetValue(int i){
		ButtonClicked (i - 1);
	}

}
