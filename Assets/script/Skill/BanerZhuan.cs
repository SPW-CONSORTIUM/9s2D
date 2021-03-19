using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanerZhuan : MonoBehaviour {

    public float f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.parent != null && this.transform.GetComponentInParent<PlayerControl>().shoot != Vector3.zero)
        {
            this.transform.GetComponent<Rigidbody>().AddForce(this.transform.GetComponentInParent<PlayerControl>().shoot * f, ForceMode.Force);

        }


    }
}
