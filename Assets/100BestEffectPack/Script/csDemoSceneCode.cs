using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csDemoSceneCode : MonoBehaviour
{
    public string[] EffectNames;
    public string[] Effect2Names;
    public Transform[] Effect;

    public Text Text1;   // GUIText → Text

    int i = 0;
    int a = 0;

    void Start()
    {
        Instantiate(Effect[i], new Vector3(0, 5, 0), Quaternion.identity);
    }

    void Update()
    {
        Text1.text = (i + 1) + " : " + EffectNames[i];

        if (Input.GetKeyDown(KeyCode.Z))
        {
            i = (i <= 0) ? 99 : i - 1;
            SpawnEffect();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            i = (i < 99) ? i + 1 : 0;
            SpawnEffect();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnEffect();
        }
    }

    void SpawnEffect()
    {
        for (a = 0; a < Effect2Names.Length; a++)
        {
            if (EffectNames[i] == Effect2Names[a])
            {
                Instantiate(Effect[i], new Vector3(0, 0.2f, 0), Quaternion.identity);
                return;
            }
        }

        Instantiate(Effect[i], new Vector3(0, 5, 0), Quaternion.identity);
    }
}
