using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public Transform targetPlayerTransform;
  

    public float movementSpeed;
    public float damage;
    public float minimumFollowingRage;
    public float maximumFollowingRange;
    public float attackCooldown;
    public float wanderRadius;
    public float idleTime;

    private Transform enemyTransform;
    private NavMeshAgent agent;
    private enum state { idle, wander, attack};
    private state currentState = state.idle;
    private float previousAttackTime;
    private float startIdleTime;
   
    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        idleStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        if (distanceToPlayer() < minimumFollowingRage && previousAttackTime + attackCooldown < Time.time)
        {
            currentState = state.attack;
        }

        switch (currentState)
        {
            case state.idle: idleStateUpdate(); break;
            case state.wander: wanderStateUpdate(); break;
            case state.attack: attackStateUpdate(); break;
        }
    }

    void idleStateEnter()
    {
        currentState = state.idle;

        startIdleTime = Time.time;
    }

    void idleStateUpdate()
    {
        agent.destination = enemyTransform.position;

        if(startIdleTime + idleTime < Time.time)
        {
            wanderStateEnter();
        }
    }

    void wanderStateEnter()
    {
        currentState = state.wander;

        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randomDirection, out navMeshHit, wanderRadius, 1);
        agent.destination = navMeshHit.position;
    }
    void wanderStateUpdate()
    {
        if (Vector3.Distance(agent.destination, transform.position) < 3f)
        {
            idleStateEnter();
        }
    }

    void attackStateUpdate()
    {
        if (distanceToPlayer() > maximumFollowingRange)
        {
            idleStateEnter();
        }
        else
        {
            agent.SetDestination(targetPlayerTransform.position);
        }
    }

    float distanceToPlayer()
    {
        return Vector3.Distance(targetPlayerTransform.position, enemyTransform.position);
    }

    void dealPlayerDamage()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            dealPlayerDamage();
            previousAttackTime = Time.time;
            currentState = state.idle;
        }
    }

    public void MoveToSpawn(Vector3[] locations) {
        //hello
    }
}
