using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageChange : MonoBehaviour
{
    [SerializeField] Tilemap blockTilemap;
    [SerializeField] Tilemap grassTilemap;
    
    public void DeleteTile()
    {
        blockTilemap.SetTile(new Vector3Int(220, -7, 0), null);
        blockTilemap.SetTile(new Vector3Int(221, -7, 0), null);
        blockTilemap.SetTile(new Vector3Int(222, -7, 0), null);
        blockTilemap.SetTile(new Vector3Int(223, -7, 0), null);

        grassTilemap.SetTile(new Vector3Int(220, -6, 0), null);
        grassTilemap.SetTile(new Vector3Int(221, -6, 0), null);
        grassTilemap.SetTile(new Vector3Int(222, -6, 0), null);
        grassTilemap.SetTile(new Vector3Int(223, -6, 0), null);
    }
}
