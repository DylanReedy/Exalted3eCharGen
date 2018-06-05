using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.IO;

public class CharacterBuilder : MonoBehaviour {

    [SerializeField]
    CharacterManager characterManager;
    [SerializeField]
    Character character = new Character();
	public InputField characterName;
	public Dropdown casteDropdown;
	public Dropdown SupernalDropdown;
//	public Dropdown MeritDropdown;
	public Dropdown AnimaDropdown;
	public InputField AnimaBanner;
    public InputField LimitTrigger;
    public InputField LimitBreak;
    public Image CasteColor;
    //public TogglePanel[] AttributePanels = new TogglePanel[Enum.GetValues(typeof(Ability)).Length];
    //public TogglePanel[] AbilityPanels = new TogglePanel[Enum.GetValues(typeof(Ability)).Length];
    public TogglePanelArrayHolder AttributePanels;
    public TogglePanelArrayHolder AbilityPanels;
    public TogglePanel MaxWillpowerPanel;
    public TogglePanel TempWillpowerPanel;
    public TogglePanel EssencePanel;
	public TogglePanel LimitPanel;
 //   public GameObject MeritPanel;
	//public Dictionary<Merit,GameObject> MeritObjects = new Dictionary<Merit,GameObject>();
	//public InputPopup MeritInput;
	public CharmCascade Charms;
	public Text PersonalPoolMax;
	public Text PeripheralPoolMax;
	public InputField PersonalPoolCurrent;
	public InputField PeripheralPoolCurrent;
	public InputField CommittedPool;
	public GameObject WeaponPanel;
	public GameObject ArmorPanel;
	public GameObject HealthPanel;
    [SerializeField]
    List<DicePoolPanel> DicePoolPanelList = new List<DicePoolPanel>();
    Dictionary<string, DicePoolPanel> DicePoolPanelDict = new Dictionary<string, DicePoolPanel>();
    Dictionary<string, BaseDicePool> DefaultDicePools = new Dictionary<string, BaseDicePool>() { { "Join Battle",new BaseDicePool{attribute = Attribute.Wits, ability = Ability.Awareness}},
        { "Evasion",new BaseDicePool{attribute = Attribute.Dexterity, ability = Ability.Dodge}},
        { "Combat Movement",new BaseDicePool{attribute = Attribute.Dexterity, ability = Ability.Dodge}},
        { "Rush/Withdraw",new BaseDicePool{attribute = Attribute.Dexterity, ability = Ability.Athletics}},
        { "Resist Poison/Disease",new BaseDicePool{attribute = Attribute.Stamina, ability = Ability.Resistance}},
        { "Senses",new BaseDicePool{attribute = Attribute.Perception, ability = Ability.Awareness}},
        { "Guile",new BaseDicePool{attribute = Attribute.Manipulation, ability = Ability.Socialize}}
    };

    public Character GetCharacter() {
        return character;
    }

    //Initialization via Awake() and Start() methods
    void Awake()
    {
        AttributePanels.InitializePanelWithEnumType<Attribute>();
        AbilityPanels.InitializePanelWithEnumType<Ability>();
        DicePoolPanelDict = DicePoolPanelList.ToDictionary(item => item.PoolName, item => item); 
    }

    void Start () {
        InitializeUIWidgets();
	}

    void InitializeUIWidgets() {
        InitializeName();
        InitializeCaste();
        InitializeSupernal();
        InitializeEnumPanels();
        InitializeWillpower();
        InitializeEssence();
        InitializeMotes();
        InitializeAnimaLevel();
        InitializeLimitPoints();
        InitializeDicePools();
        InitializeLimitTrigger();
        InitializeLimitBreak();
        InitializeAnimaBanner();
    }

    public void InitializeName()
    {
        characterName.onValueChanged.AddListener(delegate
        {
            character.name = characterName.text;
        });
    }

    public void InitializeCaste()
    {
        casteDropdown.onValueChanged.AddListener(delegate
        {
            SetCaste();
        });
        SetCaste();
    }

