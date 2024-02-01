using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class LoadTextFromCSV : MonoBehaviour
{
    public string fileName;
    

    public List<Line> data = new List<Line>();
    public List<List<int>> branchIDGroups = new List<List<int>>();


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

        //int i = 2;
        //Debug.Log(data[i].ID + " " + data[i].TITLE + " " + data[i].BODY);

        
        branchIDGroups = new List<List<int>>();
        //check numbers digit by digit WAIT THIS SHOULDNT BE DATA COUNT PLEASE CHANGE THIS FUTURE NICK
        for(int i = 0; i < data.Count; i++)
        {
            int max = -1;
            bool branchFound = false;
            //get digits line by line    
            for(int j = 0; j < data.Count; j++)
            {
                string id = data[j].ID;
                
                //if the id has the digit we are checking
                if(i < id.Length)
                {
                    //Debug.Log(id.Substring(i, 1));
                    int x = Int32.Parse(id.Substring(i, 1));
                    if(x > max)
                    {
                        max = x;
                        branchFound = true;
                        //Debug.Log("branch found at digit:" + i + " line: " + j);
                    }


                }

            }

            //this is broken or something idk
            if(branchFound)
            {
               for(int k = 0; k < max; k++)
                {
                    Debug.Log(Int32.Parse(data[k].ID));
                    branchIDGroups.Add(new List<int>(Int32.Parse(data[k].ID)));
                }
            }

        }


        foreach(List<int> list in branchIDGroups)
        {
            Debug.Log("branch: ");
            foreach(int item in list)
            {
                Debug.Log(item + " ");
            }
            
        }

        

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
