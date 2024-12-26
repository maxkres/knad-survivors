using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButtonsController : MonoBehaviour
{
    public int maxHPGain = 20;
    public int attackGain = 10;
    public int speedGain = 2;

    public List<Sprite> sprites;

    public static UpgradeButtonsController instance;

    private string[] levels = {"one", "two", "three"};

    int GetLevel(string name) {
        if (name.Equals("one")) return 1;
        if (name.Equals("two")) return 2;
        if (name.Equals("three")) return 3;
        return 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        foreach (Transform child in transform) {
            UpdateSprites(child.name, "empty", child);
        }

        TurnButtons(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp() {
        TurnButtons(true);
    }

    void TurnButtons(bool on) {
        foreach (var child in GetComponentsInChildren<Button>()) {
            child.interactable = on;
        }
    }

    bool Upgrade(string name) {
        var childObject = transform.Find(name);
        var child = childObject.GetComponent<Image>();
        var spriteName = child.sprite.name;
        var level = GetLevel(spriteName.Split(' ')[1]);
        if (level == 3) return false;
        UpdateSprites(name, levels[level], childObject);
        return true;
    }

    void UpdateSprites(string name, string level, Transform childObject) {
        Sprite active = sprites.Find(x => x.name.Equals(name + " " + level + " active_0"));
        Sprite inactive = sprites.Find(x => x.name.Equals(name + " " + level + " inactive_0"));
        childObject.GetComponent<Image>().sprite = active;
        var spriteState = childObject.GetComponent<Button>().spriteState;
        spriteState.disabledSprite = inactive;
    }

    bool ProceedUpgrade(string name) {
        if (Upgrade(name)) {
            TurnButtons(false);
            return true;
        }
        return false;
    }

    public void UpgradeHP() {
        if (ProceedUpgrade("health")) PlayerStats.instance.GainMaxHP(maxHPGain);
    }

    public void UpgradeSpeed() {
        if (ProceedUpgrade("speed")) PlayerStats.instance.baseAttack += attackGain;
    }

    public void UpgradeAttack() {
        if (ProceedUpgrade("attack")) PlayerStats.instance.speed += speedGain;
    }
}
