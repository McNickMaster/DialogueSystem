using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch
{
    
    public List<Path> myPathOptions;

    public Branch(Path[] pathOptions)
    {
        myPathOptions = new List<Path>(pathOptions);
    }

    public void ChoosePath(Path path)
    {
        //path.Choose()
    }
}
