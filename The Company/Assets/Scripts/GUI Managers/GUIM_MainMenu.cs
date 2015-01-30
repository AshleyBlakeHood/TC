using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIM_MainMenu : MonoBehaviour
{
	public Canvas mainMenuCanvas;
	public Canvas newGameCanvas;

	public Image colourPicker;

	private Image colourToChange;

	// Use this for initialization
	void Start ()
	{
		if (PlayerPrefs.GetInt ("Colours Set") == 0)
			SetDefaultSettings();
		else
			LoadColoursToGUI();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void SetObjectToColour(Image image)
	{
		colourToChange = image;
	}

	public void ShowWorldColourSetting()
	{
		colourToChange = newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - World Colour").GetComponent<Image> ();
		ShowColourPicker ();
	}

	public void ShowBackgroundColourSetting()
	{
		colourToChange = newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - Background Colour").GetComponent<Image> ();
		ShowColourPicker ();
	}

	public void ShowColourPicker()
	{
		colourPicker.gameObject.SetActive (true);
	}

	public void HideColourPicker()
	{
		colourPicker.gameObject.SetActive (false);
	}

	public void ColourPickerClicked()
	{
		int xSelection = (int)(Input.mousePosition.x - colourPicker.rectTransform.position.x + colourPicker.rectTransform.rect.width - (colourPicker.rectTransform.rect.width / 2));
		xSelection = (int)(xSelection * (colourPicker.sprite.texture.width / colourPicker.rectTransform.rect.width));
		int ySelection = (int)(Input.mousePosition.y - colourPicker.rectTransform.position.y + colourPicker.rectTransform.rect.height - (colourPicker.rectTransform.rect.height / 2));
		ySelection = (int)(ySelection * (colourPicker.sprite.texture.height / colourPicker.rectTransform.rect.height));

		Color colour = new Color ();
		colour = colourPicker.sprite.texture.GetPixel (xSelection, ySelection);
		colourToChange.color = colour;

		switch (colourToChange.name)
		{
		case "Image - World Colour":
			newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - DemoLand").GetComponent<Image>().color = colour;
			break;
		case "Image - Background Colour":
			newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - Background").GetComponent<Image>().color = colour;
			break;
		}
	}

	public void LoadMainScene()
	{
		SaveSettings ();
		GameManager.LoadMainScene ();
	}

	public void LoadColoursToGUI()
	{
		Color worldColour = new Color (PlayerPrefs.GetFloat ("World ColourR"), PlayerPrefs.GetFloat ("World ColourG"), PlayerPrefs.GetFloat ("World ColourB"));
		Color backgroundColour = new Color (PlayerPrefs.GetFloat ("Background ColourR"), PlayerPrefs.GetFloat ("Background ColourG"), PlayerPrefs.GetFloat ("Background ColourB"));
		
		newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - DemoLand").GetComponent<Image>().color = worldColour;
		newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - World Colour").GetComponent<Image> ().color = worldColour;
		
		newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - Background").GetComponent<Image>().color = backgroundColour;
		newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - Background Colour").GetComponent<Image> ().color = backgroundColour;
	}

	public void SaveSettings()
	{
		Color worldColour = newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - World Colour").GetComponent<Image> ().color;
		Color backgroundColour = newGameCanvas.transform.FindChild ("Panel - Customise The Company/Image - Background Colour").GetComponent<Image> ().color;

		PlayerPrefs.SetFloat ("World ColourR", worldColour.r);
		PlayerPrefs.SetFloat ("World ColourG", worldColour.g);
		PlayerPrefs.SetFloat ("World ColourB", worldColour.b);
		
		PlayerPrefs.SetFloat ("Background ColourR", backgroundColour.r);
		PlayerPrefs.SetFloat ("Background ColourG", backgroundColour.g);
		PlayerPrefs.SetFloat ("Background ColourB", backgroundColour.b);

		PlayerPrefs.SetInt ("Colours Set", 1);
	}

	public void SetDefaultSettings()
	{
		//"0.35294117647,0.50196078431,0.73725490196"
		PlayerPrefs.SetFloat ("World ColourR", 0.35294117647f);
		PlayerPrefs.SetFloat ("World ColourG", 0.50196078431f);
		PlayerPrefs.SetFloat ("World ColourB", 0.73725490196f);

		PlayerPrefs.SetFloat ("Background ColourR", 0.35294117647f);
		PlayerPrefs.SetFloat ("Background ColourG", 0.50196078431f);
		PlayerPrefs.SetFloat ("Background ColourB", 0.73725490196f);

		PlayerPrefs.SetInt ("Colours Set", 1);

		LoadColoursToGUI ();
	}
}
