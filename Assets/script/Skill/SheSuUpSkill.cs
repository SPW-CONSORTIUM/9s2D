using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheSuUpSkill : MonoBehaviour {

    public float upValue;

    // Use this for initialization
    void Start () {

        this.transform.parent.gameObject.GetComponent<PlayerControl>().shootimedelta= this.transform.parent.gameObject.GetComponent<PlayerControl>().shootimedelta*upValue;


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
