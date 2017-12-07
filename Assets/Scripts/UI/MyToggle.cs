using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MyToggle : Toggle{

	public bool wasClicked = false;
	// Use this for initialization
	void Start () {
		//targetGraphic = Resources.Load("
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override void OnPointerClick(PointerEventData data){
		wasClicked = true;
		base.OnPointerClick (data);	
	}

	
}
