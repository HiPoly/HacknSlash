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
    public void Walk(bool moving){
        anim.SetBool(AnimationTags.movingbool, moving);
    }
    public void Crouch(bool crouching){
        anim.SetBool(AnimationTags.crouchingbool, crouching);
    }
    //Basic Attacks 1/2/3
    public void Basic1(){
        anim.SetTrigger(AnimationTags.basic1trigger);}
    public void Basic2(){
        anim.SetTrigger(AnimationTags.basic2trigger);}
    public void Basic3(){
        anim.SetTrigger(AnimationTags.basic1trigger);}

    //Dodges 1/2
    public void Dodge1(){
    anim.SetTrigger(AnimationTags.dodge1trigger);
    }
    public void Dodge2(){
    anim.SetTrigger(AnimationTags.dodge2trigger);
    }
    public void Slide(){
        anim.SetTrigger(AnimationTags.slidetrigger);
    }
    public void Sweep(){
        anim.SetTrigger(AnimationTags.sweeptrigger);
    }
    public void Hold(){
        anim.SetTrigger(AnimationTags.holdtrigger);
    }
    public void Bounce(){
        anim.SetTrigger(AnimationTags.bouncetrigger);
    }
    public void Charge(){
        anim.SetTrigger(AnimationTags.chargetrigger);
    }
}
