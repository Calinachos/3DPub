using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;

    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        PlayerStats playerStat = player.GetComponent <PlayerStats> ();
        playerStat.experience += stateData.xpDropped;
        int coinsDropped = Random.Range(stateData.coinsDropped / 2, stateData.coinsDropped + 1);
        playerStat.coins += coinsDropped;
        base.Enter();

        //entity.anim.SetTrigger("dead");
        //entity.gameObject.SetActive(false);
        entity.aliveGO.GetComponent<BoxCollider2D>().enabled = false;
        entity.rb.gravityScale = 1;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
