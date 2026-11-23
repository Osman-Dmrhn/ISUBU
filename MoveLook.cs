using UnityEngine;

public class MoveLook : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSens;

    private float xRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")*mouseSens*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*mouseSens*Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -65f, 65f);

        transform.localRotation=Quaternion.Euler(xRotation,0f,0f);

        playerTransform.Rotate(Vector3.up*mouseX);
        
    }
}
