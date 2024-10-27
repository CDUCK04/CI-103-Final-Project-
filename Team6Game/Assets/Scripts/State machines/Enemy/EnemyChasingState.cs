using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {

        if (!InChaseRange())
        {

            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }else if (InAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        MoveToPlayer(deltaTime);
        FacePLayer();
        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit() 
    {
        stateMachine.agent.ResetPath();
        stateMachine.agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        stateMachine.agent.destination = stateMachine.Player.transform.position;
        Move(stateMachine.agent.desiredVelocity.normalized * stateMachine.MovementSpeed,deltaTime);
        stateMachine.agent.velocity = stateMachine.CharacterController.velocity;
    }

    private Boolean InAttackRange()
    {
        Vector3 toPlayer = stateMachine.Player.transform.position - stateMachine.transform.position;
        return toPlayer.magnitude <= stateMachine.AttackRange;
    }
}
