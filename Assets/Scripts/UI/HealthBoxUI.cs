using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBoxUI : MonoBehaviour {

	public Button button;
	public Image image;
	HealthBox healthBox;

	// Use this for initialization
	void Awake () {
		button = GetComponent<Button> ();
		image = GetComponent<Image> ();
		image.sprite = Resources.Load	<Sprite> ("Images/emptycircle");
	}
	
	public void initialize(HealthBox hb){
		print ("init button:");
		print (button);
		healthBox = hb;
		button.onClick.AddListener (delegate{healthBox.Advance ();});
		button.onClick.AddListener (delegate{advanceImage();});
	}

	private void advanceImage(){
		switch (healthBox.type) {
		case HealthStatus.Healthy:
			{
				image.sprite = Resources.Load<Sprite>("Images/emptycircle");
				//none
				return;
			}
		case HealthStatus.Bashing:
			{
				image.sprite = Resources.Load<Sprite> ("Images/blackdot");
				//bashing
				return;
			}
		case HealthStatus.Lethal:
			{
				image.sprite = Resources.Load<Sprite> ("Images/redcircle");
				//lethal
				return;
			}
		case HealthStatus.Aggravated:
			{
				image.sprite = Resources.Load<Sprite> ("Images/yellowcircle");
				//aggravated
				return;
			}
		default:{
				return;
			}
		}
	}
}
