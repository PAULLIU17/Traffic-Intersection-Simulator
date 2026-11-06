using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //[SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<int> unavailableIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        unavailableIndex = new List<int>();
    }

    // numOfCars:
    // 3 = easy,
    // 5 = medium,
    // 7 = hard
    public void SpawnCars(int numOfCars)
    {
        if (spawnPoints.Length == 0) return;

        for (int i = 0; i < numOfCars; i++)
        {
            // Chooses a random index from 0-7
            int randomIndex = Random.Range(0, spawnPoints.Length);

            // Checks to see if index was already chosen
            // And if so, continue choosing a random index until a new index is chosen
            if(unavailableIndex.Contains(randomIndex))
            {
                while(unavailableIndex.Contains(randomIndex))
                {
                    randomIndex = Random.Range(0, spawnPoints.Length);
                }
            }

            unavailableIndex.Add(randomIndex);
            Vector3 randomPoint = spawnPoints[randomIndex].position;

            Quaternion rotation = Quaternion.Euler(0, 0, 0);

            switch(randomIndex)
            {
                //Northwest
                case 0:
                case 1:
                    rotation = Quaternion.Euler(0, 0, 0);
                    break;
                //Northeast
                case 2:
                case 3:
                    rotation = Quaternion.Euler(0, 90, 0);
                    break;
                //Southwest
                case 4:
                case 5:
                    rotation = Quaternion.Euler(0, -90, 0);
                    break;
                //Southeast
                case 6:
                case 7:
                    rotation = Quaternion.Euler(0, -180, 0);
                    break;
            }

            Instantiate(carPrefab, randomPoint, rotation);

        }
    }
}
