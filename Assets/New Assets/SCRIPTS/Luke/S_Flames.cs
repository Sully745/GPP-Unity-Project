using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Flames : MonoBehaviour
{
    [SerializeField]
    private Transform LeftFoot;
    [SerializeField]
    private Transform RightFoot;
    [SerializeField]
    private float DurationInSeconds = 10;
    [SerializeField]
    private bool FlamesTest = false;
    [SerializeField]
    private bool SmokeTest = false;
    [SerializeField]
    private ParticleSystem FlamesPrefab;
    [SerializeField]
    private ParticleSystem SmokePrefab;

    private Quaternion up;

    private ParticleSystem LeftFlames;
    private ParticleSystem RightFlames;

    private ParticleSystem LeftSmoke;
    private ParticleSystem RightSmoke;

    void Start()
    {
        up = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        LeftFlames = Instantiate(FlamesPrefab, LeftFoot.position, up);
        LeftFlames.transform.SetParent(LeftFoot);
        LeftFlames.transform.localRotation = up;
        LeftFlames.Pause();

        RightFlames = Instantiate(FlamesPrefab, RightFoot.position, up);
        RightFlames.transform.SetParent(RightFoot);
        RightFlames.transform.localRotation = up;
        RightFlames.Pause();

        LeftSmoke = Instantiate(SmokePrefab, LeftFoot.position, up);
        LeftSmoke.transform.SetParent(LeftFoot);
        LeftSmoke.transform.localRotation = up;
        LeftSmoke.Pause();

        RightSmoke = Instantiate(SmokePrefab, RightFoot.position, up);
        RightSmoke.transform.SetParent(RightFoot);
        RightSmoke.transform.localRotation = up;
        RightSmoke.Pause();
    }

    private void Update()
    {
        if (FlamesTest)
        {
            StartCoroutine(_PlayFlames(DurationInSeconds));
            FlamesTest = false;
        }
        if (SmokeTest)
        {
            StartCoroutine(_PlaySmoke(DurationInSeconds));
            SmokeTest = false;
        }
    }

    public IEnumerator _PlayFlames(float duration)
    {
        if (!LeftFlames.isPlaying)
        {
            LeftFlames.Play(true);
        }
        if (!RightFlames.isPlaying)
        {
            RightFlames.Play(true);
        }

        yield return new WaitForSeconds(duration);

        if (LeftFlames.isPlaying)
        {
            LeftFlames.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        if (RightFlames.isPlaying)
        {
            RightFlames.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public IEnumerator _PlaySmoke(float duration)
    {
        duration = 1.5f;

        if (!LeftSmoke.isPlaying)
        {
            LeftSmoke.Play(true);
        }
        if (!RightSmoke.isPlaying)
        {
            RightSmoke.Play(true);
        }

        yield return new WaitForSeconds(duration);

        if (LeftSmoke.isPlaying)
        {
            LeftSmoke.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        if (RightSmoke.isPlaying)
        {
            RightSmoke.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}