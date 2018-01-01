using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fase0 : MonoBehaviour {
    public SceneControl cont;

    public float hp;
    public float dex;
    public float str;
    public float def;
    public float aura;

    public float[] primeiro;
    public float[] segundo;
    public float[] terceiro;
    public float[] chosen;

    public Text pri;
    public Text seg;
    public Text ter;

    public Image rendP;
    public Image rendS;
    public Image rendT;

    public string[] colors;
    
	// Use this for initialization
	void Start () {
        
       

        cont = GameObject.FindGameObjectWithTag("Control").GetComponent<SceneControl>();
        primeiro = Randomize();
        segundo = Randomize();
        terceiro = Randomize();
        pri.text = Descript(primeiro);
        seg.text = Descript(segundo);
        ter.text = Descript(terceiro);
        
        rendP.color = GetColor(colors[(int)(primeiro[5])]);
        rendS.color = GetColor(colors[(int)(segundo[5])]);
        rendT.color = GetColor(colors[(int)(terceiro[5])]);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public Color GetColor(string cor) {
        int[] oi = new int[3];
        char[] c = cor.ToCharArray();
        string t = "";
        for (int i=0;i<3;i++) {
            t += c[i];
        }
        int.TryParse(t,out oi[0]);  
        t = "";
        for (int i = 3; i < 6; i++)
        {
            t += c[i];
        }
        int.TryParse(t, out oi[1]);
        t = "";
        for (int i = 6; i < 9; i++)
        {
            t += c[i];
        }
        int.TryParse(t, out oi[2]);
        
        Color co = new Color();
        float[] norm = new float[3];
        for (int i = 0; i < 3; i++) {
            norm[i] = oi[i];
            norm[i] /= 255;
        }
        co.r = norm[0];
        co.g = norm[1];
        co.b = norm[2];
        co.a = 1;
        return co;
    }

    public float[] Randomize() {
        float[] p = new float[6];
        p[0] =5;
        p[1] = 5;
        p[2] = 5;
        p[3] = 5;

        int r = Random.Range(0,3);
        p[r] ++;
        r = Random.Range(0, 4);
        p[r]++;
        r = Random.Range(0, 4);
        p[r] --;
        r = Random.Range(0, 4);
        p[r]--;

        r = Random.Range(0, colors.Length);//Color
        p[5] = r;
        print(p[5]);
        p[0] =20+ (p[0]-5)*3;
        p[3]*= 2;
        p[4] = 0;
        return p;
    }
    public string Descript(float[] opt){
        string ret = "";
        ret += "Hp: " + opt[0] + "\n";
        ret += "dex: " + opt[1] + "\n";
        ret += "str: " + opt[2] + "\n";
        ret += "def: " + opt[3] + "\n";
        
       
        ret = ret.Replace('$', '\n');
        return ret;
        }

    public void Choose(int opt) {
        if (opt == 1) chosen = primeiro;
        if (opt == 2) chosen = segundo;
        if (opt == 3) chosen = terceiro;

        PlayerPrefs.SetFloat("Hp", chosen[0]);
        PlayerPrefs.SetFloat("Dex", chosen[1]);
        PlayerPrefs.SetFloat("Str", chosen[2]);
        PlayerPrefs.SetFloat("Def", chosen[3]);
        PlayerPrefs.SetFloat("Aura", chosen[4]);
        PlayerPrefs.SetInt("Andar", 1);
        int p = (int)chosen[5];
        PlayerPrefs.SetString("Cor",colors[p]);
        print(chosen[5]);
        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetInt("Slot" + i, 0);
        }

        cont.GoToScene("Fase1");
    }
}
