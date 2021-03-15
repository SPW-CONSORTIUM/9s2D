using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySet : MonoBehaviour {

    public float LinesLong;
    public int LineCount;
    public string Layer;
    public GameObject muBiaoObj;
    public float escMax;//目标丢失距离


    float m = 0.05f;//射线密度

    private RaycastHit2D[] hit;
    private Ray2D[] ray;
    Vector3 muBiao;

    // Use this for initialization
    void Start () {
        ray = new Ray2D[LineCount];
        hit = new RaycastHit2D[LineCount];
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
                    ray[i] = new Ray2D(transform.position, Vector3.Lerp(this.transform.up, this.transform.right, i * m).normalized * LinesLong);
                    hit[i] = Physics2D.Raycast(transform.position + transform.up, Vector3.Lerp(this.transform.up, this.transform.right, i * m), LinesLong);
                    Debug.DrawRay(transform.position, Vector3.Lerp(this.transform.up, this.transform.right, i * m).normalized * LinesLong, Color.blue);
                }

                else
                {
                    ray[i] = new Ray2D(transform.position, Vector3.Lerp(this.transform.up, this.transform.right, i * m).normalized * LinesLong * 0.1f);
                    hit[i] = Physics2D.Raycast(transform.position + transform.up, Vector3.Lerp(this.transform.up, this.transform.right, i * m), LinesLong * 0.1f);
                    Debug.DrawRay(transform.position, Vector3.Lerp(this.transform.up, this.transform.right, i * m).normalized * LinesLong * 0.1f, Color.yellow);
                }
            }

            if ((i % 2) == 1)
            {
                if (i <= 35)
                {
                    ray[i] = new Ray2D(transform.position, Vector3.Lerp(this.transform.up, -this.transform.right, i * m).normalized * LinesLong);
                    hit[i] = Physics2D.Raycast(transform.position + transform.up, Vector3.Lerp(this.transform.up, -this.transform.right, i * m), LinesLong);
                    Debug.DrawRay(transform.position, Vector3.Lerp(this.transform.up, -this.transform.right, i * m).normalized * LinesLong, Color.green);
                }

                else
                {
                    ray[i] = new Ray2D(transform.position, Vector3.Lerp(this.transform.up, -this.transform.right, i * m).normalized * LinesLong * 0.1f);
                    hit[i] = Physics2D.Raycast(transform.position + transform.up, Vector3.Lerp(this.transform.up, -this.transform.right, i * m), LinesLong * 0.1f);
                    Debug.DrawRay(transform.position, Vector3.Lerp(this.transform.up, -this.transform.right, i * m).normalized * LinesLong * 0.1f, Color.yellow);
                }
            }

            if (hit[i].collider != null)
            {
                if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer(Layer))
                {
                    muBiaoObj = hit[i].collider.gameObject;
                    
                }
            }


        }
    }

}
