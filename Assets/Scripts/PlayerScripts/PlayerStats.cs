using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IStates{
    None,
    Blocking,
    Dodging,
    Sliding,
    FrameArmour
}
public class PlayerStats : MonoBehaviour
{
    //Enums
    private IStates CurrentIState;
    //Fetched Components
    private PlayerAnim PlayerAnim;
    private EnemyAnim EnemyAnim;
    private Animator anim;
    private Rigidbody rb;
    private EnemyStats EnemyStats = null;
    //Health & Damage
    public int StartingHealth = 200;
    public int CurrentHealth;
    [SerializeField]
    private int StartingDamage = 20 ;
    public int CurrentDamage = 20;
    //ForceVars
    private float StartingForce = 0;
    [SerializeField]
    private float ForceCap;
    public float CurrentForce;
    public float ForcePerHit = 20;
    private float ForceTimer;
    [SerializeField]
    private float ForceDuration = 10;
    //GravityVars
    private bool GravEnabled;
    private float ElapsedTime;
    private float GravLerpTime = 3;
    private float CurrentGrav;
    private float LowGrav = -0.25f;
    private float MaxGrav = -2f;
    //Code later with Istates, use dodge for now
    //IStateVars
    public bool Blocking = false;
    private bool Dodging = false;
    //private bool Sturdy = false;
    //private float SturdyPercent = 0f;
    private bool Alive = true;
    //Parry vars
    [SerializeField] private float ParryTimer;
    [SerializeField] private float ParryTime = 1.0f;
    private bool CanParry = false;
    public bool BeenHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start(){
        PlayerAnim = GetComponent<PlayerAnim>();
        //EnemyAnim = GameObject.Find("Enemy").GetComponent<EnemyAnim>();
        //EnemyStats = GameObject.Find("Enemy").GetComponent<EnemyStats>();
        anim = GetComponent<Animator>();
        CurrentIState = IStates.None;
        CurrentHealth = StartingHealth;
        CurrentDamage = StartingDamage;
        CurrentForce = StartingForce;
        CurrentGrav = MaxGrav;
    }
    void Update(){
        CheckIState();
        //Checks whether animations that would make the player invulnerable are playing
        Grav();
        //Lerps back to standard value for gravity while the Player is not being hit
        LerpForce();
        //Force Decays at a constant rate while not attacking
        CheckParry();
        //Add a timer once blocking starts to see if the player can still parry and whether they are blocking
        CheckBlock();
        //Control Blocking Animation and set blocking bool
    }
    public void Hit(int damage){
        if (Dodging){
            return;
        }
        if (Blocking){
            if (CanParry == true){
                PlayerAnim.ChangeState("Parry");
                CurrentForce += ForcePerHit;
            }
            return;
        }
        ElapsedTime = 0;
        CurrentGrav = LowGrav;
        PlayerAnim.ChangeState("Hit");
        CurrentHealth -= damage;
        if (CurrentHealth > 0){
            //Direct Translation Version
            //rb.transform.position += Vector3.up * PlayerStats.CurrentForce * Time.deltaTime;
            rb.AddForce(Vector3.up * EnemyStats.CurrentForce);
        }
        GameObject.Find("TimeLord").GetComponent<HitStop>().Stop(0.1f);
        if (CurrentHealth <= 0){
            Debug.Log("this thing has died");
            PlayerAnim.ChangeState("Death");
            Alive = false;
            GetComponent<Collider>().enabled = false;
        }
    }
    void CheckBlock()
    {   //Set Animations
        if (Input.GetMouseButton(1)){
            PlayerAnim.Block(Blocking = true);
            Blocking = true;
        }
        else{
            PlayerAnim.Block(Blocking = false);
            Blocking = false;
        }
    }

    void CheckParry(){
        if (GetComponent<PlayerAnim>().currentState == "Block"){
            Blocking = true;
            ParryTimer += Time.deltaTime;
        }
        else{
            ParryTimer = 0;
            Blocking = false;
        }
        if (ParryTimer <= ParryTime){
            CanParry = true;
        }
    }
    private void CheckIState(){
        //Set IStates while animations are playing
        if (GetComponent<PlayerAnim>().currentState == "Block"){
            CurrentIState = IStates.Blocking;
        }
        else if (GetComponent<PlayerAnim>().currentState == "Dodge1")
        {
            CurrentIState = IStates.Dodging;
        }
        else if (GetComponent<PlayerAnim>().currentState == "Dodge2"){
            CurrentIState = IStates.Dodging;
        }
        else if (GetComponent<PlayerAnim>().currentState == "Slide")
        {
            CurrentIState = IStates.Dodging;
        }
        else{
            CurrentIState = IStates.None;
        }
    }

    void IStateEffects()
    {
        if (CurrentIState == IStates.Blocking){

        }
    }
    void Grav(){
        if (GravEnabled){
            if (ElapsedTime < GravLerpTime && transform.position.y != 0){
                CurrentGrav = Mathf.Lerp(CurrentGrav, MaxGrav, ElapsedTime / GravLerpTime);
                ElapsedTime += Time.deltaTime;
            }
            if (transform.position.y > 0.05f){
                rb.AddForce(0, CurrentGrav, 0, ForceMode.Force);
            }
        }
    }
    void LerpForce()
    {
        ForceTimer += Time.deltaTime / ForceDuration;
        CurrentForce = Mathf.Lerp(CurrentForce, 0, ForceTimer);
    }
    private void Goodnight()
    {
        if (!Alive)
        {
            if (transform.position.y == 0)
            {
                rb.velocity = Vector3.zero;
                GravEnabled = false;
                this.enabled = false;
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //Print the unit's current state above it in the editor
        //CurrentIState
    }
#endif
}