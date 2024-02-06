using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BranchObject : MonoBehaviour
{
    public Branch branch;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI[] options;
    public Button closeButton;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            PopulateTexts();
        }
    }

    public void PopulateTexts()
    {
        titleText.text = "You";

        

// this is broken and clips out some options sometimes
        //Debug.Log("Option length: " + options.Length + " Branch path count: " + branch.myPathOptions.Count);
        for(int i = options.Length; i > branch.myPathOptions.Count; i--)
        {
            options[i-1].transform.parent.gameObject.SetActive(false);
        }
    
        //Debug.Log("Option length: " + options.Length + " Branch path count: " + branch.myPathOptions.Count);
        for(int i = 0; i < branch.myPathOptions.Count; i++)
        {
            options[i].transform.parent.gameObject.SetActive(!branch.myPathOptions[i].locked);
            options[i].text = branch.myPathOptions[i].firstSlide.Body;
        }

        closeButton.onClick.AddListener(DialogueLoader.instance.EndConversation);
        
    }

    public void ChoosePath(int i)
    {
        if(i >= branch.myPathOptions.Count)
        {
            Debug.Log("button index " + i + "out of bounds of path options array: " + branch.myPathOptions.Count);
        } else 
        {
            DialogueLoader.instance.LoadPath(branch.myPathOptions[i]);
        }
        
    }
}
