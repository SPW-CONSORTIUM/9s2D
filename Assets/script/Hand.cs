using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SkillBox"))
        {
            other.transform.position = this.transform.parent.Find("HandPos").position;//将碰到物体的位置信息修改为，自身父节点下名字为handPos的子节点的位置信息
            other.transform.rotation = this.transform.parent.Find("HandPos").rotation;
            other.transform.SetParent(this.transform.parent.Find("HandPos"));//将这个东西放在handpos下边
            //武器与手建立固定连接（因为武器是受重力的刚体，所以需要用物理方式连接）
            var fix = this.gameObject.AddComponent<FixedJoint>();
            fix.connectedBody = other.GetComponent<Rigidbody>();

            //GameObject boxSkill = other.gameObject.GetComponent<SkillBox>().skill;
            //if (skill != boxSkill)
            //{
            //    skill = boxSkill;
            //    Instantiate(skill, this.transform.position + transform.forward, this.transform.rotation, this.transform);
            //}
        }
    }
}
