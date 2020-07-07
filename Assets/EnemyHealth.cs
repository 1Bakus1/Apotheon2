using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    private Slider enemyHealthslider;
    float delay = 7f;
    public List<GameObject> listOfWeapons;
    Animator anim;
    public GameObject enemy;
    NavMeshAgent agent;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyHealthslider = GetComponentInChildren<Slider>();
        currentHealth = startingHealth;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        enemyHealthslider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        Instantiate(RandomWeapon(), transform.position, transform.rotation);
        GetComponent<NavMeshAgent>().enabled = false;
        Destroy(gameObject,delay);

        //Instantiate(enemy, transform.position, transform.rotation);

    }


    GameObject RandomWeapon()
    {
        System.Random randNum = new System.Random();
        GameObject objCust;
        objCust = listOfWeapons[randNum.Next(listOfWeapons.Count)];
        return objCust;
    }
}
