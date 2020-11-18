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

public class PlayerActions : MonoBehaviour
{   //misc
    private bool Grounded;
    //Fetched Components
        //Scripts
        [SerializeField] private PlayerAnim PlayerAnim;
        [SerializeField] private PlayerStats PlayerStats;
    private EnemyStats EnemyStats;
    private Animator anim;
    private Rigidbody rb;
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
    //DashVars
    [SerializeField] private float DodgeSpeed;
    //Control hit windows and strikes through animation events
    private bool AttackWindow = false;
    private int Attacks = 0;
    //Tracking Charge Attack Requirements
    [SerializeField] private int ChargeTracker;
    [SerializeField] private int ChargeReady;
    [SerializeField] private Transform ChargeWaveSpawn;
    public GameObject ChargeWavePrefab;
    GameObject ChargeWaveInstance;
    //Attacking Hitbox and Size
    public Transform AttackPoint;
    [SerializeField] private float AttackRange = 0.1f;
    public LayerMask EnemyLayers;
    //Max Clamped height
    [SerializeField] private float MaxHeight = 100f;

    private List<EnemyStats> hitList = new List<EnemyStats>(); 

    private void Start(){
        CurrentComboTimer = DefaultComboTimer;
        CurrentDodgeTimer = DefaultDodgeTimer;
        CurrentComboState = BasicComboState.None;
        CurrentDodgeState = DodgeComboState.None;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        PlayerStats = GetComponent<PlayerStats>();
        EnemyStats = GameObject.Find("Enemy").GetComponent<EnemyStats>();
        Grounded = true;
        //DashTime = StartDashTime;
    }
    private void Update()
    {
        CheckHit();
        //Check if attack point is currently hitting an enemy
        CheckAttack();
        //Check inputs for special attacks and combos
        CheckDodge();
        //Check inputs for dodge related actions
        ResetComboState();
        //Check For timed out dodge and attack combos
        CheckBlock();
        //Check inputs for blocking actions
        CheckGrounded();
        //Check if the player's rigidbody is at y: 0
        ClampY();
    }
    void CheckAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SpecialMoveCheck
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
            if (Grounded){
                    CurrentComboState++;
                    activateComboTimerToReset = true;
                //BasicComboCheck
                if (CurrentComboState == BasicComboState.Basic1){
                    PlayerAnim.Basic1();
                    Debug.Log("PlayingBasic1");
                    CurrentComboTimer = DefaultComboTimer; //Change This to Time of Animation
                }
                else if (CurrentComboState == BasicComboState.Basic2){
                    PlayerAnim.Basic2();
                    Debug.Log("PlayingBasic2");
                    CurrentComboTimer = DefaultComboTimer; //Change This to Time of Animation
                }
                else if (CurrentComboState == BasicComboState.Basic3){
                    PlayerAnim.Basic3();
                    Debug.Log("PlayingBasic3");
                    CurrentComboTimer = DefaultComboTimer; //Change This to Time of Animation
                }
            }
        }
    //ChargeAttackCheck
    if (Input.GetMouseButton(0)) {
            ChargeTracker = ChargeTracker + 1;
        }
    if (ChargeTracker >= ChargeReady) {
            PlayerAnim.ChargeHold();
        }
    if (Input.GetMouseButtonUp(0) && ChargeTracker >= ChargeReady){
            ChargeTracker = 0;
            PlayerAnim.ChargeRelease();
            Instantiate(ChargeWavePrefab, ChargeWaveSpawn.position, ChargeWaveSpawn.rotation);
        }
    else if (Input.GetMouseButtonUp(0) && ChargeTracker < ChargeReady){
            ChargeTracker = 0;
        }
    }
    //Dodge and Slide Abilities
    void CheckDodge()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   // check if dodge sequence completed
            if (CurrentDodgeState >= DodgeComboState.Dodge2)
            {
                return;
            }
            //Check for Special Inputs
            else if (Input.GetKey(KeyCode.S) && (Input.GetAxis(Axis.horizontalaxis) != 0))
            {
                PlayerAnim.Slide();
                Debug.Log("I should be sliding");
                return;
            }
            else if (Input.GetKey(KeyCode.W)){
                PlayerAnim.Jump();
                Debug.Log("I am Jumping");
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
                if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge1"))
                {
                    rb.AddForce(Vector3.right * Time.deltaTime * DodgeSpeed);
                }
            }
            if (CurrentDodgeState == DodgeComboState.Dodge2)
            {
                PlayerAnim.Dodge2();
                CurrentDodgeTimer = DefaultDodgeTimer;
                Debug.Log("PlayingDodge2");
                if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge2"))
                {
                    //addforce
                }
            }
        }
    }
    void CheckBlock()
    {   //Set Animations
        if (Input.GetMouseButton(1)){
            PlayerAnim.Block(true);
        }
        else{
            PlayerAnim.Block(false);
        }
    }
    //Animation Events that open the hit window and allow the player to deal damage once per strike
    void OpenHit(){
        AttackWindow = true;
        //Attacks = 1;
    }
    void CloseHit(){
        AttackWindow = false;
        //Attacks = 0;
        foreach (EnemyStats e in hitList)
        {
            e.BeenHit = false;

        }
        hitList.Clear();
    }
    void CheckHit()
    {
        Collider[] HitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange);

        foreach(Collider enemy in HitEnemies)
        {
            if (AttackWindow == true)
            {
                if (enemy.GetComponent<EnemyStats>() != null){
                    Debug.Log("We hit " + enemy.name);
                    EnemyStats e = enemy.GetComponent<EnemyStats>();

                    e.Hit(PlayerStats.CurrentDamage);
                    hitList.Add(e);
                    PlayerStats.CurrentForce += PlayerStats.ForcePerHit;
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {   //display attack range in gizmos
        if (AttackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
    void ResetComboState(){
        if (activateComboTimerToReset)
        {
            CurrentComboTimer -= Time.deltaTime;
            if (CurrentComboTimer <= 0)
            {
                Debug.Log("BasicComboTimeout");
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
                Debug.Log("DodgeComboTimeout");
                CurrentDodgeState = DodgeComboState.None;
                activateDodgeTimerToReset = false;
                PlayerAnim.ComboEnd();
            }
        }
    }
    void ClampY(){
        Vector3 ClampedPosition = transform.position;
        ClampedPosition.y = Mathf.Clamp(ClampedPosition.y, 0f, MaxHeight);
        transform.position = ClampedPosition;
    }
    void CheckGrounded(){
        if (rb.transform.position.y == 0){
            Grounded = true;
        }
        else{
            Grounded = false;
        }
    }
}//class