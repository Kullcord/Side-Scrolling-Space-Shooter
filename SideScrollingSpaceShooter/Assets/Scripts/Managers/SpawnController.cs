using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;

    public bool canSpawn;

    private Vector3 previewsPos;

    public float waveWait;

    public float waitToStart;
    public float waitToStart2;
    public float waitToStart3;

    public int fightersToSpawn;

    public GameObject[] spawnArray;

    private void Start()
    {
        for(int i = 0; i < fightersToSpawn; i++)
        {
            StartCoroutine(SpawnWaves("Enemy1", waitToStart, waveWait));
        }

        StartCoroutine(SpawnWaves("Enemy2", waitToStart2, waveWait));
        StartCoroutine(SpawnWaves("Asteroid", waitToStart3, waveWait));   
    }

    bool done = false;

    IEnumerator SpawnWaves(string tag, float waitToStart, float waitTillNextWave)
    {
        yield return new WaitForSeconds(waitToStart);

        done = false;
        canSpawn = true;
        GameObject obj = ObjectPooling.instance.GetPooledObjects(tag);

        if (obj == null)
            yield return null;

        if (canSpawn)
        {
            if (!done) { }
            {
                int spawnPos = Random.Range(0, spawnArray.Length);

                done = true;

                GameObject go = spawnArray[spawnPos];

                if (previewsPos != go.transform.position)
                {
                    obj.transform.position = go.transform.position;
                    obj.transform.rotation = transform.rotation;
                    obj.SetActive(true);

                    previewsPos = go.transform.position;
                    canSpawn = false;
                }
            }
        }

        yield return new WaitForSeconds(waitTillNextWave);
        StartCoroutine(SpawnWaves(tag, waitToStart, waitTillNextWave));
    }              
}

