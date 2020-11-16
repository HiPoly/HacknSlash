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
    private Animator anim;
    private Rigidbody rb;
    private EnemyStats EnemyStats;
    //Health, Damage and Force
    public int StartingHealth = 200;
    public int CurrentHealth;
    [SerializeField]
    private int StartingDamage = 20 ;
    public int CurrentDamage = 20;
    [SerializeField]
    private int StartingForce;
    public int CurrentForce;
    private int CurrentForceFloat;
    public int ForcePerHit = 20;
    private float ForceTimer;
    private float ForceDuration = 3;
    //GravityVars
    private float ElapsedTime;
    private float GravLerpTime = 3;
    private float CurrentGrav;
    private float LowGrav = -0.25f;
    private float MaxGrav = -1f;
    //Code later with Istates, use dodge for now
    //IStateVars
    private bool Blocking;
    private bool Dodging;
    private bool Sturdy;
    private float SturdyPercent;
    private bool Alive;
    //Parry vars
    private float ParryTimer;
    [SerializeField] private float ParryTime;
    private bool CanParry = false;

    void Start()
    {
        PlayerAnim = GetComponent<PlayerAnim>();
        GameObject.Find("Enemy").GetComponent<EnemyStats>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        CurrentIState = IStates.None;
        CurrentHealth = StartingHealth;
        CurrentDamage = StartingDamage;
        CurrentForce = StartingForce;
        CurrentGrav = MaxGrav;
        ParryTime = 0.5f;
    }
    void Update(){
        CheckIState();
        //Checks whether animations that would make the player invulnerable are playing
        CheckParry();
        //Add a timer once blocking starts to see if the player can still parry
        Grav();
        //Lerps back to standard value for gravity while the Player is not being hit
        RemoveForceOnGround();
        //Remove continuous downward force when supported by the ground
        LerpForce();
    }
    public void Hit(int damage){
        if (CurrentIState != IStates.Blocking && CurrentIState != IStates.Dodging){
            if (CurrentIState == IStates.Blocking){
                if (CanParry == true){
                    PlayerAnim.Parry();
                    CurrentForce += ForcePerHit;
                }
                return;
            }
            ElapsedTime = 0;
            CurrentGrav = LowGrav;
            PlayerAnim.Hit();
            CurrentHealth -= damage;
            if (CurrentHealth > 0)
            {
                rb.transform.position += Vector3.up * EnemyStats.CurrentForce * Time.deltaTime;
            }
            if (CurrentHealth <= 0)
            {
                Debug.Log("this thing has died");
                PlayerAnim.Death();
                Alive = false;
                GetComponent<Collider>().enabled = false;
            }
        }
    }
    void CheckParry(){
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Blocking")){
            ParryTimer += Time.deltaTime;
        }
        else{
            ParryTimer = 0;
        }
        if (ParryTimer <= ParryTime){
        CanParry = true;
        }
    }
    private void CheckIState(){
        //Set IStates while animations are playing
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Blocking")){
            CurrentIState = IStates.Blocking;
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge1")){
            CurrentIState = IStates.Dodging;
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge2")){
            CurrentIState = IStates.Dodging;
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Slide")){
            CurrentIState = IStates.Dodging;
        }
    }
    void Grav()
    {
        if (ElapsedTime < GravLerpTime && transform.position.y != 0)
        {
            CurrentGrav = Mathf.Lerp(CurrentGrav, MaxGrav, ElapsedTime / GravLerpTime);
            ElapsedTime += Time.deltaTime;
        }
        if (transform.position.y != 0)
        {
            rb.AddForce(0, CurrentGrav, 0, ForceMode.Force);
        }
    }
    private void RemoveForceOnGround()
    {
        if (transform.position.y == 0)
        {
            rb.velocity = Vector3.zero;
        }
    }
    void LerpForce()
    {
        ForceTimer += Time.deltaTime / ForceDuration;
        CurrentForceFloat = (int)Mathf.Lerp(CurrentForce, 0, ForceTimer);
        CurrentForce = (int)CurrentForceFloat;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //Print the unit's current state above it in the editor
        //CurrentIState
    }
#endif
}