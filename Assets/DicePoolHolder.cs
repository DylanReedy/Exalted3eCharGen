using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePoolHolder : MonoBehaviour {

    const int MaxNumberOfPools = 15;
    Dictionary<string, DicePoolPanel> DicePoolPanels = new Dictionary<string, DicePoolPanel>();
    [SerializeField]
    Transform DicePoolParent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDicePoolValue(string name, int value) {
        DicePoolPanels[name].SetPoolValue(value);
    }

    public void CreateDicePool(string name, int value) {
        if (DicePoolPanels.Count <= MaxNumberOfPools)
        {
            DicePoolPanel newPanel = GeneratePoolPanel(name, value);
            DicePoolPanels.Add(name, newPanel);
            newPanel.transform.SetParent(DicePoolParent);
        }
    }

    DicePoolPanel GeneratePoolPanel(string name, int value)
    {
        DicePoolPanel panelResource = Resources.Load<DicePoolPanel>("Prefabs/DicePool");
        DicePoolPanel newPanel = Instantiate<DicePoolPanel>(panelResource);
        return newPanel;
    }

}
