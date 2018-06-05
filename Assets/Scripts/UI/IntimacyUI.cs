using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using NUnit.Framework;

public class IntimacyUI : MonoBehaviour {

	public InputField nameField;
	public InputField contextField;
	public Dropdown intensityDropdown;
	public Intimacy intimacy;

	public void AddCallbacks(){
		nameField.onEndEdit.AddListener (delegate {
			UpdateIntimacy();
		});
		contextField.onEndEdit.AddListener (delegate {
			UpdateIntimacy();
		});
		intensityDropdown.onValueChanged.AddListener (delegate {
			UpdateIntimacy();
		});
	}

	public void UpdateIntimacy(){
		intimacy.Name = nameField.text;
		intimacy.Context = contextField.text;
		intimacy.Intensity = (IntimacyIntensity)Enum.Parse(typeof(IntimacyIntensity),intensityDropdown.captionText.text);
	}

	public void LoadIntimacy(Intimacy i){
		intimacy = i;
		nameField.text = intimacy.Name;
		contextField.text = intimacy.Context;
		intensityDropdown.value = (int)intimacy.Intensity;
		intensityDropdown.RefreshShownValue ();
	}

	public void TestLoader(){
		LoadIntimacy (new Intimacy (){ Intensity = IntimacyIntensity.Defining });
	}
}
