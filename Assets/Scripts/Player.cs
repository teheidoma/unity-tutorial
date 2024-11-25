using System;
using UnityEngine;

public class Player : MonoBehaviour {
    [Range(0, 10)]
    public float speed = 0.3f;
    [Range(0, 100)]
    public float jumpforce = 0.3f;
    public Transform newPosition;
    public GameObject bullet;
    
    void Start() {
        Debug.Log("PLAYER CREATED");
    }
    
    void Update() {
        if (transform.position.y < -5) {
            transform.position = newPosition.position;
        }
        Rigidbody2D r = GetComponent<Rigidbody2D>();
        if (r.linearVelocity.y == 0)
        {
            var axis = Input.GetAxis("Vertical");
            if (axis > 0)
            {
                axis = 1;
            }
            else
            {
                axis = 0;
            }

            // r.AddForce(new Vector2(0, axis * jumpforce));
            r.linearVelocity = new Vector2(r.linearVelocity.x, axis * jumpforce);
        }


        var inputForce = Input.GetAxis("Horizontal");
        r.AddForce(new Vector2(inputForce * speed,  0), ForceMode2D.Force);
       
        
        if (Input.GetButtonDown("Fire1"))
        {
            var mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            var bulletPosition = transform.position;
            var direction = mousePosition - bulletPosition;
            var bulletObject = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletObject.GetComponent<bullet>().direction = direction.normalized;
            bulletObject.GetComponent<bullet>().owner = gameObject;
            bulletObject.GetComponent<bullet>().Shoot();
            GetComponent<AudioSource>() .Play();
        }
        
    }

    void OnGUI() {
        GUILayout.Label("X: " +Input.GetAxis("Horizontal") + " Y: " + Input.GetAxis("Vertical"));
        GUILayout.Label(GetComponent<Rigidbody2D>().linearVelocity.ToString());
    }
    
    
    
}