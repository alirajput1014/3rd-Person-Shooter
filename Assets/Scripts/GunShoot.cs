using System.Collections;// use for coroutine
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Camera cam;
    public float range = 100f;
    public float FireRate = 0.1f;

    public bool shooting = false;

    bool isReloading = false;

    public GameObject FlashPrefab;
    public GameObject firePoint;

    public AudioSource gunaudio;

    public GameObject blood;

    public CameraShake camShake;

    public AmmoUi ammoUi;

    public Animator anim;


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

        //manual reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }

    }

    private IEnumerator ShootRoutine()
    {
        //as long as left mouse button hold 
        while (shooting == true)
        {
            shoot();
            yield return new WaitForSeconds(FireRate);
        }
    }

    void shoot()
    {
        //Do not reload during reloading
        if (isReloading)
            return;

        //if all the ammos are finished
        if (ammoUi.magAmmo <= 0 && ammoUi.totalAmmo <= 0)
        {
            Debug.Log("Out Of Ammo");
            return;
        }

        //mag empty
        if (ammoUi.magAmmo <= 0)
        {
            StartReload();
            return;
        }

        if (camShake != null)
            camShake.shake(0.1f, 0.02f);

        //gun flash
        GameObject flash = Instantiate(
            FlashPrefab,
            firePoint.transform.position,
            firePoint.transform.rotation);

        Destroy(flash, 0.1f);

        //gun sound
        if (gunaudio != null)
        {
            gunaudio.Play();
        }

        //update Ammo UI
        ammoUi.ShootBullet();

        //ray gives origin and direction for raycast
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //draw ray for debug
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.1f);

        //check that the ray is collide or not if coliide then show the name of object
        if (Physics.Raycast(ray, out hit, range, hitlayers))
        {
            Debug.Log("Hit " + hit.transform.name);

            if (hit.transform.CompareTag("Enemy"))
            {
                Health enemy = hit.transform.GetComponentInParent<Health>();

                if (enemy != null)
                {
                    GameObject b = Instantiate(
                        blood,
                        hit.point,
                        Quaternion.LookRotation(hit.normal));

                    Destroy(b, 1);

                    enemy.TakeDamage(10);
                }
            }
        }

    }

    void StartReload()
    {
        //already reloading
        if (isReloading)
            return;

        //if total ammo are finsished
        if (ammoUi.totalAmmo <= 0)
            return;

        StartCoroutine(ReloadRoutine());
    }
    IEnumerator ReloadRoutine()
    {
        isReloading = true;

        Debug.Log("Reloading");

        //reload animation
        anim.SetTrigger("Reload");

        //reload time
        yield return new WaitForSeconds(3.5f);

        //ammo refill
        ammoUi.Reload();

        isReloading = false;
    }

}