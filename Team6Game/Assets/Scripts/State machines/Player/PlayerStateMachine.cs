using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField]public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public WeaponDamage weapon{ get; private set; }
    [field: SerializeField] public ForceReciever ForceReciever { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public float RotationSpeedValue { get; private set; }

    public Transform MainCameraTransform { get; private set; }
    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

}