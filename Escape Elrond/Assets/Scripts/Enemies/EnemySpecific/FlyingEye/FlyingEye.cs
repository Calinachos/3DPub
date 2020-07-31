using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : Entity
{
    public FlyingEye_IdleState idleState { get; private set; }
    public FlyingEye_MoveState moveState { get; private set; }
    public FlyingEye_PlayerDetectedState playerDetectedState { get; private set; }
    public FlyingEye_ChargeState chargeState { get; private set; }
    public FlyingEye_LookForPlayerState lookForPlayerState { get; private set; }
    public FlyingEye_MeleeAttackState meleeAttackState { get; private set; }
    public FlyingEye_StunState stunState { get; private set; }
    public FlyingEye_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new FlyingEye_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new FlyingEye_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new FlyingEye_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new FlyingEye_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new FlyingEye_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new FlyingEye_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new FlyingEye_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new FlyingEye_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }
}
