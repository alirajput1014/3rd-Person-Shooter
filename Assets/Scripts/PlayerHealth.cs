using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxhealth = 100;
    float currenthealth;
    public Animator anim;
    bool isdead = false;

    public ThirdPersonMovement ThirdPersonMovement;
    public GunShoot GunShoot;

    public GameObject Blood;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;
    }
    public void TakeDamage(float damage)
    {
        anim.SetTrigger("Hit");
        Instantiate(Blood, transform.position+Vector3.up*1.2f,Quaternion.identity);
        Destroy(Blood,1f);
        currenthealth = currenthealth - damage;
        Debug.Log(" Hit! Remaining HP" + currenthealth);

        if (currenthealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("dead");
        if (isdead) return;
        isdead = true;
        if(ThirdPersonMovement!=null) ThirdPersonMovement.enabled = false;
        if (GunShoot != null) GunShoot.enabled = false;
        anim.SetTrigger("Dead");
       // Destroy(gameObject, 5f);
        StartCoroutine(StopGame());

    }
    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0f;
    }
}
