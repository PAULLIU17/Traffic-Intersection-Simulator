using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //[SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<int> unavailableIndex;

    [SerializeField] private List<GameObject> listOfCars;
    private List<GameObject> listOfArrows;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unavailableIndex = new List<int>();
        listOfCars = new List<GameObject>();
        listOfArrows = new List<GameObject>();
    }

    // numOfCars:
    // 3 = easy
    // 5 = medium
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
                    rotation = Quaternion.Euler(0, 0, 0);
                    listOfCars.Add(Instantiate(carPrefab, randomPoint, rotation));
                    listOfCars[i].name = "NW Inner Car";
                    break;
                case 1:
                    rotation = Quaternion.Euler(0, 0, 0);
                    listOfCars.Add(Instantiate(carPrefab, randomPoint, rotation));
                    listOfCars[i].name = "NW Outer Car";
                    break;
                //Northeast
                case 2:
                    rotation = Quaternion.Euler(0, 90, 0);
                    listOfCars.Add(Instantiate(carPrefab, randomPoint, rotation));
                    listOfCars[i].name = "NE Inner Car";
                    break;
                case 3:
                    rotation = Quaternion.Euler(0, 90, 0);
                    listOfCars.Add(Instantiate(carPrefab, randomPoint, rotation));
                    listOfCars[i].name = "NE Outer Car";
                    break;
                //Southwest
                case 4:
                    rotation = Quaternion.Euler(0, -90, 0);
                    listOfCars.Add(Instantiate(carPrefab, randomPoint, rotation));
                    listOfCars[i].name = "SW Inner Car";
                    break;
                case 5:
                    rotation = Quaternion.Euler(0, -90, 0);
                    listOfCars.Add(Instantiate(carPrefab, randomPoint, rotation));
                    listOfCars[i].name = "SW Outer Car";
                    break;
                //Southeast
                case 6:
                    rotation = Quaternion.Euler(0, -180, 0);
                    listOfCars.Add(Instantiate(carPrefab, randomPoint, rotation));
                    listOfCars[i].name = "SE Inner Car";
                    break;
                case 7:
                    rotation = Quaternion.Euler(0, -180, 0);
                    listOfCars.Add(Instantiate(carPrefab, randomPoint, rotation));
                    listOfCars[i].name = "SE Outer Car";
                    break;
            }

        }
    }

    // Spawns directional arrows to tell players which direction the car will exit from
    public void SpawnArrows(int exitDirection, Transform carTransform)
    {
        switch(exitDirection)
        {
            case 0:
                listOfArrows.Add(Instantiate(arrowPrefab, carTransform.position + Vector3.up, carTransform.rotation * Quaternion.Euler(0, -90, 0)));
                break;
            case 1:
                listOfArrows.Add(Instantiate(arrowPrefab, carTransform.position + Vector3.up, carTransform.rotation * Quaternion.Euler(0,   0, 0)));
                break;
            case 2:
                listOfArrows.Add(Instantiate(arrowPrefab, carTransform.position + Vector3.up, carTransform.rotation * Quaternion.Euler(0,  90, 0)));
                break;
            default:
                break;
        }
    }

    // Destroys all directional arrows created from previous method
    // Called when submit button is pressed
    public void DeleteAllArrows() 
    {
        foreach(GameObject arrow in listOfArrows)
        {
            Destroy(arrow);
        }
    }
}
