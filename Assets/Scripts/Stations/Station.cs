using GameName.PlayerHandling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Station : MonoBehaviour
{

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        Console.Write("Trigger");
        // Check if player is on top of item
        if (collision.gameObject.name == "Player")
        {
            Player.Instance.GetComponent<PlayerController>().playerInteracts.AddListener(OnPlayerUse);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        // Check if player is on top of item
        if (collision.gameObject.name == "Player")
        {
            Player.Instance.GetComponent<PlayerController>().playerInteracts.RemoveListener(OnPlayerUse);
        }
    }

    protected abstract void OnPlayerUse();
}
