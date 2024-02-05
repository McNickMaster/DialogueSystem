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

    public bool isEnd = true;
    public Branch endBranch; 


    public Path(Slide[] slides)
    {
        this.slides = slides;
        firstSlide = slides[0];
        endSlide = slides[slides.Length-1];

        DetectIfEnd();
    }

    void DetectIfEnd()
    {
        isEnd = endSlide.Body.Contains("[end]");


    }
    




}
