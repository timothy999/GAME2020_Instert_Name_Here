using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState
{
    public AttackState()
    {
        stateID = FSMStateID.Attacking;
        curRotSpeed = 1.0f;
        playerDetectionDistance = 100.0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        float dist = Vector3.Distance(npc.position, player.position);

        if (dist > playerDetectionDistance)
        {
            Debug.Log("Switch to Idling State");
            npc.GetComponent<NPCTurretController>().SetTransition(Transition.LostPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        destPos = player.position;

        Quaternion turretRotation = Quaternion.LookRotation(destPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, turretRotation, Time.deltaTime * curRotSpeed);

        npc.GetComponent<NPCTurretController>().ShootBullet();
    }
}
