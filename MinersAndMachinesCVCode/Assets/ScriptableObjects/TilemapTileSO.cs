using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TilemapTile", menuName = "ScriptableObjects/TilemapTile")]
public class TilemapTileSO : ScriptableObject
{
    public Ground.BlockType blockType;
    public Ground.GroundType groundType;
    public TileBase tileBase;
    public List<TileBase> destroyTileBaseListProcess;
    public int gold;
    public int destroyCost;
    public float destroyResistance;
    public bool isDigRestricted;
    public bool needSpecialSkillToDestroy;
    public bool needSpecialSkillToGetGold;
}
