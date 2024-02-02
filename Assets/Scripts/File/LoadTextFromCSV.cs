using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;

public class LoadTextFromCSV : MonoBehaviour
{
    public string fileName;
    

    public List<Line> data = new List<Line>();
   // public List<List<KeyValuePair<int,int>>> branchIDGroups =  new List<List<KeyValuePair<int,int>>>();

    List<List<KeyValuePair<int,int>>> tree;

    public List<Slide> slides = new List<Slide>();
    public Path path;


    // Start is called before the first frame update
    void Awake()
    {
        LoadCSV();
        CreateObjectsFromCSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateObjectsFromCSV()
    {

        List<Branch> branches = new List<Branch>();
        foreach(List<KeyValuePair<int,int>> list in tree)
        {   
            for(int i = 0; i < list.Count; i++)
            {
   //             branches.Add(new Branch());
                

                //Debug.Log("      " + list[i].Value + " " + list[i].Key);
            }
  
        }


    }





    public void LoadCSV()
    {
        LoadCSV(fileName);
    }

    void LoadCSV(string file)
    {
        data = Sinbad.CsvUtil.LoadObjects<Line>("Assets/RawText/" + file + ".csv");

        //int i = 2;
        //Debug.Log(data[i].ID + " " + data[i].TITLE + " " + data[i].BODY);

      

        tree = FindAllBranches();
        slides = GetAllSlides();

        Debug.Log("tree value " + tree[1][0].Value);
        path = GetRestOfPath(slides.Find(x => x.ID == tree[1][0].Value));
        
        /*
        Debug.Log("lists in tree: ");
        foreach(List<KeyValuePair<int,int>> list in tree)
        {
            Debug.Log("start list");    
            for(int i = 0; i < list.Count; i++)
            {
                Debug.Log("      " + list[i].Value + " " + list[i].Key);

                
            }

            Debug.Log("end list");    
        }
        Debug.Log("end tree");
        */

        //Debug.Log("size of tree: " + tree.Count);



        

        


    }

    public Path GetRestOfPath(Slide firstSlideInPath)
    {
        

        int lineWherePathStarts = 0;

        for(int j = 0; j < data.Count; j++)
        {
            int id = Int32.Parse(data[j].ID);

            if(id == firstSlideInPath.ID)
            {
                lineWherePathStarts = j;
            }



        }

        List<Slide> tempSlides = new List<Slide>();
        for(int j = lineWherePathStarts; j < data.Count; j++)
        {
            string id = data[j].ID;
            string startID = ""+firstSlideInPath.ID;

            Debug.Log(startID + " = " + id);
            Debug.Log("     " + id.StartsWith(startID));
            bool flag = true;

            
            for(int i = startID.Length-1; i < id.Length; i++)
            {
            
                if(id[i] == '1')
                {

                } else {
                    flag = false;

                }

            }   

            flag = flag && id.StartsWith(startID);


            if(flag)
            {
                Line tempLine = data.Find(x => x.ID == id);
                tempSlides.Add(new Slide(tempLine.TITLE, tempLine.BODY, Int32.Parse(tempLine.ID)));
            }


        }


        return new Path(tempSlides.ToArray());
    }

    public List<List<KeyValuePair<int,int>>> FindAllBranches()
    {
        tree = new List<List<KeyValuePair<int,int>>>();
        List<KeyValuePair<int,int>> root = FindBranchAt(0,0);
        tree.Add(root);
        List<KeyValuePair<int,int>> temp = new List<KeyValuePair<int,int>>();
        for(int j = 0; j < 6; j++)
        {
            for(int i = 0; i < root.Count; i++)
            {
                temp = FindBranchAt(j, root[i].Key);

                if(temp == null)
                {

                } else 
                {
                    if(tree.Contains(temp) || IsTempElementInTree(tree, temp))
                    {
                        //Debug.Log("temp was already in tree");
                    } else 
                    {
                        tree.Add(temp);
                    }
                    
                }
                    
                
                
            }
        }
        

        return tree;
    }

    List<KeyValuePair<int,int>> FindBranchAt(int startDigit, int startLine)
    { 
        List<KeyValuePair<int,int>> branch1 = new List<KeyValuePair<int,int>>();
        
        int max = -999;
        int pathAtI = 0;
        int pathsFoundInThisBranch = 0;


        //get digits line by line    
        for(int j = startLine; j < data.Count; j++)
        {
            string id = data[j].ID;
        
            if(id.Length-1 < startDigit)
            {
                //Debug.Log("too small");
            } else {
                int x = Int32.Parse(id.Substring(startDigit, 1));
                if(x > max)
                {
                    max = x;
                    pathAtI = startDigit;
                    pathsFoundInThisBranch++;
                    if(Int32.Parse(id) == 0)
                    {

                    } else 
                    {
                        KeyValuePair<int, int> pair = new KeyValuePair<int, int>(j, Int32.Parse(id));
                        branch1.Add(pair);
                    }
                        

                }
            }


        }
            
        if(branch1.Count > 1)
        {
            return branch1;
        }

        return null; 
    }

    List<Slide> GetAllSlides()
    {
        List<Slide> allSlides = new List<Slide>();

        for(int j = 0; j < data.Count; j++)
        {
            Slide temp = new Slide(data[j].TITLE, data[j].BODY, Int32.Parse(data[j].ID));
            if(!allSlides.Contains(temp))
            {
                allSlides.Add(temp);
            }
        }



        return allSlides;
    }


    private bool IsTempElementInTree(List<List<KeyValuePair<int,int>>> tree, List<KeyValuePair<int,int>> temp)
    {
        bool isElement = false;

        foreach(KeyValuePair<int,int> element in temp)
        {
            foreach(List<KeyValuePair<int,int>> list in tree)
            {
                isElement = list.Contains(element);
            }
        }





        return isElement;
    }
    private static bool isNeg1(int x)
    {
        bool isNeg = x.Equals(-1);
        //Debug.Log(x + " equal to -1? " + isNeg);
        return isNeg;
    }
    private static bool isNotNeg1(int x)
    {
        return !isNeg1(x);
    }
    private static bool isNull(List<int> list)
    {
        bool isNull = list !=null;
        
        return isNull;
    }



    


}





[System.Serializable]
public class Line
{
    public string ID;
    public string TITLE;
    public string BODY;


}
