using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Entity
{
    public Goblin_IdleState idleState { get; private set; }
    public Goblin_MoveState moveState { get; private set; }
    public Goblin_PlayerDetectedState playerDetectedState { get; private set; }
    public Goblin_ChargeState chargeState { get; private set; }
    public Goblin_LookForPlayerState lookForPlayerState { get; private set; }
    public Goblin_MeleeAttackState meleeAttackState { get; private set; }
    public Goblin_StunState stunState { get; private set; }
    public Goblin_DeadState deadState { get; private set; }
    public AudioSource coins1;
    public AudioSource coins2;
    public AudioSource coins3;
    public AudioSource walkSound;
    public AudioSource goblin1Sound;
    public AudioSource goblin2Sound;
    public AudioSource goblin3Sound;

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

        moveState = new Goblin_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Goblin_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Goblin_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new Goblin_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Goblin_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Goblin_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Goblin_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new Goblin_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Update()
    {
        if (base.rb.velocity.magnitude < 0.5 && !isDead)
        {
            walkSound.Play();
        }
        if (!goblin1Sound.isPlaying && !goblin2Sound.isPlaying && !goblin3Sound.isPlaying)
        {
            switch (Random.Range(0, 1000))
            {
                case 69:
                    goblin1Sound.Play();
                    break;
                case 70:
                    goblin2Sound.Play();
                    break;
                case 71:
                    goblin3Sound.Play();
                    break;
            }
        }
        base.Update();
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
            switch(Random.Range(0, 3)) {
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
            walkSound.Stop();
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }
}
