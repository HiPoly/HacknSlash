using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimList : MonoBehaviour
{
    private Animator PlayerAnim;

    public AnimationClip Idle = null;
    public AnimationClip Walk = null;
    public AnimationClip Run = null;
    public AnimationClip Crouch =null;
    public AnimationClip Block = null;
    public AnimationClip Parry= null;
    public AnimationClip Recoil = null;
    public AnimationClip Hit = null;
    public AnimationClip Death = null;
    public AnimationClip Fall = null;
    public AnimationClip Land = null;
    public AnimationClip Stand = null;
    public AnimationClip Sweep = null;
    public AnimationClip Hold = null;
    public AnimationClip ChargeHold = null;
    public AnimationClip ChargeRelease = null;
    public AnimationClip Bounce = null;
    public AnimationClip Basic1 = null;
    public AnimationClip Basic2 = null;
    public AnimationClip Basic3 = null;
    public AnimationClip Dodge1 = null;
    public AnimationClip Dodge2 = null;
    public AnimationClip Slide = null;
    public AnimationClip Jump = null;
    public AnimationClip Recovery = null;

    //ComboEnd, trigger no longer needed?

    void Start()
    {
        PlayerAnim = GetComponent<Animator>();
        //PlayerAnim.Play(Idle, 0);
    }
}
