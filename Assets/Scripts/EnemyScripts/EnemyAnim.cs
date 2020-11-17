using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void EnemyAttack(int attack)
    {// Basic attacks 1/2/3
        if (attack == 0){
            anim.SetTrigger(AnimationTags.basic1trigger); }
        if (attack == 1){
            anim.SetTrigger(AnimationTags.basic2trigger); }
        if (attack == 2){
            anim.SetTrigger(AnimationTags.basic3trigger); }
    }
    public void Walk(){
        anim.SetBool(AnimationTags.movingbool, true); }
    public void Idle(){
        anim.Play(AnimationTags.idle); }
    public void StandUp(){
        anim.Play(AnimationTags.standuptrigger); }
    public void Hit(){
        anim.Play(AnimationTags.hittrigger); }
    public void Falling(){
        anim.Play(AnimationTags.fallingtrigger); }
    public void Land(){
        anim.Play(AnimationTags.landingtrigger); }
    public void Death(){
        anim.Play(AnimationTags.deathtrigger); }
}
