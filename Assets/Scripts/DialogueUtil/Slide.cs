using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Slide{

    public string Title;
    public string Body;
    public string ID;
    public string ConvoID;


    bool isEnd;
    Branch myBranch;

    public Slide(string myTitle, string myBody)
    {
        Title = myTitle;
        Body = myBody;
    }

    public Slide(string myTitle, string myBody, string myID)
    {
        Title = myTitle;
        Body = myBody;
        ID = myID;
    }

    public Slide(string myTitle, string myBody, string myID, string ConvoID)
    {
        Title = myTitle;
        Body = myBody;
        ID = myID;
        this.ConvoID = ConvoID;
    }

}
