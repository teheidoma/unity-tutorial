using System;
using UnityEngine;

public class bullet : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float bulletspeed = 0.05f;
    public Vector2 direction = new Vector2(1, 0);
    public GameObject owner;

    void Start() {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update() { }

    private void OnCollisionEnter2D(Collision2D other) {
        if (owner == null) {
            return;
        }

        if (owner.CompareTag("Player")) {
            var enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null) {
                enemy.hp -= 20;
                enemy.TakeDamage();
                if (enemy.hp <= 0) {
                    enemy.FatalDamage();
                    Destroy(enemy.gameObject, 0.5f);
                    other.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                }

                Destroy(gameObject);
            }
        } else {
            Debug.Log(other.gameObject);
            var player = other.gameObject.GetComponent<Player>();
            if (player != null) {
                Destroy(gameObject);
            }
        }
    }

    public void Shoot() {
        var rigi = GetComponent<Rigidbody2D>();
        if (owner.CompareTag("Player")) {
            rigi.excludeLayers = LayerMask.GetMask("Player");
        } else {
            rigi.excludeLayers = LayerMask.GetMask("Ignore Raycast");
        }

        rigi.AddForce(direction * bulletspeed, ForceMode2D.Impulse);
    }
}