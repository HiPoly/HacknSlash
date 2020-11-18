using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWave : MonoBehaviour
{
    [SerializeField]
    private float Speed;


    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move()
    {
        transform.Translate(1 * Time.deltaTime * Speed, 0, 0);
    }
}
