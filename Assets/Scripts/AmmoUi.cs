using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class AmmoUi : MonoBehaviour
{
    public TextMeshProUGUI TotalAmmo;
    public TextMeshProUGUI magzine;
    public int ta=120;
    public int m=30;
    private bool flag=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateAmmo(int ua)
    {
        m = m - ua;
        if (m < 1)
        {
            ta = ta-30;
            if (flag == false)
            {
                m = 30;
            }
            if (ta == 0)
            {
                flag=true;
            }
           
        }
        if(ta==0 && m == 0)
        {
            Debug.Log("ammos are finished");
        
        }
        UpdateUI();
    }
    void UpdateUI()
    {
        TotalAmmo.text=ta.ToString();
        magzine.text=m.ToString();
    }
}
