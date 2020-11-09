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
public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private PlayerAnim PlayerAnim;
    private Rigidbody rb;
    private bool Grounded;
    
    //Timers
    private bool activateComboTimerToReset;
    private bool activateDodgeTimerToReset;
    private float DefaultComboTimer = 1f;
    private float CurrentComboTimer;
    private float DefaultDodgeTimer = 1f;
    private float CurrentDodgeTimer;
    //Enums
    private BasicComboState CurrentComboState;
    private DodgeComboState CurrentDodgeState;

    [SerializeField] private float DashSpeed;
    [SerializeField] private float StartDashTime = 1.0f;
    //[SerializeField] private float DashTime = 1.0f;

    public Transform AttackPoint;
    [SerializeField] private float AttackRange = 10;
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
        //DashTime = StartDashTime;
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
            if (Input.GetKey(KeyCode.S) && (Input.GetAxisRaw(Axis.verticalaxis) < 0)){
                PlayerAnim.Sweep();
                Debug.Log("I am performing SWEEP");
                return; }//Sweep
            else if (Input.GetKey(KeyCode.W)){
                PlayerAnim.Hold();
                Debug.Log("I am performing HOLD");
                return; }//Air-Hold
            else if (Input.GetKey(KeyCode.S) && (!Grounded)){
                PlayerAnim.Bounce();
                Debug.Log("I am performing BOUNCE");
                return; }//Bounce

            //Charge attack goes here?

            if (Grounded)
            {
                    CurrentComboState++;
                    activateComboTimerToReset = true;

                if (CurrentComboState == BasicComboState.Basic1)
                {
                    PlayerAnim.Basic1();
                    Debug.Log("PlayingBasic1");
                    CurrentComboTimer = DefaultComboTimer; //Time of Animation
                }
                else if (CurrentComboState == BasicComboState.Basic2)
                {
                    PlayerAnim.Basic2();
                    Debug.Log("PlayingBasic2");
                    CurrentComboTimer = DefaultComboTimer; //Time of Animation
                }
                else if (CurrentComboState == BasicComboState.Basic3)
                {
                    PlayerAnim.Basic3();
                    Debug.Log("PlayingBasic3");
                    CurrentComboTimer = DefaultComboTimer; //Time of Animation
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {   // check if dodge sequence completed
            if (CurrentDodgeState >= DodgeComboState.Dodge2){
                return;
            }
            if (Input.GetKey(KeyCode.S) && (Input.GetAxis(Axis.horizontalaxis) != 0)){
                PlayerAnim.Slide();
                Debug.Log("I should be sliding");
                return;
            }
            CurrentDodgeState++;
            activateDodgeTimerToReset = true;
            CurrentDodgeTimer = DefaultDodgeTimer;

            if (CurrentDodgeState == DodgeComboState.Dodge1)
            {
                PlayerAnim.Dodge1();
                CurrentDodgeTimer = DefaultDodgeTimer;
                Debug.Log("PlayingDodge1");
                //if (Input.GetAxisRaw(Axis.horizontalaxis) < 0)
                {
                    //rb.velocity = Vector3.left * DashSpeed * Time.deltaTime;
                }
                //else if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
                {
                    //rb.velocity = Vector3.right * DashSpeed * Time.deltaTime;
                }
            }
            if (CurrentDodgeState == DodgeComboState.Dodge2)
            {
                PlayerAnim.Dodge2();
                CurrentDodgeTimer = DefaultDodgeTimer;
                Debug.Log("PlayingDodge2");
                //if (Input.GetAxisRaw(Axis.horizontalaxis) < 0)
                {
                    //rb.transform.position += Vector3.left * DashSpeed * 1.5f * Time.deltaTime;
                }
                //else if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
                {
                    //rb.transform.position = Vector3.right * DashSpeed * 1.5f * Time.deltaTime;
                }
            }
        }
    }
    void CheckHit()
    {
        Collider[] HitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange);

        foreach(Collider enemy in HitEnemies)
        {
                Debug.Log("We hit " + enemy.name);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
    void ResetComboState(){
        if (activateComboTimerToReset)
        {
            CurrentComboTimer -= Time.deltaTime;
            if (CurrentComboTimer <= 0)
            {
                Debug.Log("too slow fool");
                CurrentComboState = BasicComboState.None;
                activateComboTimerToReset = false;
                PlayerAnim.ComboEnd();
            }
        }
        if (activateDodgeTimerToReset)
        {
            CurrentDodgeTimer -= Time.deltaTime;
            if (CurrentDodgeTimer <= 0f)
            {
                Debug.Log("Timeout");
                CurrentDodgeState = DodgeComboState.None;
                activateDodgeTimerToReset = false;
                PlayerAnim.ComboEnd();
            }
        }
    }
}//class

