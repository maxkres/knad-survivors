using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int hp = 100;

    public static int xp = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp = (int) (math.sin(Time.time) * 50 + 50);
        xp = UnityEngine.Random.Range(0, 1000);
    }
}
