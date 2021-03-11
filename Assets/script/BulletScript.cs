using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public float ShootSpeed;
    public float lifeTime;
    float curTime;
    float lastTime;

	// Use this for initialization
	void Start () {
        lastTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * ShootSpeed * Time.deltaTime);
        curTime = Time.time;
        if (curTime - lastTime >= lifeTime)
        {
            Destroy(this.gameObject);
            lastTime  = curTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer!=LayerMask.NameToLayer("Monster")&&other.gameObject.layer != LayerMask.NameToLayer("BulletYou")&& other.gameObject.layer != LayerMask.NameToLayer("BulletMe")
           && other.gameObject.layer != LayerMask.NameToLayer("LuDian")&&other.gameObject.layer != LayerMask.NameToLayer("Player")
           &&other.gameObject.layer != LayerMask.NameToLayer("BlackCore"))
           //不是以上名字的标签时才销毁
        {
            Destroy(this.gameObject);
        }
    }

}
