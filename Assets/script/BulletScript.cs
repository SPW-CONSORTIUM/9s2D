using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float ShootSpeed;//子弹飞行速度
    public float lifeTime;//子弹存活时间
    float curTime;
    float lastTime;

    public bool Default;
    public bool BulletMe;
    public bool Player;
    public bool SkillBox;
    public bool RedBoom;
    public bool Monster;
    public bool BulletYou;
    public bool LuDian;
    public bool NPC;
    public bool BlackCore;
    public bool BlackBox;


    // Use this for initialization
    void Start () {
        lastTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * ShootSpeed * Time.deltaTime);//子弹飞行函数
            curTime = Time.time;
            if (curTime - lastTime >= lifeTime)
            {
                Destroy(this.gameObject);
                lastTime = curTime;
            }

        
    }

    private void OnTriggerEnter(Collider other)
    {//遇到一下标签删除自己
        if (Default && other.gameObject.layer == LayerMask.NameToLayer("Default"))
            Destroy(this.gameObject);
        else if (BulletMe && other.gameObject.layer == LayerMask.NameToLayer("BulletMe"))
            Destroy(this.gameObject);
        else if (Player && other.gameObject.layer == LayerMask.NameToLayer("Player"))
            Destroy(this.gameObject);
        else if (SkillBox && other.gameObject.layer == LayerMask.NameToLayer("SkillBox"))
            Destroy(this.gameObject);
        else if (RedBoom && other.gameObject.layer == LayerMask.NameToLayer("RedBoom"))
            Destroy(this.gameObject);
        else if (Monster && other.gameObject.layer == LayerMask.NameToLayer("Monster"))
            Destroy(this.gameObject);
        else if (BulletYou && other.gameObject.layer == LayerMask.NameToLayer("BulletYou"))
            Destroy(this.gameObject);
        else if (LuDian && other.gameObject.layer == LayerMask.NameToLayer("LuDian"))
            Destroy(this.gameObject);
        else if (NPC && other.gameObject.layer == LayerMask.NameToLayer("NPC"))
            Destroy(this.gameObject);
        else if (BlackCore && other.gameObject.layer == LayerMask.NameToLayer("BlackCore"))
            Destroy(this.gameObject);
        else if (BlackCore && other.gameObject.layer == LayerMask.NameToLayer("BlackCore"))
            Destroy(this.gameObject);
    }

}
