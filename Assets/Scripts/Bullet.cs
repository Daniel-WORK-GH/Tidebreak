using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [Tooltip("The bullet speed")]
    public float speed = 35;

    public GameObject ownerPlayer;

    void Start()
    {
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == ownerPlayer)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }

        gameObject.SetActive(false);
        Destroy(this);
    }
}