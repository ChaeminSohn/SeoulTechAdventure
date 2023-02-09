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
    public float moveSpeed;
    public float jumpPower;

    Vector3 moveVector;

    Rigidbody rigid;

    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = false;
            anim.SetBool("isJump", jumping);
        }
    }

    void move()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        running = Input.GetButton("Run");

        if(!jumping)
         moveVector = new Vector3(hAxis, 0, vAxis).normalized;
        else
         moveVector = new Vector3(hAxis, 0, vAxis).normalized/2;
        if (running && !jumping)
            transform.position += moveVector * 2 * moveSpeed * Time.deltaTime;
        else
            transform.position += moveVector * moveSpeed * Time.deltaTime;

        anim.SetBool("isWalk", moveVector != Vector3.zero);
        anim.SetBool("isRun", running && !jumping);

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

    
}

    
