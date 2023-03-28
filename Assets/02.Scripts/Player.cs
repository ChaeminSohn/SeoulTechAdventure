using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    float hAxis;
    float vAxis;
    bool running;
    bool jumping;
    bool isBorder;
    public float moveSpeed;
    public float jumpPower;

    private GameManager manager;

    private Transform tr;
    private RaycastHit slopehit;

    

    Dictionary<KeyCode, Action> keyDictionary;

    Rigidbody rigid;
    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
       
    }
    void Start()
    {
        keyDictionary = new Dictionary<KeyCode, Action>
        {
            {KeyCode.Alpha1, keyDown_1 },
            {KeyCode.Alpha2, keyDown_2 },
            {KeyCode.Alpha3, keyDown_3 },
          
        };
        tr = GetComponent<Transform>();
        manager = GameManager.instance;
        FinishPoint.OnPlayerWin += this.OnPlayerWin;
        Debug.Log("Game Start");
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();

        if (Input.anyKeyDown){
            foreach(var dic in keyDictionary){
                if (Input.GetKeyDown(dic.Key))
                    dic.Value();
            }
            if (Input.GetButton("Follow"))
                manager.GiveCommand("Follow");
            else if (Input.GetButton("Move"))
                manager.GiveCommand("Move");
            
        }
    }

    void FixedUpdate()
    {
        FreezeRotation();
        //TiltOnSlope();
        StopToWall();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = false;
            anim.SetBool("isJump", jumping);
        }
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        Debug.DrawRay(transform.position, -transform.up * 5, Color.blue);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    void TiltOnSlope()
    {
    
            var angle = Vector3.Angle(transform.up, slopehit.normal);
            Quaternion rot = Quaternion.LookRotation(slopehit.normal);
            Vector3 up = rot.eulerAngles;
            tr.forward = up;
         
    }

    void move()
    {
        Vector3 moveVector;
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        running = Input.GetButton("Run");

        if(!jumping)
         moveVector = new Vector3(hAxis, 0, vAxis).normalized;
        else
         moveVector = new Vector3(hAxis, 0, vAxis).normalized/2;
        if (!isBorder)
        {
            if (running && !jumping)
                //transform.position += moveVector * 2 * moveSpeed * Time.deltaTime;
                tr.Translate(moveVector * 2 * moveSpeed * Time.deltaTime, Space.World);
            else
                //transform.position += moveVector * moveSpeed * Time.deltaTime;
                tr.Translate(moveVector * moveSpeed * Time.deltaTime , Space.World);
        }

        anim.SetBool("isWalk", moveVector != Vector3.zero);
        anim.SetBool("isRun", running && !jumping);

        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out slopehit, 1.0f, 1 << 7))
            TiltOnSlope();

        transform.LookAt(transform.position + moveVector);
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && !jumping)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumping = true;
            anim.SetBool("isJump", jumping);
        }
    }

    void OnPlayerWin()
    {
        Debug.Log("You Win!");
    }

    void keyDown_1()
    {
        GameManager.instance.SetCtrl(1);
    }
    void keyDown_2()
    {
        GameManager.instance.SetCtrl(2);
    }
    void keyDown_3()
    {
        GameManager.instance.SetCtrl(3);
    }
}

    
