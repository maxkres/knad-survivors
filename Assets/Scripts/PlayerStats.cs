using System;
using System.Diagnostics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int hp = 100;
    public int maxHp = 100;
    public int lvl = 1;
    public int xp = 0;
    public int baseAttack = 0;
    public int speed = 5;

    public int nextLvlUp = 0;

    public static PlayerStats instance;

    void Start() {
        instance = this;

        nextLvlUp = (int) (Math.Pow(lvl, 2) * 100);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void AddXP(int amount)
    {
        xp += amount;
        if (xp >= nextLvlUp)
        {
            xp -= nextLvlUp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        lvl++;
        nextLvlUp = (int) (Math.Pow(lvl, 2) * 100);
        hp = maxHp;
        // LVLUPController.instance.lvlup();
        UpgradeButtonsController.instance.LevelUp();
        UnityEngine.Debug.Log("Level Up! New level: " + lvl);
    }

    public void Heal(int amount)
    {
        hp += amount;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void GainMaxHP(int amount) {
        maxHp += amount;
        hp = maxHp;
    }

    void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxHp);
    }
}
