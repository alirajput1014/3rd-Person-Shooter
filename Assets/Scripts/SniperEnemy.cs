using UnityEngine;
using System.Collections;

public class SniperEnemy : MonoBehaviour
{
    public Transform player;
  
    public float DetectDistance;
    public float ShootWait = 0.4f;
    public Animator anim;
    private bool isShooting = false;
    private bool Cansee = false;
    private PlayerHealth playerHealth;
    private Vector3 lastknownposition;
    public GameObject FirePoint;
    public Transform firepoint;
    public GameObject FlashPrefab;
    public AudioSource GunSound;
    public LayerMask whatIsTarget;
    public LineRenderer laser;
    private Vector3 laserend;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        laser.enabled = false;
    }

    void Update()
    {
        Checkvision();
        if (laser.enabled)
        {
            laser.SetPosition(0, firepoint.position);
            laser.SetPosition(1, laserend);
        }
        if (Cansee && !isShooting)
        {
            isShooting=true;
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
       // dir.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 5f * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 1.5f, dir, out hit, DetectDistance,whatIsTarget))
        {
            Debug.Log(hit.collider.name);
            Cansee = hit.transform.CompareTag("Player");
            laserend = hit.point;
        }
        else
        {
            Cansee = false;
        }
        if (Cansee)
        {
            lastknownposition=player.position;
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("isshooting", true);

        Vector3 shootDir = (lastknownposition - transform.position).normalized;
        transform.forward = shootDir;

        laser.enabled = true;
        laser.SetPosition(0, firepoint.position);
        laser.SetPosition(1, laserend);

        GameObject f = Instantiate(FlashPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
        Destroy(f, 0.1f);

        if (GunSound != null)
            GunSound.PlayOneShot(GunSound.clip);

        if (Cansee && playerHealth != null)
            playerHealth.TakeDamage(10);

        yield return new WaitForSeconds(0.4f);

        anim.SetBool("isshooting", false);

        yield return new WaitForSeconds(5f);

        isShooting = false;
    }

}
