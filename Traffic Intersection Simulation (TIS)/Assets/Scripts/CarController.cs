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
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private SelectionManager selectionManager;

    private bool isAtIntersection;
    private Rigidbody rb;
    private bool hasTurned;
    private Vector3 turnLocation;
    private bool allowedToMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitDirection = Random.Range(0, 3); // 0 = left, 1 = straight, 2 = right

        isAtIntersection = false;
        rb = GetComponent<Rigidbody>();
        hasTurned = false;
        allowedToMove = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        mainCamera = Camera.main;
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        spawnManager.SpawnArrows(exitDirection, transform);
        selectionManager = GameObject.Find("Selection Manager").GetComponent<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If approaching or leaving intersection
        if (!isAtIntersection) { 
            MoveCar(); 
        }

        // If at intersection
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
                    selectionManager.AddCar(gameObject);
                }
            }
        }
    }

    // Allows GameObject to move
    public void AllowToMove()
    {
        allowedToMove = true;
    }

    // Move Car
    void MoveCar()
    {
        // Car only moves when selection panel's submit button was pressed
        if (allowedToMove)
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
                turnLocation = transform.position + transform.forward * 3.25f;
            }
            else if (exitDirection == 2) 
            { 
                turnLocation = transform.position + transform.forward * 2.25f; 
            }

        }

        // If car reaches an exit...
        else if(other.CompareTag("Exit"))
        {   
            // For the last car, call gameover before destroying last car
            if (GameObject.FindGameObjectsWithTag("Car").Length == 1)
            {
                gameManager.GameOver();
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
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
