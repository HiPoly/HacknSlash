using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTags
{   //movements
    public const string idle = "Idle";
    public const string movingbool = "Moving";
    public const string crouchingbool = "Crouching";
    public const string runningbool = "Running";
    //dodges
    public const string dodge1trigger = "Dodge1";
    public const string dodge2trigger = "Dodge2";
    public const string slidetrigger = "Slide";
    public const string recoverytrigger = "Recovery";
    public const string jumptrigger = "Jump";
    //blocks
    public const string blockingbool = "Blocking";
    public const string parrytrigger = "Parry";
    //attacks
    public const string basic1trigger = "Basic1";
    public const string basic2trigger = "Basic2";
    public const string basic3trigger = "Basic3";
    //special attacks
    public const string sweeptrigger = "Sweep";
    public const string holdtrigger = "Hold";
    public const string bouncetrigger = "Bounce";
    public const string chargeholdtrigger = "ChargeHold";
    public const string chargereleasetrigger = "ChargeRelease";
    //Statuses
    public const string fallingtrigger = "Fall";
    public const string landingtrigger = "Land";
    public const string standuptrigger = "Stand";
    public const string hittrigger = "Hit";
    public const string deathtrigger = "Death";
    public const string comboendtrigger = "ComboEnd";
    public const string recoiltrigger = "Recoil";
}
public class Axis
{
    public const string horizontalaxis = "Horizontal";
    public const string verticalaxis = "Vertical";
}
public class Tags
{
    public const string groundtag = "Ground";
    public const string playertag = "Player";
    public const string enemytag = "Enemy";
    public const string maincameratag = "MainCam";
    public const string healthUI = "HealthUI";
    public const string untagged = "Untagged";
}