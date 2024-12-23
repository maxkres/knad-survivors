using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class TilemapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase groundTile;
    public TileBase[] groundStates;
    public TileBase grassTile;
    public int clusterCount = 100;
    public int minClusterSize = 20;
    public int maxClusterSize = 40;
    public float clusterSpreadChance = 0.7f;
    public float animationTime = 0.7f;
    private const int MAP_WIDTH = 100;
    private const int MAP_HEIGHT = 100;

    private void Start()
    {
        GenerateTilemap();
        StartCoroutine(ContinuousIgnition());
    }

    private void GenerateTilemap()
    {
        tilemap.ClearAllTiles();
        Vector3Int size = new Vector3Int(MAP_WIDTH, MAP_HEIGHT, 0);
        bool[,] g = new bool[MAP_WIDTH, MAP_HEIGHT];
        for (int i = 0; i < clusterCount; i++)
        {
            int sx = Random.Range(0, MAP_WIDTH);
            int sy = Random.Range(0, MAP_HEIGHT);
            GenerateCluster(sx, sy, g, size);
        }
        for (int x = 0; x < MAP_WIDTH; x++)
        {
            for (int y = 0; y < MAP_HEIGHT; y++)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                if (g[x, y]) tilemap.SetTile(p, grassTile);
                else tilemap.SetTile(p, groundTile);
            }
        }
    }

    private void GenerateCluster(int sx, int sy, bool[,] gm, Vector3Int size)
    {
        Queue<Vector2Int> q = new Queue<Vector2Int>();
        List<Vector2Int> c = new List<Vector2Int>();
        q.Enqueue(new Vector2Int(sx, sy));
        while (q.Count > 0 && c.Count < maxClusterSize)
        {
            Vector2Int t = q.Dequeue();
            int x = t.x;
            int y = t.y;
            if (x >= 0 && x < size.x && y >= 0 && y < size.y && !gm[x, y])
            {
                gm[x, y] = true;
                c.Add(t);
                if (c.Count < maxClusterSize)
                {
                    if (Random.value < clusterSpreadChance)
                    {
                        List<Vector2Int> n = new List<Vector2Int>()
                        {
                            new Vector2Int(x+1,y),
                            new Vector2Int(x-1,y),
                            new Vector2Int(x,y+1),
                            new Vector2Int(x,y-1)
                        };
                        foreach (var nb in n)
                        {
                            if (nb.x >= 0 && nb.x < size.x && nb.y >= 0 && nb.y < size.y)
                            {
                                q.Enqueue(nb);
                            }
                        }
                    }
                }
            }
        }
    }

    private IEnumerator ContinuousIgnition()
    {
        while (true)
        {
            float w = Random.Range(0.05f, 0.2f);
            yield return new WaitForSeconds(w);
            int amt = Random.Range(4, 6);
            for (int i = 0; i < amt; i++)
            {
                Vector3Int p = new Vector3Int(Random.Range(0, MAP_WIDTH), Random.Range(0, MAP_HEIGHT), 0);
                if (tilemap.GetTile(p) == groundTile) StartCoroutine(AnimateRhombusIgnition(p));
            }
        }
    }

    private IEnumerator AnimateRhombusIgnition(Vector3Int center)
    {
        List<(Vector3Int pos, int dist)> r = GetTilesWithinDistance(center, 7);
        float e = 0f;
        while (e < animationTime)
        {
            float t = e / animationTime;
            int cs = Mathf.RoundToInt(Mathf.Lerp(0, 7, t));
            foreach (var (pos, dist) in r)
            {
                int ds = Mathf.Clamp(cs - dist, 0, 7);
                int ci = GetCurrentGroundStateIndex(pos);
                if (ci >= 0 && ds > ci) tilemap.SetTile(pos, groundStates[ds]);
            }
            e += Time.deltaTime;
            yield return null;
        }
        SetTileStatesMax(r, 7);
        e = 0f;
        while (e < animationTime)
        {
            float t = e / animationTime;
            int cs = Mathf.RoundToInt(Mathf.Lerp(7, 0, t));
            foreach (var (pos, dist) in r)
            {
                int ds = Mathf.Clamp(cs - dist, 0, 7);
                int ci = GetCurrentGroundStateIndex(pos);
                if (ci >= 0 && ci > ds) tilemap.SetTile(pos, groundStates[ds]);
            }
            e += Time.deltaTime;
            yield return null;
        }
        SetTileStatesMin(r, 0);
    }

    private List<(Vector3Int pos, int dist)> GetTilesWithinDistance(Vector3Int start, int md)
    {
        List<(Vector3Int pos, int dist)> res = new List<(Vector3Int, int)>();
        HashSet<Vector3Int> v = new HashSet<Vector3Int>();
        Queue<(Vector3Int pos, int dist)> q = new Queue<(Vector3Int pos, int dist)>();
        q.Enqueue((start, 0));
        v.Add(start);
        while (q.Count > 0)
        {
            var (cp, cd) = q.Dequeue();
            res.Add((cp, cd));
            if (cd < md)
            {
                foreach (var nb in GetNeighbors(cp))
                {
                    if (!v.Contains(nb))
                    {
                        int d = Mathf.Abs(nb.x - start.x) + Mathf.Abs(nb.y - start.y);
                        if (d <= md)
                        {
                            v.Add(nb);
                            q.Enqueue((nb, d));
                        }
                    }
                }
            }
        }
        return res;
    }

    private IEnumerable<Vector3Int> GetNeighbors(Vector3Int p)
    {
        yield return p + Vector3Int.up;
        yield return p + Vector3Int.down;
        yield return p + Vector3Int.left;
        yield return p + Vector3Int.right;
    }

    private int GetCurrentGroundStateIndex(Vector3Int pos)
    {
        TileBase c = tilemap.GetTile(pos);
        if (c == null) return -1;
        for (int i = 0; i < groundStates.Length; i++)
        {
            if (groundStates[i] == c) return i;
        }
        if (c == groundTile) return 0;
        return -1;
    }

    private void SetTileStatesMax(List<(Vector3Int pos, int dist)> r, int s)
    {
        foreach (var (pos, dist) in r)
        {
            int ds = Mathf.Clamp(s - dist, 0, 7);
            int ci = GetCurrentGroundStateIndex(pos);
            if (ci >= 0 && ds > ci) tilemap.SetTile(pos, groundStates[ds]);
        }
    }

    private void SetTileStatesMin(List<(Vector3Int pos, int dist)> r, int s)
    {
        foreach (var (pos, dist) in r)
        {
            int ds = Mathf.Clamp(s - dist, 0, 7);
            int ci = GetCurrentGroundStateIndex(pos);
            if (ci >= 0 && ci > ds) tilemap.SetTile(pos, groundStates[ds]);
        }
    }
}
