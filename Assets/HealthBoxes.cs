using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxes : MonoBehaviour {

	public GameObject zero;
	public GameObject one;
	public GameObject two;
	public GameObject four;
	public GameObject inc;

	public void AddLevel(string level){
		GameObject healthbox = Resources.Load<GameObject> ("Prefabs/UI/HealthBox");
		GameObject newBox = Instantiate (healthbox);
		
		switch (level) {
		case "zero":
			{
				newBox.transform.SetParent (zero.transform);
				break;
			}
		case "one":
			{
				newBox.transform.SetParent (one.transform);
				break;
			}
		case "two":
			{
				newBox.transform.SetParent (two.transform);
				break;
			}
		case "four":
			{
				newBox.transform.SetParent (four.transform);
				break;
			}
		case "inc":
			{
				newBox.transform.SetParent (inc.transform);
				break;
			}
		default:
			{
				break;	
			}
		}
	}

}
