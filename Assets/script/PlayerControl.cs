using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float Hp; 
    public float speed;
    public float shootimedelta;//射击间隔时间
 //   public float escMax;//退出朝向跟随模式的距离
    
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject BlackCore;
    public GameObject Master;

    public bool canFire;
    public bool EndMode;
    public bool Isthis=false;
    public bool autoMode = true;
    
    bool EndModeUp;
    float lastTime;
    float curTime;
    Vector2 center = Vector2.zero;
    Vector2 centerL = Vector2.zero;
    Vector2 centerR = Vector2.zero;
    Vector3 up=Vector3.up;
    GameObject bullet;
    GameObject muBiao;
    


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
        if (this.transform.GetComponent<RaySet>().muBiaoObj != null)
        {
            
        }
        
        

    }

    void FixedUpdate ()
    {

        Vector3 move,shoot;
        move = PCInput();
        shoot = PCShootInput();
        if (Isthis)
            PlayerCtrl(ref move, ref shoot);

        /* if (Input.touchCount > 0)//尝试写触屏操作失败
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
        */
    }

    private Vector3 PCShootInput()
    {
        Vector3 shoot;
        if (autoMode&& this.transform.GetComponent<RaySet>().muBiaoObj!=null)
        {
            shoot = this.transform.GetComponent<RaySet>().muBiaoObj.transform.position - this.transform.position;
            return shoot;
        }

        else
        {
            float shootx = Input.GetAxis("Horizontal2");
            float shooty = Input.GetAxis("Vertical2");
            shoot = new Vector3(shootx, shooty, 0);
            return shoot;
        }

    }

    private void PlayerCtrl(ref Vector3 move, ref Vector3 shoot)
    {
        float y = Camera.main.transform.rotation.eulerAngles.y;
        shoot = Quaternion.Euler(0, y, 0) * shoot;
        move = Quaternion.Euler(0, y, 0) * move;
        //在3D模式下，使物体只在水平面旋转
        transform.Translate(move * Time.deltaTime * speed, Space.World);
        if(move!=Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, move);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }

            
        if (shoot != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shoot);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

            curTime = Time.time;

            if (curTime - lastTime >= shootimedelta&&canFire)
            {
                    Instantiate(bullet, this.transform.position + transform.forward, this.transform.rotation);
                    //在player下生成 
                    //Instantiate(bullet, this.transform,true);
                    lastTime = curTime;

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
        if (other.gameObject.layer == LayerMask.NameToLayer("BulletYou")|| other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Hp--;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("RedBoom"))
        {
            Hp = 0;
        }


        //if (other.gameObject.layer == LayerMask.NameToLayer("SkillBox"))//判断碰撞对象标签,这个已经移到skillbox里边去写了
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
