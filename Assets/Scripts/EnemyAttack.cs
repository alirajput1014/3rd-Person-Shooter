using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float ShootDistance = 7f;
    public float DamageAmount = 10f;
    public NavMeshAgent agent;
    public Animator anim;
    public float shootWait;
    bool ishooting = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // ? Pehle distance check karo, phir wall check
        if (distance < ShootDistance && CanSeePlayer())
        {
            if(agent.enabled==false) return;
            agent.isStopped = true;
            anim.SetBool("isShooting", true);

            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 5f * Time.deltaTime);

            if (!ishooting)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            if (agent.enabled == false) return;
            agent.isStopped = false;
            anim.SetBool("isShooting", false);
        }
    }

    // ? Naya function — wall check karta hai
    bool CanSeePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 1.5f, direction, out hit, ShootDistance))
        {
            if (hit.transform == player)
            {
                return true; // Player seedha nazar aa raha hai
            }
        }
        return false; // Wall beech mein hai
    }

    IEnumerator Attack()
    {
        ishooting = true;
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(DamageAmount);
        }
        yield return new WaitForSeconds(shootWait);
        ishooting = false;
    }
}
