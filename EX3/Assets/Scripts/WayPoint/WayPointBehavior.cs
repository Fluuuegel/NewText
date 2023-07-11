using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointBehavior : MonoBehaviour
{   
    static private WayPointSpawnSystem sWayPointSystem = null;
    static public void InitializeWayPointSpawnSystem(WayPointSpawnSystem s) { sWayPointSystem = s; }

    private int mNumHit = 0;
    private const int kHitsToDestroy = 4;
    private const float kWayPointEnergyLost = 0.25f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerCheck(collision.gameObject);
    }

    private void TriggerCheck(GameObject g)
    {
        if (g.name == "Egg(Clone)")
        {
            mNumHit++;
            if (mNumHit < kHitsToDestroy)
            {
                Color c = GetComponent<Renderer>().material.color;
                c.a = c.a * kWayPointEnergyLost;
                GetComponent<Renderer>().material.color = c;
            } else
            {
                ThisWayPointIsHit();
            }
        }
    }

    private void ThisWayPointIsHit()
    {
        string name = gameObject.name;
        Debug.Log("WayPoint " + name + " is hit");
        sWayPointSystem.OneWayPointDestroyed(name);
        Destroy(gameObject);
    }
}