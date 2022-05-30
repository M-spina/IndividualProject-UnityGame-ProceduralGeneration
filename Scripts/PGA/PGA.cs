using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PGA  // this class is the procedural generation Algorithm class 
{
    
    // Using a hash set to store unique values if the type that we store in it implements getHashCode method and equals method
    //meaning we can use in the hash set data allowing us to remove duplicates as this function can step on the same position multiple times 
    
    // StartPosition is where the agent would start the walk and the walklenght is the distance the agent would go for and return ore values

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
    
    //this method will make the random walk select a single direction and walk in this direction using the corridor length
    // and return the path created 
    // and pass in the the last position on the path to get the next start Position

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startingPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startingPosition;
        corridor.Add(currentPosition);
        
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }
    //(IMPORTANT THIS METHOD IS NO LONGER BEING USED -- AND WILL BE REMOVED. THIS ALGORITHM DID NOT WORK)
    // creating a BinarySpacePartition to create rooms by have a space to split into room that are or a certain size

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit,int minWidth,int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            // we want to get rid of room that are too small for the room generation
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                // if the room meets the size requirements we are going to split the room randomly
                if (Random.value < 0.5)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                    
                }
            }
        }
        return roomsList;
    }
//(IMPORTANT THIS METHOD IS NO LONGER BEING USED -- AND WILL BE REMOVED. THIS ALGORITHM DID NOT WORK))
    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);

    }
//(IMPORTANT THIS METHOD IS NO LONGER BEING USED -- AND WILL BE REMOVED. THIS ALGORITHM DID NOT WORK)
    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y); //  (minHeight , room.size.y - minHeight) // this would create gid like structure rather than random rooms
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}
// we create a static class so that otheer class can use its data an methdos ot get a random cardinal direction
// this is used to move the agent in the PCG algorithm .
public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int> /// store a list of direction that can be added to the current position of an agent to move
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static Vector2Int GetRandomCardinalDirection() // retruns a radnomly selected direction form the list of cardinal directions
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}
