using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanelArrayHolder : MonoBehaviour {

    [SerializeField]
    TogglePanel[] Panels;

    public void InitializePanelWithEnumType<T>() {
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].Title.text = ((T)Enum.GetValues(typeof(T)).GetValue(i)).ToString();
        }
    }

    public int GetValueOfID(int ID)
    {
        return Panels[ID].CurrentValue;
    }

    public Dictionary<int, int> GetAllValues()
    {
        var dict = new Dictionary<int, int>();
        for (int i = 0; i < Panels.Length; i++)
        {
            int value = Panels[i].CurrentValue;
            dict.Add(i, value);
        }

        return dict;
    }

    public void SetAllPanelColors(Color color) {
        foreach (TogglePanel tp in Panels) {
            tp.Title.color = Color.white;
        }
    }

    public void SetPanelColor(int panelIndex, Color color) {
        Panels[panelIndex].Title.color = color;
    }

    public TogglePanel GetPanel(int panelIndex) {
        return Panels[panelIndex];
    }

    public void AddListenerToPanel(int panelIndex, Action<int,int> action) {
        Panels[panelIndex].AddListener(panelIndex, action);
    }

    public void AddListenerToPanel(int panelIndex, ICharacterValueListener valueListener)
    {
        Panels[panelIndex].AddListener(valueListener);
    }

}
