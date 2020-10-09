using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    //In Seconds
    public int classesLenght = 300;
    public int breakLength = 30;

    public GameObject zombie;

    GameObject Player;

    GameObject[] spawn;

    float timer = 0;
    bool isBreak = false;

    int lights = 4;

    int difficulty = 1;

    GameObject[] spawnedZombies;

    public GameObject UI_Object;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectsWithTag("Spawn");
        Debug.Log(spawn.Length);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isBreak)
        {
            if (timer >= breakLength)
            {
                Debug.Log("BREAK!");
                timer = 0;
                isBreak = false;
                startClasses();
            }
        }
        else {
            if (timer >= classesLenght)
            {
                Debug.Log("CLASSES!");
                timer = 0;
                isBreak = true;
                startBreak();
                
            }
        }
    }

    void startBreak() {
        //Spawn zombies nearby
        //Based on ammount of lights

        //get closest 4
        Vector3[] closeSpawn = new Vector3[4];
        float[] spawnDistance = new float[closeSpawn.Length];
        /*
        for every spawn location
            if higher than highest
                    set new location
            else if higher then secon
                set new second higher
            else if
            */

                for (int i = 0; i < spawn.Length; i++) {
            float dist = Vector3.Distance(Player.transform.position, spawn[i].transform.position);
                for (int j = 0; j < closeSpawn.Length; j++) {
                if (dist < Vector3.Distance(Player.transform.position, spawn[j].transform.position)) {
                    Debug.Log("Value = " + j);
                    spawnDistance[j] = dist;
                    closeSpawn[i] = spawn[i].transform.position;
                    Debug.Log("Set Position");
                    break;
                    }
                }
        }

        int locDifficulty = difficulty + (4 - lights);
        foreach (Vector3 spawnLocation in closeSpawn) {
            for (int i = 0; i < locDifficulty; i++)
            {
                
                Vector3 loc = spawnLocation;
                loc.x += i * 3;
                loc.z += i * 3;
                Debug.Log(loc.y);
                spawnedZombies[i] = Instantiate(zombie, loc, new Quaternion(0, 0, 0, 0)) as GameObject;
            }
        }
    }

    void startClasses() {
        difficulty++;

        //remove all spawned zombies
        if (spawnedZombies.Length != 0 && spawnedZombies != null)
        {
            foreach (GameObject zombie in spawnedZombies)
            {
                zombie.GetComponent<EnemyController>().MoveToSpawn(getSpawnLocations());
            }
            Invoke("removeAllZombies", 15.0f);
        }
    }

    public void removeLigth() {
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
        foreach (GameObject zombie in spawnedZombies)
        {
            Destroy(zombie);
        }
    }

}
