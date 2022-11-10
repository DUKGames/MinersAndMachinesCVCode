using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSetup : MonoBehaviour
{
    public static LevelSetup i;

    [field: SerializeField]
    public GameSceneManager.Scene ActualLevel { get; set; }

    [field: SerializeField]
    public Lands.LandType ActualLandType { get; set; }

    [field: SerializeField]
    public Tilemap TilemapTerrain { get; set; }

    [field: SerializeField]
    public Tilemap TilemapSelect { get; set; }

    [field: SerializeField]
    public Tilemap TilemapFog { get; set; }

    private void Awake()
    {
        i = this;
    }
}
