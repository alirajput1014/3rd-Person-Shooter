using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public float maxhealth = 100;
    float currenthealth;
    public Animator anim;
    bool isdead=false;
    private EnemyAI ai;
    public SniperEnemy Sniperenemy;
    public CheckPointEnemy CheckPointEnemy;
    public FlankerEnemy flankerEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;
        ai= GetComponent<EnemyAI>();
    }
    public void TakeDamage(float damage)
    {
        anim.SetTrigger("Hit");
        currenthealth=currenthealth-damage;
        Debug.Log(" Hit! Remaining HP" + currenthealth);

        if(currenthealth <=0 )
        {
            Die();
        }
    }
    void Die()
    {
        if (isdead) return;
        isdead = true;

        anim.SetTrigger("Death");

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

        Destroy(gameObject, 5f);
    }
}
