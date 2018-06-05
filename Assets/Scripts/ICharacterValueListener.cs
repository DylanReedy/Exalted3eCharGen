using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterValueListener{

    void OnNotify(int value);
    void OnNotify(ICharacterValueBroadcaster broadcaster, int value);

}
