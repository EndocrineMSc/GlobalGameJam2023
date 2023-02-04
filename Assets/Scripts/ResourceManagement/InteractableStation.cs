using UnityEngine;
using UnityEngine.Events;
using GameName.PlayerHandling;
using System.Collections;

public class InteractableStation : MonoBehaviour
{
    public UnityEvent enoughResourcesAllocated;

    private float _suckSpeed = 200f;
    [SerializeField]
    private int _targetResourceAmount = 10;
    private bool _resourceMarkedForDestruction;

    public int TargetResourceAmount
    {
        get { return _targetResourceAmount; }
        private set { _targetResourceAmount = value; }
    }

    private int _currentResourceAmount = 0;

    public int CurrentResourceAmount
    {
        get { return _currentResourceAmount; }
        set { _currentResourceAmount = value; }
    }

    private void Start()
    {

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
        if (collider.gameObject.CompareTag("Resource"))
        {
            RessourceItem resource = collider.gameObject.GetComponent<RessourceItem>();


            AddResource();
            Destroy(collider.gameObject);
        }
    }

    private IEnumerator DestroyResourceItem (RessourceItem item)
    {
        _resourceMarkedForDestruction = true;
        yield return new WaitForSeconds(1f);
        AddResource();
        Destroy(item.gameObject);
        _resourceMarkedForDestruction = false;
    }
}
