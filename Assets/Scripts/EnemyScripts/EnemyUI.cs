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
        HealthSlider.value = GameObject.Find("Enemy").GetComponent<EnemyStats>().CurrentHealth;
    }
    void VisOnHit()
    {
        if (GameObject.Find("Enemy").GetComponent<EnemyStats>().CurrentHealth !=
            GameObject.Find("Enemy").GetComponent<EnemyStats>().StartingHealth)
        {
            BarContainer.SetActive(true);

        }
    }
    void DisableOnDeath()
    {
        if (GameObject.Find("Enemy").GetComponent<EnemyStats>().CurrentHealth <= 0)
        {
            BarContainer.SetActive(false);
        }
    }
}
