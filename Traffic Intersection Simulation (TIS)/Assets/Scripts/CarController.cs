using UnityEngine;

public class CarController : MonoBehaviour
{
    // Rigidbody of the GameObject (Player).
    private Rigidbody rb;

    // Car Speed
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody>();
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    // Move Car
    void moveCar()
    {
        //transform.position += transform.forward * speed * Time.deltaTime;
    }
}
