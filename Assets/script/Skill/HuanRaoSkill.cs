﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuanRaoSkill : MonoBehaviour {

    public GameObject bullet;//用的什么子弹
    public float shootime;//射击持续时间
    public float ModeUptime;//模式开始时间
    public bool ModeUp = false;//控制开始释放技能

    float shootimedelta;//子弹转速
    bool timeCanFire = false;//控制是否在开火时间
    bool s = true;
    float lastTime;
    float curTime;//实施运行时间
    float startime;//技能开启时间


    // Use this for initialization
    void Start()
    {
        lastTime = Time.time;
        shootimedelta = this.transform.parent.gameObject.GetComponent<PlayerControl>().shootimedelta;//获取自身父节点脚本下的射击时间属性

    }

    // Update is called once per frame
    void Update()
    {


        if (ModeUp)
        {
            
            if (s)//只生成一次
            {
                startime = Time.time;
                s = false;
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    Instantiate(bullet, this.transform.GetChild(i).position, this.transform.GetChild(i).rotation);
                }

            }
            curTime = Time.time;
            



            if (curTime - startime <= shootime)//计算持续时间
            {

            }
        }

    }
}
