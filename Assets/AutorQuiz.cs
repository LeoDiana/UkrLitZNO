using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class AutorQuiz : MonoBehaviour {

    public QuizController QC;

    List<Autor> Autors;
    const int NAME = 0;


	void ReadAutorsXML()
	{
		Autors = new List<Autor> ();

		TextAsset textAsset = (TextAsset) Resources.Load("Autors");  
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml ( textAsset.text );

		XmlElement xmlRoot = xmlDoc.DocumentElement;

		foreach (XmlNode xmlNode in xmlRoot)
		{
			Autor newAutor = new Autor ();

			foreach (XmlElement xmlElem in xmlNode)
			{
				if (xmlElem.Name == "Name") {
					newAutor.Data[0] = xmlElem.InnerText; 
				}
				if (xmlElem.Name == "BornName") {
					newAutor.Data[1] = xmlElem.InnerText;
				}
				if (xmlElem.Name == "Movement") {
					newAutor.Data[2] = xmlElem.InnerText;
				}
				if (xmlElem.Name == "Group") {
					newAutor.Data[3] = xmlElem.InnerText;
				}
                if(xmlElem.Name == "Quotes")
                {
                    foreach (XmlElement xmlQ in xmlElem)
                        if (xmlQ.Name == "Quote")
                        {
                            newAutor.Quotes.Add(xmlQ.InnerText);
                            //Debug.Log(xmlQ.InnerText);
                        }                            
                }
			}

			Autors.Add (newAutor);
		}
	}

	void Start () 
	{
		ReadAutorsXML ();
	}
		
	string DeleteName(string str)
	{
		string rez;
		int i=0;
		while (i < str.Length && str [i++] != ' ') {}	

		rez=str.Substring (i, str.Length - i);
		return rez;
	}

	string StrQuestionConverter(string str, int field)
	{
		if (field == 1)
			return DeleteName (str);
		else
			return str;
	}

    void MakeQuoteTest()
    {
        Text buttonText;

        int randRight;
        do{
            randRight = Random.Range(0, Autors.Count);
        } while (Autors[randRight].Quotes.Count==0);

        int randQuote = Random.Range(0, Autors[randRight].Quotes.Count);

        QC.text.text = Autors[randRight].Quotes[randQuote];

        QC.rightAns = Random.Range(0, QC.buttons.Length);
        buttonText = QC.buttons[QC.rightAns].GetComponentInChildren<Text>();        
        buttonText.text = Autors[randRight].Data[NAME];

        int[] randVal = new int[4];
        int k = 0;
        randVal[k++] = randRight;


        for (int i = 0; i < QC.buttons.Length; i++)
        {
            if (i == QC.rightAns)
                continue;

            int rand;
            while (true)
            {
                rand = Random.Range(0, Autors.Count);

                if (string.IsNullOrEmpty(Autors[rand].Data[NAME]))
                    continue;

                bool suitable = true;
                for (int j = 0; j < k; j++)
                    if (Autors[rand].Data[NAME] == Autors[randVal[j]].Data[NAME])
                        suitable = false;

                if (suitable)
                    break;
            }

            randVal[k++] = rand;
            buttonText = QC.buttons[i].GetComponentInChildren<Text>();
            buttonText.text = Autors[rand].Data[NAME];
        }
    }

	public void NewQuestion(int question)
	{
		Text buttonText;

        QC.settings.themeCon.ButtonsColor (5, 5);

        int randField = question;

        if (randField == 4)
        {
            MakeQuoteTest();
            return;
        }
        
		int randRight;
		do
			randRight = Random.Range(0, Autors.Count);
		while(string.IsNullOrEmpty(Autors[randRight].Data[randField]));


        QC.text.text=StrQuestionConverter(Autors[randRight].Data[0], randField);

        QC.rightAns = Random.Range (0, QC.buttons.Length);
		buttonText = QC.buttons [QC.rightAns].GetComponentInChildren<Text> ();
		buttonText.text = StrQuestionConverter(Autors[randRight].Data[randField], randField);

		int []randVal= new int[4];
		int k = 0;
		randVal [k++] = randRight;


		for (int i = 0; i < QC.buttons.Length; i++) {
			if (i == QC.rightAns)
				continue;

			int rand;
			while (true)
			{
				rand = Random.Range (0, Autors.Count);
		
				if (string.IsNullOrEmpty (Autors [rand].Data [randField]))
					continue;

				bool suitable = true;
				for (int j = 0; j < k; j++)
					if (Autors [rand].Data [randField]==Autors[randVal[j]].Data [randField])
						suitable = false;
				
				if(suitable)
					break;
			}

			randVal [k++] = rand;
			buttonText = QC.buttons [i].GetComponentInChildren<Text> ();
			buttonText.text = StrQuestionConverter(Autors [rand].Data[randField], randField);
		}
	}
}
