using System;
using UnityEngine;

public class bullet : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float bulletspeed = 0.05f;
    public Vector2 direction = new Vector2(1, 0);

    void Start() {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var enemy = other.gameObject.GetComponent<EnemyController>();
        if (enemy != null) {
            enemy.hp -= 20;
            enemy.TakeDamage();
            if (enemy.hp <= 0) {
                enemy.FatalDamage();
                Destroy(enemy.gameObject,0.5f);
                other.gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
                
            }
            Destroy(gameObject);
        }
    }

    public void Shoot() {
        var rigi = GetComponent<Rigidbody2D>();
        rigi.AddForce(direction * bulletspeed, ForceMode2D.Impulse);
    }
}