using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BasicComboState{
    None,
    Basic1,
    Basic2,
    Basic3,
}
public enum DodgeComboState{
    None,
    Dodge1,
    Dodge2,
}

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnim PlayerAnim;
    //Timers
    private bool activateComboTimerToReset;
    private bool activateDodgeTimerToReset;
    private float DefaultComboTimer = 0.4f;
    private float CurrentComboTimer;
    private float DefaultDodgeTimer = 0.8f;
    private float CurrentDodgeTimer;
    //Enums
    private BasicComboState CurrentComboState;
    private DodgeComboState CurrentDodgeState;

    private void Start()
    {
        CurrentComboTimer = DefaultComboTimer;
        CurrentComboState = BasicComboState.None;
        CurrentDodgeTimer = DefaultDodgeTimer;
        CurrentDodgeState = DodgeComboState.None;
    }
    private void Update()
    {
        ComboTracker();
        ResetComboState();
    }

    void ComboTracker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentComboState == BasicComboState.Basic3)
            {
                return;
            }

            CurrentComboState++;
            activateComboTimerToReset = true;
            CurrentComboTimer = DefaultComboTimer;

            if (CurrentComboState == BasicComboState.Basic1)
            {
                PlayerAnim.Basic1();
            }
            if (CurrentComboState == BasicComboState.Basic2)
            {
                PlayerAnim.Basic2();
            }
            if (CurrentComboState == BasicComboState.Basic3)
            {
                PlayerAnim.Basic3();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentDodgeState == DodgeComboState.Dodge2)
            {
                return;
            }

            //Nikki, please ask someone how to write multiple input statements
            if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
            {
                (Input.GetAxisRaw(Axis.horizontalaxis) < 0;
            }

            CurrentDodgeState++;
            activateDodgeTimerToReset = true;
            CurrentDodgeTimer = DefaultDodgeTimer;

            if (CurrentDodgeState == DodgeComboState.Dodge1)
            {
                PlayerAnim.Dodge1();
            }
            if (CurrentDodgeState == DodgeComboState.Dodge2)
            {
                PlayerAnim.Dodge2();
            }
        }
    }
    private void ResetComboState()
    {
        if (activateComboTimerToReset)
        {
            CurrentComboTimer -= Time.deltaTime;
            if (CurrentComboTimer <= 0f)
            {
                CurrentComboState = BasicComboState.None;
                activateComboTimerToReset = false;
                CurrentComboTimer = DefaultComboTimer;

            }
        }
        if (activateDodgeTimerToReset)
        {
            CurrentDodgeState = DodgeComboState.None;
            activateDodgeTimerToReset = false;
            CurrentDodgeTimer = DefaultDodgeTimer;
        }
    }
}//class
