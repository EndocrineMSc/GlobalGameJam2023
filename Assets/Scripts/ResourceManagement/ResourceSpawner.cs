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
    [SerializeField] private float[] _spawnCooldown;
    /// <summary>
    /// The amount of energy gained after a wave is completed.
    /// </summary>
    [SerializeField] private int[] rewards;
    private bool _cooldownIsActive;
    private Vector2 _spawnPosition;
    private float currentCooldown;

    public float SpawnCoolDown { get { return currentCooldown; } private set { currentCooldown = value; } }

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
        currentCooldown = _spawnCooldown[0];
           _spawnPosition = new Vector2(transform.position.x + _xSpawnOffset, transform.position.y + _ySpawnOffset);
        GameplayManager.instance.OnWaveOvercome.AddListener(OnWaveOvercome);
    }

    private void Update()
    {
        if(!_cooldownIsActive)
        {
            StartCoroutine(TimedPrefabSpawning());
        }
    }

    void OnWaveOvercome(int waveCount)
    {
        if(waveCount < _spawnCooldown.Length)
            currentCooldown = _spawnCooldown[waveCount];

        StartCoroutine(SpawnReward(rewards[waveCount]));
    }

    private IEnumerator TimedPrefabSpawning()
    {
        _cooldownIsActive = true;
        GameObject resourceInstance = SpawnPrefab();
        KickPrefab(resourceInstance);
        yield return new WaitForSeconds(currentCooldown);
        _cooldownIsActive = false;
    }

    /// <summary>
    /// Spawns the reward with a small time delay.
    /// Gets called recusively until the whole reward is spawned.
    /// </summary>
    /// <param name="howManyMore"></param>
    /// <returns></returns>
    private IEnumerator SpawnReward(int howManyMore)
    {
        SpawnPrefab();
        yield return new WaitForSeconds(0.2f);
        howManyMore--;
        if (howManyMore > 0)
            StartCoroutine(SpawnReward(howManyMore));
    }

    private GameObject SpawnPrefab()
    {
        GameObject resourceInstance = Instantiate(_resourcePrefab, _spawnPosition, Quaternion.identity);
        resourceInstance.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 5);
        return resourceInstance;
    }

    private void KickPrefab(GameObject resourceInstance)
    {
        Rigidbody2D rigidbody = resourceInstance.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.right * _spawnJumpSpeed);
        rigidbody.AddForce(Vector2.up * _spawnJumpSpeed);
    }
}
