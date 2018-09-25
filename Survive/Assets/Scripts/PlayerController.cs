using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {

    public Actions actions;

    //UI
    public Text hpT;
    public Text energyT;
    public Text foodT;
    public Text waterT;

    //Max values
    public int maxHp;
    public int maxEnergy;
    public int maxFood;
    public int maxWater;

    //Stats
    public int hp;
    public int energy;
    public int food;
    public int water;
    public float temperature;


    //states
    public bool fire;
    public bool camp;
    public bool raft;

    public int[] inventory;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        hpT.text = "HP:" + hp + "/" + maxHp;
        energyT.text = "Energy:" + energy + "/" + maxEnergy;
        foodT.text = "Food" + food + "/" + maxFood;
        waterT.text = "Water" + water + "/" + maxWater;
    }
    public void TimePass(int hours) {
        for(int i = 0; i < hours; i++)
        {
            food--;
            water--;
            energy--;
            if (food <= 0) {
                food = 0;
                ChangeHp(-1);
            }
            if (water <= 0)
            {
                water = 0;
                ChangeHp(-1);
            }
            if (energy <= 0) {
                ForceSleep();
            }
        }
    }
    public void Sleep() {
        ChangeEnergy(Random.Range(4, 10));
        ChangeHp(Random.Range(0, 4));
        ChangeFood(Random.Range(-2, 0));
        ChangeWater(Random.Range(-2, 0));
    }
    public void ForceSleep() {
        ChangeEnergy(Random.Range(0,4));
        ChangeHp(Random.Range(-3, 2));
        ChangeFood(Random.Range(-4, 0));
        ChangeWater(Random.Range(-4, 0));
    }
    public void ChangeHp(int num) {
        hp += num;
    }
    public void ChangeEnergy(int num)
    {
        energy += num;
    }
    public void ChangeWater(int num)
    {
        water += num;
    }
    public void ChangeFood(int num)
    {
        food += num;
    }
}
