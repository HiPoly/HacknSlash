using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int CurrentHealth;
    [SerializeField] private int StartingHealth = 100;
    private EnemyAnim EnemyAnim;

    void Start()
    {
        CurrentHealth = StartingHealth;
    }
    public void Hit(int damage)
    {
        EnemyAnim.Hit();
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Debug.Log("this thing has died");
            EnemyAnim.Death();
        }
    }
}
