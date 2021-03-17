using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LianSheSkill : MonoBehaviour {
    public GameObject bullet;
    public float shootime;//射击持续时间
    public float ModeUptime;//模式开始时间
    public float shootimedelta = 0.3f;//射击间隔时间
    public bool ModeUp = false;//技能启动开关

    bool timeCanFire = false;//控制是否在开火时间
    bool s = true;
    float lastTime;
    float curTime;
    float startime;//技能开启时间
                   // Use this for initialization
    void Start () {
        lastTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.parent != null&&this.transform.GetComponentInParent<PlayerControl>().Ishoot)
        {
            ModeUp = true;
        }
            
        if (ModeUp)
        {
            if (s)
            {
                startime = Time.time;
                s = false;
            }

            curTime = Time.time;

            if (curTime - lastTime >= shootimedelta)//计算时间间隔
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    Instantiate(bullet, this.transform.GetChild(i).position, this.transform.GetChild(i).rotation);
                }
                lastTime = curTime;
            }

            if (curTime - startime >= shootime)
                Destroy(this.gameObject);
        }
    }
}
