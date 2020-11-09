using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

    //Movement Walking/Running/Crouching
    public void Run(bool running){
        anim.SetBool(AnimationTags.runningbool, running);
    }

    public void Walk(bool moving){
        anim.SetBool(AnimationTags.movingbool, moving);
    }
    public void Crouch(bool crouching){
        anim.SetBool(AnimationTags.crouchingbool, crouching);
    }

    //Blocking Block/Parry
    public void Block(bool blocking){
        anim.SetBool(AnimationTags.blockingbool, blocking);
    }
    public void Parry(){
        anim.SetTrigger(AnimationTags.parrytrigger);
    }

    //Reactions Hit/Death
    public void Hit()
    {
        anim.SetTrigger(AnimationTags.hittrigger);
    }
    public void Death()
    {
        anim.SetTrigger(AnimationTags.deathtrigger);
    }

    //Special Attacks Sweep/Hold/Charge/Bounce
    public void Sweep(){
        anim.SetTrigger(AnimationTags.sweeptrigger);
    }
    public void Hold(){
        anim.SetTrigger(AnimationTags.holdtrigger);
    }
    public void Charge(){
        anim.SetTrigger(AnimationTags.chargetrigger);
    }
    public void Bounce(){
        anim.SetTrigger(AnimationTags.bouncetrigger);
    }

    //Basic Attacks 1/2/3
    public void Basic1(){
        anim.SetTrigger(AnimationTags.basic1trigger);}
    public void Basic2(){
        anim.SetTrigger(AnimationTags.basic2trigger);}
    public void Basic3(){
        anim.SetTrigger(AnimationTags.basic3trigger);}

    //Dodges Step/Roll
    public void Dodge1(){
    anim.SetTrigger(AnimationTags.dodge1trigger);
    }
    public void Dodge2(){
    anim.SetTrigger(AnimationTags.dodge2trigger);
    }
    public void Slide(){
        anim.SetTrigger(AnimationTags.slidetrigger);
    }
    public void ComboEnd()
    {
        anim.SetTrigger(AnimationTags.comboendtrigger);
    }
}
