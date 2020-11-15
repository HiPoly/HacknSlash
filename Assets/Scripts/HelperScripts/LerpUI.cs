using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpUI : MonoBehaviour
{
    public GameObject UITarget;
    private Vector3 Target; 
    [SerializeField] private float speed;
    void Update()
    {
        MoveToTarget();
        CreateTarget();
    }
    void CreateTarget()
    {
        Vector3 Target = new Vector3(UITarget.transform.position.x, 0, UITarget.transform.position.z);
    }
    void MoveToTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target, step);
    }

}
