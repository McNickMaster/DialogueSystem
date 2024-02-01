using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class LoadTextFromCSV : MonoBehaviour
{
    public string fileName;

    public List<Line> data = new List<Line>();


    // Start is called before the first frame update
    void Awake()
    {
        LoadCSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadCSV(string file)
    {
        data = Sinbad.CsvUtil.LoadObjects<Line>("Assets/RawText/" + file + ".csv");

        int i = 2;
        Debug.Log(data[i].ID + " " + data[i].TITLE + " " + data[i].BODY);


        

    }






    void LoadCSV()
    {
        LoadCSV(fileName);
    }



}

[System.Serializable]
public class Line
{
    public string ID;
    public string TITLE;
    public string BODY;


}
