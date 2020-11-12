using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IStates
{
    None,
    Blocking,
    Dodging,
    Sliding,
    FrameArmour
}
public class PlayerStats : MonoBehaviour
{
    
    [SerializeField] private int StartingHealth = 100;
    public int CurrentHealth;
    private int StartingPower = 0;
    public int CurrentPower;

    //Animation Script
    private PlayerAnim PlayerAnim;
    private Animator anim;

    private Rigidbody rb;

    //GravityVars
    private float ElapsedTime;
    private float GravLerpTime = 3;
    private float CurrentGrav;
    private float LowGrav = -0.25f;
    private float MaxGrav = -1f;


    private bool Blocking;
    private bool Dodging;
    private bool Sturdy;
    private float SturdyPercent;
    private bool Alive;

    private float ParryTimer;
    private float ParryTime;
    private bool CanParry = false;


    private IStates CurrentIState;

    void Start()
    {
        PlayerAnim = GetComponent<PlayerAnim>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        CurrentIState = IStates.None;
        CurrentHealth = StartingHealth;
        CurrentPower = StartingPower;
        CurrentGrav = MaxGrav;
        ParryTime = 0.5f;
    }

    void Update()
    {
        Grav();
        //Lerps back to standard value for gravity while the Player is not being hit
        CheckIState();
        //Checks whether animations that would make the player invulnerable are playing
        CheckParry();
        //While
    }

    void CheckParry()
    {
        while (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Blocking"))
        {
            ParryTimer += Time.deltaTime;
        }
        if (ParryTimer <= ParryTime)
        {
        CanParry = true;
        }
    }

    private void CheckIState()
    {
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
    public void Hit(int damage)
    {
        if (CurrentIState != IStates.Blocking && CurrentIState != IStates.Dodging)
        {
            ElapsedTime = 0;
            CurrentGrav = LowGrav;

            PlayerAnim.Hit();
            CurrentHealth -= damage;
            if (CurrentHealth > 0)
            {
                rb.transform.position += Vector3.up * 200 * 1.5f * Time.deltaTime;
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
}
