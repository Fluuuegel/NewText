using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownBar : MonoBehaviour
{
    public float mSecToCoolDown = 0.2f;
    public float mLastTriggered = 0f;
    private bool mActive = false;
    private float mInitBarWidth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform r = GetComponent<RectTransform>();
        mInitBarWidth = r.sizeDelta.x;  // This is the width of the Rect Transform
        mSecToCoolDown = 0.2f;
        mLastTriggered = Time.time; // time last triggered
    }

    // Update is called once per frame
    void Update()
    {
        if (mActive)//如果active，更新冷却条
            UpdateCoolDownBar();
    }

    private void UpdateCoolDownBar()//active时，冷却条会变化，由长变短
    {
        float sec = SecondsTillNext();
        float percentage = sec / mSecToCoolDown;//得到百分比

        if (sec < 0)//百分比低于零时回调，并设为不Active
        {
            mActive = false;
            percentage = 1.0f;
        }
            
        Vector2 s = GetComponent<RectTransform>().sizeDelta; //拿到cooldownbar的rectTransform
        s.x = percentage * mInitBarWidth;//长度为percentage乘初始长度就对了
        GetComponent<RectTransform>().sizeDelta = s;
    }

    public void SetCoolDownLength(float s)
    {
        mSecToCoolDown = s;
    }

    private float SecondsTillNext()//如果Active，得到冷却时间减去（当前时间与上一次触发时间的差值），用于调节bar的长度
    {
        float secLeft = -1;
        if (mActive)
        {
            float sinceLast = Time.time - mLastTriggered;
            secLeft = mSecToCoolDown - sinceLast;
        }
        return secLeft;
    }

    // returns if trigger is successful
    public bool TriggerCoolDown()//改变Active状态（发生于英雄发射子弹时）
    {
        bool canTrigger = !mActive;
        if (canTrigger)
        {
            mActive = true;
            mLastTriggered = Time.time;
            UpdateCoolDownBar();
        }

        return canTrigger;
    }

    //public bool ReadyForNext()//返回！mActive
    //{
    //    return (!mActive);
    //}
}
