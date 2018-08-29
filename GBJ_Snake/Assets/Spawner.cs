using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
    public static Spawner Instance;
    public static float difficulty = 1f;
    public GameObject playerPrefab, enemyPrefab1, enemyPrefab2;
    public Text difficultyText;

    public void SpawnPlayerWithDelay(float delay = 1)
    {
        Invoke("SpawnPlayer", delay);
        difficulty /= 1.1f;
        if (difficulty < 0.5f) difficulty = 0.5f;
        difficultyText.text = "Difficulty: " + ((int)(100*difficulty))/100f;
    }

    public void SpawnEnemy1WithDelay(float delay = 5)
    {
        Invoke("SpawnEnemy1", delay);
        difficulty *= 1.1f;
        difficultyText.text = "Difficulty: " + ((int)(100 * difficulty)) / 100f;
    }
    public void SpawnEnemy2WithDelay(float delay = 5)
    {
        Invoke("SpawnEnemy2", delay);
        difficulty *= 1.1f;
        difficultyText.text = "Difficulty: " + ((int)(100 * difficulty)) / 100f;
    }


    void SpawnPlayer()
    {
        Vector3 position = new Vector3(Random.Range(-100, 100), 50, Random.Range(-100, 100));
        Instantiate(playerPrefab, position, Quaternion.identity);
    }

    void SpawnEnemy1()
    {
        Vector3 position = new Vector3(Random.Range(-100, 100), 50, Random.Range(-100, 100));
        Instantiate(enemyPrefab1, position, Quaternion.identity);
    }
    void SpawnEnemy2()
    {
        Vector3 position = new Vector3(Random.Range(-100, 100), 50, Random.Range(-100, 100));
        Instantiate(enemyPrefab2, position, Quaternion.identity);
    }


    // Use this for initialization
    void Start ()
    {
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;
	}

}
