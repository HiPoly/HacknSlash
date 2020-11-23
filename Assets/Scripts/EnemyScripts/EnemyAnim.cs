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
            anim.SetTrigger(AnimationTags.basic1); }
        if (attack == 1){
            anim.SetTrigger(AnimationTags.basic2); }
        if (attack == 2){
            anim.SetTrigger(AnimationTags.basic3); }
    }
    public void Walk(){
        anim.Play(AnimationTags.walk); }
    public void Idle(){
        anim.Play(AnimationTags.idle); }
    public void StandUp(){
        anim.Play(AnimationTags.standup); }
    public void Hit(){
        anim.Play(AnimationTags.hit); }
    public void Recoil(){
        anim.Play(AnimationTags.recoil); }
    public void Falling(){
        anim.Play(AnimationTags.falling); }
    public void Land(){
        anim.Play(AnimationTags.landing); }
    public void Death(){
        anim.Play(AnimationTags.death); }
}
