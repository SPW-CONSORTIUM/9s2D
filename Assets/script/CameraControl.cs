﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject Player1;

   
    public float speed;
    public float speedq;
    public Vector3 campos;
    public Vector3 rote;
    GameObject Player2;
    Vector3 move;
    float movex, movez;
	// Use this for initialization
	void Start () {
        this.transform.position = Player1.transform.position + campos;
	}
	
	// Update is called once per frame
    void FixedUpdate ()
    {
        //PlayersCtrl();
        GuDingRota();
        flowPlayer();
        //CamEndMode2D();

    }

    private void CamEndMode2D()//原2D玩法的摄像机方法
    {
        if (Player1 == null)
        {
            Destroy(this);
        }
        else
        {
            float r = Input.GetAxis("CamCtrl");
            float y = Camera.main.transform.rotation.eulerAngles.y;
            Vector3 camerapos = Quaternion.Euler(0, y, 0) * campos;
            Vector3 cameraPosition = new Vector3(Player1.transform.position.x, 0, -10);
            transform.position = cameraPosition;

            transform.RotateAround(Player1.transform.position, Vector3.up, speedq * Time.deltaTime * r);
        }
    }

    private void flowPlayer()//与玩家同步运动
    {
        movex = Input.GetAxis("Horizontal");
        movez = Input.GetAxis("Vertical");
        move = new Vector3(movex, 0, movez);
        float y = Camera.main.transform.rotation.eulerAngles.y;
        move = Quaternion.Euler(0, y, 0) * move;
        transform.Translate(move * Time.deltaTime * speed, Space.World);
    }

    private void GuDingRota()//摄像机的固定旋转
    {
        if (transform.rotation.eulerAngles.y >= rote.y + 10 || transform.rotation.eulerAngles.y <= rote.y - 10)
        {
            transform.RotateAround(Player1.transform.position, Vector3.up, speed * Time.deltaTime); //摄像机围绕目标旋转
        }
    }

    private void PlayersCtrl()//摄像机视野缩放控制，大概用不上了
    {
        float addx = (Player1.transform.position.x + Player2.transform.position.x);
        float addy = (Player1.transform.position.y + Player2.transform.position.y);
        campos = new Vector3(addx / 2, addy / 2, -1);
        this.transform.position = campos;
        float delax = Player1.transform.position.x - Player2.transform.position.x;
        float delay = Player1.transform.position.y - Player2.transform.position.y;
        if (Mathf.Pow((Mathf.Pow(delax, 2) + Mathf.Pow(delay, 2)), 1f / 2f) < 10)
        {
            GetComponent<Camera>().orthographicSize = 5;
        }
        else
        {
            GetComponent<Camera>().orthographicSize = Mathf.Pow((Mathf.Pow(delax, 2) + Mathf.Pow(delay, 2)), 1f / 3f);
        }
    }
}
