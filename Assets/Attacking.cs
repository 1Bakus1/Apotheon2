using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    bool playerInRange;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    public float range = 1f;
    int shootableMask;
    public int damagePerShot = 20;
    public bool hitted;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenAttacks)
        {
            timer = 0f;
            anim.SetBool("Attack", true);
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (hitted)
            {
                enemyHealth.TakeDamage(damagePerShot);
            }
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

}
