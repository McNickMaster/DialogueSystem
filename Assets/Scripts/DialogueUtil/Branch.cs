using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Branch
{
    [HideInInspector]
    public Branch parentBranch;
    public List<Path> myPathOptions;

    public Branch(Path[] pathOptions)
    {
        myPathOptions = new List<Path>(pathOptions);


/*
        foreach(Path p in myPathOptions)
        {
            //p.myBranch = this;
        }
        */
    }

    public Slide[] GetFirstSlidesOfPath()
    {
        Slide[] slides = new Slide[myPathOptions.Count];

        for(int i = 0; i < myPathOptions.Count; i++)
        {
            slides[i] = myPathOptions[i].firstSlide;
        }

        return slides;

    }

    public Branch GetReturnBranch()
    {
        return (parentBranch != null) ? parentBranch : this;
    }

}
