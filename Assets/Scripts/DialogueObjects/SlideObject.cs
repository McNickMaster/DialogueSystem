using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlideObject : MonoBehaviour
{

    public Slide slide;
    public TextMeshProUGUI titleText, bodyText;



    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(DialogueLoader.instance.LoadNextInPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateTexts()
    {
        titleText.text = slide.Title + " " + slide.ID;
        bodyText.text = slide.Body;
    }
}
