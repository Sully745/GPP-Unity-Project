﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{

    public GameObject[] upgrades;
    public GameObject spawner;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    int randUpgrade;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }
    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            randUpgrade = Random.Range(0, 1);

            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));
            Instantiate(upgrades[randUpgrade], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);

            yield return new WaitForSeconds(spawnWait);
        }
        //Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));
        //Instantiate(upgrades[randUpgrade], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
    }
}
