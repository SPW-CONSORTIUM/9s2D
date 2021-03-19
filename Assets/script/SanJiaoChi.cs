using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanJiaoChi : MonoBehaviour
{
    public float ShootSpeed;//子弹飞行速度
    public bool ModeUp = false;//技能启动开关


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent != null && this.transform.GetComponentInParent<PlayerControl>().shoot!=Vector3.zero)
        {
            ModeUp = true;
        }
    }

    private void FixedUpdate()
    {
        if (ModeUp)
        {
            transform.Translate(Vector3.forward * ShootSpeed * Time.deltaTime);//子弹飞行函数
            this.transform.parent = null;
        } 

    }

    private void OnCollisionEnter(Collision collision)
    {
        ModeUp = false;
    }
}
