using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Global : MonoBehaviour
{
    // pirate ships spawn
    public GameObject pirateShipL;
    public GameObject pirateShipR;
    private float spawnTimer;
    private float spawnPeriod;
    private int numberSpawnedEachPeriod;

    // game difficulty increment
    private float gameTimer;
    private float gamePeriod;

    // score tracking
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(5f, 7f);
        spawnPeriod = 15f;
        numberSpawnedEachPeriod = 1;
        gameTimer = 0;
        gamePeriod = 30f;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnPeriod)
        {
            spawnTimer = 0;
            Spawn();
        }
        gameTimer += Time.deltaTime;
        if (gameTimer >= gamePeriod)
        {
            gameTimer = 0;
            IncreaseDifficulty();
        }
    }

    private void Spawn()
    {
        List<int> spaceChecker = new List<int>();
        for (int i = 0; i < 13; i++)
        {
            spaceChecker.Add(0);
        }
        int leftNum = 0;
        int rightNum = 0;
        if (numberSpawnedEachPeriod > 1)
        {
            leftNum = (int)Random.Range(1.0f, numberSpawnedEachPeriod - 0.1f);
            rightNum = numberSpawnedEachPeriod - leftNum;
        }
        else
        {
            float roll = Random.Range(0f, 1f);
            if (roll >= 0.5f)
            {
                leftNum = 1;
            }
            else
            {
                rightNum = 1;
            }
        }
        for (int i = 0; i < leftNum; i++)
        {
            bool find = false;
            int posZ = 0;
            while (!find)
            {
                posZ = (int)Random.Range(2f, 12.5f);
                if (spaceChecker[posZ] == 0)
                {
                    spaceChecker[posZ] = 1;
                    find = true;
                }
            }
            Vector3 spawnPos = new Vector3(Random.Range(-250f, -200f), 0f, posZ * 10f);
            GameObject obj = Instantiate(pirateShipL, spawnPos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
            pirate p = obj.GetComponent<pirate>();
            p.dir = 1f;
        }
        for (int j = 0; j < rightNum; j++)
        {
            bool find = false;
            int posZ = 0;
            while (!find)
            {
                posZ = (int)Random.Range(2f, 12.5f);
                if (spaceChecker[posZ] == 0)
                {
                    spaceChecker[posZ] = 1;
                    find = true;
                }
            }
            Vector3 spawnPos = new Vector3(Random.Range(200f, 250f), 0f, posZ * 10f);
            GameObject obj = Instantiate(pirateShipR, spawnPos, Quaternion.Euler(0f, -90f, 0f)) as GameObject;
            pirate p = obj.GetComponent<pirate>();
            p.dir = -1f;
        }
    }

    private void IncreaseDifficulty()
    {
        spawnPeriod -= 2f;
        numberSpawnedEachPeriod += 1;
        gamePeriod += 30f;
    }
}
