using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera mainCamera;
    public float zoomedInSize = 5f;
    public float zoomedOutSize = 10f;
    public float interactionRange = 2f;

    private bool isInVehicle = false;
    private VehicleController currentVehicle;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isInVehicle)
        {
            HandleMovement();
            CheckForNearbyVehicle();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInVehicle)
            {
                ExitVehicle();
            }
            else if (currentVehicle != null)
            {
                EnterVehicle();
            }
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, moveY, 0f);
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void CheckForNearbyVehicle()
    {
        VehicleController[] vehicles = FindObjectsByType<VehicleController>(FindObjectsSortMode.None);
        currentVehicle = null;

        foreach (var vehicle in vehicles)
        {
            if (Vector3.Distance(transform.position, vehicle.transform.position) <= interactionRange)
            {
                currentVehicle = vehicle;
                break;
            }
        }
    }

    void EnterVehicle()
    {
        isInVehicle = true;
        transform.position = currentVehicle.transform.position; // Snap to vehicle
        currentVehicle.TakeControl();

        // Make the camera follow the vehicle without rotating
        mainCamera.transform.SetParent(currentVehicle.transform);
        mainCamera.transform.localPosition = new Vector3(0, 0, -10); // Adjust camera position if needed

        mainCamera.orthographicSize = zoomedOutSize;
        spriteRenderer.enabled = false; // Hide the player
    }

    void ExitVehicle()
    {
        isInVehicle = false;
        transform.position = currentVehicle.transform.position;
        currentVehicle.ReleaseControl();

        // Detach the camera
        mainCamera.transform.SetParent(transform);
        mainCamera.transform.localRotation = Quaternion.identity; // Ensure rotation is reset
        mainCamera.orthographicSize = zoomedInSize;

        transform.position += new Vector3(5, 0, 0); // Offset exit position

        spriteRenderer.enabled = true; // Show the player
    }
}
