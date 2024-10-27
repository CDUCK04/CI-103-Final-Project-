using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform player;
    public float speed = 1.8f;
    public float stoppingDistance = 2.0f;
    public float followDelay = 1.0f;
    private Animator animator;
    private float timer = 0.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        transform.LookAt(player);
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > stoppingDistance)
        {
            timer += Time.deltaTime;
            if (timer >= followDelay)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                controller.Move(direction * speed * Time.deltaTime);
                animator.SetFloat("Speed", speed);
            }
        }
        else
        {
            timer = 0.0f;
            animator.SetFloat("Speed", 0);
        }
    }
}
