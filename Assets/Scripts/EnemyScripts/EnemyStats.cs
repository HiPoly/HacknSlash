using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int UnitHealth;
    [SerializeField] private int StartingHealth = 100;
    private EnemyAnim EnemyAnim;

    void Start()
    {
        UnitHealth = StartingHealth;
    }
    public void Hit(int damage)
    {
        EnemyAnim.Hit();
        UnitHealth -= damage;
        if (UnitHealth <= 0)
        {
            Debug.Log("this thing has died");
            EnemyAnim.Death();
        }
    }
}
