using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float PLayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public CharacterController CharacterController{ get; private set; }
    [field: SerializeField] public ForceReciever ForceReciever { get; private set; }
    [field: SerializeField] public NavMeshAgent agent{ get; private set; }
    [field: SerializeField] public WeaponDamage weapon{ get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    public GameObject Player { get; private set; }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        agent.updatePosition = false;
        agent.updateRotation = false;
        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;   
        Gizmos.DrawWireSphere(transform.position, PLayerChasingRange);
    }
}
