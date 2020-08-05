﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredSkeleton_DeadState : DeadState
{
    private ArmoredSkeleton enemy;

    public ArmoredSkeleton_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, ArmoredSkeleton enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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
