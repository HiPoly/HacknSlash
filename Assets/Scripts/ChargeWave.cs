using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWave : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private PlayerStats PlayerStats;
    private void Start()
    {
        this.gameObject.SetActive(true);
        PlayerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    void Update()
    {
        Move();
    }
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.GetComponent<EnemyStats>() != null)
        {
            Debug.Log("We hit " + enemy.name);
            EnemyStats e = enemy.GetComponent<EnemyStats>();
            e.Hit(PlayerStats.CurrentDamage);
        }
    }
    void Move()
    {
        transform.Translate(1 * Time.deltaTime * Speed, 0, 0);
    }
}
