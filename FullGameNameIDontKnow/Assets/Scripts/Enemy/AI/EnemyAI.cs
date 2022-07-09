using UnityEngine;
using UnityEngine.AI;
using GD.MinMaxSlider;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    public EnemyData enemyData;

    [Header("Components")]
    public Transform mainTarget;
    public LayerMask whatIsGround, whatIsPlayer, whatIsCanFollow;
    private NavMeshAgent agent;
    private Rigidbody rb;

    // States
    private float attackRange;
    private float followRange;

    private bool playerInAttackRange;
    private bool playerInFollowRange;
    private bool isFollowingAnother = false;
    
    private GameObject followingObject;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SetParam();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInFollowRange = Physics.CheckSphere(transform.position, followRange, whatIsCanFollow);

        if (!playerInAttackRange && !playerInFollowRange)
            ChasePlayer();
        else if (!playerInAttackRange && playerInFollowRange)
            ChaseArrowOrLantern();
        else if (playerInAttackRange)
            AttackPlayer();
    }

    // Go to lanterns
    private void ChasePlayer()
    {
        agent.SetDestination(mainTarget.position);
        isFollowingAnother = false;
    }

    private void ChaseArrowOrLantern()
    {
        // Verification for optimization
        if (!isFollowingAnother)
        {
            Collider[] followCollider = Physics.OverlapSphere(transform.position, followRange, whatIsCanFollow);
            followingObject = followCollider[0].transform.gameObject;
            isFollowingAnother = true;
        }

        if (followingObject != null && isFollowingAnother)
            agent.SetDestination(followingObject.transform.position);
    }

    // Attacking
    public void AttackPlayer() {
        agent.SetDestination(transform.position);
    }

     /* public void TakeDamage(int damage)
     {
         health -= damage;

         if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
     }

     private void DestroyEnemy()
     {
         Destroy(gameObject);
     } */

    // Set Mob parameter's
    private void SetParam() {
        attackRange = enemyData.attackRange;
        followRange = enemyData.followRange;
        rb.mass = enemyData.Mass;
        agent.speed = enemyData.Speed;
        agent.acceleration = enemyData.Acceleration;
    }

    // Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange);
    }
}
