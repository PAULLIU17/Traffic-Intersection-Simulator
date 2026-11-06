using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int exitDirection;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Camera mainCamera;

    private bool isAtIntersection;
    private Rigidbody rb;
    private bool wasClickedOn;
    private bool hasTurned;
    private Vector3 turnLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitDirection = Random.Range(0, 3); // 0 = left, 1 = straight, 2 = right

        isAtIntersection = false;
        rb = GetComponent<Rigidbody>();
        wasClickedOn = false;
        hasTurned = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // If approaching or leaving intersection
        if (!isAtIntersection) { 
            MoveCar(); 
        }

        // If still at intersection
        if (isAtIntersection)
        {
            CrossIntersection();
        }

        // Detects left mouse button clicks/presses
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Creates a ray that points to where the mouse is clicking
            // And creates a visible red ray
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red, 2f);

            // Detects if mouse clicks on Car
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Car was clicked.");
                    wasClickedOn = true;
                }
            }
        }
    }

    // Move Car
    void MoveCar()
    {
        // Car only moves when the game has started and clicked on
        if (gameManager.IsGameStarted() && wasClickedOn)
        {
            rb.position += transform.forward * speed * Time.deltaTime;
        }

    }

    // Rotate Car
    void RotateCar()
    {
        // Rotates based on exit direction
        // 0 = Turn Left  = -90.0f
        // 2 = Turn Right =  90.0f
        if(!hasTurned)
        {
            switch (exitDirection)
            {
                case 0:
                    //rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.Euler(0, rb.rotation.y - 90.0f, 0), 90.0f * Time.deltaTime);
                    rb.rotation *= Quaternion.Euler(0, -90, 0);
                    hasTurned = true;
                    break;
                case 2:
                    //rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.Euler(0, rb.rotation.y + 90.0f, 0), 90.0f * Time.deltaTime);
                    rb.rotation *= Quaternion.Euler(0, 90, 0);
                    hasTurned = true;
                    break;
                default:
                    break;
            }
        }
    }

    // Detects when car reaches intersection or exit
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Crosswalk Trigger"))
        {
            isAtIntersection = true;

            // Sets turnLocation depending on exitDirection value
            // This allows the car to turn at the appropriate time
            if (exitDirection == 0) 
            { 
                turnLocation = transform.position + transform.forward * 3.5f; 
            }
            else if (exitDirection == 2) 
            { 
                turnLocation = transform.position + transform.forward * 2.0f; 
            }

        }

        else if(other.CompareTag("Exit"))
        {
            Destroy(gameObject);
        }
    }

    // Detects when car leaves intersection
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crosswalk Trigger"))
        {
            isAtIntersection = false;
        }
    }

    // What to do when car is at crosswalk
    void CrossIntersection()
    {
        MoveCar();
        
        if(Vector3.Distance(transform.position, turnLocation) < 0.1f)
        {
            RotateCar();
        }
    }
}
