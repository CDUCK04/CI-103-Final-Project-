using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForwardSpeed");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRightSpeed");
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += Oncancel;
        stateMachine.Animator.Play(TargetingBlendTreeHash);
    }
    public override void Tick(float deltaTime)
    {

        if (stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        Vector3 Movement = CalculateMovement();
        Move(Movement*stateMachine.TargetingMovementSpeed,deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= Oncancel;
    }

    private void Oncancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    private Vector3 CalculateMovement()
    {
        Vector3 Movement = new Vector3();
        Movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        Movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return Movement;
    }
    private void UpdateAnimator(float deltaTime)
    {
        if(stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0,0.01f,deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.01f, deltaTime);
        }
        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.01f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value, 0.01f, deltaTime);
        }
    }
}
  

