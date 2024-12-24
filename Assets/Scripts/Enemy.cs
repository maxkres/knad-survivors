using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int maxHP;
    public int damage;
    public float knockbackForce = 2f;

    private Transform player;

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
            transform.position += direction * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log(collision);
        if (collision.CompareTag("Player"))
        {
            PlayerStats.TakeDamage(damage);

            Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

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

        // Лог для отладки
        UnityEngine.Debug.Log($"Enemy initialized with HP: {hp}, Damage: {damage}");
    }

}
