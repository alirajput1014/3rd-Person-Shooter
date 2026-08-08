using System.Collections;
using UnityEngine;

public class CheckPointEnemy : MonoBehaviour
{
    public Transform player;
    public float DetectDistance;
    public float ShootWait = 0.4f;
    public float CrouchWaitTime = 2f;
    public Animator anim;

    private bool isShooting = false;
    private bool Cansee = false;
    private PlayerHealth playerHealth;
    private Vector3 lastknownposition;
    public GameObject FirePoint;
    public GameObject FlashPrefab;
    public AudioSource GunSound;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        Checkvision();
        if (Cansee && !isShooting)
        {
            StartCoroutine(Attack());
        }
    }

    void Checkvision()
    {
        float Distance = Vector3.Distance(transform.position, player.position);
        if (Distance > DetectDistance)
        {
            Cansee = false;
            return;
        }
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 5f * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 1.5f, dir, out hit, DetectDistance))
        {
            Cansee = hit.transform.CompareTag("Player");
        }
        if (Cansee)
        {
            lastknownposition=player.position;
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
           

            isShooting = true;

            // Shoot
            anim.SetBool("isshooting", true);
            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < 3; i++)
            {

                Vector3 shootDir = (lastknownposition - transform.position).normalized;

                transform.forward = new Vector3(shootDir.x, 0, shootDir.z);

                //gun flash
                GameObject f = Instantiate(FlashPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
                Destroy(f, 0.1f);

                //gun sound
                if (GunSound != null)
                {
                    GunSound.PlayOneShot(GunSound.clip);
                }


                if (Cansee && playerHealth != null)
                    playerHealth.TakeDamage(10);

                yield return new WaitForSeconds(ShootWait);
            }

            // Stop shooting
            anim.SetBool("isshooting", false);
            yield return new WaitForSeconds(5f);

            // Crouch
            anim.SetBool("iscrouch", true);

            // Crouch wait
            yield return new WaitForSeconds(CrouchWaitTime);

            // Stand up
            anim.SetBool("iscrouch", false);

            yield return new WaitForSeconds(0.5f);

            isShooting = false;
        }
    }
}