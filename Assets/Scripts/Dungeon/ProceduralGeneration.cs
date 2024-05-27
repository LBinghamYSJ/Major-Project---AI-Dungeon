using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class ProceduralGeneration
{
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> Path = new HashSet<Vector2Int>();

        Path.Add(startPosition);
        var previousPosition = startPosition;

        for(int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction.GetRandomDirection();
            Path.Add(newPosition);
            previousPosition = newPosition;
        }
        return Path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction.GetRandomDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt> ();
        List<BoundsInt> roomsList = new List<BoundsInt> ();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minHeight, roomsQueue, room);
                    }
                    else
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
                    else
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction
{
    public static List<Vector2Int> DirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), // Up Direction
        new Vector2Int(1,0), // Right Direction
        new Vector2Int(0, -1), // Down Direction
        new Vector2Int(-1, 0) // Left Direction
    };

    public static List<Vector2Int> DiagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), // Up-Right Direction
        new Vector2Int(1,-1), // Right-Down Direction
        new Vector2Int(-1, -1), // Down-Left Direction
        new Vector2Int(-1, 1) // Left-Up Direction
    };

    public static List<Vector2Int> EightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), // Up Direction
        new Vector2Int(1,1), // Up-Right Direction
        new Vector2Int(1,0), // Right Direction
        new Vector2Int(1,-1), // Right-Down Direction
        new Vector2Int(0, -1), // Down Direction
        new Vector2Int(-1, -1), // Down-Left Direction
        new Vector2Int(-1, 0), // Left Direction
        new Vector2Int(-1, 1) // Left-Up Direction
    };

    public static Vector2Int GetRandomDirection()
    {
        return DirectionsList[Random.Range(0, DirectionsList.Count)];
    }
}
