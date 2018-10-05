using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoomBase {
    public string text;

    public int[] Convert() {
        int[] grid=new int[64];
        char[] c = text.ToCharArray();
        for (int i = 0; i< 64; i++) {
            string s = "";
            s += c[i];
            int.TryParse(s, out grid[i]);
            }
        return grid;
      
        }
}