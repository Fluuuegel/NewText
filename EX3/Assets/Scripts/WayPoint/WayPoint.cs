using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    static private WayPointSpawnSystem sWayPointSystem = null;
    static public void InitializeWayPointSpawnSystem(WayPointSpawnSystem s) { sWayPointSystem = s; }

    public GameObject[] mWayPoints;
    private int mCurrentWayPoint = 0;

    private int mNextWayPoint = 0;

    private bool isSequence = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // press J to change to Sequence / random
        if (Input.GetKeyDown(KeyCode.J)) {
            isSequence = !isSequence;
        }

        if (isSequence)
        {
            SequenceWayPoint();
        } else
        {
            RandomWayPoint();
        }

    }

    private void SequenceWayPoint()
    {
        if (Vector3.Distance(this.transform.position, mWayPoints[mCurrentWayPoint].transform.position) < 3f)
        {
            mNextWayPoint++;
            if (mNextWayPoint >= mWayPoints.Length)
            {
                mNextWayPoint = 0;
            }
            mCurrentWayPoint = mNextWayPoint;
        }
    }

    private void RandomWayPoint()
    {
        if (Vector3.Distance(this.transform.position, mWayPoints[mCurrentWayPoint].transform.position) < 3f)
        {
            while(mNextWayPoint == mCurrentWayPoint) {
                mNextWayPoint = Random.Range(0, mWayPoints.Length);
            }
        }
    }

    public Vector3 GetmNextWayPointPos()
    {
        return mWayPoints[mNextWayPoint].transform.position;
    }

}
