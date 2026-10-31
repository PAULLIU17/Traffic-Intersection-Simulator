using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int exitDirection;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        exitDirection = Random.Range(0, 3); // 0 = left, 1 = straight, 2 = right
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // Move Car
    void moveCar()
    {
        //transform.position += transform.forward * speed * Time.deltaTime;
    }
}
