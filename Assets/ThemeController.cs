using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class ThemeController : MonoBehaviour {

	Color buttonColor;

	Text[] text;//all text in in UI
	Image[] imagebg;//backgroung image
	Image[] imagebtn;//button image

	public void ChangeTheme(Color txtcol, Color backcol, Color btncol)
	{
		foreach (Text tx in text)
			tx.color = txtcol;
		
		foreach (Image img in imagebg)
			img.color = backcol;
		
		foreach (Image img in imagebtn)
			img.color = btncol;

		buttonColor = btncol;

		//Save (txtcol, backcol, btncol);
	}

	void Save(Color txtcol, Color backcol, Color btncol)
	{
		XmlDocument doc = new XmlDocument ();//new XmlDeclaration("1.0", "utf-8", "yes"));
		//doc.CreateXmlDeclaration ("1.0", "utf-8", "yes");
		doc.LoadXml("<Theme></Theme>");

		XmlNode root = doc.DocumentElement;
		XmlElement togElem = doc.CreateElement ("Toggles");



		XmlNode node = doc.CreateNode ("element", "t0", "");
		node.InnerText="true";
		togElem.AppendChild (node);
		//doc.AppendChild (togElem);
		root.AppendChild (togElem);


		/*XmlElement newElem = doc.CreateElement("TextColor");
		newElem.InnerText = txtcol.r+";"+txtcol.g+";"+txtcol.b+";"+txtcol.a+";";
		doc.DocumentElement.AppendChild(newElem);

		newElem = doc.CreateElement("BackgroundColor");
		newElem.InnerText = backcol.r+";"+backcol.g+";"+backcol.b+";"+backcol.a+";";
		doc.DocumentElement.AppendChild(newElem);

		newElem = doc.CreateElement("ButtonColor");
		newElem.InnerText = btncol.r+";"+btncol.g+";"+btncol.b+";"+btncol.a+";";
		doc.DocumentElement.AppendChild(newElem);*/


		// Save the document to a file. White space is
		// preserved (no white space).
		//doc.PreserveWhitespace = true;
		doc.Save(Application.dataPath+"/Resources/Settings.xml");
	}

	Color ConvertToColor(string str)
	{
		Color rez;

		string[] s=new string[4];
		int i = 0;

		for (int j = 0; j < 4; j++)
		{
			while (str [i] != ';')
				s[j] += str [i++];
			i++;
		}

		rez = new Color (float.Parse (s [0]), float.Parse (s [1]),
			float.Parse (s [2]), float.Parse (s [3]));

		return rez;
	}

	public void Load()
	{
		Color txtcol=Color.cyan, backcol=Color.cyan, btncol=Color.cyan;


		TextAsset textAsset = (TextAsset) Resources.Load("Settings");  
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml ( textAsset.text );

		XmlElement xmlRoot = xmlDoc.DocumentElement;

		foreach (XmlNode xmlNode in xmlRoot)
		{
			if (xmlNode.Name == "TextColor") 
				txtcol = ConvertToColor(xmlNode.InnerText); 

			if (xmlNode.Name == "BackgroundColor") 
				backcol = ConvertToColor(xmlNode.InnerText); 

			if (xmlNode.Name == "ButtonColor") 
				btncol = ConvertToColor(xmlNode.InnerText); 
		}

		ChangeTheme (txtcol, backcol, btncol);
	}

	public void ButtonsColor(int correct, int wrong)
	{
		if (correct == 5 && wrong == 5)
			foreach (Image img in imagebtn)
				img.color = buttonColor;
		else 
		{
			imagebtn [wrong].color = Color.red;
			imagebtn [correct].color = Color.green;
		}		
			
	}

	void Start()
	{
		text = GameObject.FindObjectsOfType<Text>();

		GameObject[] go = GameObject.FindGameObjectsWithTag("Background");
		imagebg = new Image[go.Length];
		for (int i = 0; i < imagebg.Length; i++)
			imagebg [i] = go [i].GetComponent<Image> ();

		GameObject[] go2 = GameObject.FindGameObjectsWithTag("Button");
		imagebtn = new Image[go2.Length];
		for (int i = 0; i < imagebtn.Length; i++)
			imagebtn [i] = go2 [i].GetComponent<Image> ();


		//sorting buttons by name (6-pos of number)
		for (int i = 0; i < imagebtn.Length; i++)
			if (imagebtn [i].name[6] != i)
			{
				Image buf = imagebtn [i];
				int id = imagebtn [i].name [6]-48;
				imagebtn [i] = imagebtn [id];
				imagebtn [id] = buf;
			}
				

		//Load ();
	}

}
