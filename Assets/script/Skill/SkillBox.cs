using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBox : MonoBehaviour {

    public GameObject skill;
    public float CD;
    Vector3 Position;
    GameObject[] playerSkill;
    bool findSkill=false;

    // Use this for initialization
    void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)//unity2d碰撞检测函数每秒执行
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.transform.childCount != 0)//遍历子节点名称，找到名字与skill相同的节点则find为true
            {
                playerSkill = new GameObject[other.transform.childCount];
                for (int i = 0; i < other.transform.childCount; i++)
                {
                    playerSkill[i] = other.transform.GetChild(i).gameObject;
                    if (other.transform.GetChild(i).gameObject.name == skill.name)
                    {
                        var name = skill.name;
                        //other.transform.GetChild(i).gameObject.GetComponent().    //重置该技能冷却时间
                        findSkill = true;
                        break;
                    }
                }

            }
                
            else if(!findSkill)
                Instantiate(skill, other.transform.position + transform.forward, other.transform.rotation, other.transform);

        }
    }
}
