using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected int iterations = 10;
    [SerializeField] protected int walkLength = 10;
    [SerializeField] protected bool startRandomlyEachIteration = true;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> FloorPositions = RunRandomWalk(startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(FloorPositions);
        WallGenerator.CreateWalls(FloorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> FloorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGeneration.RandomWalk(currentPosition, walkLength);
            FloorPositions.UnionWith(path);
            if(startRandomlyEachIteration)
            {
                currentPosition = FloorPositions.ElementAt(UnityEngine.Random.Range(0, FloorPositions.Count));
            }
        }
        return FloorPositions;
    }
}
