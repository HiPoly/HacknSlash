using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyAnim EnemyAnim;
    private Rigidbody myBody;
    public float speed = 5f;
    private Transform playerTarget;

    public float AttackDistance = 1f;
    public float ChasePlayerAfterAttack = 1f;

    private float CurrentAttackTime;
    private float DefaultAttackTime = 2f;

    private bool FollowPlayer, AttackPlayer;
    void Awake()
    {
        EnemyAnim = GetComponentInChildren<EnemyAnim>();
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag(Tags.playertag).transform;
    }

    private void Start()
    {
        FollowPlayer = true;
        CurrentAttackTime = DefaultAttackTime;
    }
    void Update()
    {
        Attack();
    }
    private void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (!FollowPlayer)
            return;
        if (Vector3.Distance(transform.position, playerTarget.position) > AttackDistance)
        {
            transform.LookAt(playerTarget);
            myBody.velocity = transform.forward * speed;

            if(myBody.velocity.sqrMagnitude != 0)
            {
                EnemyAnim.Walk(true);
            }
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) <= AttackDistance)
        {
            myBody.velocity = Vector3.zero;
            EnemyAnim.Walk(false);
            FollowPlayer = false;
            AttackPlayer = true;
        }
    }

    void Attack()
    {
        if (!AttackPlayer)
            return;
        
        CurrentAttackTime += Time.deltaTime;
        if(CurrentAttackTime > DefaultAttackTime)
        {
            EnemyAnim.EnemyAttack(Random.Range(0, 3));
            CurrentAttackTime = 0f;
        }

        if(Vector3.Distance(transform.position, playerTarget.position) >
                AttackDistance + ChasePlayerAfterAttack)
        {
            AttackPlayer = false;
            FollowPlayer = true;
        }

    }

}
