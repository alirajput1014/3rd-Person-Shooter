using UnityEngine;
using UnityEngine.UI;

public class PulseEffect : MonoBehaviour
{
    public Image bloodImage;
    public float threshold = 0.3f; // 30% health se neeche pulse chalega

    bool lowHealth = false;

    void Update()
    {
        if (lowHealth)
        {
            float alpha = (Mathf.Sin(Time.time * 2f) + 1f) / 2f * 0.4f;
            Color c = bloodImage.color;
            c.a = alpha;
            bloodImage.color = c;
        }
        else
        {
            Color c = bloodImage.color;
            c.a = 0f;
            bloodImage.color = c;
        }
    }

    public void CheckHealth(float current, float max)
    {
        lowHealth = (current / max) <= threshold;
    }
}