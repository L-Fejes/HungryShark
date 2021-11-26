using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private Rigidbody2D rigidBody;

    private Camera cam;

    Vector2 movement;
    Vector2 pointDir;

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

        if (Input.GetButtonDown("Space") && target != null) {
            Bite(target);
        }
    }

    private void FixedUpdate ()
    {
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 facingDir   = pointDir - rigidBody.position;
        float angle         = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg;
        rigidBody.rotation  = angle;
    }

    void OnGUI ()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        pointDir.x = point.x;
        pointDir.y = point.y;

        Debug.Log(pointDir);

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Fish") {
            target = collision.gameObject;
        }
            
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.tag == "Fish") {
            target = null;
        }
    }

    private void Bite (GameObject other) {
        
    }
}
