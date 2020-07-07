using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    bool playerInRange;
    EnemyHealth enemyHealth;
    float timer;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            playerInRange = true;
            anim.SetBool("Attack", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            playerInRange = false;
            anim.SetBool("Attack", false);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth >0)
        {
            Attack();
        }
        if(playerHealth.currentHeath <=0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }
    void Attack()
    {
        timer = 0f;
        if(playerHealth.currentHeath >0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    
}
