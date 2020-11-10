using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int CurrentHealth;
    [SerializeField] private int StartingHealth = 100;
    private EnemyAnim EnemyAnim;
    private Rigidbody rb;
    [SerializeField] private float HitForce = 100;
    private bool Grounded;

    void Start()
    {
        EnemyAnim = GetComponent<EnemyAnim>();
        rb = GetComponent<Rigidbody>();
        CurrentHealth = StartingHealth;
    }
    public void Hit(int damage)
    {
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
            GetComponent<EnemyMovement>().Die();
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
