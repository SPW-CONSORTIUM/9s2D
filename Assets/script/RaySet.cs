using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySet : MonoBehaviour {

    public float LinesLong;
    public int LineCount;

    public GameObject muBiaoObj;
    public float escMax;//目标丢失距离

    public float m;//射线密度
    public float h;//射线高度（重心到头顶间的位置）

    public bool Player;
    public bool Monster;
    public bool NPC;

    private RaycastHit[] hit;
    private Ray[] ray;
    Vector3 muBiao;

    // Use this for initialization
    void Start () {
        ray = new Ray[LineCount];
        hit = new RaycastHit[LineCount];
    }
	
	// Update is called once per frame
	void Update () {
        if (muBiaoObj!=null)
        {
            muBiao = muBiaoObj.transform.position - this.transform.position;
            if (muBiao.magnitude >= escMax)
                muBiaoObj = null;
        }


        for (int i = 0; i < ray.Length; i++)
        {

            if ((i % 2) == 0)
            {
                if (i < 35)
                {
                    ray[i] = new Ray(transform.position + transform.up*h, Vector3.Lerp(this.transform.forward, this.transform.right, i * m).normalized * LinesLong);
                    Physics.Raycast(transform.position, Vector3.Lerp(this.transform.forward, this.transform.right, i * m), out hit[i],LinesLong);
                    Debug.DrawRay(transform.position+transform.up * h, Vector3.Lerp(this.transform.forward, this.transform.right, i * m).normalized * LinesLong, Color.blue);
                }

                else
                {
                    ray[i] = new Ray(transform.position + transform.up * h, Vector3.Lerp(this.transform.forward, this.transform.right, i * m).normalized * LinesLong * 0.1f);
                    Physics.Raycast(transform.position + transform.forward, Vector3.Lerp(this.transform.forward, this.transform.right, i * m), out hit[i],LinesLong * 0.1f);
                    Debug.DrawRay(transform.position + transform.up * h, Vector3.Lerp(this.transform.forward, this.transform.right, i * m).normalized * LinesLong * 0.1f, Color.yellow);
                }
            }

            if ((i % 2) == 1)
            {
                if (i <= 35)
                {
                    ray[i] = new Ray(transform.position + transform.up * h, Vector3.Lerp(this.transform.forward, -this.transform.right, i * m).normalized * LinesLong);
                    Physics.Raycast(transform.position + transform.forward, Vector3.Lerp(this.transform.forward, -this.transform.right, i * m), out hit[i], LinesLong);
                    Debug.DrawRay(transform.position + transform.up * h, Vector3.Lerp(this.transform.forward, -this.transform.right, i * m).normalized * LinesLong, Color.blue);
                }

                else
                {
                    ray[i] = new Ray(transform.position + transform.up * h, Vector3.Lerp(this.transform.forward, -this.transform.right, i * m).normalized * LinesLong * 0.1f);
                    Physics.Raycast(transform.position + transform.forward, Vector3.Lerp(this.transform.forward, -this.transform.right, i * m), out hit[i], LinesLong * 0.1f);
                    Debug.DrawRay(transform.position + transform.up * h, Vector3.Lerp(this.transform.forward, -this.transform.right, i * m).normalized * LinesLong * 0.1f, Color.yellow);
                }
            }

            if (hit[i].collider != null)
            {
                if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer("Player") && Player)
                    muBiaoObj = hit[i].collider.gameObject;
                else if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer("Monster") && Monster)
                    muBiaoObj = hit[i].collider.gameObject;
                else if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer("NPC") && NPC)
                    muBiaoObj = hit[i].collider.gameObject;
            }


        }
    }

}
