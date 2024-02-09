using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sinbad;
using System.IO;

public class LoadTextFromCSV : MonoBehaviour
{
    public string fileName;
    
    [HideInInspector]
    public List<Line> data = new List<Line>();
   // public List<List<KeyValuePair<int,int>>> branchIDGroups =  new List<List<KeyValuePair<int,int>>>();

    List<List<KeyValuePair<int,int>>> tree;

    
    [HideInInspector]
    public List<Slide> slides = new List<Slide>();
    [HideInInspector]
    public List<Branch> branches = new List<Branch>();
    [HideInInspector]
    public Path path;


    int numberConvos = -999;


    // Start is called before the first frame update
    void Awake()
    {
        //LoadCSV();

        
        //Sinbad.CsvUtil.SaveObjects<Line>();

        StreamReader sr = new StreamReader("Assets/RawText/"+fileName+".csv");
        string bigInput = sr.ReadToEnd();

        string[] splits = bigInput.Split("@");

        string output;
        for(int i = 0; i < splits.Length; i++){
            using (StreamWriter outputFile = new StreamWriter("Assets/RawText/output" + i + ".csv"))
            {
                output = splits[i];
                if(i > 0)
                {
                    output = "ID,TITLE,BODY\n" + output;
                }
                outputFile.Write(output);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    public void LoadCSV()
    {
        LoadCSV(fileName);
    }

    public void LoadCSV(string file)
    {
        data = Sinbad.CsvUtil.LoadObjects<Line>("Assets/RawText/" + file + ".csv");

        //int i = 2;
        //Debug.Log(data[i].ID + " " + data[i].TITLE + " " + data[i].BODY);

      
        
        slides = GetAllSlides();
        tree = FindAllBranches();
        //conversations = CreateConversationObjects();
        branches = CreateBranchObjects();

        //branches = CreateBranchObjects("A");
        
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

        
        Debug.Log("start branches");  
        foreach(Branch branch in branches)
        {
            Debug.Log("start paths");  
            foreach(Path path in branch.myPathOptions)
            {
               
                    
                    Debug.Log("start slides");
                    foreach(Slide slide in path.slides)
                    {
                        Debug.Log("     " + slide.ID + " " + slide.Body);
                    }
                    Debug.Log("end slides");

                
            }
            Debug.Log("end paths");
        }
        Debug.Log("end branches");
  
        //Debug.Log("size of tree: " + tree.Count);

/*
        Debug.Log("start branches");  
        foreach(Branch branch in branches)
        {
            Debug.Log("start paths");  
            foreach(Path path in branch.myPathOptions)
            {
                if(path != null)
                {
                    Debug.Log("     " + path.firstSlide.ID + " " + path.firstSlide.Body);

                }
            }
            Debug.Log("end paths");
        }
        Debug.Log("end branches");
 */ 



        


    }
    
    /*
    List<Conversation> CreateConversationObjects()
    {
        List<Conversation> tempConvos = new List<Conversation>();

        string convertedID;
        for(int i = 0; i < numberConvos; i++)
        {
            convertedID = (Convert.ToChar(i+65)).ToString();
            //Debug.Log("converted id: " + convertedID);
            branches = CreateBranchObjects(convertedID);
            Conversation convo = new Conversation(convertedID, branches);
            tempConvos.Add(convo);
        }

        return tempConvos;
        
    }
    */

    List<Branch> CreateBranchObjects()
    {

        List<Branch> tempBranches = new List<Branch>();
        foreach(List<KeyValuePair<int,int>> list in tree)
        {   
            Path[] pathChoices = new Path[list.Count];
            for(int i = 0; i < list.Count; i++)
            {
                //if it could find a slide with matching id and 
                //Debug.Log("list value: " + list[i].Value.ToString());
                Slide slide = slides.Find(x => ((x.ID) == list[i].Value.ToString()));// && (x.ConvoID == convoID));
                if(slide != null)
                {
                    pathChoices[i] = GetRestOfPath(slide);
                }

                //Debug.Log("      " + list[i].Value + " " + list[i].Key);
            }
        
            
            tempBranches.Add(new Branch(pathChoices));
  
        }

        


        return tempBranches;


    }

    List<Branch> CreateBranchObjects_broke(string convoID)
    {

        List<Branch> tempBranches = new List<Branch>();
        foreach(List<KeyValuePair<int,int>> list in tree)
        {   
            Path[] pathChoices = new Path[list.Count];
            
            for(int i = 0; i < list.Count; i++)
            {
                Slide slide = null;
                //if it could find a slide with matching id and convoID, dont add it
                for(int j = 0; j < slides.Count; j++)
                {
                    bool condition1 = (slides[j].ID.Equals(list[i].Value.ToString()));
                    //Debug.Log("slideID: " + slides[j].ID + " list value: " + list[i].Value.ToString());
                    bool condition2 = (slides[j].ConvoID.Equals(convoID));
                    //Debug.Log("convoID: " + convoID + " list value: " + list[i].Value.ToString() + " slideID: " + slides[j].ID + " slideConvoID: " + slides[j].ConvoID + " 1: " + condition1 + " 2: " + condition2);
                    if((condition1 && condition2))
                    {
                        Debug.Log("convoID: " + convoID + " list value: " + list[i].Value.ToString() + " slideID: " + slides[j].ID + " slideConvoID: " + slides[j].ConvoID);
                        pathChoices[i] = GetRestOfPath(slides[j]);
                    }
                }
                
                //list[i].Value + " " + list[i].Key

                //Debug.Log("      " + list[i].Value + " " + list[i].Key);
            }
        
            
            //pathChoices.RemoveAll(x => x == null);
            tempBranches.Add(new Branch(pathChoices));
  
        }

        


        return tempBranches;


    }

    public Path GetRestOfPath(Slide firstSlideInPath)
    {
        

        int lineWherePathStarts = 0;

        for(int j = 0; j < data.Count; j++)
        {
            string id = data[j].ID;

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

//            Debug.Log(startID + " = " + id);
//            Debug.Log("     " + id.StartsWith(startID));
            bool flag = true;

            //int x;
            for(int i = startID.Length; i < id.Length; i++)
            {
                
                
                if(id[i] == '1')
                {

                } else {
                    flag = false;                                   
                }

                string idPlusOne = (id.Substring(0,i) + (Int32.Parse(id[i].ToString())+1).ToString() + id.Substring(i+1, id.Length-i-1));
                string idMinusOne = (id.Substring(0,i) + (Int32.Parse(id[i].ToString())-1).ToString() + id.Substring(i+1, id.Length-i-1));
                if(null != slides.Find(x => x.ID + "" == idPlusOne) || null != slides.Find(x => x.ID + "" == idMinusOne))
                {
//                    Debug.Log("found branch neighbor in slides");
                    flag = false;

                    return new Path(tempSlides.ToArray());
                }
            
            }   

            flag = flag && id.StartsWith(startID);


            if(flag)
            {
                Line tempLine = data.Find(x => x.ID == id);
                tempSlides.Add(new Slide(tempLine.TITLE, tempLine.BODY, tempLine.ID));
            }


        }


        return new Path(tempSlides.ToArray());
    }

/*
okay so this function is just incorrect. it does not look at the rest of the string when comparing digits. we need it to FindBranchAt the location, but also checks
    that the new branch has members of the same number stem, i.e. (111, 112, 133) have the same stem of 11.
    it also needs to loop through a couple times, remembering which stems are complete/which ones havent been done yet.

    idk the best way to do this. it ignores 316 when looking at the 3rd correctly with the substring method i have here, but it does not loop back around to 
    put 316 in the branch it belongs



*/
    public List<List<KeyValuePair<int,int>>> FindAllBranches()
    {
        tree = new List<List<KeyValuePair<int,int>>>();
        List<KeyValuePair<int,int>> root = FindBranchAt(0,0,data.Count,"");
        tree.Add(root);
        List<KeyValuePair<int,int>> temp = new List<KeyValuePair<int,int>>();

        int startLine, endLine;
        
        for(int j = 0; j < 8; j++)
        {
            for(int i = 0; i < root.Count; i++)
            {
                
                string s = "";
                if(tree.Count > 1)
                {
                    s=root[i]+"";
                } else 
                {
                    s=root[0].Value + "";
                }

                startLine = root[i].Key;
                if(i+1 < root.Count)
                { 
                    endLine = root[i+1].Key;
                } else 
                {
                    endLine = data.Count;
                }

                temp = FindBranchAt(j, startLine, endLine, "" + s);
//                Debug.Log("finding branches: " + root[i].Value + " " + s);

                if(temp == null)
                {

                } else 
                {
                    if(tree.Contains(temp) || IsTempElementInTree(tree, temp))
                    {
//                        Debug.Log("temp was already in tree: " + temp[0].Value);
                    } else 
                    {
                        //tree.Add(temp);
                    }

                    tree.Add(temp);
                    
                }
            }
        }


        return tree;
    }

    List<KeyValuePair<int,int>> FindBranchAt(int startDigit, int startLine, int endLine, string subString)
    { 
        List<KeyValuePair<int,int>> branch1 = new List<KeyValuePair<int,int>>();
        
        int max = -999;
        int pathAtI = 0;
        int pathsFoundInThisBranch = 0;


        //get digits line by line    
        for(int j = startLine; j < endLine; j++)
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
                    //Debug.Log("start digit: " + startDigit + " id: " + id);
                    pathsFoundInThisBranch++;
                    if(Int32.Parse(id) == 0)
                    {
                        Debug.Log("zero found");
                    } else 
                    {
                        KeyValuePair<int, int> pair = new KeyValuePair<int, int>(j, Int32.Parse(id));
                        
                        bool check = true;
                        if(GetAllButLastStr(id).Equals(subString))
                        {

                        } else if(id.Length > 1)
                        {
                            //check = false;
                        }

                        if(check)
                        {
                          branch1.Add(pair);
                        }

                        Debug.Log("pair to add: " + pair.Key + " " + pair.Value);
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
            
            Slide temp = new Slide(data[j].TITLE, data[j].BODY, data[j].ID);
            if(!allSlides.Contains(temp))
            {
                allSlides.Add(temp);
                //Debug.Log("data: " + data[j].ID);
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
//                Debug.Log("is " + element.Value + " in list already? " + list.Contains(element));
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

    string GetAllButLastStr(string s)
    {
        if(s.Length < 2)
        {
            return s;
        }
        return s.Substring(0, s.Length-2);
    }

    public List<Branch> GetBranches()
    {
        return branches;
    }

    public Slide GetFirstSlide()
    {
        Debug.Log("first slide: " + data[0].BODY);
        return new Slide(data[0].TITLE, data[0].BODY, data[0].ID);
    }



}





[System.Serializable]
public class Line
{
    public string ID;
    public string TITLE;
    public string BODY;


}
