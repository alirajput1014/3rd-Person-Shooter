using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FlankerEnemy : MonoBehaviour
{
    public Transform player;
    private float Distance;
    public float DetectDistance;
    public Transform point;
    public NavMeshAgent agent;
    public Animator anim;
    bool cansee=false;
    bool isshooting=false;
    private PlayerHealth PlayerHealth;
    public GameObject FlashPrefab;
    public AudioSource GunSound;
    public GameObject FirePoint;
    bool reach=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealth =player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVision();
        if (cansee && !isshooting && reach )
        {
            isshooting = true;
            agent.isStopped = true;
            StartCoroutine(Shoot());
        }
    }
    void CheckVision()
    {
        Distance=Vector3.Distance(transform.position, player.position);
        if (Distance < DetectDistance)
        {
            anim.SetBool("Running", true);
            agent.SetDestination(point.position);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                reach = true;
                anim.SetBool("Running", false);
            }
          
        }
        Vector3 dir=(player.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 5f * Time.deltaTime);
        RaycastHit Hit;
        if(Physics.Raycast(transform.position+Vector3.up*1.5f,dir,out Hit,DetectDistance))
        {
            if (Hit.transform.CompareTag("Player"))
            {
                cansee=true;
            }
            else
            {
                cansee = false;
            }
        }
    }
    IEnumerator Shoot()
    {
        //gun flash
        GameObject f = Instantiate(FlashPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
        Destroy(f, 0.1f);

        //gun sound
        if (GunSound != null)
        {
            GunSound.PlayOneShot(GunSound.clip);
        }

        if(cansee && PlayerHealth!=null)
        {
            PlayerHealth.TakeDamage(10);
        }

        yield return new WaitForSeconds(4f);
        isshooting = false;
    }
  
}
