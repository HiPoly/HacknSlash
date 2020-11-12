using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    private EnemyAnim EnemyAnim;
    private Rigidbody rb;
    public float speed = 5f;
    private Transform PlayerTarget;

    public float AttackDistance = 1f;
    public float ChasePlayerAfterAttack = 1f;

    private float CurrentAttackTime;
    private float DefaultAttackTime = 4f;

    private bool FollowPlayer, AttackPlayer;
    private bool Grounded;

    //Control hit windows and strikes through animation events
    private bool AttackWindow = false;
    private int Attacks = 0;

    //Attacking Hitbox and Size
    public Transform AttackPoint;
    [SerializeField] private float AttackRange = 0.1f;
    public int AttackDamage = 30;
    public LayerMask EnemyLayers;

    //Max Clamped height, add dying if this height is reached
    [SerializeField] private float MaxHeight = 100f;
   
    void Awake()
    {
        EnemyAnim = GetComponentInChildren<EnemyAnim>();
        rb = GetComponent<Rigidbody>();

        PlayerTarget = GameObject.FindWithTag(Tags.playertag).transform;
    }

    private void Start()
    {
        FollowPlayer = true;
        CurrentAttackTime = DefaultAttackTime;
    }
    void Update(){
        CheckGrounded();
        //Check if the player's rigidbody is at y: 0
        CheckHit();
        //Check if attack point is currently hitting an enemy
        CheckAttack();
        //Check inputs for special attacks and combos
        ClampY();
        //Keep the enemy above world space Y: 0 and below the max-height
        

        CheckGrounded();
    }
    private void FixedUpdate(){
        //Look at and move to attacking range of the player 
        FollowTarget();
    }

    void CheckGrounded()
    {
        if (transform.position.y == 0){
            Grounded = true;
        }
        else{
            Grounded = false;
        }
    }
    void FollowTarget()
    {
        if (!FollowPlayer)
            return;
        if (Vector3.Distance(transform.position, PlayerTarget.position) > AttackDistance && Grounded)
        {
            Vector3 PlayerPosition = new Vector3
                (PlayerTarget.position.x, this.transform.position.y, PlayerTarget.position.z);
            transform.LookAt(PlayerPosition);
            rb.velocity = transform.forward * speed;
            if(rb.velocity.sqrMagnitude != 0)
            {
                EnemyAnim.Walk();
            }
        }
        else if (Vector3.Distance(transform.position, PlayerTarget.position) <= AttackDistance)
        {
            rb.velocity = Vector3.zero;
            FollowPlayer = false;
            AttackPlayer = true;
        }
    }
    //Animation Events that open the hit window and allow the player to deal damage once per strike
    void OpenHit(){
        AttackWindow = true;
        Attacks = 1;
    }
    void CloseHit(){
        AttackWindow = false;
        Attacks = 0;
    }
    void CheckHit()
    {
        Collider[] HitEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange);

        foreach (Collider Enemy in HitEnemies)
        {
            if (AttackWindow == true && Attacks > 0)
            {
                Debug.Log("We hit " + Enemy.name);
                Enemy.GetComponent<PlayerStats>().Hit(AttackDamage);
                Attacks = 0;
            }
        }
    }
    void CheckAttack()
    {
        if (!AttackPlayer)
            return;
        
        CurrentAttackTime += Time.deltaTime;
        if(CurrentAttackTime > DefaultAttackTime)
        {
            EnemyAnim.EnemyAttack(Random.Range(0, 3));
            CurrentAttackTime = 0f;
        }

        if(Vector3.Distance(transform.position, PlayerTarget.position) >
                AttackDistance + ChasePlayerAfterAttack)
        {
            AttackPlayer = false;
            FollowPlayer = true;
        }
    }
    void ClampY()
    {
        Vector3 ClampedPosition = transform.position;
        ClampedPosition.y = Mathf.Clamp(ClampedPosition.y, 0f, MaxHeight);
        transform.position = ClampedPosition;
    }
    public void Die()
    {
        AttackPlayer = false;
        FollowPlayer = false;
        this.enabled = false;
    }
}
