using UnityEngine;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;

using SimpleJSON;

public class LoadTextFromJson : MonoBehaviour
{

    public string fileName = "dialogue_test";


    string[] textPath, textSlide;
    public Path[] myPathList;
    Slide[] mySlideList;

    Branch[] branches;

    // Start is called before the first frame update
    void Awake()
    {
        LoadDialogue();
        //LoadJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDialogue()
    {
        LoadJson(fileName);
    }

    

    public void LoadJson(string file)
    {
        fileName = file;
        
        fileName = "Assets/Dialogue/" + file + ".json";
        StreamReader r = new StreamReader(fileName);
        string json = r.ReadToEnd();
        json = json.Trim();

        JSONNode root = JSON.Parse(json);

        GeneratePaths(root);


        

    }

    public void GeneratePaths(JSONNode root)
    {
        JSONNode branch = root["Branch"];

        
        int pathCount = branch.AsArray.Count;
        int slideCount;
        //Debug.Log("pathCount: " + pathCount);

        myPathList = new Path[pathCount];
        List<Slide> tempSlides = new List<Slide>();
        for(int i = 0; i < pathCount; i++)
        {
            
            tempSlides = new List<Slide>();
            slideCount = branch[i][0].Count;
            for(int j = 0; j < slideCount; j++)
            {
                JSONNode pathNode = branch[i][0][j][0];
                
                //Debug.Log(root["Conversation"][i][0][j][0]);

                string slideTitle = pathNode[0];
                string slideBody = pathNode[1];

                Slide tempSlide = new Slide(slideTitle, slideBody);

                tempSlides.Add(tempSlide);
            }

            myPathList[i] = new Path(tempSlides.ToArray());
        }

    }

    public void GenerateBranches()
    {

    }

    
    public void GenerateTree()
    {

    }


}
