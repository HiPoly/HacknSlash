using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BasicComboState{
    None,
    Basic1,
    Basic2,
    Basic3,
}
public enum DodgeComboState{
    None,
    Dodge1,
    Dodge2,
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerAnim PlayerAnim;
    private Rigidbody rb;
    private bool Grounded;
    //Timers
    private bool activateComboTimerToReset;
    private bool activateDodgeTimerToReset;
    private float DefaultComboTimer = 0.8f;
    private float CurrentComboTimer;
    private float DefaultDodgeTimer = 0.8f;
    private float CurrentDodgeTimer;
    //Enums
    private BasicComboState CurrentComboState;
    private DodgeComboState CurrentDodgeState;

    [SerializeField] private float DashSpeed;
    [SerializeField] private float DashTime;
    [SerializeField] private float StartDashTime;

    private void Start()
    {
        CurrentComboTimer = DefaultComboTimer;
        CurrentDodgeTimer = DefaultDodgeTimer;
        CurrentComboState = BasicComboState.None;
        CurrentDodgeState = DodgeComboState.None;
        rb = GetComponent<Rigidbody>();
        Grounded = true;
        DashTime = StartDashTime;
    }
    private void Update()
    {
        MoveTracker();
        ResetComboState();
    }
    void MoveTracker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SpecialMoveChecks
            if (Input.GetAxisRaw(Axis.horizontalaxis) > 0 && (Input.GetAxisRaw(Axis.verticalaxis) < 0))
            {
                PlayerAnim.Sweep();
                return;
            }//Sweep
            if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
            {
                PlayerAnim.Hold();
                return;
            }//Air-Hold
            if (Input.GetAxisRaw(Axis.verticalaxis) < 0 && (!Grounded))
            {
                PlayerAnim.Bounce();
                return;
            }//Bounce

            //check if basic combo finished
            if (CurrentComboState >= BasicComboState.Basic3)
            {
                return;
            }

            if (Grounded)
            {
                CurrentComboState++;
                activateComboTimerToReset = true;
                CurrentComboTimer = DefaultComboTimer;

                if (CurrentComboState == BasicComboState.Basic1)
                {
                    PlayerAnim.Basic1();
                }
                if (CurrentComboState == BasicComboState.Basic2)
                {
                    PlayerAnim.Basic2();
                }
                if (CurrentComboState == BasicComboState.Basic3)
                {
                    PlayerAnim.Basic3();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {   // check if dodge sequence completed
            if (CurrentDodgeState == DodgeComboState.Dodge2)
            {
                return;
            }
            if (Input.GetAxis(Axis.horizontalaxis) > 0 && (Input.GetAxis(Axis.horizontalaxis) < 0))
            {
                PlayerAnim.Slide();
                return;
            }
            CurrentDodgeState++;
            activateDodgeTimerToReset = true;
            CurrentDodgeTimer = DefaultDodgeTimer;

            if (CurrentDodgeState == DodgeComboState.Dodge1)
            {
                PlayerAnim.Dodge1();
                if (Input.GetAxisRaw(Axis.horizontalaxis) < 0)
                {
                    rb.velocity = Vector3.left * DashSpeed * Time.deltaTime;
                }
                else if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
                {
                    rb.velocity = Vector3.right * DashSpeed * Time.deltaTime;
                }
                if (CurrentDodgeState == DodgeComboState.Dodge2)
                {
                    PlayerAnim.Dodge2();
                    if (Input.GetAxisRaw(Axis.horizontalaxis) < 0)
                    {
                        rb.transform.position += Vector3.left * DashSpeed * 1.5f * Time.deltaTime;
                    }
                    else if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
                    {
                        rb.transform.position = Vector3.right * DashSpeed * 1.5f * Time.deltaTime;
                    }
                }
            }
        }
    }
    void ResetComboState(){
        if (activateComboTimerToReset)
        {
            CurrentComboTimer -= Time.deltaTime;
            if (CurrentComboTimer <= 0f)
            {
                CurrentComboState = BasicComboState.None;
                activateComboTimerToReset = false;
                CurrentComboTimer = DefaultComboTimer;
            }
        }
        if (activateDodgeTimerToReset)
        {
            CurrentComboTimer -= Time.deltaTime;
            if (CurrentComboTimer <= 0f)
            {
                CurrentDodgeState = DodgeComboState.None;
                activateDodgeTimerToReset = false;
                CurrentDodgeTimer = DefaultDodgeTimer;
            }
        }
    }
}//class

