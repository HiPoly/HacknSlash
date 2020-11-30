using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    //Misc
    private bool Grounded;
    //Fetched Components
        //Scripts
        private EnemyStats EnemyStats;
        private PlayerStats PlayerStats;
        private EnemyAnim EnemyAnim;
    private Rigidbody rb;
    private Transform PlayerTarget;
    //AttackVars
    private bool FollowPlayer, AttackPlayer;
    public float speed = 5f;
    public float AttackDistance = 1f;
    [SerializeField] private float AggroDistance = 10f;
    public float ChasePlayerAfterAttack = 1f;
    [SerializeField]
    private float CurrentAttackTime;
    private float DefaultAttackTime = 4f;
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

    [SerializeField]
    private GameObject CollisionBox;
    private Collider HitBox;

    private List<PlayerStats> hitList = new List<PlayerStats>();

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
        EnemyStats = GetComponent<EnemyStats>();
        PlayerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        HitBox = GetComponent<Collider>();
    }
    void Update(){
        CheckHit();
        //Check if attack point is currently hitting an enemy
        CheckAttack();
        //Check inputs for special attacks and combos
        CheckGrounded();
        //Check if the player's rigidbody is at y: 0
        ClampY();
        //Keep the enemy above world space Y: 0 and below the max-height
        CheckGrounded();
        //Check if the player's rigidbody is at approximately y: 0
    }
    private void FixedUpdate(){
        //Look at and move to attacking range of the player 
        FollowTarget();
    }
    void CheckGrounded()
    {
        if (transform.position.y <= 0.05){
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
        if (Vector3.Distance(transform.position, PlayerTarget.position) > AttackDistance
        && (Vector3.Distance(transform.position, PlayerTarget.position) < AggroDistance && Grounded))
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
    //Animation Events that open the hit window and allow the Enemy to deal damage once per strike
    void OpenHit()
    {
        AttackWindow = true;
    }
    void CloseHit()
    {
        AttackWindow = false;
        //Attacks = 0;
        foreach (PlayerStats p in hitList)
        {
            p.BeenHit = false;
        }
        hitList.Clear();
    }
    void CheckHit()
    {
        Collider[] HitPlayers = Physics.OverlapSphere(AttackPoint.position, AttackRange);

        foreach (Collider player in HitPlayers)
        {
            if (AttackWindow == true && Attacks > 0)
            {
                if (GetComponent<PlayerStats>().Blocking == true){
                    EnemyAnim.Recoil();
                    return;
                }
                if (player.GetComponent<PlayerStats>() != null){
                    Debug.Log("We hit Player");
                    PlayerStats p = player.GetComponent<PlayerStats>();
                    hitList.Add(p);
                    EnemyStats.CurrentForce += EnemyStats.ForcePerHit;
                }
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
        CollisionBox.SetActive(false);
        HitBox.enabled = false;
        AttackPlayer = false;
        FollowPlayer = false;
        this.enabled = false;
    }
}
