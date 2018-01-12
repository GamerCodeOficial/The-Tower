

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEditor;

public class EffectsWindow : EditorWindow
{

    int id;
    string iName;
    float iDex;
    float iStr;
    float iDef;
    float iDamage;
    int iDuration;
    public EffectDb itemDb;

    [MenuItem("Window/Effects")]
    public static void ShowWindow()
    {
        GetWindow<EffectsWindow>("Effects");
    }

    public void Awake()
    {
        GetItems();
        if (itemDb.list[0].name != "None")
        {
            Effect I = new Effect();
            iName = "None";
            AddNew();
        }
    }
    private void OnGUI()
    {

   
        if (GUILayout.Button("Refresh"))
        {
            GetItems();
        }
        GUILayout.Label("Handle Effects");
        id = EditorGUILayout.IntField("Id", id);
        iName = EditorGUILayout.TextField("Name", iName);
        iDex = EditorGUILayout.FloatField("Dex", iDex);
        iStr = EditorGUILayout.FloatField("Str", iStr);
        iDef = EditorGUILayout.FloatField("Def", iDef);
        iDamage = EditorGUILayout.FloatField("Damage", iDamage);
        iDuration = EditorGUILayout.IntField("Duration", iDuration);
        if (GUILayout.Button("Change Status"))
        {
            Change();
            SaveItems();
        }
        if (GUILayout.Button("Add New Effect"))
        {
            AddNew();
            SaveItems();
        }
        GUILayout.TextArea(GenerateText());
  
    }




    public string GenerateText()
    {
        string t = "";
        if (itemDb.list[0] != null)
        {
            foreach (Effect it in itemDb.list)
            {
                t += it.id + "- Nome: *" + it.name + "* Dex:" + it.dex + " Str:" + it.str + " Def:" + it.def + " Damage:" + it.damage + " Duration:" + it.duration + "\n";

            }

        }
        t = t.Replace('$', '\n');

        return t;
    }

    public void Change()
    {


        foreach (Effect it in itemDb.list)
        {
            if (it.id == id)
            {
                it.name = iName;
                it.dex = iDex;
                it.str = iStr;
                it.def = iDef;
                it.damage = iDamage;
                it.duration = iDuration;
                Debug.Log("Match");
            }
        }
    }

    public void AddNew()
    {
        Effect it = new Effect();
        it.id = itemDb.list.Count;
        it.name = iName;
        it.dex = iDex;
        it.str = iStr;
        it.def = iDef;
        it.damage = iDamage;
        it.duration = iDuration;
        itemDb.list.Add(it);
    }
    public void SaveItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(EffectDb));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/Xml/Effects.xml", FileMode.Create);
        serializer.Serialize(stream, itemDb);
        stream.Close();
    }
    public void GetItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(EffectDb));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/Xml/Effects.xml", FileMode.Open);
        itemDb = serializer.Deserialize(stream) as EffectDb;
        stream.Close();
    }
}


