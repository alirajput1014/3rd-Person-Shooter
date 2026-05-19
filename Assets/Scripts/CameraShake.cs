using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 startpos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.localPosition;
    }
    public void shake(float amount,float time)
    {
        Debug.Log("Shake called!");
        StopAllCoroutines();
        StartCoroutine(Shakeroutine(amount, time));
    }
    IEnumerator Shakeroutine(float amount,float time)
    {
        float t = 0;
        while (t < time)
        {
            transform.localPosition = startpos+Random.insideUnitSphere * amount;
            t += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = startpos;

    }
}
