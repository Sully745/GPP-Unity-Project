using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ColourGradient : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] particleSystems;
    [SerializeField]
    private bool randomColours = true;
    private Gradient gradient;
    private GradientColorKey[] gck;
    private GradientAlphaKey[] gak;
    private bool waiting = false;

    void Update()
    {
        if (randomColours && !waiting)
        {
            StartCoroutine(_newColours());
        }
    }

    IEnumerator _newColours()
    {
        waiting = true;
        foreach (ParticleSystem ps in particleSystems)
        {
            var col = ps.colorOverLifetime;
            col.color = NewGradient();          
        }
        yield return new WaitForSeconds(5.0F);
        waiting = false;
    }

    Gradient NewGradient()
    {
        gradient = new Gradient();

        gck = new GradientColorKey[2];
        gck[0].color = new Color(rn(), rn(), rn());
        gck[0].time = 0.33F;
        gck[1].color = new Color(rn(), rn(), rn());
        gck[1].time = 0.66F;

        gak = new GradientAlphaKey[3];
        gak[0].alpha = 0.0F;
        gak[0].time = 0.0F;
        gak[1].alpha = 1.0F;
        gak[1].time = 0.50F;
        gak[2].alpha = 0.0F;
        gak[2].time = 1.0F;

        gradient.SetKeys(gck, gak);

        return gradient;
    }

    float rn()
    {
        return Random.Range(0.0F, 1.0F);
    }
}
