using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredSkeleton : Entity
{
    public ArmoredSkeleton_IdleState idleState { get; private set; }
    public ArmoredSkeleton_MoveState moveState { get; private set; }
    public ArmoredSkeleton_PlayerDetectedState playerDetectedState { get; private set; }
    public ArmoredSkeleton_ChargeState chargeState { get; private set; }
    public ArmoredSkeleton_LookForPlayerState lookForPlayerState { get; private set; }
    public ArmoredSkeleton_MeleeAttackState meleeAttackState { get; private set; }
    public ArmoredSkeleton_StunState stunState { get; private set; }
    public ArmoredSkeleton_DeadState deadState { get; private set; }
    public AudioSource coins1;
    public AudioSource coins2;
    public AudioSource coins3;

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

        moveState = new ArmoredSkeleton_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new ArmoredSkeleton_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new ArmoredSkeleton_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new ArmoredSkeleton_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new ArmoredSkeleton_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new ArmoredSkeleton_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new ArmoredSkeleton_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new ArmoredSkeleton_DeadState(this, stateMachine, "dead", deadStateData, this);

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
            switch (Random.Range(0, 3))
            {
                case 0:
                    coins1.Play();
                    break;
                case 1:
                    coins2.Play();
                    break;
                case 2:
                    coins3.Play();
                    break;
            }
            stateMachine.ChangeState(deadState);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }
}
