using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualiser : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap, wallTileMap;

    [SerializeField] private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, wallInnerCornerDownLeft, wallInnerCornerDownRight, wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

    public void PaintFloorTiles(IEnumerable<Vector2Int> FloorPositions)
    {
        PaintTiles(FloorPositions, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap TileMap, TileBase Tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(TileMap, Tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if(WallBytesTypes.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if(WallBytesTypes.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallBytesTypes.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallBytesTypes.wallBottom.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallBytesTypes.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }    
        if (tile != null)
        {
            PaintSingleTile(wallTileMap, tile, position);
        }
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType,2);
        TileBase tile = null;

        if(WallBytesTypes.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallBytesTypes.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallBytesTypes.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallBytesTypes.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallBytesTypes.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallBytesTypes.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallBytesTypes.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallBytesTypes.wallBottomEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        if (tile != null)
        {
            PaintSingleTile(wallTileMap, tile, position);
        }
    }
}
