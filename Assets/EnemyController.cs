using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float hp = 100;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void TakeDamage()
    {
        GetComponent<AudioSource>().clip = hurtSound;
        GetComponent<AudioSource>() .Play();
    }
    public void FatalDamage()
    {
        GetComponent<AudioSource>().clip = deathSound;
        GetComponent<AudioSource>() .Play();
    }
}
