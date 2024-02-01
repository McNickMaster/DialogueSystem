using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SlideObj
{
    public string Title;
    public string Body;
    int id;


    bool isEnd;
    BranchObj myBranch;

    public SlideObj(string myTitle, string myBody)
    {
        Title = myTitle;
        Body = myBody;
    }

}
