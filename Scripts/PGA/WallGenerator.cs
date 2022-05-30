using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this class creates methods to draw walls of a generated level

public static class WallGenerator 
{   // after finding all the walls and storing them in a HashSet we want to paint the walls uisng the hasHSet of positions.
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }
    // we first want to find potential wall position usng the floor position of the level
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions) // we run a nested foreach loop to go through the floor psoition and check if the position incremented by a cradinal direction 
        { //is in teh floor position hash set . if not we nno that there is neighbourPosition that is not a floor so we ass that positon o the wall hashSet
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition) == false)
                    wallPositions.Add(neighbourPosition);
            }
        }
        return wallPositions;
    }

    
}
