using TMPro;
using UnityEngine;

public class AmmoUi : MonoBehaviour
{
    public TextMeshProUGUI totalAmmoText;
    public TextMeshProUGUI magAmmoText;

    public int totalAmmo = 120;
    public int maxTotalAmmo = 210;
    public int magAmmo = 30;
    public int magSize = 30;

    void Start()
    {
        UpdateUI();
    }

    public void ShootBullet()
    {
        if (magAmmo > 0)
        {
            magAmmo--;
            UpdateUI();
        }
    }

    public void Reload()
    {
        if (totalAmmo <= 0)
            return;

        magAmmo = 30;
        totalAmmo -= 30;

        if (totalAmmo < 0)
            totalAmmo = 0;

        UpdateUI();
    }
    public void AddAmmo(int amount)
    {
        totalAmmo =Mathf.Min(totalAmmo + amount, maxTotalAmmo);
        UpdateUI();
    }

    void UpdateUI()
    {
        totalAmmoText.text = totalAmmo.ToString();
        magAmmoText.text = magAmmo.ToString();
    }
}