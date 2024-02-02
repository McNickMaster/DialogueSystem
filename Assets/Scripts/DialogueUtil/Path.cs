using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Path
{
    public Slide[] slides;
    public Slide endSlide;
    public Branch parentBranch;


    public Path(Slide[] slides)
    {
        this.slides = slides;
//        endSlide = slides[slides.Length-1];
    }
    




}
