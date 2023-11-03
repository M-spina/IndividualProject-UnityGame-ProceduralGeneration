using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
// this class allows us to visualize the agent positions that was collected to procedually generate a room
public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop;
    // takes in a ennum state that has Vector2Int floor positions to paint the floor
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }
    // for each psotion in the floor tile psotion we pain a tile  this takes into acound what tile to paint and on which tile map
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }
    // this call the paint paint single tiles method using the wall position insted of the floor 
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }
    // this paint the actuall tile in the correct cell in the gameworld 
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2 position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3)position);
        tilemap.SetTile(tilePosition, tile);
    }
    // this method clear the already existing painted tiles in each tile man so that when er generate a new level we do not implemented ontop of an existing level
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
