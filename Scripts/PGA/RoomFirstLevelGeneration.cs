using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class RoomFirstLevelGeneration : SimpleRandomWalkLevelGeneration
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;  
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;
    
    private Vector2 postions;

    [SerializeField] private float spawnRadius = 7;

    public GameObject spawn;
    public GameObject portal;
    public GameObject[] enemies;
    private GameObject[] enemyList;
    

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = PGA.BinarySpacePartitioning(new BoundsInt((Vector3Int) startPosition, 
                new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRandomRooms(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }
        //floor = CreateSimpleRooms(roomsList);
        // might connet the player spawn to a room center

        List<Vector2Int> roomCenetrs = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenetrs.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        
        Spawn(spawn,roomCenetrs);
        SpawnPortal(portal,roomCenetrs);
        loadEnemies(roomCenetrs);
        

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenetrs);
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor,tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRandomRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter =
                new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset)
                                                             && position.y >= (roomBounds.yMin - offset) &&
                                                             position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
                    
            }
        }

        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenetrs)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        //select a random room form our random room center list
        var currentRoomCenter = roomCenetrs[Random.Range(0,roomCenetrs.Count)];
        // remove it form the randomCenter Hashset so we dont connecte to this room again
        roomCenetrs.Remove(currentRoomCenter);
        while (roomCenetrs.Count > 0)
        {
            Vector2Int nearest = FindNearestPointTo(currentRoomCenter, roomCenetrs);
            roomCenetrs.Remove(nearest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, nearest);
            currentRoomCenter = nearest;
            corridors.UnionWith(newCorridor);
        }

        return corridors;

    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int endPoint)
    {
        
        //we create a Hash set to store the position of the currentRoom center
        // we the do a loop for both the y and x coordinates of the endpoint comparing it to the current position
        // the loop determines the direction of where the corridor need to go in order to reach the end points x and y corrdinates
        // once found the position is added to the hash set
        // and we return the hash set
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != endPoint.y)
        {
            if (endPoint.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (endPoint.y < position.y)
            {
                position += Vector2Int.down;
            }

            corridor.Add(position);
        }
        while (position.x != endPoint.x)
        {
            if (endPoint.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (endPoint.x < position.x)
            {
                position+= Vector2Int.left;
            }

            corridor.Add(position);
        }

        return corridor;
    }
    
    //find the nearest point to the current room center
    private Vector2Int FindNearestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenetrs)
    {
        // find the distance between the current roomCenter and the roomCenter we are checking 
        //and if the distance is smaller then the Max value we are going to set the new length as the length
        // and the nearest as the currently iterated center room
        Vector2Int nearest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenetrs)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                nearest = position;
            }
        }
        return nearest;
    }

    //for each point in the bounds we created a floor position , added those position to the floor hashset and we loop through each room
    //so the has set contains all the floors for all the rooms
    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row <room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int) room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }   
            }
        }
        return floor;
    }
    
    // place the player spawn point at the center of room one
    private void Spawn(GameObject spawn , List<Vector2Int> center)
    {
        int posX = center[0].x;
        int PosY = center[0].y;
        
        //spawn.gameObject.transform.position = center[1];
        spawn.transform.position = new Vector3(posX, PosY,0);
    }
    // place the end spawn point at the center of the last room 
    private void SpawnPortal(GameObject endPoint , List<Vector2Int> center)
    {
        int posX = center[^1].x;
        int PosY = center[^1].y;
        
        //spawn.gameObject.transform.position = center[1];
        endPoint.transform.position = new Vector3(posX, PosY,0);
    }

    
    // load enemies into the current level 
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
    // spawn the enemies 
    private void SpawnEneimes(List<Vector2Int> center)
    {
        // we take in the center room list
        for (int i = 1; i < center.Count - 1; i++)
        {
            // we loop through the list to take the x,y coordinates for the position of the enemies 
            postions.x = center[i].x;
            postions.y = center[i].y;
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
