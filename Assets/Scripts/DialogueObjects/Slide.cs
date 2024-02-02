using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Slide{

    public string Title;
    public string Body;
    public int ID;


    bool isEnd;
    Branch myBranch;

    public Slide(string myTitle, string myBody)
    {
        Title = myTitle;
        Body = myBody;
    }

    public Slide(string myTitle, string myBody, int myID)
    {
        Title = myTitle;
        Body = myBody;
        ID = myID;
    }

}
