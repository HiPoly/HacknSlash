﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLogger : MonoBehaviour
{
    //Ask Jacob how to store these inputs to a list efficiently and potentially print them in a practice range with representative icons
    //including how to make bindable keys in control settings
    //and how to tell forward from back when inputting (I can probably work this one out)
    private List<string> InputList;
    private bool FacingRight;

    private void Start()
    {
        FacingRight = true;
    }

    void Update()
    {
        //need something to remove expired or excess inputs aswell

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        if (Input.GetMouseButtonDown(0))
        {

        }
        if (Input.GetAxisRaw(Axis.horizontalaxis) < 0)
        {

        }
        if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
        {

        }
        if (Input.GetAxisRaw(Axis.verticalaxis) < 0)
        {

        }
        if (Input.GetAxisRaw(Axis.verticalaxis) > 0)
        {

        }
    }
}
