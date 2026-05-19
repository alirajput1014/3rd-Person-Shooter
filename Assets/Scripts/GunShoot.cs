using System.Collections;// use for coroutine
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;
    public float FireRate = 0.1f;

    bool shooting=false;

    public GameObject FlashPrefab;
    public GameObject firePoint;

    public AudioSource gunaudio;

    public GameObject blood;

    public CameraShake camShake;


    //add layermask to detect everything except player so that ray pass through player and dont detect it
    public LayerMask hitlayers;

    // Update is called once per frame
    void Update()
    {
        //left mouse button hold
        if (Input.GetMouseButtonDown(0))
        {
            shooting = true;
            StartCoroutine(ShootRoutine());
        }
        //left mouse button release
        if (Input.GetMouseButtonUp(0))
        {
            shooting = false;
        }
        
    }

    private IEnumerator ShootRoutine()
    {
        //as long as left mouse button hold 
        while (shooting==true)
        {
            shoot();
            yield return new WaitForSeconds(FireRate);
        }
    }
    void shoot()
    {
        if (camShake != null) camShake.shake(0.5f, 0.3f);

        //gun flash
        GameObject flash = Instantiate( FlashPrefab,firePoint.transform.position, firePoint.transform.rotation);
        Destroy(flash, 0.1f); 

        //gun sound
        if(gunaudio != null)
        {
            gunaudio.PlayOneShot(gunaudio.clip);
        }

        //ray gives origin and direction for raycast
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //draw ray for debug
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.1f);

        //check that the ray is collide or not if coliide then show the name of object
        if (Physics.Raycast(ray,out hit, range,hitlayers))
        {
            Debug.Log("Hit" + hit.transform.name);

            if (hit.transform.CompareTag("Enemy"))
            {
                Health enemy = hit.transform.GetComponentInParent<Health>();

                if (enemy != null)
                {
                    GameObject b = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(b, 1);

                    enemy.TakeDamage(10);
                }
            }
        }
       
    }
    
}
