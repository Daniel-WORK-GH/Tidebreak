using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase deepWaterTile;
    public TileBase waterTile;
    public TileBase sandTile;
    public TileBase grassTile;

    public int width = 100;
    public int height = 100;

    public float islandThreshold = 0.5f;
    public float sandThreshold = 0.3f;
    public float grassThreshold = 0.3f;

    public float scale = 10f;               
    public Vector2 offset;               

    void Start()
    {
        GenerateTilemap();
    }

    void GenerateTilemap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float perlinValue = Mathf.PerlinNoise((x + offset.x) / scale, (y + offset.y) / scale);

                TileBase tile = GetTile(perlinValue);
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    TileBase GetTile(float perlinValue)
    {
        if (perlinValue > islandThreshold)
        {
            return deepWaterTile;
        }
        else if (perlinValue > sandThreshold)
        {
            return waterTile;
        }
        else if (perlinValue > grassThreshold)
        {
            return sandTile;
        }
        else
        {
            return grassTile;
        }
    }
}
