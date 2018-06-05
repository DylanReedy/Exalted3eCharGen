using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class TogglePanel : MonoBehaviour, ICharacterValueBroadcaster
{

    public Text Title;
    public Button[] Buttons = new Button[5];
    public Image[] ButtonImages = new Image[5];
	public int CurrentValue = 0;
    List<ICharacterValueListener> Listeners = new List<ICharacterValueListener>();

	void Awake(){
		for (int i = 0; i < Buttons.Length; i++) {
			int j = i;
			Buttons[j].onClick.AddListener(delegate { ButtonClicked(j);});
		}	
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
        OnBroadcast();
    }

    public void AddListener(UnityAction action) {
        foreach (Button b in Buttons)
        {
            b.onClick.AddListener(action);
        }
    }

    public void AddListener(int value, Action<int,int> input){
		foreach (Button b in Buttons) {
			b.onClick.AddListener (delegate { input(value, CurrentValue); });
		}
	}

	public void SetPanel(int i){
		ButtonClicked (i-1);
	}

	public void SetValue(int i){
		ButtonClicked (i - 1);
	}

    public void OnBroadcast()
    {
        foreach (ICharacterValueListener listener in Listeners) {
            listener.OnNotify(this, CurrentValue);
        }
    }

    public void AddListener(ICharacterValueListener listener)
    {
        Listeners.Add(listener);
    }
}
