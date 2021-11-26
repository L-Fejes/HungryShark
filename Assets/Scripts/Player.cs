using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameController gameController;

    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private Rigidbody2D rigidBody;

    private int health = 10;

    private Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    GameObject target;


    private void Awake ()
    {
        if (rigidBody == null) {
            rigidBody = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x  = Input.GetAxisRaw("Horizontal");
        movement.y  = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate ()
    {
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 facingDir   = mousePos - rigidBody.position;
        float angle         = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg;
        rigidBody.rotation  = angle;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            target = collision.gameObject;
            health++;
            gameController.UpdatePlayerInfo(health, true);
            Destroy(target);
        }

        if (collision.gameObject.tag == "Poison")
        {
            target = collision.gameObject;
            health--;
            gameController.UpdatePlayerInfo(health, false);
            Destroy(target);
        }
    }
}
