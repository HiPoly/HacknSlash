using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnim : MonoBehaviour
{   //
    private Animator anim;
    public string currentState;
    public int currentPriority;

    //Activates while the player shouldnt be returning to idle
    public bool Busy;

    //Get Animation Clips
    //public AnimationClip Idle = null;
    //public AnimationClip Walk = null;
    //public AnimationClip Run = null;
    //public AnimationClip Crouch = null;
    //public AnimationClip Block = null;
    //public AnimationClip Parry = null;
    //public AnimationClip Recoil = null;
    //public AnimationClip Hit = null;
    //public AnimationClip Death = null;
    //public AnimationClip Fall = null;
    //public AnimationClip Land = null;
    //public AnimationClip Stand = null;
    //public AnimationClip Sweep = null;
    //public AnimationClip Hold = null;
    //public AnimationClip ChargeHold = null;
    //public AnimationClip ChargeRelease = null;
    //public AnimationClip Bounce = null;
    //public AnimationClip Basic1 = null;
    //public AnimationClip Basic2 = null;
    //public AnimationClip Basic3 = null;
    //public AnimationClip Dodge1 = null;
    //public AnimationClip Dodge2 = null;
    //public AnimationClip Slide = null;
    //public AnimationClip Jump = null;
    //public AnimationClip Recovery = null;

    void Awake(){
        anim = GetComponent<Animator>();
    }
    public void ChangeState(string newState, float blendTime = 0, int Priority = 0)
    {
        //Stop the same animation from interrupting itself
        if (currentState == newState){
            return; 
        }
        if (Priority >= currentPriority){
            if (blendTime > 0){
                //blend animation with specified time
                anim.CrossFade(newState, blendTime);
            }
            else{
                //Play the animation
                anim.Play(newState);
            }
        }
        //If priority animations are finished playing reset priority to neutral
        if (!animIsPlaying()){
            currentPriority = 0;
        }
        //Reassign the current state and priority
        currentState = newState;
        currentPriority = Priority;
    }
    private void Update()
    {
        CheckIdle();
        //Checks if the player is moving or attacking and can transition to idle
        animIsPlaying();
    }
    void CheckIdle()
    {
        if (GetComponent<PlayerActions>().Acting == false
            && GetComponent<PlayerMovement>().Moving == false
            && GetComponent<PlayerStats>().Blocking == false
            && transform.position.y <= 0.05f)
        {
            ChangeState(AnimationTags.idle, 0.325f);
        }
    }
    bool animIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    //====================================================
    //All Animation Functions
    //====================================================

    public void Block(bool Blocking)
    {
        if (Blocking == true){
            anim.SetLayerWeight(1, 1f);
        }
        else if (Blocking == false){
            anim.SetLayerWeight(1, 0f);
        }
    }
    public void Move(int MoveState)
    {
        if (MoveState == 0){
            //
        }
        else if (MoveState == 1){
            ChangeState("Walk");
        }
        else if (MoveState == 2){
            ChangeState("Run");
        }
    }

    //Movement Walking/Running/Crouching
    //public void Walk(bool moving) {
    //    anim.Play("Walk"); }
    //public void Run(bool running) {
    //    anim.Play("Run"); } 
    //public void Crouch(bool crouching) {
    //    anim.Play("Crouch"); }
    ////Blocking Block/Parry
    //public void Block(bool blocking) {
    //    anim.Play("Block"); }
    //public void Parry() {
    //    anim.Play("Parry"); }
    ////Reactions Recoil/Hit/Death/Knockdown/Standup
    //public void Recoil(){
    //    anim.Play("Recoil"); }
    //public void Hit() {
    //    anim.Play("Hit"); }
    //public void Death() {
    //    anim.Play("Death"); }
    //public void Fall() {
    //    anim.Play("Fall"); }
    //public void Land() {
    //    anim.Play("Land"); }
    //public void Standup() {
    //    anim.Play("Stand"); }
    ////Special Attacks Sweep/Hold/Charge/Bounce
    //public void Sweep() {
    //    anim.Play("Sweep"); }
    //public void Hold() {
    //    anim.Play("Hold"); }
    //public void ChargeHold() {
    //    anim.Play("ChargeHold"); }
    //public void ChargeRelease() {
    //    anim.Play("ChargeRelease"); }
    //public void Bounce() {
    //    anim.Play("Bounce"); }
    ////Basic Attacks 1/2/3
    //public void Basic1() {
    //    anim.Play("Basic1"); }
    //public void Basic2() {
    //    anim.Play("Basic2"); }
    //public void Basic3() {
    //    anim.Play("basic3"); }
    ////Dodges Step/Roll/Slide/Jump
    //public void Dodge1() {
    //anim.Play("Dodge1"); }
    //public void Dodge2() {
    //anim.Play("Dodge2"); }
    //public void Slide() {
    //    anim.Play("Slide"); }
    //public void Jump() {
    //    anim.Play("Jump");
    //}
    //ComboEnd
    //public void ComboEnd() {
    //    anim.SetTrigger(AnimationTags.comboendtrigger); }
}
