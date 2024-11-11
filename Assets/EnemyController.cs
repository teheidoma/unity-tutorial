using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    public float hp = 100;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public GameObject[] path;
    public int currentPoint = 0;
    public int currentDirection = 1;//1 - вперед -1 - назад

    void Start() {
        GetComponent<NavMeshAgent>().SetDestination(path[0].transform.position);
    }

    // Update is called once per frame
    void Update() {
        var distance = Vector3.Distance(transform.position, path[currentPoint].transform.position);
        if (distance < 2) {
            currentPoint += currentDirection;
            if (currentPoint == 4 || currentPoint == -1) {
                
                if (currentDirection == 1) {
                    currentPoint = 2;
                    currentDirection = -1;
                } else if (currentDirection == -1) {
                    currentPoint = 1;
                    currentDirection = 1;
                }
            }
            GetComponent<NavMeshAgent>().SetDestination(path[currentPoint].transform.position);
        }


        if (Input.GetButtonDown("Fire2")) {
            var mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            GetComponent<NavMeshAgent>().SetDestination(mousePosition);
            Debug.Log(mousePosition);
        }
    }

    public void TakeDamage() {
        GetComponent<AudioSource>().clip = hurtSound;
        GetComponent<AudioSource>().Play();
    }

    public void FatalDamage() {
        GetComponent<AudioSource>().clip = deathSound;
        GetComponent<AudioSource>().Play();
    }
}