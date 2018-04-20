using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Lightning : MonoBehaviour
{
    [SerializeField]
    private Transform[] Positions;
    [SerializeField]
    private ParticleSystem[] Lightning;
    [SerializeField]
    private float DurationInSeconds = 10;
    [SerializeField]
    private bool LightningTest = false;
    [SerializeField]
    private ParticleSystem LightningPrefab;

    private Quaternion up;

    void Start()
    {
        up = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        Lightning = new ParticleSystem[Positions.Length];

        int i = 0;
        foreach (Transform position in Positions)
        {
            Lightning[i] = Instantiate(LightningPrefab, position.position, up);
            Lightning[i].transform.SetParent(position);
            Lightning[i].transform.localRotation = up;
            Lightning[i].Pause();
            i++;
        }
    }

    private void Update()
    {
        if (LightningTest)
        {
            StartCoroutine(_PlayFlames(DurationInSeconds));
            LightningTest = false;
        }
    }

    public IEnumerator _PlayFlames(float duration)
    {
        foreach (ParticleSystem l in Lightning)
        {
            if (!l.isPlaying)
            {
                l.Play(true);
            }
        }

        yield return new WaitForSeconds(duration);

        foreach (ParticleSystem l in Lightning)
        {
            if (l.isPlaying)
            {
                l.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}
