using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEditor;

public class ItemWindow : EditorWindow {

    int id;
    string iName;
    int iSlot;
    float iHp;
    float iDex;
    float iStr;
    float iDef;

    public ItemDataBase itemDb;

    [MenuItem("Window/Item")]
    public static void ShowWindow() {
        GetWindow<ItemWindow>("Item");
    }
    public void Awake() {
        GetItems();
        if (itemDb.list[0].name != "Nothing") {
            Item I = new Item();
            iName = "Nothing";
            AddNew();
        }
    }
    private void OnGUI()
    {
       
        
            if (GUILayout.Button("Refresh"))
        {
            GetItems();
        }
        GUILayout.Label("Handle Items");
        id=EditorGUILayout.IntField("id", id);
        iName = EditorGUILayout.TextField("Name", iName);
        iSlot = EditorGUILayout.IntField("Slot", iSlot);
        iHp = EditorGUILayout.FloatField("Hp", iHp);
        iDex = EditorGUILayout.FloatField("Dex", iDex);
        iStr = EditorGUILayout.FloatField("Str", iStr);
        iDef = EditorGUILayout.FloatField("Def", iDef);

        
        if (GUILayout.Button("Change Status")) {
            Change();
            SaveItems();
        }
        if (GUILayout.Button("Add New Item"))
        {
            AddNew();
            SaveItems();
        }
        GUILayout.TextArea(GenerateText());
        
    }




    public string GenerateText() {
        string t = "";
        if (itemDb.list[0] != null) { 
        foreach (Item it in itemDb.list)
        {
            t += it.id + "- Nome: *" + it.name + "* Slot:" + it.slot + " Hp:" + it.hp + " Dex:" + it.dex + " Str:" + it.str + " Def:" + it.def + "\n";

        }

    }
        t = t.Replace('$', '\n');

        return t;
    }

    public void Change()
    {
     

        foreach (Item it in itemDb.list) {
            if (it.id == id) {
                it.name = iName;
                it.slot = iSlot;
                it.hp = iHp;
                it.dex = iDex;
                it.str = iStr;
                it.def = iDef;

                Debug.Log("Match");
            }
        }
    }

    public void AddNew()
    {
        Item it = new Item();
        it.id = itemDb.list.Capacity;
        it.name = iName;
        it.slot = iSlot;
        it.hp = iHp;
        it.dex = iDex;
        it.str = iStr;
        it.def = iDef;

        itemDb.list.Add(it);
    }
    public void SaveItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDataBase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/Xml/Items.xml", FileMode.Create);
        serializer.Serialize(stream, itemDb);
        stream.Close();
    }
    public void GetItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDataBase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/Xml/Items.xml", FileMode.Open);
        itemDb = serializer.Deserialize(stream) as ItemDataBase;
        stream.Close();
    }
}


