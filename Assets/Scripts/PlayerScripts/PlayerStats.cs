using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int PlayerHealth;
    [SerializeField] private int StartingHealth = 100;
    public float PowerGauge;
    private PlayerAnim PlayerAnim;

    void Start()
    {
        PlayerHealth = StartingHealth;
    }
    public void Hit(int damage)
    {
        PlayerHealth -= damage;
        PlayerAnim.Hit();
        if (PlayerHealth <= 0)
        {
            Debug.Log("The player has died");
            PlayerAnim.Death();
        }
    }
}
