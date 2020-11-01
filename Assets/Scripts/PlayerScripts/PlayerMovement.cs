using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerAnim playerAnim;
    private Rigidbody playerBody;

    public float walkSpeed = 100f;
    public float zSpeed;

    private float rotationY = -90;
    private float rotationSpeed = 15f;

    void Start()
    {
        playerAnim = GetComponentInChildren<PlayerAnim>();
        playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RotatePlayer();
        AnimatePlayerWalk();
    }
    void FixedUpdate()
    {
        DetectMovement();
    }
    void DetectMovement()
    {
        playerBody.velocity = new Vector3
        (
            Input.GetAxisRaw(Axis.horizontalaxis) * (walkSpeed *Time.deltaTime),
            playerBody.velocity.y,
            playerBody.velocity.z
        );
    }

    void RotatePlayer()
    {
        if(Input.GetAxisRaw(Axis.horizontalaxis) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotationY), 0f);
        }
        else if(Input.GetAxisRaw(Axis.horizontalaxis) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotationY), 0f);
        }
    }
    void AnimatePlayerWalk()
    {
        if (Input.GetAxisRaw(Axis.horizontalaxis) != 0){
        playerAnim.Walk(true);
        }
        else{
        playerAnim.Walk(false);
        }

    }

} // class
