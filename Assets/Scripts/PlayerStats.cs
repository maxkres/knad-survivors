using System;
using System.Diagnostics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int hp = 100;
    public static int maxHp = 100;
    public static int lvl = 1;
    public static int xp = 0;
    public static int baseAttack = 0;
    public static int speed = 5;

    public static int nextLvlUp = (int) (Math.Pow(lvl, 2) * 100);

    public static void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            UnityEngine.Debug.Log("Player is dead!");
        }
    }

    public static void AddXP(int amount)
    {
        xp += amount;
        if (xp >= nextLvlUp)
        {
            xp -= nextLvlUp;
            LevelUp();
        }
    }

    private static void LevelUp()
    {
        lvl++;
        nextLvlUp = (int) (Math.Pow(lvl, 2) * 100);
        hp = maxHp;
        LVLUPController.instance.lvlup();
        UnityEngine.Debug.Log("Level Up! New level: " + lvl);
    }

    public static void Heal(int amount)
    {
        hp += amount;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public static void GainMaxHP(int amount) {
        maxHp += amount;
        hp = maxHp;
    }

    void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxHp);
    }
}
