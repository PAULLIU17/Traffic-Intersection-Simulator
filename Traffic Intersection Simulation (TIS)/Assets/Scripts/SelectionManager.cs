using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;

    private List<GameObject> listOfSelectedCars;
    private Queue<GameObject> queueOfSelectedCars;
    private bool wasSubmitButtonPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listOfSelectedCars = new List<GameObject>();
        queueOfSelectedCars = new Queue<GameObject>();
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
            selectionPanel.transform.GetChild(i).GetComponent<TMPro.TextMeshProUGUI>().text = listOfSelectedCars[i].name;
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
    // Used by CarController to move cars
    public bool ReturnSubmitButtonState()
    {
        return wasSubmitButtonPressed;
    }

    // Will be called after "Submit" Button is pressed
    // Disables Selection Panel
    // Allows car to move
    public void RunGame()
    {
        wasSubmitButtonPressed = true;
        selectionPanel.gameObject.SetActive(false);
    }
}
