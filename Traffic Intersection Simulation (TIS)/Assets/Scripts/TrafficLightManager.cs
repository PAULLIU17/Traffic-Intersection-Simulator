using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{
    [SerializeField] private GameObject leftTrafficLight;
    [SerializeField] private GameObject rightTrafficLight;
    [SerializeField] private int startingRoad;

    private GameObject leftTrafficLightGreen;
    private GameObject leftTrafficLightYellow;
    private GameObject leftTrafficLightRed;

    private GameObject rightTrafficLightGreen;
    private GameObject rightTrafficLightYellow;
    private GameObject rightTrafficLightRed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // Left Traffic Lights Are Assigned
        leftTrafficLightGreen = leftTrafficLight.transform.GetChild(0).gameObject;
        leftTrafficLightYellow = leftTrafficLight.transform.GetChild(1).gameObject;
        leftTrafficLightRed = leftTrafficLight.transform.GetChild(2).gameObject;

        // Right Traffic Lights Are Assigned
        rightTrafficLightGreen = rightTrafficLight.transform.GetChild(0).gameObject;
        rightTrafficLightYellow = rightTrafficLight.transform.GetChild(1).gameObject;
        rightTrafficLightRed = rightTrafficLight.transform.GetChild(2).gameObject;

        //0 = NW-SE Road; 1 = NE-SW Road
        startingRoad = Random.Range(0, 2);
        UpdateTrafficLights(startingRoad);

    }

    //0 = NW-SE Road; 1 = NE-SW Road
    void UpdateTrafficLights(int road)
    {
        switch(road)
        {
            case 0:
                leftTrafficLightGreen.SetActive(true);
                leftTrafficLightYellow.SetActive(false);
                leftTrafficLightRed.SetActive(false);
                rightTrafficLightGreen.SetActive(false);
                rightTrafficLightYellow.SetActive(false);
                rightTrafficLightRed.SetActive(true);
                break;
            case 1:
                leftTrafficLightGreen.SetActive(false);
                leftTrafficLightYellow.SetActive(false);
                leftTrafficLightRed.SetActive(true);
                rightTrafficLightGreen.SetActive(true);
                rightTrafficLightYellow.SetActive(false);
                rightTrafficLightRed.SetActive(false);
                break;
            default:
                break;
        }
    }
}
