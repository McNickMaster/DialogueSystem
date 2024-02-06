using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[Serializable]
public class Path
{
    public Slide[] slides;
    public Slide firstSlide, endSlide;
    public Branch myBranch;

    public PathEndBehaviour pathEndBehaviour = PathEndBehaviour.END;
    public string gotoID = "";
    public Branch endBranch;

    public bool locked = false;
    public string unlockPathID;
    


    public Path(Slide[] slides)
    {
        this.slides = slides;
        firstSlide = slides[0];
        endSlide = slides[slides.Length-1];

        DetectPathEnd();
    }

    void DetectPathEnd()
    {
        locked = firstSlide.Body.Contains("[locked]");
        if(locked)
        {
            firstSlide.Body = firstSlide.Body.Substring(8, firstSlide.Body.Length-8);
        }

        if(endSlide.Body.Contains("[unlock"))
        { 
            int index = endSlide.Body.IndexOf("[unlock")+7;
            unlockPathID = endSlide.Body.Substring(index, endSlide.Body.Length - index - 1);
            endSlide.Body = endSlide.Body.Substring(0, endSlide.Body.Length - unlockPathID.Length - 8);
            Debug.Log("endSlide after unlock removal: " + endSlide.Body);
        }

        if(endSlide.Body.Contains("[end]"))
        {
            pathEndBehaviour = PathEndBehaviour.END;

            int index = endSlide.Body.IndexOf("[end]");
            endSlide.Body = endSlide.Body.Substring(0, index-1);
        } else if(endSlide.Body.Contains("[goto"))
        {
            pathEndBehaviour = PathEndBehaviour.GOTO;
            //whole section     Debug.Log(endSlide.Body.Substring(endSlide.Body.IndexOf("[back"), endSlide.Body.Length - endSlide.Body.IndexOf("[back")));
            int index = endSlide.Body.IndexOf("[goto")+5;
            gotoID = endSlide.Body.Substring(index, endSlide.Body.Length - index - 1) + "1";
            Debug.Log("gotoID: " + gotoID);
            endSlide.Body = endSlide.Body.Substring(0, index - 2 - gotoID.Length);
        } else 
        {
            pathEndBehaviour = PathEndBehaviour.CONTINUE;
        }


        

    }
    




}

public enum PathEndBehaviour 
{
    END, CONTINUE, GOTO
}
