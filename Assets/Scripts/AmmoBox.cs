using UnityEngine;

public class AmmoBox : MonoBehaviour
{
  
    public AmmoUi ammoUi; // Inspector me AmmoManager object drag karna
    public GameObject pickupEffect;

    public int[] possibleAmmoAmounts = { 30, 60, 90 };

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ammoUi != null)
            {
                int randomIndex = Random.Range(0, possibleAmmoAmounts.Length);
                int ammoAmount = possibleAmmoAmounts[randomIndex];

                ammoUi.AddAmmo(ammoAmount);

                if (pickupEffect != null)
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
    }
}