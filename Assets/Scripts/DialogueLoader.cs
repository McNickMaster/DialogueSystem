using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class DialogueLoader : MonoBehaviour
{
    public static DialogueLoader instance;

    public GameObject textAsset;
    LoadTextFromCSV csvLoader;

    public string convoIDToLoad = "";

    Conversation conversation;

    public Transform dialogueParent;
    public GameObject slidePrefab, branchPrefab;

    private GameObject currentUIObject;
    private Path currentPath;
    private Branch currentBranch;
    private int pathIndex;

    void Awake()
    {
        instance = this;


        csvLoader = GetComponent<LoadTextFromCSV>();
        csvLoader.LoadCSV();

        //LoadDialogueBranch("1");
        conversation = LoadConversation(convoIDToLoad); 


        //SpawnSlide(new Slide("ME", "HI HYD"));

        StartConversation();
        //Debug.Log(conversation.myBranches[0].GetFirstSlidesOfPath()[0].Body);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*
    public void LoadDialogueBranch(string idOfSlide1)
    {
        foreach(Branch b in branches)
        {
            foreach(Path p in b.myPathOptions)
            {
                if(p.firstSlide.ID == Int32.Parse(idOfSlide1))
                {
                    path = p;
                }
            }
        }
    }
*/

    public void StartConversation()
    {
        
        SpawnBranch(conversation.myBranches[0]);
        
    }

    void StartConversation(string id)
    {
        SpawnBranch(LoadConversation(id).myBranches[0]);
    }


    public void LoadNextInPath()
    {
        Destroy(currentUIObject);

        if(pathIndex <= currentPath.slides.Length-1)
        {
            SpawnSlide(currentPath.slides[pathIndex]);
            pathIndex++;
        } else 
        {
            if(currentPath.isEnd)
            {
                //StartConversation() // resets all conversations back to the beggining
                RespawnBranch(); // resets no conversations back to the beggining


            } else 
            {
                SpawnBranch(currentPath.endBranch);
            }

        }
    }

    public void LoadPath(Path p)
    {
        p.myBranch = currentBranch;

        Slide temp = csvLoader.slides[1 + csvLoader.slides.FindIndex(x => x.ID == p.endSlide.ID)];
        p.endBranch = conversation.myBranches.Find(x => x.myPathOptions[0].firstSlide.ID == temp.ID);
        currentPath = p;
        pathIndex = 0;
        LoadNextInPath();
    }


    void SpawnSlide(Slide s)
    {
        
        Destroy(currentUIObject);
        SlideObject spawnSlide = Instantiate(slidePrefab, Vector3.zero, Quaternion.identity, dialogueParent).GetComponent<SlideObject>();
        spawnSlide.gameObject.transform.localPosition = Vector3.zero;
        currentUIObject = spawnSlide.gameObject;
        spawnSlide.slide = s;
        spawnSlide.PopulateTexts();
        
    }

    void SpawnBranch(Branch b)
    {
        Destroy(currentUIObject);
        pathIndex = 0;

        BranchObject spawnBranch = Instantiate(branchPrefab, Vector3.zero, Quaternion.identity, dialogueParent).GetComponent<BranchObject>();
        spawnBranch.gameObject.transform.localPosition = Vector3.zero;
        spawnBranch.branch = b;
        currentUIObject = spawnBranch.gameObject;

        if(currentBranch != spawnBranch.branch)
        {
            spawnBranch.branch.parentBranch = currentBranch;
        }
        currentBranch = spawnBranch.branch;
        spawnBranch.PopulateTexts();
    }

    void RespawnBranch()
    {
        pathIndex = 0;

        BranchObject spawnBranch = Instantiate(branchPrefab, Vector3.zero, Quaternion.identity, dialogueParent).GetComponent<BranchObject>();
        spawnBranch.gameObject.transform.localPosition = Vector3.zero;
        spawnBranch.branch = currentBranch;
        currentUIObject = spawnBranch.gameObject;
        spawnBranch.PopulateTexts();
    }



    public Conversation LoadConversation(string convoID)
    {
        return csvLoader.conversations.Find(c => c.ID == convoID);
    }





}
