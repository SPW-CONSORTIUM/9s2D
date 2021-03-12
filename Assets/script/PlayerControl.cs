using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float Hp; 
    public float speed;
    public float shootimedelta;//射击间隔时间
    public float JuLi;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject BlackCore;
    public GameObject Master;
    public GameObject skill=null;//目前没啥用，以后可能用于存储

    public bool canFire;
    public bool sanshe;//散射开关
    public bool fangshe;//放射开关
    public bool lianshe;//连射开关
    public bool EndMode;
    public bool Isthis=false;
    public string Id;
    bool EndModeUp;
    float lastTime;
    float curTime;
    Vector2 center = Vector2.zero;
    Vector2 centerL = Vector2.zero;
    Vector2 centerR = Vector2.zero;
    Vector3 up=Vector3.up;
    GameObject bullet;


    // Use this for initialization
    void Start () {
        Input.multiTouchEnabled = true;
        lastTime = Time.time;
        //canFire = false;
        bullet = bullet1;
        
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Hp <= 0/*|| Vector3.Distance(transform.position,Master.transform.position)>JuLi*/)
        {
            Instantiate(BlackCore, this.transform.position + transform.forward, this.transform.rotation);
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(Id))
        {
            Isthis = !Isthis;

          //  Ray2D ray;
          //  ray = new Ray2D(transform.position, (Master.transform.position - transform.position).normalized);

        }
            

    }

    void FixedUpdate ()
    {

        Vector3 move,shoot;
        move = PCInput();
        shoot = PCShootInput();

        if (Input.touchCount > 0)
        {
            for (int i=0;i< Input.touchCount;i++)
            {
                Touch touch = Input.touches[i];
                if (touch.phase == TouchPhase.Began)
                {
                    lastTime = Time.time;
                    center = touch.position;
                }
                if (center.x <= Screen.width / 2)
                {
                    centerL = center;
                    move.x = touch.position.x - centerL.x;
                    move.z = touch.position.y - centerL.y;
                }
                else
                {
                    centerR = center;
                    shoot.x = touch.position.x - centerR.x;
                    shoot.z = touch.position.y - centerR.y;
                }
            }

        }

        if(Isthis)
            PlayerCtrl(ref move, ref shoot);
    }

    private static Vector3 PCShootInput()
    {
        float shootx = Input.GetAxis("Horizontal2");
        float shooty = Input.GetAxis("Vertical2");
        Vector3 shoot = new Vector3(shootx, shooty, 0);
        return shoot;
    }

    private void PlayerCtrl(ref Vector3 move, ref Vector3 shoot)
    {
        float y = Camera.main.transform.rotation.eulerAngles.y;//这块是为了兼容3D模式做的处理，为了让shoot和moce只沿着水平面旋转
        shoot = Quaternion.Euler(0, y, 0) * shoot;//沃日，前边的shoot是这个函数里的，后边的shoot应该是个全局变量；
        move = Quaternion.Euler(0, y, 0) * move;

        transform.Translate(move * Time.deltaTime * speed, Space.World);
        if(move!=Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, move);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }

        if(canFire)
        {
            curTime = Time.time;
            if (shoot != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shoot);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);


                if (curTime - lastTime >= shootimedelta)
                {
                    Instantiate(bullet, this.transform.position + transform.forward, this.transform.rotation);
                    //在player下生成 
                    //Instantiate(bullet, this.transform,true);
                    lastTime = curTime;

                }
            }
        }

        if (sanshe)
        {
            curTime = Time.time;

            if (shoot != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shoot);//根据jkli定义面部朝向向量
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);//用Slerp方法让主角转向上一步计算出的向量方向
                Quaternion rotationL = Quaternion.LookRotation(Vector3.forward, shoot);


                if (curTime - lastTime >= shootimedelta)
                {
                    Instantiate(bullet, this.transform.position + transform.forward, this.transform.rotation);
                    Instantiate(bullet, this.transform.position + transform.right, this.transform.rotation*Quaternion.Euler(0,0,-30));
                    Instantiate(bullet, this.transform.position - transform.right, this.transform.rotation * Quaternion.Euler(0, 0, 30));
                    //在player下生成 
                    //Instantiate(bullet, this.transform,true);
                    lastTime = curTime;

                }
            }
        }



    }

    private Vector3 PCInput()
    {
        Vector3 move;
        float movex, movey;
        if (EndMode&&EndModeUp)
        {
            movex = 2;
            bullet = bullet2;
        }
            
        else
            movex = Input.GetAxis("Horizontal");
        movey = Input.GetAxis("Vertical");
        move = new Vector3(movex, movey, 0);
        return move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("BlackCore"))
        {
            canFire = true;
            EndModeUp = true;
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BulletYou"))
        {
            Hp--;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("RedBoom"))
        {
            Hp = 0;
        }

        //if (other.gameObject.layer == LayerMask.NameToLayer("SkillBox"))//判断碰撞对象标签
        //{
        //    GameObject boxSkill = other.gameObject.GetComponent<SkillBox>().skill;
        //    if (skill != boxSkill)
        //    {
        //        skill = boxSkill;
        //        Instantiate(skill, this.transform.position + transform.forward, this.transform.rotation, this.transform);
        //    }
        //}
    }
}
