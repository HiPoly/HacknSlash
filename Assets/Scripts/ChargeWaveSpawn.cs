using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWaveSpawn : MonoBehaviour
{

    private bool upPressed;
    private bool sidePressed;
    private bool facingRight;

    void Update()
    {
        CheckInputs();
        SetRotation();
    }
    void CheckInputs()
    {
        if (Input.GetKey(KeyCode.W)){
            upPressed = true; }
        else{
            upPressed = false; }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            sidePressed = true; }
        else{
            sidePressed = false; }
        //Direction
        if (Input.GetKeyDown(KeyCode.A)){
            facingRight = false; }
        if (Input.GetKeyDown(KeyCode.D)){
            facingRight = true; }
    }
    void SetRotation()
    {
        if (upPressed && !sidePressed)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (upPressed && sidePressed)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
        }
        else if (!upPressed && sidePressed)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
