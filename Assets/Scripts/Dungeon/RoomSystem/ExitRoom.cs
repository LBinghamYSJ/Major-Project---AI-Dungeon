using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitRoom : RoomGenerator
{
    public GameObject Exit;

    public List<ItemPlacementData> itemData;

    [SerializeField] private PrefabPlacer prefabPlacer;

    public override List<GameObject> ProcessRoom(Vector2Int roomCenter, HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorNoCorridors)
    {

        ItemPlacementHelper itemPlacementHelper = new ItemPlacementHelper(roomFloor, roomFloorNoCorridors);

        List<GameObject> placedObjects = prefabPlacer.PlaceAllItems(itemData, itemPlacementHelper);

        Vector2Int ExitSpawnPoint = roomCenter;

        GameObject ExitObject = prefabPlacer.CreateObject(Exit, ExitSpawnPoint + new Vector2(0.5f, 0.5f));

        placedObjects.Add(ExitObject);

        return placedObjects;
    }
}

