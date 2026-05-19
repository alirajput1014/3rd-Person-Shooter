using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public float maxhealth = 100;
    float currenthealth;
    public Animator anim;
    bool isdead=false;
    private EnemyAI ai;
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
        Debug.Log("dead");
        if(isdead) return;
        isdead= true;
        anim.SetTrigger("Death");
        if (ai != null) ai.enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        Destroy(gameObject,5f);
    }
}
