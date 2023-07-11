using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    static private WayPointSpawnSystem sWayPointSystem = null;
    static public void InitializeWayPointSpawnSystem(WayPointSpawnSystem s) { sWayPointSystem = s; }

    public GameObject[] mWayPoints;
    private int mCurrentWayPoint = 0;

    private bool isSequence = true;

    private Vector3 enemyPosition;
    private Vector3 waypointPosition;
    private Vector3 direction;
    public float enemySpeed = 20f;
    private Vector3 directionNext;
    public float enemyRotateRate = 0.05f;
    private bool isTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        mWayPoints = sWayPointSystem.mWayPointTemplatesSt;
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

    public string GetWayPointStatus()
    {
        if (isSequence)
            return "WAYPOINTS: Sequence";
        else return "WAYPOINTS: Random";
    }

    private void SequenceWayPoint()
    {
        if (Vector3.Distance(this.transform.position, mWayPoints[mCurrentWayPoint].transform.position) < 25f)
        {
            isTurn = true;
            Debug.Log(mWayPoints[mCurrentWayPoint].transform.position);
            Debug.Log(Vector3.Distance(this.transform.position, mWayPoints[mCurrentWayPoint].transform.position));
            mCurrentWayPoint++;
            if (mCurrentWayPoint >= mWayPoints.Length)
            {
                mCurrentWayPoint = 0;
            }
        }
        if (isTurn)
        {
            if(Mathf.Abs(Vector3.Angle(transform.up, (mWayPoints[mCurrentWayPoint].transform.position-transform.position))) <= 5f)
            {
                isTurn = false;
            }
            enemyTurn();
        }
        else
        {
            enemyMove();
        }
    }

    private void RandomWayPoint()
    {
        if (Vector3.Distance(this.transform.position, mWayPoints[mCurrentWayPoint].transform.position) < 25f)
        {
            isTurn = true;
            Debug.Log("Distance < 25f from WayPoint.cs");
            mCurrentWayPoint = Random.Range(0, mWayPoints.Length);
        }
        if (isTurn)
        {
            if (Mathf.Abs(Vector3.Angle(transform.up, (mWayPoints[mCurrentWayPoint].transform.position - transform.position))) <= 5f)
            {
                isTurn = false;
            }
            enemyTurn();
        }
        else
        {
            enemyMove();
        }
    }

    //enemy move
    private void enemyMove()
    {
        enemyPosition = transform.position;//enemy的位置
        waypointPosition = mWayPoints[mCurrentWayPoint].transform.position;//下一个waypoint的位置
        transform.up = Vector3.Normalize(-(enemyPosition - waypointPosition));//获得waypoint的方向
        transform.position += enemySpeed * Time.smoothDeltaTime * transform.up;//move forward
    }

    //enemy turn
    private void enemyTurn()
    {
        Debug.Log("e up: " + transform.up + ";w dir: " + mWayPoints[mCurrentWayPoint].transform.position);
        Vector3 p = mWayPoints[mCurrentWayPoint].transform.localPosition;
        Vector3 v = p - transform.localPosition;
        transform.up = Vector3.LerpUnclamped(transform.up, v, enemyRotateRate * Time.smoothDeltaTime);
        transform.position += enemySpeed * Time.smoothDeltaTime * transform.up;//move forward
    }
}
