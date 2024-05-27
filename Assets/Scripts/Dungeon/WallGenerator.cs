using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualiser tileMapVisualiser)
    {
        var WallPositions = FindWallsInDirections(floorPositions, Direction.DirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction.DiagonalDirectionsList);

        CreateBasicWalls(tileMapVisualiser, WallPositions, floorPositions);
        CreateCornerWalls(tileMapVisualiser, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWalls(TileMapVisualiser tileMapVisualiser, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction.EightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileMapVisualiser.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWalls(TileMapVisualiser tileMapVisualiser, HashSet<Vector2Int> WallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in WallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction.DirectionsList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileMapVisualiser.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionsList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
}
