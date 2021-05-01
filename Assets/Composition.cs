using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composition : MonoBehaviour {

    public int Fields = 6;
    public string[] Data;
    public List<string> Characters;

    //0 - public string Name;
    //1 - public string Autor;
    //2 - public string Movement 
    //3 - public string Genre;
    //4 - public string Dedicaded;
    //5 - public string Place;


    public Composition()
    {
        Data = new string[Fields];
        Characters = new List<string>();
    }
}
