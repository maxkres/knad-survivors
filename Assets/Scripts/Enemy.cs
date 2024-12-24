using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int maxHP;
    public int damage;
    public float knockbackForce = 10f;

    public float speed = 1f;

    private Transform player;

    public float playerKnockbackThreshold = 0.2f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        hp = UnityEngine.Random.Range(10, 20) + PlayerStats.lvl * 2;
        maxHP = hp;
        damage = UnityEngine.Random.Range(5, 10) + PlayerStats.lvl;
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        List<Collider2D> contacts = new();
        GetComponent<CapsuleCollider2D>().GetContacts(contacts);

        foreach(var collision in contacts) {
            if (collision.CompareTag("Player")) {
                if (collision.GetComponent<Rigidbody2D>().linearVelocity.magnitude <= playerKnockbackThreshold)
                {
                    PlayerStats.TakeDamage(damage);

                    Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    collision.GetComponent<Rigidbody2D>().linearVelocity.Set(0, 0);
                    collision.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Debug.Log(collision);
    //     if (collision.CompareTag("Player") && collision.GetComponent<Rigidbody2D>().linearVelocity.Equals(Vector3.zero))
    //     {
    //         PlayerStats.TakeDamage(damage);

    //         Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
    //         collision.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    //     }
    // }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
            EnemySpawner.spawnedEnemies -= 1;
            PlayerStats.AddXP((maxHP + damage) * 2); 
        }
    }

    public void InitializeEnemy()
    {
        player = GameObject.FindWithTag("Player").transform;
        hp = UnityEngine.Random.Range(10, 20) + PlayerStats.lvl * 2;
        damage = UnityEngine.Random.Range(5, 10) + PlayerStats.lvl;

        // UnityEngine.Debug.Log($"Enemy initialized with HP: {hp}, Damage: {damage}");
    }

}
