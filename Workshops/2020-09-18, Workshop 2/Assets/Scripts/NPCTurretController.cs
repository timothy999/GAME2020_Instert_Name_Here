using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTurretController : EnemyFSM
{
    public GameObject Bullet;

    protected override void Initialize()
    {
        elapsedTime = 0.0f;
        shootRate = 2.0f;

        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        elapsedTime += Time.deltaTime;
    }

    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(playerTransform, transform);
        CurrentState.Act(playerTransform, transform);
    }

    public void SetTransition(Transition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {
        IdleState idle = new IdleState();
        idle.AddTransition(Transition.SawPlayer, FSMStateID.Attacking);

        AttackState attack = new AttackState();
        attack.AddTransition(Transition.LostPlayer, FSMStateID.Idling);

        AddFSMState(idle);
        AddFSMState(attack);
    }

    public void ShootBullet()
    {
        if (elapsedTime >= shootRate)
        {
            Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            elapsedTime = 0.0f;
        }
    }
}
