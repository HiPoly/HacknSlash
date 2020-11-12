using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerAnim PlayerAnim;
    private Rigidbody playerBody;

    public float walkSpeed = 100f;

    private float rotationY = -90;
    private float rotationSpeed = 15f;

    //Max Clamp Height
    [SerializeField] private float MaxHeight = 100f; 

    void Awake()
    {
        PlayerAnim = GetComponent<PlayerAnim>();
        playerBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        ClampY();
        RotatePlayer();
        CheckCrouch();
        CheckWalk();
        CheckRun();
    }
    void FixedUpdate()
    {
        DetectMovement();
    }
    void DetectMovement()
    {
        if (Input.GetAxisRaw(Axis.verticalaxis) >= 0)
        {
            playerBody.velocity = new Vector3
            (Input.GetAxisRaw(Axis.horizontalaxis) * (walkSpeed * Time.deltaTime),
            playerBody.velocity.y,
            playerBody.velocity.z);
        }
    }

    void ClampY()
    {
        Vector3 ClampedPosition = transform.position;
        ClampedPosition.y = Mathf.Clamp(ClampedPosition.y, 0f, MaxHeight);
        transform.position = ClampedPosition;
    }

    void RotatePlayer()
    {
        if(Input.GetAxisRaw(Axis.horizontalaxis) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotationY) * rotationSpeed, 0f);
        }
        else if(Input.GetAxisRaw(Axis.horizontalaxis) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotationY) * rotationSpeed, 0f);
        }
    }

    void CheckCrouch()
    {
        if (Input.GetKey(KeyCode.S))
        {
            PlayerAnim.Crouch(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
            PlayerAnim.Crouch(false);
    }
    void CheckWalk()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKeyDown(KeyCode.S)) {
            PlayerAnim.Walk(true);
            }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKeyDown(KeyCode.S)){
            PlayerAnim.Walk(true);
            }
        else {
            PlayerAnim.Walk(false);
        }
    }
    void CheckRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            PlayerAnim.Run(true);
        }
        else
        {
            PlayerAnim.Run(false);
        }
    }

} // class
