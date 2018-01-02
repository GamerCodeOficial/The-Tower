using UnityEngine;
using UnityEditor;

public class ItemWindow : EditorWindow {

    int id;
    string iName;
    int iSlot;
    float iHp;
    float iDex;
    float iStr;
    float iDef;
    
    [MenuItem("Window/Item")]
    public static void ShowWindow() {
        GetWindow<ItemWindow>("Item");
    }
    private void OnGUI()
    {
        GUILayout.Label("Change Item");
        id=EditorGUILayout.IntField("id", id);
        iName = EditorGUILayout.TextField("Name", iName);
        iSlot = EditorGUILayout.IntField("Slot", iSlot);
        iHp = EditorGUILayout.FloatField("Hp", iHp);
        iDex = EditorGUILayout.FloatField("Dex", iDex);
        iStr = EditorGUILayout.FloatField("Str", iStr);
        iDef = EditorGUILayout.FloatField("Def", iDef);
        if (GUILayout.Button("Change all")) {
            PlayerPrefs.SetString("iName"+id,iName);
            PlayerPrefs.SetInt("iSlot" + id, iSlot);
            PlayerPrefs.SetFloat("iHp" + id, iHp);
            PlayerPrefs.SetFloat("iDex" + id, iDex);
            PlayerPrefs.SetFloat("iStr" + id, iStr);
            PlayerPrefs.SetFloat("iDef" + id, iDef);
        }
        if (GUILayout.Button("Add New Item"))
        {
            id = CalculateLast();
            PlayerPrefs.SetString("iName" + id, iName);
            PlayerPrefs.SetInt("iSlot" + id, iSlot);
            PlayerPrefs.SetFloat("iHp" + id, iHp);
            PlayerPrefs.SetFloat("iDex" + id, iDex);
            PlayerPrefs.SetFloat("iStr" + id, iStr);
            PlayerPrefs.SetFloat("iDef" + id, iDef);
        }
        GUILayout.TextArea(GenerateText());
        if (GUILayout.Button("Delete ALL !!!NÃO CLIQUE PELO AMOR DE DEUS!!! Delete ALL"))
        {
            Debug.Log(CalculateLast());
            for (id = 0; id < 100; id++)
            {
                Debug.Log(id);
                PlayerPrefs.SetString("iName" + id, "");
                PlayerPrefs.SetInt("iSlot" + id, 0);
                PlayerPrefs.SetFloat("iHp" + id, 0);
                PlayerPrefs.SetFloat("iDex" + id, 0);
                PlayerPrefs.SetFloat("iStr" + id, 0);
                PlayerPrefs.SetFloat("iDef" + id, 0);
            }
        }
    }
    public int CalculateLast() {
        
        if (PlayerPrefs.GetString("iName0") == "")
        {
            return 0;
        }
        else {
            int i = 0;
            while (PlayerPrefs.GetString("iName"+i) != "") {
                i++;
            }
            return i;
        }
        
        
    }
    public string GenerateText() {
        string t = "";
        
        int m = CalculateLast();

        if (m > 0)
        {
            for (int i = 0; i < m; i++)
            {
                t += "Nome: *" + PlayerPrefs.GetString("iName" + i) + "* Slot: " + PlayerPrefs.GetInt("iSlot" + i) + " Hp: " + PlayerPrefs.GetFloat("iHp" + i) + " Dex: " + PlayerPrefs.GetFloat("iDex" + i) + " Str: " + PlayerPrefs.GetFloat("iStr" + i) + " Def: " + PlayerPrefs.GetFloat("iDef" + i) + "\n";


            }
        }
       
        t = t.Replace('$', '\n');

        return t;
    }
}
