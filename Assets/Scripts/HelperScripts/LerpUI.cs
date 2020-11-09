using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpUI : MonoBehaviour
{
    float t;
    Vector3 startPosition;
    public Vector3 UITarget;
    float timeToReachTarget;
    void Start()
    {
        startPosition = UITarget = transform.position;
    }
    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, UITarget, t);
    }
    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        UITarget = destination;
    }
}
