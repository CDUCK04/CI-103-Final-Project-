using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciever : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;

    private float verticalVelocity;
    private Vector3 impact;
    private Vector3 dampingVelocity;
    public Vector3 movement => impact + Vector3.up * verticalVelocity;
    // Update is called once per frame
    void Update()
    {
        if (verticalVelocity< 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y*Time.deltaTime;
        }
        impact = Vector3.SmoothDamp(impact,Vector3.zero,ref dampingVelocity,drag);
    }
    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
