using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class CharmManager : MonoBehaviour {

	public Dropdown CascadeDropdown;
	public GameObject CascadeAnchor;
	public Dictionary<int,GameObject> CharmUILookup = new Dictionary<int, GameObject>();

	void Awake(){
		PopulateCharmMenu ();
	}

	void PopulateCharmMenu(){
		foreach (AbilityName abil in Enum.GetValues(typeof(AbilityName))) {
			CascadeDropdown.options.Add (new Dropdown.OptionData (abil.ToString ()));
		}
		CascadeDropdown.onValueChanged.AddListener(delegate{LoadCascade(CascadeDropdown.value);});
	}

	void LoadCascade(int i){
		string abilityToLoad = ((AbilityName)i).ToString ();
		GameObject charm = Resources.Load<GameObject> ("Prefabs/UI/Charm");
		string jsonCharms = File.ReadAllText ("Assets/Resources/Data/ArcheryCharms.txt");
		CharmContainer charms = JsonUtility.FromJson<CharmContainer> (jsonCharms);
		foreach (Charm c in charms.Charms) {
			if (c.Ability == abilityToLoad) {
				GameObject newCharm = Instantiate (charm);
				newCharm.GetComponent<CharmUI> ().Load(c);
				newCharm.transform.SetParent(CascadeAnchor.transform,false);
				newCharm.transform.localPosition = Vector3.zero;
				if (c.Node.x != 0) {
					newCharm.transform.Translate(325f * (float)c.Node.x, -175f * (float)c.Node.y, 0f);
					print (newCharm.GetComponent<CharmUI> ().charm.Name + " moved to " + newCharm.transform.position.x + ", " + newCharm.transform.position.y);
				}
				foreach (Node n in c.Children) {
					GameObject line = new GameObject ();
					line.transform.SetParent (CascadeAnchor.transform, false);
					line.transform.localPosition = Vector3.zero;
					line.AddComponent<Image> ();
					Vector3 start = new Vector3 (newCharm.transform.localPosition.x + newCharm.GetComponent<RectTransform> ().rect.width / 2f, newCharm.transform.localPosition.y - newCharm.GetComponent<RectTransform> ().rect.height / 2f, 0f);
					Vector3 finish = new Vector3(325f * (n.x) + newCharm.GetComponent<RectTransform>().rect.width/2f, -175f * (n.y) - newCharm.GetComponent<RectTransform>().rect.height/2f, 0f);
					MakeLine (start, finish, line.GetComponent<Image>());
				}
			}
		}
		CascadeAnchor.GetComponent<RectTransform> ().sizeDelta = new Vector2 (325f * (charms.width + 1) + 25f, 175f * (charms.height + 1) + 25f);
		CascadeAnchor.transform.localPosition = Vector3.zero;
	}

	void MakeLine(Vector3 pointB, Vector3 pointA, Image lineImage){

		Vector3 differenceVector = pointB - pointA;

		lineImage.rectTransform.sizeDelta = new Vector2 (differenceVector.magnitude, 10f);
		lineImage.rectTransform.pivot = new Vector2 (0, 0.5f);
		lineImage.transform.localPosition = pointA;
		float angle = Mathf.Atan2 (differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
		lineImage.rectTransform.rotation = Quaternion.Euler (0, 0, angle);
	}

}
