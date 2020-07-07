using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 15;
    public float aggroRange = 10;
    public float attackRange = 2;
    public Transform[] waypoints;
    int index;
    float speed, agentSpeed;
    Transform player;
    Animator animator;
    NavMeshAgent agent;
    public string tagToCompare = "Player";

    

   

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if(agent != null) { agentSpeed = agent.speed; }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);
        InvokeRepeating("Tick", 0, 0.5f);


        if(waypoints.Length >0)
        {
            InvokeRepeating("Patrol", 0, patrolTime);
        }
    }

    void Update()
    {
       
        if(agent.velocity.magnitude < 2)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }


    void Tick()
    {
        if (agent != null)
        {
            agent.destination = waypoints[index].position;
            agent.speed = agentSpeed / 2;
        }

        if (player != null && agent != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }
    }

}
