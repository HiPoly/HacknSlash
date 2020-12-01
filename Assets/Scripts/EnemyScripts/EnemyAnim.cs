using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private string currentState;
    private int currentPriority;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void EnemyAttack(int attack)
    {// Basic attacks 1/2/3
        if (attack == 0){
            ChangeState("Basic1"); }
        if (attack == 1){
            ChangeState("Basic2"); }
        if (attack == 2){
            ChangeState("Basic3"); }
    }
    public void ChangeState(string newState, float blendTime = 0, int Priority = 0){
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
    bool animIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    public void Walk(){
        ChangeState("Walk"); }
    public void Idle(){
        ChangeState("Idle"); }
    public void StandUp(){
        ChangeState("StandUp"); }
    public void Hit(){
        ChangeState("Hit"); }
    public void Recoil(){
        ChangeState("Recoil"); }
    public void Falling(){
        ChangeState("Falling"); }
    public void Land(){
        ChangeState("Land"); }
    public void Death(){
        ChangeState("Death"); }
}
