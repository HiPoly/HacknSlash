using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerAnim PlayerAnim;
    private Rigidbody playerBody;
    private Animator anim;
    public float walkSpeed = 100f;
    private float RunMultiplier;
    private float rotationY = -90;
    [SerializeField]
    private GameObject CollisionBox = null;
    public bool Moving;
    private bool Running;

    void Awake()
    {
        PlayerAnim = GetComponent<PlayerAnim>();
        playerBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        CheckWalk();
        CheckRun();
        RotatePlayer();
        CheckCrouch();
        CheckDodge();
    }
    void FixedUpdate()
    {
        DetectMovement();
    }
    void DetectMovement()
    {
        if (Input.GetAxisRaw(Axis.verticalaxis) >= 0)
        {
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Run")){
                RunMultiplier = 2.0f;
            }
            else{
                RunMultiplier = 1.0f;
            }
            playerBody.velocity = new Vector3
            (Input.GetAxisRaw(Axis.horizontalaxis) * (RunMultiplier * walkSpeed * Time.deltaTime),
             playerBody.velocity.y,
             playerBody.velocity.z);
        }
    }
    void RotatePlayer()
    {
        if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotationY), 0f);
        }
        else if (Input.GetAxisRaw(Axis.horizontalaxis) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotationY), 0f);
        }
    }
    void CheckCrouch()
    {
        if (Input.GetKey(KeyCode.S) && transform.position.y == 0)
        {
            PlayerAnim.ChangeState(AnimationTags.crouch, 0.1f, 1);
            Moving = true;
        }
    }
    void CheckWalk()
    {
        if (Input.GetKey(KeyCode.A) 
        && !Input.GetKeyDown(KeyCode.S)
        && GetComponent<PlayerActions>().Acting == false) 
        {
            if (Running) 
            {
                PlayerAnim.Move(2);
            }
            else 
            {
                PlayerAnim.Move(1);
            }
            Moving = true;
        }
        else if (Input.GetKey(KeyCode.D)
        && !Input.GetKeyDown(KeyCode.S)
        && GetComponent<PlayerActions>().Acting == false)
        {
            if (Running)
            {
                PlayerAnim.Move(2);
            }
            else
            {
                PlayerAnim.Move(1);
            }
            Moving = true;
        }
        else {
            Moving = false;
        }
    }
    void CheckRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Running = false;
        }
    }
    void CheckDodge()
    {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge1")
            || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge2")
            || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            CollisionBox.SetActive(false);
        }
        else
        {
            CollisionBox.SetActive(true);
        }
    }

} // class
