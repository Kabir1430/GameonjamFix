using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public GameObject EnemyAI;


    [Header("Animation")]

    public Animator EnemyAnim;



    [Header("PH")]

    public PlayerEmu  Player;

    public int Playerdamage;

    [Header("Reload")]

    public float RT;

    private void Awake()
    {
     
        //player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (health < 0)
        {
          //  EnemyAI.SetActive(false);
            DestroyEnemy();
          //  Debug.Log("Health 0");
        }
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        if (health < 0)
        {
          //  EnemyAI.SetActive(false);
            DestroyEnemy();
          //  Debug.Log("Health 0");
        }
    }

    private void ChasePlayer()
    {
        EnemyAnim.SetBool("Attack", false);
        EnemyAnim.SetBool("walk", true);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {



       agent.SetDestination(transform.position);


        Player.PlayerDamage(Playerdamage);
        transform.LookAt(player);
        EnemyAnim.SetBool("Attack", true);
        EnemyAnim.SetBool("walk", false);
        if (!alreadyAttacked)
        {
            //Attack code
          //  Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //    rb.AddForce(transform.forward *32f, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public  void TakeDamage(int damage)
    {
        health -= damage;
      //  if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

     void DestroyEnemy()
    {
        Destroy(EnemyAI);
      //  EnemyAI.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
