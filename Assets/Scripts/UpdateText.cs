using UnityEngine;
using TMPro;
using System;

public class UpdateText : MonoBehaviour
{
    public TMP_Text stats;

    // public TMP_Text hpText;
    // public TMP_Text lvlText;
    // public TMP_Text xpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        stats.text = PlayerStats.instance.lvl + "\n" + PlayerStats.instance.hp + "\n" + PlayerStats.instance.maxHp + "\n\n" + PlayerStats.instance.baseAttack + "\n\n" + PlayerStats.instance.speed;
        // hpText.text = "HP: " + PlayerStats.instance.hp;
        // lvlText.text = "LVL: " + PlayerStats.instance.lvl;
        // xpText.text = "XP: " + PlayerStats.instance.xp + "/" + PlayerStats.instance.nextLvlUp;
    }
}
