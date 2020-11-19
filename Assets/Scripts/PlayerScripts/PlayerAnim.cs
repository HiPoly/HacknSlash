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
    //Movement Walking/Running/Crouching/Blocking
    public void Run(bool running) {
        anim.SetBool(AnimationTags.runningbool, running); }
    public void Walk(bool moving) {
        anim.SetBool(AnimationTags.movingbool, moving); }
    public void Crouch(bool crouching) {
        anim.SetBool(AnimationTags.crouchingbool, crouching); }
    //Blocking Block/Parry
    public void Block(bool blocking) {
        anim.SetBool(AnimationTags.blockingbool, blocking); }
    public void Parry() {
        anim.SetTrigger(AnimationTags.parrytrigger); }
    //Reactions Recoil/Hit/Death/Knockdown/Standup
    public void Recoil(){
        anim.SetTrigger(AnimationTags.recoiltrigger); }
    public void Hit() {
        anim.SetTrigger(AnimationTags.hittrigger); }
    public void Death() {
        anim.SetTrigger(AnimationTags.deathtrigger); }
    public void Fall() {
        anim.SetTrigger(AnimationTags.fallingtrigger); }
    public void Land() {
        anim.SetTrigger(AnimationTags.landingtrigger); }
    public void Standup() {
        anim.SetTrigger(AnimationTags.standuptrigger); }
    //Special Attacks Sweep/Hold/Charge/Bounce
    public void Sweep() {
        anim.SetTrigger(AnimationTags.sweeptrigger); }
    public void Hold() {
        anim.SetTrigger(AnimationTags.holdtrigger); }
    public void ChargeHold() {
        anim.SetTrigger(AnimationTags.chargeholdtrigger); }
    public void ChargeRelease() {
        anim.SetTrigger(AnimationTags.chargereleasetrigger); }
    public void Bounce() {
        anim.SetTrigger(AnimationTags.bouncetrigger); }
    //Basic Attacks 1/2/3
    public void Basic1() {
        anim.SetTrigger(AnimationTags.basic1trigger); }
    public void Basic2() {
        anim.SetTrigger(AnimationTags.basic2trigger); }
    public void Basic3() {
        anim.SetTrigger(AnimationTags.basic3trigger); }
    //Dodges Step/Roll/Slide/Jump
    public void Dodge1() {
    anim.SetTrigger(AnimationTags.dodge1trigger); }
    public void Dodge2() {
    anim.SetTrigger(AnimationTags.dodge2trigger); }
    public void Slide() {
        anim.SetTrigger(AnimationTags.slidetrigger); }
    public void Jump() {
        anim.SetTrigger(AnimationTags.jumptrigger);
    }
    //ComboEnd
    public void ComboEnd() {
        anim.SetTrigger(AnimationTags.comboendtrigger); }
}
