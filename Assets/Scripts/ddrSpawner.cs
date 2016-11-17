using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ddrSpawner : MonoBehaviour {

    public float spawnTime = 5f;        // The amount of time between each spawn.
    public float spawnDelay = 3f;       // The amount of time before spawning starts.        
    public GameObject[] notes;        // Array of enemy prefabs.
    public int PrefabLimit = 10;
    public int count;

    public void Start()
    {
        notes = Resources.LoadAll<GameObject>("DDRnotes");
        // Start calling the Spawn function repeatedly after a delay .
        count = 0;
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    void Spawn()
    {
        // Instantiate a random enemy.
        int enemyIndex = Random.Range(0, notes.Length);
        Instantiate(notes[enemyIndex], transform.position, transform.rotation);
        count++;
    }

    // Update is called once per frame
    void Update () {
        if (count >= 10)
            CancelInvoke();
	}
}
