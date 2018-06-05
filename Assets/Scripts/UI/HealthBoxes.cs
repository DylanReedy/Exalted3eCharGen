using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBoxes : MonoBehaviour {

	public GameObject zero;
	public GameObject one;
	public GameObject two;
	public GameObject four;
	public GameObject inc;
	public List<GameObject> healthBoxList = new List<GameObject> ();

	public void AddBox(HealthBox hb){
		GameObject healthbox = Resources.Load<GameObject> ("Prefabs/UI/HealthBox");
		GameObject newBox = Instantiate (healthbox);
		healthBoxList.Add (newBox);
		newBox.GetComponent<HealthBoxUI> ().initialize (hb);
		switch (hb.level) {
		case 7:
			{
				newBox.transform.SetParent (inc.transform);
				break;
			}
		case 0:
			{
				newBox.transform.SetParent (zero.transform);
				break;
			}
		case 1:
			{
				newBox.transform.SetParent (one.transform);
				break;
			}
		case 2:
			{
				newBox.transform.SetParent (two.transform);
				break;
			}
		case 4:
			{
				newBox.transform.SetParent (four.transform);
				break;
			}
		default:
			{
				break;	
			}
		}
	}

}
