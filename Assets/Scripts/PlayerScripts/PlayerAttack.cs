using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BasicComboState{
    None,
    Basic1,
    Basic2,
    Basic3,
    Basic4,
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
    //private bool MidCombo;
    
        //Timers
    private bool activateComboTimerToReset;
    private bool activateDodgeTimerToReset;
    private float DefaultComboTimer = 10;
    private float CurrentComboTimer;
    private float DefaultDodgeTimer = 2.0f;
    private float CurrentDodgeTimer;
    //Enums
    private BasicComboState CurrentComboState;
    private DodgeComboState CurrentDodgeState;

    [SerializeField] private float DashSpeed;
    [SerializeField] private float StartDashTime = 1.0f;
    [SerializeField] private float DashTime = 1.0f;

    public Transform AttackPoint;
    [SerializeField] private float AttackRange;
    public int BaseDamage;
    public LayerMask EnemyLayers;

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
        CheckHit();
    }
    void MoveTracker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SpecialMoveChecks
            if (Input.GetAxisRaw(Axis.horizontalaxis) > 0 && (Input.GetAxisRaw(Axis.verticalaxis) < 0)){
                PlayerAnim.Sweep();
                return;}//Sweep
            if (Input.GetAxisRaw(Axis.horizontalaxis) > 0){
                PlayerAnim.Hold();
                return;
            }//Air-Hold
            if (Input.GetAxisRaw(Axis.verticalaxis) < 0 && (!Grounded))
            {PlayerAnim.Bounce();
                return;
            }//Bounce

            if (Grounded)
            {
                if (CurrentComboState == BasicComboState.None)
                {
                    CurrentComboState = BasicComboState.Basic1;
                }

                //MidCombo = true;
                CurrentComboTimer = DefaultComboTimer;
                activateComboTimerToReset = true;

                if (CurrentComboState == BasicComboState.Basic1)
                {
                    CurrentComboState++;
                    PlayerAnim.Basic1();
                    Debug.Log("PlayingBasic1");
                }
                else if (CurrentComboState == BasicComboState.Basic2)
                {
                    CurrentComboState++;
                    PlayerAnim.Basic2();
                    Debug.Log("PlayingBasic2");
                }
                else if (CurrentComboState == BasicComboState.Basic3)
                {
                    CurrentComboState++;
                    PlayerAnim.Basic3();
                    Debug.Log("PlayingBasic3");
                }
                else if (CurrentComboState == BasicComboState.Basic4)
                {
                    CurrentComboState = BasicComboState.None;
                    //MidCombo = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {   // check if dodge sequence completed
            if (CurrentDodgeState >= DodgeComboState.Dodge2){
                return;
            }
            if (Input.GetAxis(Axis.horizontalaxis) > 0 && (Input.GetAxis(Axis.horizontalaxis) < 0)){
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

    void CheckHit()
    {
        Collider[] HitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange);

        foreach(Collider enemy in HitEnemies)
        {
            enemy.GetComponent<EnemyStats>().Hit(BaseDamage);
            Debug.Log("We hit " + enemy.name);
        }
    }
    void ResetComboState(){
        if (activateComboTimerToReset)
        {
            CurrentComboTimer -= Time.deltaTime;
            if (CurrentComboTimer <= 0)
            {
                Debug.Log("too slow fool");
                //MidCombo = false; 
                //CurrentComboState = BasicComboState.None;
                activateComboTimerToReset = false;
                //CurrentComboTimer = DefaultComboTimer;
            }
        }
        if (activateDodgeTimerToReset)
        {
            CurrentDodgeTimer -= Time.deltaTime;
            if (CurrentDodgeTimer <= 0f)
            {
                Debug.Log("Timeout");
                //CurrentDodgeState = DodgeComboState.None;
                activateDodgeTimerToReset = false;
                //CurrentDodgeTimer = DefaultDodgeTimer;
            }
        }
    }
}//class

