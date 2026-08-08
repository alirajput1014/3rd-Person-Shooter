using UnityEngine;

public class MedKit : MonoBehaviour
{
    public float minHeal = 20f;
    public float maxHeal = 40f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                float healAmount = Random.Range(minHeal, maxHeal);
                ph.Heal(healAmount);

                Destroy(gameObject);
            }
        }
    }
}