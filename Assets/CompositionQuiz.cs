using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class CompositionQuiz : MonoBehaviour {

    public QuizController QC;

    List<Composition> Compositions;  
    const int NAME = 0;

    string compName="";

    public Button comp;

    void ReadCompositionsXML()
    {
        Compositions = new List<Composition>();

        TextAsset textAsset = (TextAsset)Resources.Load("Compositions");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlElement xmlRoot = xmlDoc.DocumentElement;

        foreach (XmlNode xmlNode in xmlRoot)
        {
            Composition newComp = new Composition();

            foreach (XmlElement xmlElem in xmlNode)
            {
                if (xmlElem.Name == "Name")
                {
                    newComp.Data[0] = xmlElem.InnerText;
                }
                if (xmlElem.Name == "Autor")
                {
                    newComp.Data[1] = xmlElem.InnerText;
                }
                if (xmlElem.Name == "Movement")
                {
                    newComp.Data[2] = xmlElem.InnerText;
                }
                if (xmlElem.Name == "Genre")
                {
                    newComp.Data[3] = xmlElem.InnerText;
                }
                if (xmlElem.Name == "Dedicaded")
                {
                    newComp.Data[4] = xmlElem.InnerText;
                }
                if (xmlElem.Name == "Place")
                {
                    newComp.Data[5] = xmlElem.InnerText;
                }
                if (xmlElem.Name == "Characters")
                {
                    foreach (XmlElement xmlQ in xmlElem)
                        if (xmlQ.Name == "Character")
                        {
                            newComp.Characters.Add(xmlQ.InnerText);
                        }
                }
            }

            Compositions.Add(newComp);
        }
    }

    void CharacterQuestion()
    {
        Text buttonText;

        int randRight;
        do
        {
            randRight = Random.Range(0, Compositions.Count);
        } while (Compositions[randRight].Characters.Count == 0);

        int randQuote = Random.Range(0, Compositions[randRight].Characters.Count);

        QC.text.text = Compositions[randRight].Characters[randQuote];

        QC.rightAns = Random.Range(0, QC.buttons.Length);
        buttonText = QC.buttons[QC.rightAns].GetComponentInChildren<Text>();
        buttonText.text = Compositions[randRight].Data[NAME];

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
                rand = Random.Range(0, Compositions.Count);

                if (string.IsNullOrEmpty(Compositions[rand].Data[NAME]))
                    continue;

                bool suitable = true;
                for (int j = 0; j < k; j++)
                    if (Compositions[rand].Data[NAME] == Compositions[randVal[j]].Data[NAME])
                        suitable = false;

                if (suitable)
                    break;
            }

            randVal[k++] = rand;
            buttonText = QC.buttons[i].GetComponentInChildren<Text>();
            buttonText.text = Compositions[rand].Data[NAME];
        }
    }

    void Char_CharQuestion()
    {
        comp.GetComponentInChildren<Text>().text = "Твір";

        Text buttonText;
       
        int randRight;
        do
        {
            randRight = Random.Range(0, Compositions.Count);
        } while (Compositions[randRight].Characters.Count == 0);
        

        int randCH = Random.Range(0, Compositions[randRight].Characters.Count);

        QC.text.text = Compositions[randRight].Characters[randCH];

        QC.rightAns = Random.Range(0, QC.buttons.Length);
        buttonText = QC.buttons[QC.rightAns].GetComponentInChildren<Text>();

        int rand2;
        
        do
        {
            rand2 = Random.Range(0, Compositions[randRight].Characters.Count);
        } while (rand2==randCH);

        buttonText.text = Compositions[randRight].Characters[rand2];
        
        List<string> randStr = new List<string>();
        for(int j=0;j< Compositions[randRight].Characters.Count;j++)
            randStr.Add(Compositions[randRight].Characters[j]);

        for (int i = 0; i < QC.buttons.Length; i++)
        {
            if (i == QC.rightAns)
                continue;

            int rand;
            while (true)
            {
                rand = Random.Range(0, Compositions.Count);

                if (Compositions[rand].Characters.Count==0)
                    continue;
                
                bool suitable = true;
                for (int j = 0; j < randStr.Count; j++)
                    for(int g = 0; g < Compositions[rand].Characters.Count; g++)
                        if(Compositions[rand].Characters[g] == randStr[j])
                            suitable = false;

                if (suitable)
                    break;
            }

            for (int j = 0; j < Compositions[rand].Characters.Count; j++)
                randStr.Add(Compositions[rand].Characters[j]);

            int randChar= Random.Range(0, Compositions[rand].Characters.Count);
            
            buttonText = QC.buttons[i].GetComponentInChildren<Text>();
            buttonText.text = Compositions[rand].Characters[randChar];

            comp.enabled = true;
            compName = Compositions[randRight].Data[NAME];
        }
    }

    public void ShowComp()
    {
        comp.GetComponentInChildren<Text>().text = compName;
    }

    void CharactersQuestion()
    {
        Text buttonText;

        int randRight;
        do
        {
            randRight = Random.Range(0, Compositions.Count);
        } while (Compositions[randRight].Characters.Count == 0);

        string mainText = "";

        for (int i = 0; i < Compositions[randRight].Characters.Count-1; i++)
            mainText += Compositions[randRight].Characters[i] + ", ";
        mainText += Compositions[randRight].Characters[Compositions[randRight].Characters.Count - 1] + ".";

        QC.text.text = mainText;

        QC.rightAns = Random.Range(0, QC.buttons.Length);
        buttonText = QC.buttons[QC.rightAns].GetComponentInChildren<Text>();
        buttonText.text = Compositions[randRight].Data[NAME];

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
                rand = Random.Range(0, Compositions.Count);

                if (string.IsNullOrEmpty(Compositions[rand].Data[NAME]))
                    continue;

                bool suitable = true;
                for (int j = 0; j < k; j++)
                    if (Compositions[rand].Data[NAME] == Compositions[randVal[j]].Data[NAME])
                        suitable = false;

                if (suitable)
                    break;
            }

            randVal[k++] = rand;
            buttonText = QC.buttons[i].GetComponentInChildren<Text>();
            buttonText.text = Compositions[rand].Data[NAME];
        }
    }

    public void ReverseQuestion(int question)
    {
        question -= 4;

        Text buttonText;

        int randRight;
        do
            randRight = Random.Range(0, Compositions.Count);
        while (string.IsNullOrEmpty(Compositions[randRight].Data[question]));


        QC.text.text = Compositions[randRight].Data[question];

        QC.rightAns = Random.Range(0, QC.buttons.Length);
        buttonText = QC.buttons[QC.rightAns].GetComponentInChildren<Text>();
        buttonText.text = Compositions[randRight].Data[NAME];

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
                rand = Random.Range(0, Compositions.Count);

                if (string.IsNullOrEmpty(Compositions[rand].Data[NAME]))
                    continue;

                bool suitable = true;
                for (int j = 0; j < k; j++)
                    if (Compositions[rand].Data[NAME] == Compositions[randVal[j]].Data[NAME])
                        suitable = false;

                if (suitable)
                    break;
            }

            randVal[k++] = rand;
            buttonText = QC.buttons[i].GetComponentInChildren<Text>();
            buttonText.text = Compositions[rand].Data[NAME];
        }
    }

    public void NewQuestion(int question)
    {
        Text buttonText;

        QC.settings.themeCon.ButtonsColor(5, 5);

        if (question == 8 || question == 9)
        {
            ReverseQuestion(question);
            return;
        }
        if(question==10)
        {
            CharacterQuestion();
            return;
        }
        if(question==11)
        {
            CharactersQuestion();
            return;
        }
        if(question==12)
        {
            Char_CharQuestion();
            return;
        }

        int randField = question-4;


        int randRight;
        do
            randRight = Random.Range(0, Compositions.Count);
        while (string.IsNullOrEmpty(Compositions[randRight].Data[randField]));


        QC.text.text = Compositions[randRight].Data[0];

        QC.rightAns = Random.Range(0, QC.buttons.Length);
        buttonText = QC.buttons[QC.rightAns].GetComponentInChildren<Text>();
        buttonText.text = Compositions[randRight].Data[randField];

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
                rand = Random.Range(0, Compositions.Count);

                if (string.IsNullOrEmpty(Compositions[rand].Data[randField]))
                    continue;

                bool suitable = true;
                for (int j = 0; j < k; j++)
                    if (Compositions[rand].Data[randField] == Compositions[randVal[j]].Data[randField])
                        suitable = false;

                if (suitable)
                    break;
            }

            randVal[k++] = rand;
            buttonText = QC.buttons[i].GetComponentInChildren<Text>();
            buttonText.text = Compositions[rand].Data[randField];
        }
    }


    void Start ()
    { 
        ReadCompositionsXML();
    }
}
