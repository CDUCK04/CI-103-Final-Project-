using UnityEngine;

public class UpdateFreeLookSpeed : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine playerStateMachine;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("FreeLookSpeed", playerStateMachine.FreeLookMovementSpeed);
        }
    }
}