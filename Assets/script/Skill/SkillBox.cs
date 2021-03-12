using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBox : MonoBehaviour {

    public GameObject skill;
    public float CD;
    Vector3 Position;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)//unity2d碰撞检测函数每秒执行
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))//判断碰撞对象标签
        {
            GameObject playerskill= other.gameObject.GetComponent<PlayerControl>().skill;//获取碰撞物体身上脚本里的属性
            if (playerskill != skill)
            {
                other.gameObject.GetComponent<PlayerControl>().skill = skill;
                Instantiate(skill, other.transform.position + transform.forward, other.transform.rotation, other.transform);
            }

        }
    }
}
