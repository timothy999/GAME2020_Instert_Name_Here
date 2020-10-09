using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    //In Seconds
    public int classesLenght = 300;
    public int breakLength = 30;

    public GameObject zombiePrefab;

    GameObject Player;

    GameObject[] spawn;

    float timer = 0;
    bool isBreak = false;

    int lights = 4;

    int difficulty = 1;

    List<GameObject> spawnedZombies = new List<GameObject>();

    public GameObject UI_Object;

    public LeverManager leverManager;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectsWithTag("Spawn");
        leverManager.totalAmountOfLevers = 4;
        leverManager.numberOfLeversOn = 4;
        Debug.Log(spawn.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(leverManager.numberOfLeversOn < lights)
        {
            removeLight();
        }

        timer += Time.deltaTime;
        if (isBreak)
        {
            if (timer >= breakLength)
            {

                Debug.Log("BREAK!");
                timer = 0;
                isBreak = false;
                startBreak();
            }
        }
        else {
            if (timer >= classesLenght)
            {
                Debug.Log("CLASSES!");
                timer = 0;
                isBreak = true;
                startClasses();
                
            }
        }
    }

    void startBreak()
    {

        List<Vector3> closeSpawn = new List<Vector3>();
        foreach (GameObject spawnLocal in spawn)
        {
            closeSpawn.Add(spawnLocal.transform.position);
        }

        int locDifficulty = difficulty + (4 - lights);
        Debug.Log("Amount of zombies is : " + closeSpawn.Count);
        foreach (Vector3 spawnLocation in closeSpawn)
        {
            for (int i = 0; i < locDifficulty; i++)
            {
                Vector3 loc = spawnLocation;
                loc.x += i * 3;
                loc.z += i * 3;
                spawnedZombies.Add(Instantiate(zombiePrefab, loc, new Quaternion(0, 0, 0, 0)));
            }
        }
    }

    void startClasses() {
        difficulty++;

        //remove all spawned zombies
            Debug.Log("Removing zombies");
            foreach (GameObject zombie in spawnedZombies)
            {
                zombie.GetComponent<EnemyController>().MoveToSpawn(getSpawnLocations());
            }
            Invoke("removeAllZombies", 1.0f);
    }

    public void removeLight() {
        lights--;
    }

    public Vector3[] getSpawnLocations() {
        Vector3[] spawnLocations = new Vector3[spawn.Length];
        for (int i = 0; i < spawn.Length; i++) {
            spawnLocations[i] = spawn[i].transform.position;
        }
        return spawnLocations;
    }

    void removeAllZombies() {
        Debug.Log("Removed Zombies");
        Debug.Log(spawnedZombies.Count);
        foreach (GameObject zombie in spawnedZombies) {
            Destroy(zombie);
        }
        spawnedZombies.Clear();
        spawnedZombies = new List<GameObject>();
        Debug.Log(spawnedZombies.Count);
    }

}
