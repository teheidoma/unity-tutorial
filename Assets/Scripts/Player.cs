using UnityEngine;

public class Player : MonoBehaviour {
    [Range(0, 10)]
    public float speed = 0.3f;
    public Transform newPosition;
    
    
    
    
    void Start() {
        Debug.Log("PLAYER CREATED");
    }

    
    
    
    
    
    
    void Update() {
        if (transform.position.y < -5) {
            transform.position = newPosition.position;
        }
        Rigidbody2D r = GetComponent<Rigidbody2D>();


        var inputForce = Input.GetAxis("Horizontal");
        r.AddForce(new Vector2(inputForce * speed,  0), ForceMode2D.Force);
        
        
        
        
        r.AddForce(new Vector2(0, Input.GetAxis("Vertical") * 2));
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    void OnGUI() {
        GUILayout.Label("X: " +Input.GetAxis("Horizontal") + " Y: " + Input.GetAxis("Vertical"));
        GUILayout.Label(GetComponent<Rigidbody2D>().velocity.ToString());
    }
}