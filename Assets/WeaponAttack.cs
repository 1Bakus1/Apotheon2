using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public bool hitted;
    public int damagePerShot = 20;
    public float timeBetweenAttacks = 0.5f;
    float timer;
    //GameObject enemy;
    GameObject player;
    EnemyHealth enemyHealth;
    Animator anim;
    public bool attacked;
    // Start is called before the first frame update
    void Start()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        //enemyHealth = enemy.GetComponent<EnemyHealth>();
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= timeBetweenAttacks)
        {
            timer = 0f;
            anim.SetBool("Attack", true);
            attacked = true;
        }
        else
        {
            anim.SetBool("Attack", false);
            attacked = false;
        }

        //if(hitted && attacked && enemyHealth != null)
       //{
       //    enemyHealth.TakeDamage(damagePerShot);
       //}

    }
  
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            hitted = true;
           Attack(enemyHealth);
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            hitted = false;
            print("wyjscie");
        }
    }
    */

    void Attack(EnemyHealth enemyHealth)
    {
        if(hitted && attacked && enemyHealth != null)
        {
        enemyHealth.TakeDamage(damagePerShot);
        }
    }
}
