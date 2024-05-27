using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferSceneData : MonoBehaviour
{

    private GameObject Escort;
    private GameObject Player;
    private GameObject RespawnPoint;
    private GameObject TransferSceneDataObject;
    private static TransferSceneData transferSceneInstance;
    private void Awake()
    {
        if (transferSceneInstance == null)
        {
            transferSceneInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        TransferSceneDataObject = GameObject.FindWithTag("DontDestroy");
        Player = GameObject.FindWithTag("Player");
        RespawnPoint = GameObject.FindWithTag("RespawnPoint");
        DontDestroyOnLoad(TransferSceneDataObject);
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(RespawnPoint);
    }

    public void SetNewRespawnPoint()
    {
        Player = GameObject.FindWithTag("Player");
        RespawnPoint = GameObject.FindWithTag("RespawnPoint");
        RespawnPoint.transform.position = Player.transform.position;
    }

    public void SaveEscortObject()
    {
        TransferSceneDataObject = GameObject.FindWithTag("DontDestroy");
        Escort = GameObject.FindWithTag("Escort");
        Escort.transform.parent = TransferSceneDataObject.transform;
    }
}
