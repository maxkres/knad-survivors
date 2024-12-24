using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceProps : MonoBehaviour
{
    public float propFill = 0.03f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        var tiles = tilemap.GetTilesBlock(tilemap.cellBounds);
        
        tilemap.ClearAllTiles();

        for (int i = 0; i < 100; i++) {
            for (int j = 0; j < 100; j++) {
                if (Random.Range(0, 1f) <= propFill) {
                    Debug.Log(i + " " + j);
                    tilemap.SetTile(new Vector3Int(i, j, 0), tiles[Random.Range(0, tiles.Length)]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
