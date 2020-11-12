using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int CurrentHealth;
    [SerializeField] private int StartingHealth = 100;
    private EnemyAnim EnemyAnim;
    private Rigidbody rb;
    [SerializeField] private float HitForce = 200;
    private bool Grounded;
    private bool Dead = false;

    private float ElapsedTime;
    private float GravLerpTime = 3;
    private float CurrentGrav;
    private float LowGrav = -0.25f;
    private float MaxGrav = -1f;

    void Start()
    {
        EnemyAnim = GetComponent<EnemyAnim>();
        rb = GetComponent<Rigidbody>();
        CurrentHealth = StartingHealth;
        CurrentGrav = MaxGrav;
    }

    void Update()
    {
        Grav();
        //Lerps back to standard value for gravity while the Enemy is not being hit
        Goodnight();
        //Checks if the player is dead and execute an end of life routine 
        RemoveForceOnGround();
    }

    private void RemoveForceOnGround()
    {
        if (transform.position.y == 0)
        {
            rb.velocity = Vector3.zero;
        }

    }

    private void Goodnight()
    {
        if (Dead)
        {

        }
    }
    void Grav()
    {
        if (ElapsedTime < GravLerpTime && transform.position.y != 0)
        {
            CurrentGrav = Mathf.Lerp(CurrentGrav, MaxGrav, ElapsedTime / GravLerpTime);
            ElapsedTime += Time.deltaTime; 
        }
        if (!Grounded)
        {
            rb.AddForce(0, CurrentGrav, 0, ForceMode.Force);
        }
    }
    public void Hit(int damage)
    {
        ElapsedTime = 0;
        CurrentGrav = LowGrav;
        
        EnemyAnim.Hit();
        CurrentHealth -= damage;
        if (CurrentHealth > 0) 
        {
            rb.transform.position += Vector3.up * HitForce * 1.5f * Time.deltaTime;
        }
        if (CurrentHealth <= 0)
        {
            Debug.Log("this thing has died");
            EnemyAnim.Death();
            Dead = true;
            GetComponent<EnemyActions>().Die();
            GetComponent<Collider>().enabled = false;
        }
    }
}
