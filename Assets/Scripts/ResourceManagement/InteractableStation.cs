using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableStation : MonoBehaviour
{
    public UnityEvent enoughResourcesAllocated;

    private int targetResourceAmount;
    private bool _playerIsClose;

    public int TargetResourceAmount
    {
        get { return targetResourceAmount; }
        private set { targetResourceAmount = value; }
    }

    private int _currentResourceAmount;

    public int CurrentResourceAmount
    {
        get { return _currentResourceAmount; }
        set { _currentResourceAmount = value; }
    }  

    private void AddResource()
    {
        CurrentResourceAmount++;

        if (CurrentResourceAmount >= TargetResourceAmount)
        {
            if (enoughResourcesAllocated != null)
            {
                enoughResourcesAllocated.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Contains("Player"))
        {
            _playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name.Contains("Player"))
        {
            _playerIsClose = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerIsClose)
        {

        }
    }
}
