using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class Settings : MonoBehaviour {

	public bool[] EnableQuestions; 
	public GameObject panel;
	public int EnabledMarks;
	public Toggle[] toggles;
	public Button button;
    public Slider slider;

	Color TextColor;
	Color BackgroundColor;
	Color ButtonColor;

	public ThemeController themeCon;
    public Text text;

    string fileName;

	public void ChangeTheme(Button but)
	{
		if (but.tag == "Night") {
			TextColor = Color.white;
			ButtonColor = but.image.color;
		} 
		else {
			TextColor = new Color32(50, 50, 50, 255);
			ButtonColor = Color.white;
		}

		BackgroundColor = but.image.color;

        themeCon.ChangeTheme (TextColor, BackgroundColor, ButtonColor);
    }

	public void ButtonClicked()
	{
        if (panel.activeSelf && isLoaded)
            Save(TextColor, BackgroundColor, ButtonColor);

        panel.SetActive (!panel.activeSelf);
    }

    bool isLoaded;

	public void ToggleClicked(int id)
	{
		EnableQuestions [id] = !EnableQuestions [id];
		if (EnableQuestions [id])
            EnabledMarks++;
		else
            EnabledMarks--;

		button.onClick.Invoke();			
	}

	void Save(Color txtcol, Color backcol, Color btncol)
	{
        XmlDocument doc = new XmlDocument ();
		doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Settings></Settings>");

        XmlNode root = doc.DocumentElement;
		XmlElement togElem = doc.CreateElement ("Toggles");

		for (int i = 0; i < toggles.Length; i++) 
		{
			XmlNode node = doc.CreateNode ("element", "Toggle", "");
			if(toggles[i].isOn)
				node.InnerText=i+";t;";
			else
				node.InnerText=i+";f;";

            togElem.AppendChild (node);	
		}
		root.AppendChild (togElem);


        XmlElement themeElem = doc.CreateElement ("Theme");

		for (int i = 0; i < 3; i++) 
		{
			XmlNode node=doc.CreateNode ("element", "empty", "");;
			if (i == 0) 
			{
				node = doc.CreateNode ("element", "TextColor", "");
				node.InnerText=txtcol.r+";"+txtcol.g+";"+txtcol.b+";"+txtcol.a+";";
            }
			if (i == 1) 
			{
				node = doc.CreateNode ("element", "BackgroundColor", "");
				node.InnerText = backcol.r+";"+backcol.g+";"+backcol.b+";"+backcol.a+";";
            }
			if (i == 2) 
			{
				node = doc.CreateNode ("element", "ButtonColor", "");
				node.InnerText = btncol.r+";"+btncol.g+";"+btncol.b+";"+btncol.a+";";
            }
				
			themeElem.AppendChild (node);	
		}
		root.AppendChild (themeElem);

        XmlElement delayElem = doc.CreateElement("DelayNode");
        XmlNode nodeDelay = doc.CreateNode("element", "Delay", "");
        nodeDelay.InnerText = slider.value.ToString();
        delayElem.AppendChild(nodeDelay);
        root.AppendChild(delayElem);

        string path = Application.persistentDataPath + fileName;//getPath();

        StreamWriter outStream = System.IO.File.CreateText(path);
        doc.Save(outStream);
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
			//Debug.Log ("Con " + j + " " + s [j]);
		}

		rez = new Color (float.Parse (s [0]), float.Parse (s [1]),
			float.Parse (s [2]), float.Parse (s [3]));

		return rez;
	}

	void SetToggle(string str)
	{
		int i=0;
		string subst="";
		while (str [i] != ';')
			subst  += str [i++];
		
		int index = int.Parse (subst);
		if (str [i + 1] == 't')
			toggles [index].isOn = true;
		else
			toggles [index].isOn = false;	
	}

	public void Load()
	{
        isLoaded = true;

        string path = Application.persistentDataPath + fileName;
        Debug.Log(path);

        if (File.Exists(path))
        {
            XmlReader reader = XmlReader.Create(path);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);


            XmlElement xmlRoot = xmlDoc.DocumentElement;

            foreach (XmlNode xmlNode in xmlRoot)
            {
                foreach (XmlElement xmlElem in xmlNode)
                {
                    if (xmlElem.Name == "Toggle")
                        SetToggle(xmlElem.InnerText);


                    if (xmlElem.Name == "TextColor")
                        TextColor = ConvertToColor(xmlElem.InnerText);

                    if (xmlElem.Name == "BackgroundColor")
                        BackgroundColor = ConvertToColor(xmlElem.InnerText);

                    if (xmlElem.Name == "ButtonColor")
                        ButtonColor = ConvertToColor(xmlElem.InnerText);

                    if (xmlElem.Name == "Delay")
                        slider.value = float.Parse(xmlNode.InnerText);
                }
            }
        }
        else
        {
            TextColor = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1f);
            BackgroundColor = new Color(0.9485294f, 0.9485294f, 0.9485294f, 1f);
            ButtonColor = new Color(1f, 1f, 1f, 1f);

            Save(TextColor, BackgroundColor, ButtonColor);
        }

		themeCon.ChangeTheme (TextColor, BackgroundColor, ButtonColor);
        isLoaded = true;
	}

	void Start ()
	{
        isLoaded = false;
		ButtonClicked ();//hide settings

        int n = toggles.Length+1;//12;  //new Autor ().Fields+1;//+1-Quotes
		EnableQuestions = new bool[n];
		EnabledMarks = n-1;

		for (int i = 0; i < n; i++)
			EnableQuestions [i] = true;
		EnableQuestions [0] = false;

        fileName = "Settings.xml";

        Load();
	}

    /*private string getPath()
    {
    #if UNITY_EDITOR
            return Application.dataPath + "/Resources/" + fileName;
    #elif UNITY_ANDROID
            return Application.persistentDataPath+fileName;
    #elif UNITY_IPHONE
            return Application.persistentDataPath+"/"+fileName;
    #else
            return Application.dataPath +"/"+ fileName;
    #endif
    }*/
}
