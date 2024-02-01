using UnityEngine;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using JsonFx.Json;
using UnityEditor;

using SimpleJSON;

public class LoadTextFromJson : MonoBehaviour
{

    public string fileName = "dialogue_test";


    string[] textPath, textSlide;
    Path[] myPathList;
    Slide[] mySlideList;

    // Start is called before the first frame update
    void Awake()
    {
        LoadDialogue();
        //LoadJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDialogue()
    {
        LoadJson_Class(fileName);
    }

    public void LoadJson_NestedDict(string file)
    {
        TextAsset src = AssetDatabase.LoadAssetAtPath("Assets/Dialogue/" + file + ".json", typeof(TextAsset)) as TextAsset;

        var reader = new JsonReader();
        dynamic output = reader.Read(src.ToString().Trim());

        Dictionary<string, object>[] tempPath = output["Dialogue"];



        
        foreach(Dictionary<string, object> dictionary in tempPath)
        {
            foreach(KeyValuePair<string, object> obj in dictionary)
            {
                Debug.Log(obj.Key + " ||| " + obj.Value);

                
                Debug.Log(obj.Value.ToDictionary());


                
                
                //Debug.Log(obj["Path"]);
                
                Path newPath;
                //Debug.Log(obj.Value[0]);
/*
                foreach(var obj2 in dict)
                {
                    Debug.Log("2 " + obj2.Key + " ||| " + obj2.Value);
                }
                */

                //Path newPath = new Path();
                //Slide[] slides;
                
                //populate slides here

                //newPath = new Path(slides);
                
            }

        }


    }

    public void LoadJson_Class(string file)
    {
        fileName = file;
        
        fileName = "Assets/Dialogue/" + file + ".json";
        StreamReader r = new StreamReader(fileName);
        string json = r.ReadToEnd();
        json = json.Trim();

        JSONNode root = JSON.Parse(json);

        JSONNode convo = root["Conversation"];

        
        JSONArray array = root["Conversation"][0][0].AsArray;
        Debug.Log(array.Count);

        for(int i = 0; i < array.Count; i++)
        {
            Debug.Log(root["Conversation"][0][0][i][0][0][0] + " " + root["Conversation"][0][0][i][0][1][0]);
        }



        

    }
    
    public void GetTree()
    {

    }


}

[Serializable]
[JsonName("Dialogue")]
public partial class Dialogue
{
    public Conversation[] Conversation { get; set; }
}

[Serializable]
[JsonName("Conversation")]
public partial class Conversation
{
    public Path[] Path { get; set; }
}

[Serializable]
[JsonName("Path")]
public partial class Path
{
    public Slide[] Slide { get; set; }
}

[Serializable]
[JsonName("Slide")]
public partial class Slide
{
    public string Title { get; set; }
    public string Text { get; set; }
}




public static class ObjectToDictionaryHelper
{
    public static IDictionary<string, object> ToDictionary(this object source)
    {
        return source.ToDictionary<object>();
    }

    public static IDictionary<string, T> ToDictionary<T>(this object source)
    {
        if (source == null)
            ThrowExceptionWhenSourceArgumentIsNull();

        var dictionary = new Dictionary<string, T>();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            AddPropertyToDictionary<T>(property, source, dictionary);
        return dictionary;
    }

    private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
    {
        object value = property.GetValue(source);
        if (IsOfType<T>(value))
            dictionary.Add(property.Name, (T)value);
    }

    private static bool IsOfType<T>(object value)
    {
        return value is T;
    }

    private static void ThrowExceptionWhenSourceArgumentIsNull()
    {
        throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
    }
}
