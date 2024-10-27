using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{

    private float previousFrametime;
    private Attack attack;
    private bool alreadyAppliedForce;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
       attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.weapon.SetAttack(attack.Damage);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName,0.1f);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);
        if (normalizedTime >= previousFrametime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.isAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }

        previousFrametime = normalizedTime;
    }
    public override void Exit()
    {
       
    }
    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1)
        {
            return;
        }
        if (normalizedTime < attack.ComboAttackingTime)
        {
            return;
        }
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));

    }
    private void TryApplyForce()
    {   if(alreadyAppliedForce) { return; }

        stateMachine.ForceReciever.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyAppliedForce = true;
    }
    
   

  
}
