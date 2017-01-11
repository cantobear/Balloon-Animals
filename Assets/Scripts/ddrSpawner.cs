using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ddrSpawner : MonoBehaviour {

    public float spawnTime = 5f;        // The amount of time between each spawn.
    public float spawnDelay = 3f;       // The amount of time before spawning starts.
    public float intervalDelay = 3f;   // The amount of time between each set of 10 notes      
    public GameObject[] notes;        // Array of enemy prefabs.
    public int PrefabLimit = 10;
    public int count;

    public void Start()
    {
        notes = Resources.LoadAll<GameObject>("DDRnotes");
        // Start calling the Spawn function repeatedly after a delay .
        InvokeR(spawnDelay);
    }

    void InvokeR(float delay1)
    {
        count = 0;
        InvokeRepeating("Spawn", delay1, spawnTime);
    }

    void Spawn()
    {
        // Instantiate a random enemy.
        int enemyIndex = Random.Range(0, notes.Length);
        Instantiate(notes[enemyIndex], transform.position, transform.rotation);
        count++;
        if (count >= 10)
        {
            CancelInvoke();
            InvokeR(intervalDelay);
        }
    }

    // Update is called once per frame
    void Update () {
        
	}
}
