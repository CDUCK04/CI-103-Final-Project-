using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCOllider;
    private List<Collider> alreadyCollided = new List<Collider>();
    private int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCOllider)
        {
            return;
        }

        if (alreadyCollided.Contains(other))
        {
            return;
        }

        alreadyCollided.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
        alreadyCollided.Clear();
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
