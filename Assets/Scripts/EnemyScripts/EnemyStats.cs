using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //Misc
    private bool Grounded;
    private bool Alive = true;
    //Fetched Components
    private EnemyAnim EnemyAnim;
    private Animator anim;
    private Rigidbody rb;
    private PlayerStats PlayerStats;
    //Health, Damage and Force
    public int StartingHealth = 100;
    public int CurrentHealth;
    [SerializeField] 
    private int StartingDamage;
    public int CurrentDamage = 20;
    [SerializeField]
    private int StartingForce;
    public int CurrentForce;
    public int ForcePerHit;
    //GravityVars
    private bool GravEnabled = true;
    private float ElapsedTime;
    private float GravLerpTime = 3;
    private float CurrentGrav;
    private float LowGrav = -0.25f;
    private float MaxGrav = -5f;
    //IStateVars
    private bool Blocking;
    private bool HitPerAttack;
    //AttackWindowCheck
    public bool BeenHit;
    //ColourHit
    Renderer rend;
    public Material defaultMat;
    public Material HitMat;
    public float colourTime = 0.1f;


    void Start(){
        rend = GetComponent<Renderer>();
        Alive = true;
        EnemyAnim = GetComponent<EnemyAnim>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        PlayerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        CurrentHealth = StartingHealth;
        CurrentDamage = StartingDamage;
        CurrentGrav = MaxGrav;
    }
    void Update(){
        Grav();
        //Lerps back to standard value for gravity while the Enemy is not being hit
        RemoveForceOnGround();
        //Remove continuous downward force when supported by the ground
        Goodnight();
        //Checks if the player is dead and execute an end of life routine
    }
    public void Hit(int damage)
    {
        if (!BeenHit)
        {
            BeenHit = true;
            ElapsedTime = 0;
            CurrentGrav = LowGrav;
            EnemyAnim.Hit();
            CurrentHealth -= damage;
            if (CurrentHealth > 0){
                //Direct Translation Version
                //rb.transform.position += Vector3.up * PlayerStats.CurrentForce * Time.deltaTime;
                rb.AddForce(Vector3.up * PlayerStats.CurrentForce);
                GameObject.Find("TimeLord").GetComponent<HitStop>().Stop(0.05f);
            }
            if (CurrentHealth <= 0){
                Debug.Log("this thing has died");
                EnemyAnim.Death();
                GetComponent<EnemyActions>().Die();
                Alive = false;
                if (Grounded){
                    rb.velocity = Vector3.zero;
                }
            }
        }
    }
    IEnumerator HitColour()
    {
        rend.sharedMaterial = HitMat;
        yield return new WaitForSecondsRealtime(colourTime);
        rend.sharedMaterial = defaultMat;
    }

    void Grav(){
        if (GravEnabled){
            if (ElapsedTime < GravLerpTime && transform.position.y != 0){
                CurrentGrav = Mathf.Lerp(CurrentGrav, MaxGrav, ElapsedTime / GravLerpTime);
                ElapsedTime += Time.deltaTime;
            }
            if (!Grounded){
                rb.AddForce(0, CurrentGrav, 0, ForceMode.Force);
            }
        }
        if (rb.velocity.y <= -2)
        {
            EnemyAnim.Falling();
        }
    }
    private void RemoveForceOnGround(){
        if (transform.position.y == 0){
            rb.velocity = Vector3.zero;
        }
    }
    private void Goodnight(){
        if (!Alive){
            if (transform.position.y <= 0.05){
                rb.velocity = Vector3.zero;
                GravEnabled = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                this.enabled = false;
            }
        }
    }
}
