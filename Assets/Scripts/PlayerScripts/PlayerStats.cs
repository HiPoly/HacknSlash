using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    [SerializeField] private int StartingHealth = 100;
    public int CurrentHealth;
    private int StartingPower = 0;
    public int CurrentPower;

    private PlayerAnim PlayerAnim;
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

    void Start()
    {
        PlayerAnim = GetComponent<PlayerAnim>();
        rb = GetComponent<Rigidbody>();
        CurrentHealth = StartingHealth;
        CurrentPower = StartingPower;
        CurrentGrav = MaxGrav;
    }

    void Update()
    {
        Grav();
        //Lerps back to standard value for gravity while the Player is not being hit
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
        if (!Blocking && !Dodging)
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
