using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int exitDirection;
    [SerializeField] private bool isAtIntersection;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAtIntersection = false;
        rb = GetComponent<Rigidbody>();

        exitDirection = Random.Range(0, 3); // 0 = left, 1 = straight, 2 = right
    }

    // Update is called once per frame
    void Update()
    {   
        // If approaching or leaving intersection
        if(!isAtIntersection) { 
            MoveCar(); 
        }

        // If still at intersection
        if (isAtIntersection)
        {
            CrossIntersection();
        }
    }

    // Detects when mouse clicks car
    private void OnMouseDown()
    {
        Debug.Log("Car was clicked.");
    }

    // Move Car
    void MoveCar()
    {
        //rb.AddForce(Vector3.forward * speed * Time.deltaTime);
        rb.position += transform.forward * speed * Time.deltaTime;
    }

    // Rotate Car
    // 0 = left
    // 2 = right
    void RotateCar()
    {
        if(exitDirection == 0)
        {
            //rb.AddTorque(Vector3.up * speed * Time.deltaTime * -1);
            rb.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.left), 1);
        }

        else if(exitDirection == 2)
        {
            rb.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.right), 1);
        }
    }

    // Detects when car reaches intersection
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Crosswalk Trigger"))
        {
            isAtIntersection = true;
            Debug.Log("Car reached intersection.");
        }
    }

    // Detects when car leaves intersection
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crosswalk Trigger"))
        {
            isAtIntersection = false;
            Debug.Log("Car left intersection.");
        }
    }

    // What to do when car is at crosswalk
    void CrossIntersection()
    {
        switch(exitDirection)
        {
            // If 0 (turn left)
            case 0:
                MoveCar();
                RotateCar();
                break;
            // If 1 (go straight)
            case 1:
                MoveCar();
                break;
            // If 2 (turn right)
            case 2:
                MoveCar();
                RotateCar();
                break;
            default:
                break;
        }

    }
}
