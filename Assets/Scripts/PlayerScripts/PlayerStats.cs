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

    void Start()
    {
        CurrentHealth = StartingHealth;
        CurrentPower = StartingPower;
    }

    public void Hit(int damage)
    {
        CurrentHealth -= damage;
        PlayerAnim.Hit();
        if (CurrentHealth <= 0)
        {
            Debug.Log("The player has died");
            PlayerAnim.Death();
        }
    }
}
