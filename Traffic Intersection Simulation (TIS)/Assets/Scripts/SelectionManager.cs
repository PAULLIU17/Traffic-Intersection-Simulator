using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;

    [SerializeField] private SpawnManager spawnManager;

    private List<GameObject> listOfSelectedCars;
    private bool wasSubmitButtonPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listOfSelectedCars = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Updates the choices in the Selection Panel
    public void UpdateSelectionPanel()
    {
        for(int i = 0; i < listOfSelectedCars.Count; i++)
        {
            selectionPanel.transform.GetChild(i).GetComponent<TMPro.TextMeshProUGUI>().text = i+1 + ". " + listOfSelectedCars[i].name;
        }
    }

    // Adds car to list (selects)
    // Or removes car from list if car already exists (unselects)
    public void AddCar(GameObject selectedCar)
    {
        // If list does not contain car, then add
        if(!listOfSelectedCars.Contains(selectedCar))
        {
            listOfSelectedCars.Add(selectedCar);
        }
        // Else, remove car from list
        else
        {
            listOfSelectedCars.Remove(selectedCar);
        }

        UpdateSelectionPanel();
    }

    // Return wasSubmitButtonPressed
    // Used by GameManager for timer countdown
    public bool ReturnSubmitButtonState()
    {
        return wasSubmitButtonPressed;
    }

    // Will be called after "Submit" Button is pressed
    // Checks to make sure player has chosen all cars
    // Runs the game after player submits their choices
    // And allows cars to move one by one
    public void RunGame()
    {
        if(listOfSelectedCars.Count == spawnManager.ReturnNumOfCarsSpawned())
        {
            wasSubmitButtonPressed = true;
            //selectionPanel.gameObject.SetActive(false);
            spawnManager.DeleteAllArrows();
            StartCoroutine(GoThroughCarQueue());
        }
    }

    // Iterates through a "queue" of car using listOfSelectedCars
    // Cars leave in order designated by the Player
    IEnumerator GoThroughCarQueue()
    {
        while(listOfSelectedCars.Count > 0)
        {
            listOfSelectedCars[0].gameObject.GetComponent<CarController>().AllowToMove();
            listOfSelectedCars.Remove(listOfSelectedCars[0]);
            yield return new WaitForSeconds(3);
        }
    }
}
