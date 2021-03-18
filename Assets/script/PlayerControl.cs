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
    public float f;//力的系数

    public bool Ishoot;//是否有shoot向量；用于开启技能
    public bool canFire;
    public bool EndMode;
    public bool Isthis=false;
    public bool autoMode = true;
    
    bool EndModeUp;
    float lastTime;
    float curTime;

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
        if (Hp <= 0)
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
        if (shoot != Vector3.zero)
            Ishoot = true;
        else
            Ishoot = false;
        if (Isthis)
            PlayerCtrl(ref move, ref shoot);
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
            float shootz = Input.GetAxis("Vertical2");
            shoot = new Vector3(shootx, 0, shootz);
            return shoot;
        }

    }

    private void PlayerCtrl(ref Vector3 move, ref Vector3 shoot)
    {
        float y = Camera.main.transform.rotation.eulerAngles.y;
        shoot = Quaternion.Euler(0, y, 0) * shoot;
        move = Quaternion.Euler(0, y, 0) * move;

        //在3D模式下，使物体只在水平面旋转
        //transform.Translate(move * Time.deltaTime * speed, Space.World);//位移驱动
        this.transform.Find("Foot").GetComponent<Rigidbody>().AddForce(move*f, ForceMode.Acceleration);
        this.transform.Find("Foot").GetComponent<Rigidbody>().AddTorque(move * f, ForceMode.Acceleration);
        //if(move!=Vector3.zero)
        //{
        //    Quaternion rotation = Quaternion.LookRotation(move, Vector3.up);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        //}


        if (shoot != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(shoot, Vector3.up);
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
        float movex, movez;
        if (EndMode&&EndModeUp)
        {
            movex = 2;
            bullet = bullet2;
        }
            
        else
            movex = Input.GetAxis("Horizontal");
        movez = Input.GetAxis("Vertical");
        move = new Vector3(movex, 0, movez);

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
