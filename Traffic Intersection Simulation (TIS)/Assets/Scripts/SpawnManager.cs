using NUnit.Framework;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject[] carPrefab;
    [SerializeField] private Transform[] spawnPoints; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // numOfCars: 3 = easy, 5 = medium, 7 = hard
    public void SpawnCars(int numOfCars)
    {
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomPoint = spawnPoints[randomIndex];

        for (int i = 0; i < numOfCars; i++)
        {
            //SpawnAtRandomLocation();
        }
    }
}
