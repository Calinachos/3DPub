﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttack stateData;

    protected AttackDetails attackDetails;

    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.aliveGO.transform.position;
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            //collider.transform.SendMessage("Damage", attackDetails);

            if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<PlayerStats>().defense < (int)stateData.attackDamage)
            {
                //collider.gameObject.GetComponent<PlayerStats>().life += collider.gameObject.GetComponent<PlayerStats>().defense - (int)stateData.attackDamage;
                collider.gameObject.GetComponent<PlayerStats>().TakeDamage((int)stateData.attackDamage - collider.gameObject.GetComponent<PlayerStats>().defense);
                /*
                if (collider.gameObject.tag == "Player")
                {
                    collider.gameObject.GetComponent<PlayerStats>().life -= (int)stateData.attackDamage;
                }
                */

            }

            if (collider.gameObject.tag == "DarkSide")
            {
                collider.gameObject.GetComponent<CloneStats>().TakeDamage(10);
            }
        }


    }
}
