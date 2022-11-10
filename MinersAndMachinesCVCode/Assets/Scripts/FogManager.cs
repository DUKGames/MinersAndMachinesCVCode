using UnityEngine;
using UnityEngine.Tilemaps;

public class FogManager : MonoBehaviour
{
    private Tilemap tilemap;

    private void OnEnable()
    {
        tilemap = LevelSetup.i.TilemapFog;
    }

    public void DeleteFogAroundPoint(Vector3Int point)
    {
        // CHANGE TO COROUTINE
        SetTile(new Vector3Int(point.x -1, point.y, 0), null);
        SetTile(new Vector3Int(point.x - 1, point.y - 1, 0), null);
        SetTile(new Vector3Int(point.x, point.y - 1, 0), null);
        SetTile(new Vector3Int(point.x + 1, point.y - 1, 0), null);
        SetTile(new Vector3Int(point.x + 1, point.y, 0), null);
    }

    public void DeleteFogInPoint(Vector3Int point)
    {
        SetTile(point, null);
    }

    public bool IsAnyTileHere(Vector3Int position, int leftCells, int rightCells, int downCells)
    {
        for (int i = position.x - leftCells; i < position.x + rightCells; i++)
        {
            for(int y = position.y; y >= position.y - downCells; y--)
            {
                if (tilemap.HasTile(new Vector3Int(i, y, 0)))
                    return true;
            }
        }

        return false;
    }

    private void SetTile(Vector3Int position, TileBase newTile)
    {
        if (tilemap.HasTile(position))
            tilemap.SetTile(position, newTile);
    }
}
