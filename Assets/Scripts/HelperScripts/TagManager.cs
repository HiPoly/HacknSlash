using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTags 
{   //movements
    public const string idle = "Idle";
    public const string moving = "Moving";
    //dodges
    public const string dodge1trigger = "Dodge1";
    public const string dodge2trigger = "Dodge2";
    public const string slidetrigger = "Slide";
    public const string recoverytrigger = "Recovery";
    //blocks
    public const string blocktrigger = "Block";
    public const string parrytrigger = "Parry";
    //attacks
    public const string basic1trigger = "Basic1";
    public const string basic2trigger = "Basic2";
    public const string basic3trigger = "Basic3";
    //special attacks
    public const string sweeptrigger = "Sweep";
    public const string holdtrigger = "Hold";
    public const string bouncetrigger = "Bounce";
    public const string chargetrigger = "Charge";

    //Statuses
    public const string knockdowntrigger = "KnockDown";
    public const string standuptrigger = "StandUp";
    public const string hittrigger = "Hit";
    public const string deathtrigger = "Death";
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
    public const string leftarmtag = "LeftArm";
    public const string leftlegtag = "LeftLeg";
    public const string maincameratag = "MainCam";
    public const string healthUI = "HealthUI";
    public const string untagged = "Untagged";
}