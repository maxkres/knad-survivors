using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase innerTile;

    void Start()
    {
        GenerateTilemap();
    }

    void GenerateTilemap()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                tilemap.SetTile(position, innerTile);
            }
        }
    }
}
