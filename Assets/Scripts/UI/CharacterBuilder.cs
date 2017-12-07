using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class CharacterBuilder : MonoBehaviour {

	public Dropdown CasteDropdown;
	public Dropdown SupernalDropdown;
	public Dropdown MeritDropdown;
	public InputField CharacterName;
	public Image CasteColor;
	public TogglePanel[] AttributePanels = new TogglePanel[Enum.GetValues(typeof(AttributeName)).Length];
	public TogglePanel[] AbilityPanels = new TogglePanel[Enum.GetValues(typeof(AbilityName)).Length];
	public TogglePanel WillpowerPanel;
    public TogglePanel EssencePanel;
	public TogglePanel LimitPanel;
    public GameObject MeritPanel;
	public Dictionary<Merit,GameObject> MeritObjects = new Dictionary<Merit,GameObject>();
	public InputPopup MeritInput;
	public CharmContainer Charms;
	public Text PersonalPoolMax;
	public Text PeripheralPoolMax;
	public InputField PersonalPoolCurrent;
	public InputField PeripheralPoolCurrent;
	public InputField CommittedPool;
	public Dropdown AnimaValue;
	public CharacterManager charManager;
	public GameObject WeaponPanel;


	void Awake(){
		for (int i = 0; i < AbilityPanels.Length; i++) {
			AbilityPanels [i].Title.text = ((AbilityName)Enum.GetValues (typeof(AbilityName)).GetValue(i)).ToString() + "_______________";
		}
	}

	void Start () {
		
		CasteDropdown.onValueChanged.AddListener (delegate {
			SetCaste ();
		});

		SupernalDropdown.onValueChanged.AddListener (delegate { SetSupernal();
		});

		WillpowerPanel.SetValue(5);
		EssencePanel.SetValue (1);
		LimitPanel.SetValue(0);
		int personal = ((charManager.character.Essence * 3) + 10);
		int peripheral = ((charManager.character.Essence * 7) + 26);
		PersonalPoolMax.text = "/ " + personal.ToString();
		PeripheralPoolMax.text = "/ " + peripheral.ToString();
		PersonalPoolCurrent.text = personal.ToString();
		PeripheralPoolCurrent.text = peripheral.ToString();
		CommittedPool.text = "0";
	}

	void SetSupernal(){
		charManager.character.SupernalAbility = SupernalDropdown.value;// (int)Enum.Parse (typeof(AbilityName), SupernalDropdown.captionText.text);
	}

	void SetCaste(){
		foreach (TogglePanel tp in AbilityPanels) {
			tp.Title.color = Color.white;
		}
		charManager.character.caste = CasteDropdown.captionText.text;
		int[] abilitiesToChange = CasteAttributes.AttributeLookup [charManager.character.caste];
		Color color;
		switch (charManager.character.caste) {
		case Caste.DAWN:{
				color = Color.red;
				break;
			}
		case Caste.ECLIPSE:{
				color = Color.magenta;
				break;
			}
		case Caste.NIGHT:
			{
				color = Color.blue;
				break;
			}
		case Caste.TWILIGHT:
			{
				color = Color.cyan;
				break;
			}
		case Caste.ZENITH:
			{
				color = Color.yellow;
				break;
			}
		default:
			{
				color = Color.green;
				break;
			}
		}

		for (int i = 0; i < abilitiesToChange.Length; i++) {
			AbilityPanels [abilitiesToChange[i]].Title.color = color;
		}
		CasteColor.color = color;
		InitializeSupernal ();
	}

//    public void AddMerit() {
//		MeritInput.gameObject.SetActive(true);
//		Merit merit = Merit.MeritLookup [MeritDropdown.captionText.text];
//		if (charManager.character.canAddMerit (merit)) {
//			GameObject button = Resources.Load("Prefabs/UI/InputButton") as GameObject;
//			List<GameObject> Buttons = new List<GameObject> ();
//			for (int j = 0; j < merit.Cost.Count; j++) {
//				GameObject newButton = Instantiate(button);
//				Buttons.Add (newButton);
//				int k = j;
//				newButton.transform.SetParent(MeritInput.buttonPanel.transform);
//				newButton.GetComponentInChildren<Text>().text = merit.Cost[k].ToString();
//				newButton.GetComponentInChildren<Button>().onClick.AddListener(delegate{
//					//Handle the Merit Data
//					merit.Level = merit.Cost[k];
//					charManager.character.Merits.Add(merit);
//					//When the button is clicked, disable the MeritInput and clear the buttons
//					MeritInput.gameObject.SetActive(false);
//					foreach(GameObject go in Buttons){
//						Destroy(go);
//					}
//					//Create and populate the UI elements of a MeritObject
//					GameObject meritObj = Resources.Load ("Prefabs/UI/MeritChoice") as GameObject;
//					GameObject newMerit = Instantiate (meritObj);
//					newMerit.transform.SetParent (MeritPanel.transform);
//					foreach (Text text in newMerit.GetComponentsInChildren<Text>()) {
//						if (text.gameObject.name == "MeritName") {
//							text.text = merit.Name;
//						} else if (text.gameObject.name == "MeritCost") {
//							text.text = merit.Level.ToString();
//						}
//					}			
//					//Set the Remove Merit button listener
//					newMerit.GetComponentInChildren<Button> ().onClick.AddListener (delegate {
//						RemoveMerit(merit);
//					});
//					//Store a reference of the Merit Object
//					MeritObjects.Add (merit, newMerit);
//				});
//			}
//		} else {
//			print ("failed to create merit string object");
//		}
//	}

//	public void RemoveMerit(Merit merit){
//		charManager.character.Merits.Remove(merit);
//		MeritObjects.Remove (merit);
//		GameObject meritObj = MeritObjects [merit];
//		MeritObjects.Remove (merit);
//		Destroy (meritObj);
//
//	}

	void InitializeAnimaDropdown(){
		AnimaValue.onValueChanged.AddListener (delegate {
			charManager.character.AnimaLevel = AnimaValue.value;
		});
	}

	void InitializeAbilityButtons(){
		for (int i = 0; i < AbilityPanels.Length; i++) {
			int j = i;
			AbilityPanels [j].AddListener (delegate {
				charManager.character.SetAbility(j, AbilityPanels [j].CurrentValue);
			});
		}
	}

	void InitializeAttributeButtons(){
		for (int i = 0; i < AttributePanels.Length; i++) {
			int j = i;
			AttributePanels [j].AddListener (delegate {
				print("Attribute Panel button clicked for " + (AttributeName)j);
				charManager.character.SetAttribute (j, AttributePanels [j].CurrentValue);
				print((AttributeName)j + " is now " + AttributePanels[j].CurrentValue);
			});
		}
	}

	void InitializeWillpowerButtons(){
		WillpowerPanel.AddListener (delegate {
			charManager.character.Willpower = WillpowerPanel.CurrentValue;
		});
	}

	void InitializeLimitButtons(){
		LimitPanel.AddListener (delegate {
			charManager.character.Limit= LimitPanel.CurrentValue;
		});
	}

    void InitializeEssenceButtons() {
        EssencePanel.AddListener(delegate {
			charManager.character.Essence = EssencePanel.CurrentValue;
			PersonalPoolMax.text = ((charManager.character.Essence * 3) + 10).ToString();
			PeripheralPoolMax.text = ((charManager.character.Essence * 7) + 26).ToString();
        });
    }

    void InitializeSupernal(){
		SupernalDropdown.ClearOptions();
		if (charManager.character.caste != null) {
			for (int i = 0; i < CasteAttributes.AttributeLookup [charManager.character.caste].Length; i++) {
				SupernalDropdown.options.Add (new Dropdown.OptionData (){ text = ((AbilityName)CasteAttributes.AttributeLookup [charManager.character.caste] [i]).ToString() });
			}
		}
	}

//	void InitializeMerits(){
//		MeritContainer meritTest = Utils.LoadMerits ("Assets/JSON/Merits.txt");
//		foreach (Merit m in meritTest.Merits) {
//			MeritDropdown.options.Add (new Dropdown.OptionData (){ text = m.Name });
//		}
//	}

	public void InitializeInterface(){
		InitializeAbilityButtons ();
		InitializeAttributeButtons ();
		InitializeWillpowerButtons ();
		InitializeEssenceButtons ();
		InitializeLimitButtons ();
		InitializeAnimaDropdown ();
		print ("interface initialized");
	}

	public void LoadPlayScreen(){
	}

	public void SetCharacterName(){
		charManager.character.Name = CharacterName.text;
	}

	public void SetCharacterMotes(){
		charManager.character.PersonalMotes = Int32.Parse (PersonalPoolCurrent.text);
		charManager.character.PeripheralMotes = Int32.Parse (PeripheralPoolCurrent.text);
		charManager.character.CommittedMotes = Int32.Parse (CommittedPool.text);
	}
		
//	public void LoadMerit(Merit merit) {
//		//Create and populate the UI elements of a MeritObject
//		GameObject meritObj = Resources.Load ("Prefabs/UI/MeritChoice") as GameObject;
//		GameObject newMerit = Instantiate (meritObj);
//		newMerit.transform.SetParent (MeritPanel.transform);
//		foreach (Text text in newMerit.GetComponentsInChildren<Text>()) {
//			if (text.gameObject.name == "MeritName") {
//				text.text = merit.Name;
//			} else if (text.gameObject.name == "MeritCost") {
//				text.text = merit.Level.ToString();
//			}
//		}			
//		//Set the Remove Merit button listener
//		newMerit.GetComponentInChildren<Button> ().onClick.AddListener (delegate {
//			RemoveMerit(merit);
//		});
//		//Store a reference of the Merit Object
//		MeritObjects.Add (merit, newMerit);
//	}

	public void LoadCharacter(){
		Character character = charManager.character;
		CharacterName.text = character.Name;
		for (int i = 0; i < AttributePanels.Length; i++) {
			AttributePanels [i].SetValue (character.Attributes [i]);
		}
		for (int i = 0; i < AbilityPanels.Length; i++) {
			AbilityPanels [i].SetValue (character.Abilities [i]);
		}

		EssencePanel.SetValue (character.Essence);
		WillpowerPanel.SetValue(character.Willpower);
		LimitPanel.SetValue (character.Limit);

		foreach (Dropdown.OptionData option in CasteDropdown.options) {
			if (option.text == character.caste) {
				CasteDropdown.value = CasteDropdown.options.IndexOf (option);
				CasteDropdown.RefreshShownValue ();
			}
		}

		SupernalDropdown.value = character.SupernalAbility;
		SupernalDropdown.RefreshShownValue ();

		int personal = ((character.Essence * 3) + 10);
		int peripheral = ((character.Essence * 7) + 26);
		PersonalPoolMax.text = "/ " + personal.ToString();
		PeripheralPoolMax.text = "/ " + peripheral.ToString();
		PersonalPoolCurrent.text = character.PersonalMotes.ToString ();
		PeripheralPoolCurrent.text = character.PeripheralMotes.ToString();
		CommittedPool.text = character.CommittedMotes.ToString();
		GameObject weapon = Resources.Load<GameObject> ("Prefabs/UI/Weapon");
		for (int i = 0; i < 5; i++) {
			GameObject newWeapon = Instantiate<GameObject> (weapon);
			newWeapon.GetComponent<WeaponUI> ().charManager = charManager;
			if (character.GearList.Weapons.Count > i) {
				newWeapon.GetComponent<WeaponUI>().weapon = character.GearList.Weapons [i];
				newWeapon.transform.SetParent (WeaponPanel.transform);
				newWeapon.GetComponent<WeaponUI> ().LoadWeapon();
				//load the weapon into a ui component and make it
			}else{
				newWeapon.transform.SetParent (WeaponPanel.transform);
				//load a default blank weapon
			}
		}
	}
		
	public void CharmTest(){
		print (GameData.CharmLibrary.Archery [5].Description);
		print (GameData.CharmLibrary.Athletics [5].Description);
		print (GameData.CharmLibrary.Awareness [5].Description);
	}

}
