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
    public bool Acting;
    //Fetched Components
        //Scripts
        [SerializeField] private PlayerAnim PlayerAnim = null;
        [SerializeField] private PlayerStats PlayerStats = null;
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
    [SerializeField]
    private float ActionTimer;
    //Enums
    private BasicComboState CurrentComboState;
    private DodgeComboState CurrentDodgeState;
    //DashVars
    [SerializeField] private float DodgeSpeed = 0;
    //Control hit windows and strikes through animation events
    private bool AttackWindow = false;
    //Tracking Charge Attack Requirements
    [SerializeField] private float ChargeTracker = 0;
    [SerializeField] private float ChargeReady = 0.8f;
    [SerializeField] private Transform ChargeWaveSpawn = null;
    public GameObject ChargeWavePrefab;
    GameObject ChargeWaveInstance;
    //Attacking Hitbox and Size
    public Transform AttackPoint;
    private float defaultAttackSize = 0.2f;
    [SerializeField]
    private Transform AttackPointLeg = null;
    [SerializeField]
    private Transform AttackPointBlade = null;
    [SerializeField]
    private Transform AttackPointAoE = null;

    [SerializeField] private float AttackRange = 0.1f;
    public LayerMask EnemyLayers;
    //Max Clamped height
    [SerializeField] private float MaxHeight = 100f;
    private List<EnemyStats> hitList = new List<EnemyStats>();

    private int forceTick;

    private void Start(){
        CurrentComboTimer = DefaultComboTimer;
        CurrentDodgeTimer = DefaultDodgeTimer;
        CurrentComboState = BasicComboState.None;
        CurrentDodgeState = DodgeComboState.None;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        PlayerStats = GetComponent<PlayerStats>();
        //EnemyStats = GameObject.Find("Enemy").GetComponent<EnemyStats>();
        Grounded = true;
        //DashTime = StartDashTime;
    }
    private void Update()
    {
        CheckAttack();
        //Check inputs for special attacks and combos
        CheckHit();
        //Check if attack point is currently hitting an enemy
        CheckDodge();
        //Check inputs for dodge related actions
        ResetComboState();
        //Check For timed out dodge and attack combos
        CheckGrounded();
        //Check if the player's rigidbody is at y: 0
        ClampY();
        //clamps the y position to >= 0
        RemoveForceOnGround();
        //removes excess downward force when touching the ground
    }
    void CheckAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SpecialMoveCheck
            if (Input.GetKey(KeyCode.S) && (Input.GetAxisRaw(Axis.verticalaxis) < 0)){
                PlayerAnim.ChangeState("Sweep", 0, 10);
                Debug.Log("I am performing SWEEP");
                AttackPoint = AttackPointBlade;
                AttackRange = defaultAttackSize;
                ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                return; }//Sweep
            else if (Input.GetKey(KeyCode.W)){
                PlayerAnim.ChangeState("Hold", 0, 10); ;
                Debug.Log("I am performing HOLD");
                AttackPoint = AttackPointLeg;
                AttackRange = defaultAttackSize + 0.1f;
                ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                return; }//Air-Hold
            else if (Input.GetKey(KeyCode.S) && (!Grounded)){
                PlayerAnim.ChangeState("Bounce", 0, 10); ;
                Debug.Log("I am performing BOUNCE");
                AttackPoint = AttackPointAoE;
                AttackRange = 0.65f;
                ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                return; }//Bounce
            if (Grounded){
                if (AttackWindow == true){
                    AttackWindow = false;
                }
                CurrentComboState++;
                activateComboTimerToReset = true;
                AttackPoint = AttackPointBlade;
                AttackRange = 0.1f;
                if (CurrentComboState == BasicComboState.Basic1){
                    PlayerAnim.ChangeState("Basic1", 0.1f, 10);
                    Debug.Log("PlayingBasic1");
                    CurrentComboTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                    ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                }
                else if (CurrentComboState == BasicComboState.Basic2){
                    PlayerAnim.ChangeState("Basic2", 0.1f, 10);
                    Debug.Log("PlayingBasic2");
                    CurrentComboTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                    ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                }
                else if (CurrentComboState == BasicComboState.Basic3){
                    PlayerAnim.ChangeState("Basic3", 0.1f, 10);
                    Debug.Log("PlayingBasic3");
                    CurrentComboTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                    ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                }
            }
        }
    //ChargeAttackCheck
    if (Input.GetMouseButton(0)) {
            ChargeTracker += Time.deltaTime;
        }
    if (ChargeTracker >= ChargeReady) {
            PlayerAnim.ChangeState("ChargeHold", 0.1f, 10);
            Debug.Log("I am playing chargehold");
        }
    if (Input.GetMouseButtonUp(0) && ChargeTracker >= ChargeReady){
            PlayerAnim.ChangeState("ChargeRelease", 0.1f, 10);
            Instantiate(ChargeWavePrefab, ChargeWaveSpawn.position, ChargeWaveSpawn.rotation);
            ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
            ChargeTracker = 0;
            Debug.Log("I am playing chargerelease");
        }
    else if (Input.GetMouseButtonUp(0) && ChargeTracker < ChargeReady){
            ChargeTracker = 0;
        }
    }
    //Animation Events that open the hit window and allow the player to deal damage once per strike
    void OpenHit(){
        AttackWindow = true;
        forceTick = 1;
    }
    void CloseHit(){
        AttackWindow = false;
        //Attacks = 0;
        foreach (EnemyStats e in hitList)
        {
            e.BeenHit = false;
        }
        hitList.Clear();
        forceTick = 0;
    }
    void CheckHit()
    {
        Collider[] HitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange);

        foreach(Collider enemy in HitEnemies)
        {
            if (AttackWindow == true)
            {
                if (enemy.GetComponent<EnemyStats>() != null)
                {
                    EnemyStats e = enemy.GetComponent<EnemyStats>();
                    e.Hit(PlayerStats.CurrentDamage);
                    hitList.Add(e);

                    if (forceTick == 1){
                    PlayerStats.CurrentForce += PlayerStats.ForcePerHit;
                        forceTick = 0; }
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
            }
        }
        
        if (ActionTimer > 0)
        {
            Acting = true;
            ActionTimer -= Time.deltaTime;
        }
        else
        {
            Acting = false;
        }
    }
    void ClampY(){
        Vector3 ClampedPosition = transform.position;
        ClampedPosition.y = Mathf.Clamp(ClampedPosition.y, 0f, MaxHeight);
        transform.position = ClampedPosition;
    }
    private void RemoveForceOnGround()
    {
        if (transform.position.y <= 0.05 && rb.velocity.y < 0)
        {
            rb.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    void CheckGrounded(){
        if (rb.transform.position.y == 0){
            Grounded = true;
        }
        else{
            Grounded = false;
        }
    }
    //Dodge and Slide Abilities
    void CheckDodge()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   // check if dodge sequence completed
            if (CurrentDodgeState >= DodgeComboState.Dodge2){
                return;
            }
            //Check for Special Inputs
            else if (Input.GetKey(KeyCode.S) && (Input.GetAxis(Axis.horizontalaxis) != 0)){
                PlayerAnim.ChangeState("Slide", 0.1f, 10);
                ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                Debug.Log("I should be sliding");
                return;
            }
            else if (Input.GetKey(KeyCode.W)){
                PlayerAnim.ChangeState("Jump", 0, 10);
                //rb.velocity = new Vector3
                //(rb.velocity.x,
                // Input.GetAxisRaw(Axis.horizontalaxis) * (100f * Time.deltaTime),
                // rb.velocity.z
                //);
                Debug.Log("I am Jumping");
                return;
            }
            CurrentDodgeState++;
            activateDodgeTimerToReset = true;
            CurrentDodgeTimer = DefaultDodgeTimer;

            if (CurrentDodgeState == DodgeComboState.Dodge1)
            {
                PlayerAnim.ChangeState("Dodge1", 0.1f, 10);
                CurrentDodgeTimer = DefaultDodgeTimer;
                ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                Debug.Log("PlayingDodge1");
                if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge1"))
                {
                    if (transform.rotation.y > 0){
                    rb.AddForce(Vector3.right * DodgeSpeed);
                    }
                    else{
                    rb.AddForce(Vector3.left * DodgeSpeed);
                    }
                }
            }
            if (CurrentDodgeState == DodgeComboState.Dodge2)
            {
                PlayerAnim.ChangeState("Dodge2", 0.1f, 10);
                CurrentDodgeTimer = DefaultDodgeTimer;
                ActionTimer = anim.GetCurrentAnimatorStateInfo(0).length;
                Debug.Log("PlayingDodge2");
                if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge2"))
                {
                    if (transform.rotation.y > 0){
                        rb.AddForce(Vector3.right * DodgeSpeed);
                    }
                    else{
                        rb.AddForce(Vector3.left * DodgeSpeed);
                    }
                }
            }
        }
    }
}//class