using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSMState
{
    public IdleState()
    {
        stateID = FSMStateID.Idling;
        playerDetectionDistance = 100.0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        float dist = Vector3.Distance(npc.position, player.position);

        if (dist <= playerDetectionDistance)
        {
            Debug.Log("Switch to Attaking State");
            npc.GetComponent<NPCTurretController>().SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        // Do nothing while idling?
    }
}
