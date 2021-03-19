using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanerZhuan : MonoBehaviour {

    public float f;
    Vector3 muBiao;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.parent != null && Input.GetAxis("Jump")!=0)
        {
            if (this.transform.GetComponentInParent<RaySet>().muBiaoObj != null)
                muBiao = this.transform.GetComponentInParent<RaySet>().muBiaoObj.transform.position - this.transform.position;
            else if (this.transform.GetComponentInParent<PlayerControl>().move != Vector3.zero)
                muBiao = this.transform.GetComponentInParent<PlayerControl>().move;

            this.transform.GetComponent<Rigidbody>().AddForce( muBiao * f, ForceMode.Force);

        }


    }
}
