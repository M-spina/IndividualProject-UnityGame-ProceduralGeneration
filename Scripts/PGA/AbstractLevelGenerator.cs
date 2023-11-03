using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Some variables and methods would be used in different iterations of the PGC algorithm,
//so we want to create an abstract class so that other classes can override them 
//and use them if they inherit from this class
public abstract class AbstractLevelGenerator : MonoBehaviour
{
    [SerializeField] //used to visualize the position of the agents as tiles in the game
    protected TileMapVisualizer tilemapVisualizer = null;
    [SerializeField] // stores the position where we start our algorithm in the game world
    protected Vector2Int startPosition = Vector2Int.zero;
    // used to clear up tiles that have been visualized, followed by calling RunProcedualGeneration(). 
    //GenerateDungeon() is only called in the CustomEditor class that creates a button in the inspector of any game object 
    //and calls this method to run the PGC algorithm.
    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();

}
