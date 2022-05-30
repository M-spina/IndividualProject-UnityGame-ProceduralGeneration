using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
// This class contains the algorithm to generate random room layouts uisng aganets to move around the game world and store its position to draw the positons to create a room
// This class inherites form AbstractLevelGenerator to use its methods to overide them.
public class SimpleRandomWalkLevelGeneration : AbstractLevelGenerator
{
    [SerializeField] // this implements parameters that are used in the room generation , itteration and walk lenght 
    protected SimpleRandomWalkData randomWalkParameters;

    

    // this overrides the RunProceduralGeneration that creates a HashSet that would store floor position that was returned form the RunRandomWalk Method 
    // tehen it will clear tany generated room that was already in existance and the we start painting the room given the floor postion and walls
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }
    // this method takes in parameters form the SimpleRandomWalkData and Vector2Int postion to start runing the algorithm from.
    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData parameters , Vector2Int position)
    {
        var currentPosition = position; // after setting the current psotion to the starting psotion we create a HashSet for floor position to store the upcoming positions by the agents 
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParameters.iterations; i++) // we use a loop to iterrate through the PGA SimpleRandomWalk methdod that would add agents position to the floor position HashSet 
        {
            var path = PGA.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
            floorPositions.UnionWith(path);
            if (randomWalkParameters.startRandomlyEachIteration) // we also dont want the algorith to always start a the start position so we chose a random floor psotion to be the current psoition to begin moveing the agent
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions; // we then return the HashSet to use in the RunProceduralGeneration method
    }

    
}
