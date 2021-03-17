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
            other.transform.position = this.transform.parent.Find("HandPos").position;
            other.transform.rotation = this.transform.parent.Find("HandPos").rotation;
            other.transform.SetParent(this.transform.parent.Find("HandPos"));
            

            //GameObject boxSkill = other.gameObject.GetComponent<SkillBox>().skill;
            //if (skill != boxSkill)
            //{
            //    skill = boxSkill;
            //    Instantiate(skill, this.transform.position + transform.forward, this.transform.rotation, this.transform);
            //}
        }
    }
}
