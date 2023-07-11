using UnityEngine;
using System.Collections;

public partial class EnemyBehavior : MonoBehaviour {

    // All instances of Enemy shares this one WayPoint and EnemySystem
    static private EnemySpawnSystem sEnemySystem = null;
    static public void InitializeEnemySystem(EnemySpawnSystem s) { sEnemySystem = s; }

    private int mNumHit = 0;
    private const int kHitsToDestroy = 4;
    private const float kEnemyEnergyLost = 0.8f;
    //public float enemySpeed = 20f;
    //public float enemyRotateRate = 0.03f / 60f;

    //WayPoint wayPoint;
    //private GameObject wp;
    //private GameObject wpNext;
    //private Vector3 direction;
    //private Vector3 directionNext;
    //private Vector3 enemyPosition;
    //private Vector3 waypointPosition;
    //private Vector3 waypointPosNext;
    //private float distance;
    

    private void Update()
    {
        //enemy�ƶ�
        //enemyPosition = transform.position;//enemy��λ��

        //WayPoint waypoint = GetComponent<WayPoint>();
        //waypointPosition = waypoint.GetmNextWayPointPos();//��һ��waypoint��λ��
        //Debug.Log("waypointPosition: " + waypointPosition);

        //direction = Vector3.Normalize(enemyPosition - waypointPosition);//���waypoint�ķ���

        //int nextWayPointNum = waypoint.GetmNextWayPointNum();
        //transform.LookAt(waypoint.mWayPoints[nextWayPointNum].transform);//����waypoint
        //transform.up = Vector3.Normalize(waypointPosition -  transform.position);
        //transform.position += enemySpeed * Time.smoothDeltaTime * transform.up;//��waypointǰ��

        //enemyת��
        /*
        distance = (enemyPosition - waypointPosition).magnitude;//��þ���
        if (distance <= 3f)
        {
            Debug.Log("Distance < 0.25f from Enemy.cs");
            //Ѱ����һ��waypoint
            waypointPosNext = waypoint.GetmNextWayPointPos();
            directionNext = waypointPosNext - transform.position;//���waypoint����
            //transform.up = Vector3.LerpUnclamped(transform.up, directionNext, enemyRotateRate);//��ֵ��waypoint��ת
        }
        */
    }


    #region Trigger into chase or die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Emeny OnTriggerEnter");
        TriggerCheck(collision.gameObject);
    }

    private void TriggerCheck(GameObject g)
    {
        if (g.name == "Hero")
        {
            ThisEnemyIsHit();

        } else if (g.name == "Egg(Clone)")
        {
            mNumHit++;
            if (mNumHit < kHitsToDestroy)
            {
                Color c = GetComponent<Renderer>().material.color;
                c.a = c.a * kEnemyEnergyLost;
                GetComponent<Renderer>().material.color = c;
            } else
            {
                ThisEnemyIsHit();
            }
        }
    }

    private void ThisEnemyIsHit()
    {
        sEnemySystem.OneEnemyDestroyed();
        Destroy(gameObject);
    }
    #endregion
}
