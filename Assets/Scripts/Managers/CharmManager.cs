using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Newtonsoft.Json;

public class CharmManager : MonoBehaviour {

	public Dropdown CascadeDropdown;
	public GameObject CascadeAnchor;
	public GameObject CharmLayer;
	public GameObject EdgeLayer;
	public Dictionary<int,GameObject> CharmUILookup = new Dictionary<int, GameObject>();
	public List<GameObject> ObjectsInCascade = new List<GameObject> ();
	public CharmCascade currentCascade;
	public GameObject charmPrefab;
    [SerializeField]
    CharacterBuilder characterBuilder;
	bool fullCascade = false;
    Character character;

    private void OnEnable()
    {
        character = characterBuilder.GetCharacter();
    }

    void Awake(){
		PopulateCascadeMenu ();
		charmPrefab = Resources.Load<GameObject> ("Prefabs/UI/Charm");
	}
		
	void PopulateCascadeMenu(){
		foreach (Ability abil in Enum.GetValues(typeof(Ability))) {
			CascadeDropdown.options.Add (new Dropdown.OptionData (abil.ToString ()));
		}
		CascadeDropdown.onValueChanged.AddListener(delegate{LoadCascade(CascadeDropdown.value);});
	}

	void LoadCascade(int i){
        print(String.Format("load cascade {0}", i));
		ClearCascade ();
		string abilityToLoad = ((Ability)i).ToString();

		string jsonCharms = File.ReadAllText ("Assets/Resources/Data/" + abilityToLoad + "Charms.txt");
        currentCascade = JsonConvert.DeserializeObject<CharmCascade>(jsonCharms); //JsonUtility.FromJson<CharmCascade> (jsonCharms);
		CascadeAnchor.GetComponent<RectTransform> ().sizeDelta = new Vector2 (325f * (currentCascade.width + 1), 175f * (currentCascade.height + 1));
		CascadeAnchor.transform.localPosition = Vector3.zero;
		RefreshCascade ();
	}

    bool CanAccessCharms(Character character, Charm charm)
    {
        bool canAccess = character.essence >= charm.MinEssence && character.abilities[(int)charm.Ability] >= charm.MinAbility;
        if (charm.Prerequisites != null)
        {
            foreach (string pre in charm.Prerequisites)
            {
                canAccess = canAccess && character.charmReference.Contains(pre);
            }
        }
        return canAccess;
    }

    void ClearCascade(){
		for (int i = 0; i < ObjectsInCascade.Count; i++) {
			Destroy (ObjectsInCascade [i]);
		}
		CharmLayer.GetComponent<Image> ().sprite = null;
		CharmLayer.GetComponent<Image> ().color = Color.black;
	}

    public void BuyCharm(CharmUI c)
    {
        if (!character.charmReference.Contains(c.charm.Name))
        {
            character.AddCharm(c.charm);
        }
        else
        {
            character.RemoveCharm(c.charm);
            RevalidateCharms(character, c.charm);
            ClearCascade();
        }
        RefreshCascade();
        print(character.charms.Count);
    }

    void RefreshCascade()
    {
        foreach (Charm c in currentCascade.Charms)
        {
            if (CanAccessCharms(character, c))
            {
                GameObject newCharm = Instantiate(charmPrefab);
                ObjectsInCascade.Add(newCharm);
                newCharm.GetComponent<CharmUI>().Load(c);
                newCharm.GetComponent<CharmUI>().charmButton.onClick.AddListener(delegate
                {
                    BuyCharm(newCharm.GetComponent<CharmUI>());
                });
                if (character.charmReference.Contains(c.Name))
                {
                    newCharm.GetComponent<CharmUI>().GetComponent<Image>().color = Color.green;
                }
                newCharm.transform.SetParent(CharmLayer.transform, false);
                newCharm.transform.localPosition = Vector3.zero;
                if (c.Node.x != 0)
                {
                    newCharm.transform.Translate(325f * (float)c.Node.x, -175f * (float)c.Node.y, 0f);
                }
                //foreach (Node n in c.Parents)
                //{
                //    Vector3 start = new Vector3(newCharm.transform.localPosition.x, newCharm.transform.localPosition.y, 0f);
                //    Vector3 finish = new Vector3(325f * (n.x), -175f * (n.y), 0f);
                //    MakeLine(start, finish);
                //}
            }
        }
    }

    void RevalidateCharms(Character character, Charm charm)
    {
        List<Charm> charms = character.charms;
        for (int i = charms.Count - 1; i >= 0; i--)
        {
            foreach (string s in charms[i].Prerequisites)
                if (charms[i].Prerequisites.Contains(charm.Name))
                {
                    RevalidateCharms(character, charms[i]);
                    character.RemoveCharm(charms[i]);
                }
        }
        //when a prereq is killed, everything below it should also be killed
    }

    public void ShowFullCascade(){
		if (fullCascade == false) {
			CharmLayer.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Images/ArcheryCascade");//" + CascadeDropdown.value as Ability);
			CharmLayer.GetComponent<Image> ().color = Color.white;
			CascadeAnchor.GetComponent<RectTransform> ().sizeDelta = new Vector2 (2500f, 2000f);
			CascadeAnchor.transform.localPosition = Vector3.zero;
			fullCascade = true;
			print ("here");
		} else {
			fullCascade = false;
			ClearCascade ();
			print ("there");
		}
	}
		
}
