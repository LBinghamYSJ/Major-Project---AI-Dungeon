using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EscortRoom : RoomGenerator
{
    public GameObject Escort;

    private GameObject Player;
    private PlayerMovement PlayerScript;

    public List<ItemPlacementData> itemData;

    [SerializeField] private PrefabPlacer prefabPlacer;

    public override List<GameObject> ProcessRoom(Vector2Int roomCenter, HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorNoCorridors)
    {
        Player = GameObject.FindWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerMovement>();
        ItemPlacementHelper itemPlacementHelper = new ItemPlacementHelper(roomFloor, roomFloorNoCorridors);

        List<GameObject> placedObjects = prefabPlacer.PlaceAllItems(itemData, itemPlacementHelper);

        Vector2Int EscortSpawnPoint = roomCenter;

        GameObject EscortObject = prefabPlacer.CreateObject(Escort, EscortSpawnPoint + new Vector2(0.5f, 0.5f));

        placedObjects.Add(EscortObject);

        PlayerScript.GrabEscort();

        return placedObjects;
    }
}

