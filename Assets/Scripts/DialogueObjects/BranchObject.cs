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
        
    }

    public void PopulateTexts()
    {
        titleText.text = "You";

// this is broken and clips out some options sometimes
        for(int i = options.Length - 1; i < branch.myPathOptions.Count; i++)
        {
            options[i].gameObject.transform.parent.gameObject.SetActive(false);
        }
    

        for(int i = 0; i < branch.myPathOptions.Count; i++)
        {
            options[i].text = branch.myPathOptions[i].firstSlide.Body;
        }

        closeButton.onClick.AddListener(DialogueLoader.instance.StartConversation);
        
    }

    public void ChoosePath(int i)
    {
        DialogueLoader.instance.LoadPath(branch.myPathOptions[i]);
    }
}
