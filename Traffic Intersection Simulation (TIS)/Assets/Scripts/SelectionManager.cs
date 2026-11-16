using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;

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
    // Runs the game after player submits their choices
    // And allows cars to move one by one
    public void RunGame()
    {
        wasSubmitButtonPressed = true;
        StartCoroutine(GoThroughCarQueue());
    }

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
