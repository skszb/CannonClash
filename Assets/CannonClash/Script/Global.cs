using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class Global : MonoBehaviour
{
    // pirate ships spawn
    public GameObject pirateShipL;
    public GameObject pirateShipR;
    private float spawnTimer;
    private float spawnPeriod;
    private int numberSpawnedEachPeriod;
    private List<int> spaceChecker;

    // game difficulty increment
    private float gameTimer;
    private float gamePeriod;

    // score tracking
    private int score;
    public TextMeshProUGUI tmp;

    //background music
    private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(10f, 15f);
        spawnPeriod = 20f;
        numberSpawnedEachPeriod = 1;
        gameTimer = 0;
        gamePeriod = 60f;
        score = 0;
        tmp.text = 0.ToString();

        spaceChecker = new List<int>();
        for (int i = 0; i < 16; i++)
        {
            spaceChecker.Add(0);
        }

        backgroundMusic = GetComponent<AudioSource>();
        playMusic();
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
            HashSet<int> spawned = new HashSet<int>();
            while (!find)
            {
                posZ = (int)Random.Range(2f, 8.99f);
                if (!spawned.Contains(posZ) && spaceChecker[posZ] >= 0)
                {
                    spawned.Add(posZ);
                    spaceChecker[posZ] += 1;
                    find = true;
                }
            }
            Vector3 spawnPos = new Vector3(Random.Range(-250f, -200f), 0f, posZ * 7f);
            GameObject obj = Instantiate(pirateShipL, spawnPos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
            pirate p = obj.GetComponent<pirate>();
            float number = 0;
            if (posZ == 2)
            {
                number = Random.Range(9.5f, 10f);
                
            }
            else if (posZ==3)
            {
                number = Random.Range(13.5f, 14f);
            }
            else if (posZ == 4)
            {
                number = Random.Range(16.5f, 17f);
            }
            else if (posZ == 5)
            {
                number = Random.Range(18.5f, 19f);
            }
            else if(posZ == 6)
            {
                number = Random.Range(20.5f, 21f);
            }
            else if (posZ == 7)
            {
                number = Random.Range(22.5f, 23f);
            }
            else if (posZ == 8)
            {
                number = Random.Range(24.5f, 25f);
            }
            p.setpirateshotpower(number);
            p.dir = 1f;
            p.row = posZ;
        }
        for (int j = 0; j < rightNum; j++)
        {
            bool find = false;
            int posZ = 0;
            HashSet<int> spawned = new HashSet<int>();
            while (!find)
            {
                posZ = (int)Random.Range(2f, 8.99f);
                if (!spawned.Contains(posZ) && spaceChecker[posZ] <= 0)
                {
                    spawned.Add(posZ);
                    spaceChecker[posZ] -= 1;
                    find = true;
                }
            }
            Vector3 spawnPos = new Vector3(Random.Range(200f, 250f), 0f, posZ * 7f);
            GameObject obj = Instantiate(pirateShipR, spawnPos, Quaternion.Euler(0f, -90f, 0f)) as GameObject;
            pirate p = obj.GetComponent<pirate>();
            float number = 0;
            if (posZ == 2)
            {
                number = Random.Range(9.5f, 10f);

            }
            else if (posZ == 3)
            {
                number = Random.Range(13.5f, 14f);
            }
            else if (posZ == 4)
            {
                number = Random.Range(16.5f, 17f);
            }
            else if (posZ == 5)
            {
                number = Random.Range(18.5f, 19f);
            }
            else if (posZ == 6)
            {
                number = Random.Range(20.5f, 21f);
            }
            else if (posZ == 7)
            {
                number = Random.Range(22.5f, 23f);
            }
            else if (posZ == 8)
            {
                number = Random.Range(24.5f, 25f);
            }
            p.setpirateshotpower(number);
            p.dir = -1f;
            p.row = posZ;
        }
    }

    private void IncreaseDifficulty()
    {
        spawnPeriod -= 2f;
        numberSpawnedEachPeriod += 1;
        gamePeriod += 30f;
    }

    public void increScore() 
    {
        score++;
        tmp.text = score.ToString();
    }

    public void leftShipDestroy(int row) 
    {
        spaceChecker[row] -= 1;
    }

    public void rightShipDestroy(int row) 
    {
        spaceChecker[row] += 1;
    }

    private void playMusic() 
    {
        if (!backgroundMusic.isPlaying)
            backgroundMusic.Play();
    }

    private void stopMusic() 
    {
        if (backgroundMusic.isPlaying)
            backgroundMusic.Stop();
    }

    public void gameOver() 
    {
        // clean up
        CannonBall[] cannonballs = FindObjectsOfType<CannonBall>();
        foreach (CannonBall obj in cannonballs)
        {
            Destroy(obj.gameObject);
        }
        pirate[] pirates = FindObjectsOfType<pirate>();
        foreach (pirate obj in pirates)
        {
            Destroy(obj.gameObject);
        }
        stopMusic();
    }
}
