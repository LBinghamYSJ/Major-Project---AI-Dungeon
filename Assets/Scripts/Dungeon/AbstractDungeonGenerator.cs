using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TileMapVisualiser tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    private respawnpoint Respawn;

    private GameObject DontDestroy;
    private GameObject Player;
    private GameObject Exit;
    private TransferSceneData DontDestroyScript;


    public void GenerateDungeon()
    {
        DontDestroy = GameObject.FindWithTag("DontDestroy");
        DontDestroyScript = DontDestroy.GetComponent<TransferSceneData>();
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
        Respawn = FindObjectOfType<respawnpoint>();
        Respawn.PlaceRespawnPoint();
        Exit = GameObject.FindWithTag("Exit");
        Player = GameObject.FindWithTag("Player");
        Player.transform.position = Exit.transform.position;
        DontDestroyScript.SetNewRespawnPoint();
        DontDestroyScript.SaveEscortObject();
    }

    protected abstract void RunProceduralGeneration();
}