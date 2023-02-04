using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSubscriber : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Interactable>().PlayerUse.AddListener(OnPlayerUse);
    }

    private void OnDisable()
    {
        GetComponent<Interactable>().PlayerUse.RemoveListener(OnPlayerUse);
    }

    void OnPlayerUse()
    {
    
    }
}
