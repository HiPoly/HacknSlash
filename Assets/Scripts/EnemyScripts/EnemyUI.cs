using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public GameObject Enemy;
    public Slider HealthSlider;
    [SerializeField]
    private GameObject BarContainer;
    private float CurrentHealth;
    private float MaxHealth;

    void Start()
    {
        GetComponent<EnemyStats>();
        gameObject.SetActive(true);
    }
    void Update()
    {
        UpdateHealth();
        VisOnHit();
        DisableOnDeath();
    }
    public void UpdateHealth()
    {
        HealthSlider.minValue = 0;
        HealthSlider.value = Enemy.GetComponent<EnemyStats>().CurrentHealth;
        HealthSlider.maxValue = Enemy.GetComponent<EnemyStats>().StartingHealth;
    }
    void VisOnHit()
    {
        if (Enemy.GetComponent<EnemyStats>().CurrentHealth !=
            Enemy.GetComponent<EnemyStats>().StartingHealth)
        {
            BarContainer.SetActive(true);
        }
    }
    void DisableOnDeath()
    {
        if (Enemy.GetComponent<EnemyStats>().CurrentHealth <= 0)
        {
            BarContainer.SetActive(false);
        }
    }
}
