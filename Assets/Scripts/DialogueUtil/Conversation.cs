using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class Conversation
{

    public string ID;
    public List<Branch> myBranches;

    public Conversation(string id, List<Branch> branches)
    {
        ID = id;
        myBranches = branches;
    }

}
