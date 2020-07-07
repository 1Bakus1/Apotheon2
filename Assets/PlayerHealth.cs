using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHeath = 100;
    public int currentHeath;
    public Slider healthslider;

    Animator anim;
    PlayerMovement playerMovement;
    bool isDead;
    bool damaged;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHeath = startingHeath;
    }

    // Update is called once per frame
    void Update()
    {
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHeath -= amount;
        healthslider.value = currentHeath;

        if(currentHeath <= 0 && !isDead)
        {
            Death();
        }
    }
    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        playerMovement.enabled = false;
    }
}
