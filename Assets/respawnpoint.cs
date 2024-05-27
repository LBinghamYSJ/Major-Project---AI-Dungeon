using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnpoint : MonoBehaviour
{

    private GameObject Player;
    private static respawnpoint respawnPointInstance;
    private void Awake()
    {
        if (respawnPointInstance == null)
        {
            respawnPointInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaceRespawnPoint()
    {
        Player = GameObject.FindWithTag("Player");
        transform.position = Player.transform.position;
    }
}
