using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpUI : MonoBehaviour
{
    public GameObject UITarget;
    private Vector3 Target; 
    [SerializeField] private float speed;
    private float duration = 2f;

    void Start()
    {
        transform.position = UITarget.transform.position;
    }

    void Update()
    {
        MoveToTarget();
        CreateTarget();

    }
    void CreateTarget()
    {
        Target = new Vector3(UITarget.transform.position.x, transform.position.y, transform.position.z);
    }
    void MoveToTarget()
    {
        float step = speed * Time.deltaTime/duration;
        transform.position = Vector3.Lerp(transform.position, Target, step);
    }

}
