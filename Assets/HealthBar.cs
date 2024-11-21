using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public EnemyController enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(enemy.hp / 100f*2f, 0.3f, 1f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