    void SetCaste()
    {
        AbilityPanels.SetAllPanelColors(Color.white);
        character.caste = (Caste)Enum.Parse(typeof(Caste), casteDropdown.captionText.text);
        Ability[] abilitiesToChange = GameData.CasteAbilities.AbilityLookup[character.caste];
        Color color;
        switch (character.caste)
        {
            case Caste.Dawn:
                {
                    color = Color.red;
                    break;
                }
            case Caste.Eclipse:
                {
                    color = Color.magenta;
                    break;
                }
            case Caste.Night:
                {
                    color = Color.blue;
                    break;
                }
            case Caste.Twilight:
                {
                    color = Color.cyan;
                    break;
                }
            case Caste.Zenith:
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
        foreach (Ability ability in abilitiesToChange) {
            AbilityPanels.SetPanelColor((int)ability, color);
        }

        CasteColor.color = color;
        InitializeSupernal();
    }

    void InitializeSupernal()
    {
        SupernalDropdown.ClearOptions();
        for (int i = 0; i < GameData.CasteAbilities.AbilityLookup[character.caste].Length; i++)
        {
            SupernalDropdown.options.Add(new Dropdown.OptionData() { text = ((Ability)GameData.CasteAbilities.AbilityLookup[character.caste][i]).ToString() });
        }
        SupernalDropdown.onValueChanged.AddListener(delegate
        {
            character.supernalAbility = (Ability)GameData.CasteAbilities.AbilityLookup[character.caste][SupernalDropdown.value];
        });
    }

    void InitializeEnumPanels() {
        for (int i = 0; i < Enum.GetNames(typeof(Ability)).Length; i++)
        {
            int j = i;
            AbilityPanels.AddListenerToPanel(j, SetAbility);
        }
        for (int i = 0; i < Enum.GetNames(typeof(Attribute)).Length; i++)
        {
            int j = i;
            AttributePanels.AddListenerToPanel(j, SetAttribute);
        }
    }

    void SetAbility(int ability, int value)
    {
        character.SetAbility(ability, value);
    }

    void SetAttribute(int attribute, int value)
    {
        character.SetAttribute(attribute, value);
    }

    void InitializeWillpower()
    {
        MaxWillpowerPanel.AddListener(delegate
        {
            character.maxWillpower = MaxWillpowerPanel.CurrentValue;
        });
        TempWillpowerPanel.AddListener(delegate
        {
            character.tempWillpower = TempWillpowerPanel.CurrentValue;
        });
    }

    void InitializeEssence()
    {
        EssencePanel.AddListener(delegate
        {
            character.essence = EssencePanel.CurrentValue;
            PersonalPoolMax.text = ((character.essence * 3) + 10).ToString();
            PeripheralPoolMax.text = ((character.essence * 7) + 26).ToString();
        });
    }

    void InitializeMotes()
    {
        PersonalPoolCurrent.onValueChanged.AddListener(delegate
        {
            character.personalMotes = Convert.ToInt32(PersonalPoolCurrent.text);
        });
        PeripheralPoolCurrent.onValueChanged.AddListener(delegate
        {
            character.peripheralMotes = Convert.ToInt32(PeripheralPoolCurrent.text);
        });
        CommittedPool.onValueChanged.AddListener(delegate
        {
            character.committedMotes = Convert.ToInt32(CommittedPool.text);
        });

    }

    void InitializeAnimaLevel()
    {
        AnimaDropdown.ClearOptions();
        foreach(AnimaIntensity intensity in Enum.GetValues(typeof(AnimaIntensity)))
        {
            AnimaDropdown.options.Add(new Dropdown.OptionData() { text = intensity.ToString()});
        }
        AnimaDropdown.onValueChanged.AddListener(delegate
        {
            character.animaLevel = (AnimaIntensity)AnimaDropdown.value;
        });
    }

    void InitializeLimitPoints() {
        LimitPanel.AddListener(delegate {
            character.limit = LimitPanel.CurrentValue;
        });
    }

    void InitializeLimitTrigger()
    {
        LimitTrigger.onValueChanged.AddListener(delegate
        {
            character.limitTrigger = LimitTrigger.text;
        });
    }

    void InitializeLimitBreak()
    {
        LimitBreak.onValueChanged.AddListener(delegate
        {
            character.limitBreak = LimitBreak.text;
        });
    }

    void InitializeAnimaBanner()
    {
        AnimaBanner.onValueChanged.AddListener(delegate
        {
            character.animaBanner = AnimaBanner.text;
        });
    }

    void InitializeDicePools()
    {
        print("initializing dice pools");
        foreach (KeyValuePair<string, BaseDicePool> kvpair in DefaultDicePools) {
            AttributePanels.AddListenerToPanel((int)kvpair.Value.attribute, DicePoolPanelDict[kvpair.Key]);
            AbilityPanels.AddListenerToPanel((int)kvpair.Value.ability, DicePoolPanelDict[kvpair.Key]);
        }
    }

    //public void InitializeConcept(){
    //TODO: Add a Concept widget to the UI and wire it up to the concept field of Character
    //}

    //void InitializeSpecialty(){
    //	//TODO: Implement specialty widgets and wire them to character
    //}

    //void InitializeXP(){
    //	//TODO: make ui widgets for current/total xp/solarxp in the top panel, wire up
    //}

    ////Calls all Initialize functions to wire up the UI
    //public void InitializeInterface(){
    //	InitializeName ();
    //	//InitializeCaste ();
    //	InitializeConcept ();
    //	InitializeAnimaLevel ();
    //	InitializeAnimaBanner ();
    //	InitializeSupernal ();
    //	//InitializeAttributes ();
    //	//InitializeAbilities ();
    //	InitializeSpecialty ();
    //	InitializeWillpower ();
    //	InitializeEssence ();
    //	InitializeLimitTrigger ();
    //	InitializeLimit ();
    //	InitializeMotes ();
    //	InitializeXP ();
    //	print ("interface initialized");
    //}

    //public void AddIntimacy(){
    //	//TODO: figure out weapon stuff
    //}

    //public void AddCharm(){
    //	//TODO: figure out weapon stuff
    //}

    //public void AddWeapon(){
    //	//TODO: figure out weapon stuff
    //}

    //public void AddArmor(){
    //	//TODO: figure out weapon stuff
    //}



    //public void ImportCharacter(){
    //	Character character = characterManager.character;
    //	characterName.text = character.name;
    //	for (int i = 0; i < AttributePanels.Length; i++) {
    //		AttributePanels [i].SetValue (character.attributes [i]);
    //	}
    //	for (int i = 0; i < AbilityPanels.Length; i++) {
    //		AbilityPanels [i].SetValue (character.abilities [i]);
    //	}

    //	EssencePanel.SetValue (character.essence);
    //	WillpowerPanel.SetValue(character.willpower);
    //	LimitPanel.SetValue (character.limit);

    //	foreach (Dropdown.OptionData option in casteDropdown.options) {
    //		if (option.text == character.caste.ToString()) {
    //			casteDropdown.value = casteDropdown.options.IndexOf (option);
    //			casteDropdown.RefreshShownValue ();
    //		}
    //	}

    //	SupernalDropdown.value = (int)character.supernalAbility;
    //	SupernalDropdown.RefreshShownValue ();

    //	AnimaDropdown.value = (int)character.animaLevel;
    //	AnimaDropdown.RefreshShownValue ();
    //	AnimaBanner.text = character.animaBanner;



    //	int personal = ((character.essence * 3) + 10);
    //	int peripheral = ((character.essence * 7) + 26);
    //	PersonalPoolMax.text = "/ " + personal.ToString();
    //	PeripheralPoolMax.text = "/ " + peripheral.ToString();
    //	PersonalPoolCurrent.text = character.personalMotes.ToString ();
    //	PeripheralPoolCurrent.text = character.peripheralMotes.ToString();
    //	CommittedPool.text = character.committedMotes.ToString();
    //	LimitTrigger.text = character.limitTrigger;

    //	//load saved weapons
    //	GameObject weapon = Resources.Load<GameObject> ("Prefabs/UI/Weapon");
    //	for (int i = 0; i < 5; i++) {
    //		GameObject newWeapon = Instantiate<GameObject> (weapon);
    //		if (character.weapons.Count > i) {
    //			newWeapon.GetComponent<WeaponUI>().weapon = character.weapons [i];
    //			newWeapon.transform.SetParent (WeaponPanel.transform);
    //			newWeapon.GetComponent<WeaponUI> ().LoadWeapon();
    //			//load the weapon into a ui component and make it
    //		}else{
    //			newWeapon.transform.SetParent (WeaponPanel.transform);
    //			//load a default blank weapon
    //		}
    //	}

    //	//load saved armors
    //	GameObject armor = Resources.Load<GameObject> ("Prefabs/UI/Armor");
    //	for (int i = 0; i < 2; i++) {
    //		GameObject newArmor= Instantiate<GameObject> (armor);
    //		if (character.armors.Count > i) {
    //			newArmor.GetComponent<ArmorUI>().armor= character.armors[i];
    //			newArmor.transform.SetParent (ArmorPanel.transform);
    //			newArmor.GetComponent<ArmorUI> ().LoadArmor();
    //			//load the armor into a ui component and make it
    //		}else{
    //			newArmor.transform.SetParent (ArmorPanel.transform);
    //			//load a default blank armor
    //		}
    //	}

    //	//load health levels
    //	foreach(HealthBox h in character.healthLevels){
    //		HealthPanel.GetComponent<HealthBoxes> ().AddBox (h);
    //	}

    //}

    //public void CharmTest(){
    //	print (GameData.CharmLibrary.Archery [5].description);
    //	print (GameData.CharmLibrary.Athletics [5].description);
    //	print (GameData.CharmLibrary.Awareness [5].description);
    //}

    //public void Test(){
    //	GameObject test = Resources.Load<GameObject> ("Prefabs/UI/Weapon");
    //	GameObject newTest = Instantiate<GameObject> (test);
    //	newTest.transform.SetParent (WeaponPanel.transform);
    //}

    //	void InitializeMerits(){
    //		MeritContainer meritTest = Utils.LoadMerits ("Assets/JSON/Merits.txt");
    //		foreach (Merit m in meritTest.Merits) {
    //			MeritDropdown.options.Add (new Dropdown.OptionData (){ text = m.Name });
    //		}
    //	}

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

}
