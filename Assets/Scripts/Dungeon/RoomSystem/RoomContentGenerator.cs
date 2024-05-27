using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RoomContentGenerator : MonoBehaviour
{
    [SerializeField] private RoomGenerator defaultRoom, escortRoom, exitRoom;

    List<GameObject> spawnedObjects = new List<GameObject>();

    [SerializeField] private GraphTest graphTest;


    public Transform itemParent;

    public UnityEvent RegenerateDungeon;
    public void GenerateRoomContent(DungeonData dungeonData)
    {
        foreach (GameObject item in spawnedObjects)
        {
            DestroyImmediate(item);
        }
        spawnedObjects.Clear();

        SelectEscortSpawnPoint(dungeonData);
        SelectExitSpawnPoint(dungeonData);
        SelectEnemySpawnPoints(dungeonData);

        foreach (GameObject item in spawnedObjects)
        {
            if (item != null)
                item.transform.SetParent(itemParent, false);
        }
    }

    private void SelectEscortSpawnPoint(DungeonData dungeonData)
    {
        int randomRoomIndex = UnityEngine.Random.Range(0, dungeonData.roomsDictionary.Count);
        Vector2Int escortSpawnPoint = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);

        graphTest.RunDijkstraAlgorithm(escortSpawnPoint, dungeonData.floorPositions);

        Vector2Int roomIndex = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);

        List<GameObject> placedPrefabs = escortRoom.ProcessRoom(escortSpawnPoint, dungeonData.roomsDictionary.Values.ElementAt(randomRoomIndex), dungeonData.GetRoomFloorWithoutCorridors(roomIndex));

        spawnedObjects.AddRange(placedPrefabs);

        dungeonData.roomsDictionary.Remove(escortSpawnPoint);
    }


    private void SelectExitSpawnPoint(DungeonData dungeonData)
    {
        int randomRoomIndex = UnityEngine.Random.Range(0, dungeonData.roomsDictionary.Count);
        Vector2Int exitSpawnPoint = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);

        graphTest.RunDijkstraAlgorithm(exitSpawnPoint, dungeonData.floorPositions);

        Vector2Int roomIndex = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);

        List<GameObject> placedPrefabs = exitRoom.ProcessRoom(exitSpawnPoint, dungeonData.roomsDictionary.Values.ElementAt(randomRoomIndex), dungeonData.GetRoomFloorWithoutCorridors(roomIndex));

        spawnedObjects.AddRange(placedPrefabs);

        dungeonData.roomsDictionary.Remove(exitSpawnPoint);
    }

    private void SelectEnemySpawnPoints(DungeonData dungeonData)
    {
        foreach (KeyValuePair<Vector2Int, HashSet<Vector2Int>> roomData in dungeonData.roomsDictionary)
        {
            spawnedObjects.AddRange(
                defaultRoom.ProcessRoom(
                    roomData.Key,
                    roomData.Value,
                    dungeonData.GetRoomFloorWithoutCorridors(roomData.Key)
                    )
            );

        }
    }

}
