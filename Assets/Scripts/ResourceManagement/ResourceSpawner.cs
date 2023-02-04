using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public static ResourceSpawner Instance { get; private set; }

    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private float _xSpawnOffset = 0.5f;
    [SerializeField] private float _ySpawnOffset = 1f;
    [SerializeField] private float _spawnJumpSpeed = 1.5f;
    [SerializeField] private float _spawnCooldown = 5f;
    private bool _cooldownIsActive;
    private Vector2 _spawnPosition;

    public float SpawnCoolDown { get { return _spawnCooldown; } private set { _spawnCooldown = value; } }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        _spawnPosition = new Vector2(transform.position.x + _xSpawnOffset, transform.position.y + _ySpawnOffset);
    }

    private void Update()
    {
        if(!_cooldownIsActive)
        {
            StartCoroutine(TimedPrefabSpawning());
        }
    }

    private IEnumerator TimedPrefabSpawning()
    {
        _cooldownIsActive = true;
        GameObject resourceInstance = SpawnPrefab();
        KickPrefab(resourceInstance);
        yield return new WaitForSeconds(_spawnCooldown);
        _cooldownIsActive = false;
    }

    private GameObject SpawnPrefab()
    {
        GameObject resourceInstance = Instantiate(_resourcePrefab, _spawnPosition, Quaternion.identity);
        return resourceInstance;
    }

    private void KickPrefab(GameObject resourceInstance)
    {
        Rigidbody2D rigidbody = resourceInstance.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.right * _spawnJumpSpeed);
        rigidbody.AddForce(Vector2.up * _spawnJumpSpeed);
    }

    public void SetNewResourceCooldownTime(float time)
    {
        _spawnCooldown = time;
    }
}
