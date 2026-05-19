using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public Transform[] patrolPoints;

    int currentPoint = 0;

    public float detectDistance = 40f;

    public bool chasing = false;

    public float loseDistance = 15f;

    public float fieldOfViewAngle = 0.7f;

    public Animator anim;

    void Start()
    {
        agent.SetDestination(patrolPoints[currentPoint].position);
    }

    void Update()
    {
        //distance calculate
        float distance = Vector3.Distance(transform.position, player.position);

        //calculate direction 
        Vector3 direcToPlayer = (player.position - agent.transform.position).normalized;

        //dot 1 means player fornt 0 mean side -1 means behind 
        float dot = Vector3.Dot(transform.forward, direcToPlayer);

        RaycastHit hit;
        bool cansee = false;
        //enemy cannot see if the player is behind the wall
        Physics.Raycast(transform.position + Vector3.up * 1.5f, direcToPlayer, out hit, detectDistance);
        Debug.DrawRay(transform.position + Vector3.up * 1.5f, direcToPlayer * detectDistance, Color.red);

        Debug.Log(distance);
        Debug.Log(dot);
        Debug.Log(hit.transform.name);

        if (hit.transform.CompareTag("Player"))
        {
            cansee = true;
        }

        // player detect
        // dot 0.2 means larger detection cone 0.8 means small cone
        if (!chasing && distance < detectDistance && dot > fieldOfViewAngle && cansee)
        {
            chasing = true;
            anim.SetBool("chase", true);
        }

        if (chasing && distance > loseDistance)
        {
            chasing = false;
            anim.SetBool("chase", false);
        }


        if (chasing)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.5f)
        {
            currentPoint++;

            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }

            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }
}
