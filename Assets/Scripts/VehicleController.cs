using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    public float moveSpeed = 8f;
    private bool isControlled = false;
    public float rotationSpeed = 100f; // Speed at which the car rotates

    public Camera mainCamera;

    public Cannon[] cannons;

    public GameObject bulletPrefab;

    private float elpshoot;
    private const float shoottime = 0.5f;

    void Start()
    {

    }

    void Update()
    {
        if (isControlled)
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        bool driving = false;

        // Handle movement
        float movement = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            movement = moveSpeed * Time.deltaTime;
            driving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = -moveSpeed * Time.deltaTime;
            driving = true;
        }

        transform.position += movement * transform.up;//Apply movement

        if (driving)
        {
            float rotation = 0f;
            if (Input.GetKey(KeyCode.A))
            {
                rotation = rotationSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rotation = -rotationSpeed * Time.deltaTime;
            }

            transform.Rotate(0, 0, rotation);// Apply rotation
        }

        if (elpshoot > shoottime)
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject obj = Instantiate(bulletPrefab, cannons[i].transform.position, cannons[i].transform.rotation);
                    Bullet bullet = obj.GetComponent<Bullet>();
                    bullet.ownerPlayer = gameObject;
                }
                elpshoot = 0;
            }
            if (Input.GetMouseButton(1))
            {
                for (int i = 4; i < 8; i++)
                {
                    GameObject obj = Instantiate(bulletPrefab, cannons[i].transform.position, cannons[i].transform.rotation);
                    Bullet bullet = obj.GetComponent<Bullet>();
                    bullet.ownerPlayer = gameObject;
                }

                elpshoot = 0;
            }
        }

        elpshoot += Time.deltaTime;

        mainCamera.transform.localRotation = Quaternion.Inverse(transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    public void TakeControl()
    {
        isControlled = true;
    }

    public void ReleaseControl()
    {
        isControlled = false;
    }
}