using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public float maxhealth = 100;
    float currenthealth;
    public Animator anim;
    bool isdead = false;

    public ThirdPersonMovement ThirdPersonMovement;
    public GunShoot GunShoot;

    public GameObject Blood;

    public Image FillHealth;
    private EnemyAI ai;
    public SniperEnemy Sniperenemy;
    public CheckPointEnemy CheckPointEnemy;
    public FlankerEnemy flankerEnemy;
    public EnemyAttack EnemyAttack;

    public PulseEffect pulseEffect;


    void Start()
    {
        currenthealth = maxhealth;
    }
    public void TakeDamage(float damage)
    {
       

        //Hit Animation
        anim.SetTrigger("Hit");

        //Blood Effect
        GameObject b=  Instantiate(Blood, transform.position+Vector3.up*1.2f,Quaternion.identity);
        Destroy(b,1f);

        //Update Health
        currenthealth = currenthealth - damage;
        Debug.Log(" Hit! Remaining HP" + currenthealth);
    //pulse effect
        pulseEffect.CheckHealth(currenthealth, maxhealth);

        //UI Bar
        FillHealth.fillAmount = currenthealth / maxhealth;

        if (currenthealth <= 0)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        currenthealth = Mathf.Min(currenthealth + amount, maxhealth); // maxhealth se zyada nahi jayega
        FillHealth.fillAmount = currenthealth / maxhealth;
        pulseEffect.CheckHealth(currenthealth, maxhealth);
    }
    void Die()
    {
        Debug.Log("dead");
        if (isdead) return;
        isdead = true;
        if(ThirdPersonMovement!=null) ThirdPersonMovement.enabled = false;
        if (GunShoot != null)
        {
            GunShoot.StopAllCoroutines();   
            GunShoot.shooting = false;     
            GunShoot.enabled = false;
        }
        // EnemyAI + NavMesh
        if (ai != null) ai.enabled = false;

        NavMeshAgent nav = GetComponent<NavMeshAgent>();
        if (nav != null) nav.enabled = false;

        // Sniper
        if (Sniperenemy != null)
        {
            Sniperenemy.laser.enabled = false;
            Sniperenemy.enabled = false;
        }

        // CheckPoint
        if (CheckPointEnemy != null)
        {
            CheckPointEnemy.StopAllCoroutines();
            CheckPointEnemy.enabled = false;
        }

        // Flanker
        if (flankerEnemy != null)
        {
            flankerEnemy.StopAllCoroutines();
            flankerEnemy.enabled = false;
        }
        if (EnemyAttack != null)
        {
            EnemyAttack.StopAllCoroutines();
            EnemyAttack.enabled = false;
        }



        anim.SetTrigger("Dead");
       // Destroy(gameObject, 5f);
        StartCoroutine(StopGame());

    }
    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0f;
        UIManager.Instance.ShowGameOver();
    }
}
