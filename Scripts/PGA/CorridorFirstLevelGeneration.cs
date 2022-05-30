using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CorridorFirstLevelGeneration : SimpleRandomWalkLevelGeneration
{
    [SerializeField]
    private int corridorLenght = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.6f;
    
    private Vector2 postions;

    [SerializeField] private float spawnRadius = 7;
    
    public GameObject spawn;
    public GameObject portal;
    public GameObject[] enemies;
    private GameObject[] enemyList;


    //private void Start()
    //{
        //RunProceduralGeneration();
    //}

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPosition = new HashSet<Vector2Int>();


        CreateCorridors(floorPositions, potentialRoomPosition);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPosition);
        
        // check floor position for dead ends and ad a room to them

        List<Vector2Int> deadEnd = FindDeadEnd(floorPositions);

        CreateRoomsAtDeadEnd(deadEnd, roomPositions);

        floorPositions.UnionWith(roomPositions);
        // new
        List<Vector2Int> roomCenetrs = new List<Vector2Int>();
        foreach (var room in potentialRoomPosition)
        {
            roomCenetrs.Add(room);
            Debug.Log(room);
        }
        Spawn(spawn,roomCenetrs);
        SpawnPortal(portal,roomCenetrs);
        loadEnemies(roomCenetrs);
        
        //

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnd, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnd)
        {
            // if the is no dead end position in the the position where rooms are at then we create a room
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindDeadEnd(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnd = new List<Vector2Int>();
        //if we loop through each direction at the current position and the create a new position in that direction
        // and check against the list in the floor position , if there not in the list it is no neighbour 
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            // if there is a neighbour in only one direction then it is a dead end 
            
            // we see if there is a floorPosition in the cardinalDirection List to add to the neighbour count
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
            }
            if(neighboursCount == 1)
                deadEnd.Add(position);
            
        }

        return deadEnd;
    }

    // random sorting our potential room position hash set and taken form it the rooms to create count and covert it in to a list 
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPosition)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPosition.Count * roomPercent);
        // to query the collections so we can extract some subset we want to acesse 
        // we want to sort out the potential room position in a random order
        
        //we are creating a unique value for the potential room position we can sort using this value
        List<Vector2Int> roomsToCreate =
            potentialRoomPosition.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        
        // this would generate room at the position we have selected at random
        foreach (var roomPosition  in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPosition, HashSet<Vector2Int> potentialRoomPosition)
    {
        var currentPosition = startPosition;
        potentialRoomPosition.Add(currentPosition);
        
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = PGA.RandomWalkCorridor(currentPosition, corridorLenght);
            currentPosition = corridor[corridor.Count -1];
            potentialRoomPosition.Add(currentPosition);
            floorPosition.UnionWith(corridor);
        }
    }
    //new
    private void Spawn(GameObject spawn , List<Vector2Int> center)
    {
        //int posXC = center[1].x;
        //int PosYC = center[1].y;
        
        //spawn.gameObject.transform.position = center[1];
        spawn.transform.position = new Vector3(startPosition.x, startPosition.y,0);
    }
    private void SpawnPortal(GameObject endPoint , List<Vector2Int> center)
    {
        int posX = center[center.Count-1].x;
        int PosY = center[center.Count-1].y;
        
        //spawn.gameObject.transform.position = center[1];
        endPoint.transform.position = new Vector3(posX, PosY,0);
    }
    
    private void loadEnemies(List<Vector2Int> center)
    {
        // if there are no enemies spawned then we call the spawn enemies function  and place them into a array
        if (GameObject.FindGameObjectWithTag("Fighter") == null)
        {
            SpawnEneimes(center);
            enemyList = GameObject.FindGameObjectsWithTag("Fighter");
        }
        // if there are existing enemies in the level  then we destroy the existing enemies first  
        //  before we call the spawn enemies function and place them into a array
        else if(GameObject.FindGameObjectWithTag("Fighter") != null)
        {
            for (int i = 0; i < enemyList.Length; i++)
            {
                GameObject.DestroyImmediate(enemyList[i]);
            }
            SpawnEneimes(center);
            enemyList = GameObject.FindGameObjectsWithTag("Fighter");
        }
    }
    private void SpawnEneimes(List<Vector2Int> center)
    {
        // we take in the center room list
        for (int i = 1; i < center.Count-1; i++)
        {
            // we loop through the list to take the x,y coordinates for the position of the enemies 
            postions.x = center[i].x;
            postions.y = center[i].y;
            //Debug.Log(postions.x + "," +postions.y);
            
            // if I remove this commented old code bellow the algorithm doesnt work for unknown reasons 
            
            // we then create the spawn point for the enemies in a unitCircle using the spawn radius to determine the size of the circle 
            //Vector2 spawnPos = new Vector2(postions.x, postions.y);
            //spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
            // we instantiate the enemies using the enemies in the enemies array and setting the spawn point and rotation of the enemy instances
            //Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
            for (int j = 0; j < 2; j++)
            {
                // we then create the spawn point for the enemies in a unitCircle using the spawn radius to determine the size of the circle 
                Vector2 spawnPos = new Vector2(postions.x, postions.y);
                spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
                // we instantiate the enemies using the enemies in the enemies array and setting the spawn point and rotation of the enemy instances
                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
            }
            
        }
    }
}
