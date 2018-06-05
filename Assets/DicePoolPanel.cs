using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DicePoolPanel : MonoBehaviour,ICharacterValueListener {

    [SerializeField]
    Text PanelNameText;
    [SerializeField]
    Text PanelValueText;
    public string PoolName;
    int PoolValue;
    Dictionary<ICharacterValueBroadcaster, int> valuesToListenFor = new Dictionary<ICharacterValueBroadcaster, int>();
    
    public void SetPoolName(string name) {
        PoolName = name;
        PanelNameText.text = name;
    }

    public void SetPoolValue(int value) {
        PoolValue = value;
        PanelValueText.text = value.ToString();
    }

    public void OnNotify(int value)
    {
        
    }

    public void OnNotify(ICharacterValueBroadcaster broadcaster, int value)
    {
        if (valuesToListenFor.ContainsKey(broadcaster))
        {
            valuesToListenFor[broadcaster] = value;
        }
        else {
            valuesToListenFor.Add(broadcaster, value);
        }
        UpdatePoolValue();
    }

    void UpdatePoolValue() {
        int newValue = 0;
        foreach (KeyValuePair<ICharacterValueBroadcaster, int> kvpair in valuesToListenFor) {
            newValue += kvpair.Value;
        }
        SetPoolValue(newValue);
    }
}
