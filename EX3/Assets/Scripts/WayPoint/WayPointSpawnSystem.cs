using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointSpawnSystem
{

    private Vector2 mSpawnRegionMin, mSpawnRegionMax;

    public GameObject[] mWayPointTemplates = null;

    public GameObject[] mWayPointTemplatesSt = null;

    private const int kTotalWayPoint = 6;

    private Vector2[] mInitPos = null;

    public WayPointSpawnSystem()
    {
        WayPoint.InitializeWayPointSpawnSystem(this);
        WayPointBehavior.InitializeWayPointSpawnSystem(this);

        mWayPointTemplates = new GameObject[kTotalWayPoint];
        mWayPointTemplatesSt = new GameObject[kTotalWayPoint];

        for (int i = 0; i < kTotalWayPoint; i++)
        {
            mWayPointTemplates[i] = Resources.Load<GameObject>("Prefabs/Number" + (i + 1));
        }
        mSpawnRegionMin = new Vector2(-15f, -15f);
        mSpawnRegionMax = new Vector2(15f, 15f);

        GenerateWayPoint();
    }

    private void SetPos() {
        mInitPos = new Vector2[kTotalWayPoint];
        mInitPos[0] = new Vector2(-70f, 70f);
        mInitPos[1] = new Vector2(70f, -70f);
        mInitPos[2] = new Vector2(30f, 0f);
        mInitPos[3] = new Vector2(-70f, -70f);
        mInitPos[4] = new Vector2(70f, 70f);
        mInitPos[5] = new Vector2(-30f, 0f);

    }
    private void GenerateWayPoint() {
        SetPos();
        for (int i = 0; i < kTotalWayPoint; i++)
        {
            GameObject p = GameObject.Instantiate(mWayPointTemplates[i]) as GameObject;
            p.transform.position = mInitPos[i];
            mWayPointTemplatesSt[i] = p;
        }
    }

    private void GenerateWayPointByName(string name) {
        for (int i = 0; i < kTotalWayPoint; i++)
        {
            if (mWayPointTemplates[i].name + "(Clone)" == name) {
                GameObject p = GameObject.Instantiate(mWayPointTemplates[i]) as GameObject;
                p.transform.position = new Vector3(mInitPos[i].x + Random.Range(mSpawnRegionMin.x, mSpawnRegionMax.x), 
                                                    mInitPos[i].y + Random.Range(mSpawnRegionMin.y, mSpawnRegionMax.y),
                                                     0f);
            }
        }
    }
    public void OneWayPointDestroyed(string name) { ReplaceOneWayPoint(name); }
    public void ReplaceOneWayPoint(string name) { GenerateWayPointByName(name); }
}
