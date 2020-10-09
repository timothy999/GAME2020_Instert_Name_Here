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

    private Transform enemyTransform;
    private NavMeshAgent agent;
    private enum state { idle, attack};
    private state currentState = state.idle;
    private float previousAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case state.idle: idleStateUpdate(); break;
            case state.attack: attackStateUpdate(); break;
        }
    }

    float distanceToPlayer()
    {
        return Vector3.Distance(targetPlayerTransform.position, enemyTransform.position);
    }

    void dealPlayerDamage()
    {

    }

    void idleStateUpdate()
    {
        if (distanceToPlayer() < minimumFollowingRage && previousAttackTime + attackCooldown < Time.time)
            currentState = state.attack;

        agent.destination = enemyTransform.position;
    }

    void attackStateUpdate()
    {
        if (distanceToPlayer() > maximumFollowingRange)
        {
            currentState = state.idle;
        }
        else
        {
            agent.SetDestination(targetPlayerTransform.position);
        }
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
