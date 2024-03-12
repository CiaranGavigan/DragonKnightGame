using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used this https://www.youtube.com/watch?v=yxzg8jswZ8A video tutorial to help get the health system working

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public  float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                //player 
                if (GetComponent<PlayerMovement>() != null)
                {

                    GetComponent<PlayerMovement>().enabled = false;
                }
                //enemy
                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponentInParent<EnemyMovement>().enabled = false;
                }
                if (GetComponent<meleEnemy>() != null)
                {
                    GetComponent<meleEnemy>().enabled = false;
                }
                dead = true;
            }
        }
    }

    

}
