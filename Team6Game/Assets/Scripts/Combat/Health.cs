using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth=100;
    [SerializeField] private Slider healthBar ;
    
    private int health;

    
    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        Die();
        if (health <= 0) { return; }

        health = Mathf.Max(health - damage, 0);
        if (damage > health) 
        { 
            health = 0;
        }
        healthBar.value= healthBar.value-damage;
        Debug.Log(health);
        

    }

    public void Die()
    {
        if (health > 0) { return; }
        if (gameObject.tag == "Player")
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene("Youwin");
        }
    }
  
}
