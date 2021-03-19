using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {
    public float movespeed;
    public float speedq;
    public GameObject bullet;
    public float shootTime;

    public float EscMax;


    public int Hp;
    public GameObject Ludian;
    public bool FenLieMode;
    public bool Loop;
    public bool canFindNPC;
    public bool Find;
    public GameObject Monster;

    private RaycastHit[] hit;
    private Ray[] ray;

    GameObject Player;
    float lastTime;
    float curTime;

    int Jb=0;
    Vector3 mubiao;


    enum State
    {
        Go, Back
    }

    State mState = State.Go;

	// Use this for initialization
	void Start () {
        lastTime = Time.time;

        Player = GameObject.Find("Player");
		
	}

	// Update is called once per frame
	void Update ()
    {

        if (this.transform.GetComponent<RaySet>().muBiaoObj != null)
        {
            Find = true;
            Player = this.transform.GetComponent<RaySet>().muBiaoObj;
            if (this.transform.parent != null)
            {
                MonsterScript[] Monsters;
                Monsters = this.transform.parent.gameObject.GetComponentsInChildren<MonsterScript>();
                for (int t = 0; t < Monsters.Length; t++)
                {
                    if (!Monsters[t].Find)
                        Monsters[t].Find = true;
                }
            }
        }

        if (Hp <= 0)
        {
            if (FenLieMode)
            {
                if (Monster != null)
                {
                    Instantiate(Monster, this.transform.position + transform.right * 2f, Quaternion.identity);
                    Instantiate(Monster, this.transform.position - transform.right * 2f, Quaternion.identity);
                }
                Destroy(this.gameObject);

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Find)
        {
            if (Player != null)
            {
                mubiao = Player.transform.position - this.transform.position;
                Follow();
                MonsterAct();
            }
            else
                Find = false;
            

        }
        else
        {
            if (Ludian != null)
            {
                if(Loop)
                {
                    if (Jb >= Ludian.transform.childCount)
                    {
                        Jb = 0;
                    }
                }
                else
                {
                    if (Jb >= Ludian.transform.childCount)
                    {
                        Jb = Ludian.transform.childCount - 2;
                        mState = State.Back;
                    }
                    if (Jb <= 0)
                    {
                        Jb = 0;
                        mState = State.Go;
                    }
                }
                mubiao = Ludian.transform.GetChild(Jb).position - this.transform.position;
                MonsterAct();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BulletMe"))
        {
            Hp--;
        }

        if(other.gameObject.layer==LayerMask.NameToLayer("LuDian")&& other.transform.parent.name == Ludian.name)
        {
            Jb = other.transform.GetSiblingIndex();

            switch(mState)
            {
                case State.Go:
                    
                    Jb++;
                    break;
                case State.Back:
                    
                    Jb--;
                    break;
            }
        
        }

    }

    private void Follow()
    {
        curTime = Time.time;


        if (curTime - lastTime >= shootTime)
        {
            Instantiate(bullet, this.transform.position, this.transform.rotation);
            lastTime = curTime;
        }


        if (Player != null)
        {
            Vector3 distance;
            distance = Player.transform.position - transform.position;

            if (distance.magnitude >= EscMax)
            {
                Find = false;
            }
        }
        else
            Find = false;
            

    }

    private void MonsterAct()
    {
        Quaternion target = Quaternion.LookRotation(mubiao, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, speedq);
        transform.Translate(Vector3.forward * movespeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("RedBoom"))
        {
            Hp = 0;
        }
    }
}


