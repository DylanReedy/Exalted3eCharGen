using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class CharmManager : MonoBehaviour {

	public Dropdown CascadeDropdown;
	public GameObject CascadeAnchor;
	public GameObject CharmLayer;
	public GameObject EdgeLayer;
	public Dictionary<int,GameObject> CharmUILookup = new Dictionary<int, GameObject>();
	public List<GameObject> ObjectsInCascade = new List<GameObject> ();
	public List<Node> NodesPurchased = new List<Node>();

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
		ClearCurrentCascade ();
		string abilityToLoad = ((AbilityName)i).ToString ();
		GameObject charm = Resources.Load<GameObject> ("Prefabs/UI/Charm");
		string jsonCharms = File.ReadAllText ("Assets/Resources/Data/" + abilityToLoad + "Charms.txt");
		CharmCascade charms = JsonUtility.FromJson<CharmCascade> (jsonCharms);
		CascadeAnchor.GetComponent<RectTransform> ().sizeDelta = new Vector2 (325f * (charms.width + 1), 175f * (charms.height + 1));
		CascadeAnchor.transform.localPosition = Vector3.zero;
		foreach (Charm c in charms.Charms) {
			if (c.Ability == abilityToLoad) {
				GameObject newCharm = Instantiate (charm);
				ObjectsInCascade.Add (newCharm);
				newCharm.GetComponent<CharmUI> ().Load(c);
				newCharm.GetComponent<CharmUI> ().charmButton.onClick.AddListener (delegate {
					AddNode (newCharm.GetComponent<CharmUI>());
				});
				newCharm.transform.SetParent(CharmLayer.transform,false);
				newCharm.transform.localPosition = Vector3.zero;
				if (c.Node.x != 0) {
					newCharm.transform.Translate(325f * (float)c.Node.x, -175f * (float)c.Node.y, 0f);
					print (newCharm.GetComponent<CharmUI> ().charm.Name + " moved to " + newCharm.transform.localPosition.x + ", " + newCharm.transform.localPosition.y);
				}
				foreach (Node n in c.Parents) {
					Vector3 start = new Vector3 (newCharm.transform.localPosition.x, newCharm.transform.localPosition.y, 0f);
					Vector3 finish = new Vector3(325f * (n.x), -175f * (n.y), 0f);
					MakeLine (start, finish);
				}
			}
		}
		print ("cascade size" + CascadeAnchor.GetComponent<RectTransform> ().rect.width);
		print ("cascade size" + CascadeAnchor.GetComponent<RectTransform> ().rect.width);
	}

	void ClearCurrentCascade(){
		for (int i = 0; i < ObjectsInCascade.Count; i++) {
			Destroy (ObjectsInCascade [i]);
		}
	}

	void MakeLine(Vector3 pointB, Vector3 pointA){
		GameObject line = new GameObject ();
		ObjectsInCascade.Add (line);
		line.transform.SetParent (EdgeLayer.transform, false);
		line.transform.localPosition = Vector3.zero;
		line.AddComponent<Image> ();
		Image lineImage = line.GetComponent<Image> ();
		Vector3 differenceVector = pointB - pointA;

		lineImage.rectTransform.sizeDelta = new Vector2 (differenceVector.magnitude, 10f);
		lineImage.rectTransform.pivot = new Vector2 (0, 0.5f);
		lineImage.transform.localPosition = pointA;
		float angle = Mathf.Atan2 (differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
		lineImage.rectTransform.rotation = Quaternion.Euler (0, 0, angle);
	}

	public void AddNode(CharmUI c){
		if (NodesPurchased.Contains (c.charm.Node)) {
			print ("already purchased");
		} else {
			NodesPurchased.Add (c.charm.Node);
			c.gameObject.GetComponent<Image> ().color = Color.green;
		}
	}

}
