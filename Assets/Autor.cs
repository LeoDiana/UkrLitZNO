using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autor{

	public int Fields=4;
	public string[] Data;
    public List<string> Quotes;

    //0 - public string Name;
    //1 - public string BornName;
    //2 - public string Movement 
    //3 - public string Group;


    public Autor()
	{
		Data= new string[Fields];
        Quotes = new List<string>();
	}
}
