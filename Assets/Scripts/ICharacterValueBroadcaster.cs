using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterValueBroadcaster {

    void OnBroadcast();
    void AddListener(ICharacterValueListener listener);

}
