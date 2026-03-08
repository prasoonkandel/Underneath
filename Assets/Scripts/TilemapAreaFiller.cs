using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapAreaFiller : MonoBehaviour
{
    public TileBase tileToPlace;

    public Vector3Int startPosition;
    public Vector3Int endPosition;

    public GameObject blockingTilemapObject;

    private Tilemap tilemap;
    private Tilemap blockingTilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        if (blockingTilemapObject != null)
            blockingTilemap = blockingTilemapObject.GetComponent<Tilemap>();

        FillArea();
    }

    void FillArea()
    {
        for (int x = startPosition.x; x <= endPosition.x; x++)
        {
            for (int y = startPosition.y; y <= endPosition.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                bool isBlocked = false;

                if (blockingTilemap != null)
                {
                    if (blockingTilemap.HasTile(pos))
                        isBlocked = true;
                }

                if (!isBlocked && !tilemap.HasTile(pos))
                {
                    tilemap.SetTile(pos, tileToPlace);
                }
            }
        }
    }
}
