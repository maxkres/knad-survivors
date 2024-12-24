using UnityEngine;
using TMPro;
using System;

public class UpdateText : MonoBehaviour
{
    public TMP_Text hpText;
    public TMP_Text lvlText;
    public TMP_Text xpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "HP: " + PlayerStats.hp;
        lvlText.text = "LVL: " + PlayerStats.lvl;
        xpText.text = "XP: " + PlayerStats.xp + "/" + ((int)Math.Pow(PlayerStats.lvl, 2) * 1000);
    }
}
