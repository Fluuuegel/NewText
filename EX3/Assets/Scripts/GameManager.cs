using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager sTheGlobalBehavior = null;

    public Text mGameStateEcho = null;  // Defined in UnityEngine.UI
    public HeroBehavior mHero = null;
    private EnemySpawnSystem mEnemySystem = null;
    public WayPoint mWayPoint = null;
    private WayPointSpawnSystem mWayPointSystem = null;

    private CameraSupport mMainCamera;

    private void Start()
    {
        GameManager.sTheGlobalBehavior = this;  // Singleton pattern
        Debug.Assert(mHero != null);

        mMainCamera = Camera.main.GetComponent<CameraSupport>();
        Debug.Assert(mMainCamera != null);

        Bounds b = mMainCamera.GetWorldBound();
        mEnemySystem = new EnemySpawnSystem(b.min, b.max);
        mWayPointSystem = new WayPointSpawnSystem();
        mWayPoint = FindObjectOfType<WayPoint>();
    }

	void Update () {
        EchoGameState(); // always do this

        if (Input.GetKey(KeyCode.Q))
            Application.Quit();
    }

    //private string GetWayPointMode(WayPoint wp)
    //{
    //    string wpStatus = "WAYPOINTS:";
    //    if (wp.GetWayPointStatus())
    //        return wpStatus + "Sequence";
    //    else 
    //        return wpStatus + "Random";
    //}

    #region Bound Support
    public CameraSupport.WorldBoundStatus CollideWorldBound(Bounds b) { return mMainCamera.CollideWorldBound(b); }
    #endregion 

    private void EchoGameState()
    {
        mGameStateEcho.text =  mWayPoint.GetWayPointStatus() + " " + mHero.GetHeroState() + "  " + mEnemySystem.GetEnemyState();
    }
}