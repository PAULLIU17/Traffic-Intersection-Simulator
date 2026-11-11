using UnityEngine;

public class SwitchCameraAngle : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 overheadPosition;
    private Quaternion originalRotation;
    private Quaternion overheadRotation;
    public bool isOriginal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOriginal = true;
        originalPosition = new Vector3(4, 4, 4);
        originalRotation = Quaternion.Euler(45, -135, 0);
        overheadPosition = new Vector3(0, 8, 0);
        overheadRotation = Quaternion.Euler(90, -135, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Switches between camera angles (original and overhead) when clicked
    public void SwitchAngle()
    {
        // If in original view, switch to overhead view
        if(isOriginal)
        {
            isOriginal = false;
            transform.position = overheadPosition;
            transform.rotation = overheadRotation;
        }

        // If in overhead view, switch to original view
        else
        {
            isOriginal = true;
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
