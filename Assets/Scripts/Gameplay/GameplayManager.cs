using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameName.Audio;
using GameName;

public enum GameplayStates {Attack, WaitForEndOfAttack, Peace}

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    public UnityEvent<GameplayStates> OnStateChange;
    public UnityEvent<int> OnWaveStartSpawning;
    public UnityEvent<int> OnWaveOvercome;
    public UnityEvent OnWonGame;
    public UnityEvent OnLostGame;

    [SerializeField]
    private float timeBetweenWavesInSeconds = 40;
    [SerializeField]
    private Transform leftSpawnPoint;
    [SerializeField]
    private Transform rightSpawnPoint;

    [SerializeField]
    private GameObject beetlePrefab;
    [SerializeField]
    private GameObject fireflyPrefab;

    public EnumCollection.SFX[] enemySpawnSounds;
    public EnemyWave[] waves;

    [SerializeField]
    GameplayStates currentState;

    int enemiesLeft = 0;
    
    int currentWave = 0;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _victoryCanvas;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }


    private void Start()
    {
        ChangeState(GameplayStates.Peace);

        if (TreeBark.leftTreebark != null)
            TreeBark.leftTreebark.OnDeath.AddListener(TreeBarkDestroyed);
        else
            Debug.LogError("Could not find left tree bark");

        if (TreeBark.rightTreebark != null)
            TreeBark.rightTreebark.OnDeath.AddListener(TreeBarkDestroyed);
        else
            Debug.LogError("Could not find right tree bark");

    }

    public void WonGame()
    {
        Debug.Log("Won Game");

        // TODO: implement.
        if (OnWonGame != null)
            OnWonGame.Invoke();

        _victoryCanvas.SetActive(true);
    }

    public void LostGame()
    {
        Debug.Log("Lost Game");

        if (OnLostGame != null)
            OnLostGame.Invoke();

        _gameOverCanvas.SetActive(true);
    }

    void TreeBarkDestroyed(HealthEntity treeBark)
    {
        LostGame();
    }

    void ChangeState(GameplayStates newState)
    {
        switch (newState)
        {
            case GameplayStates.Peace:
                if(currentWave >= waves.Length)
                    WonGame();
                else
                    StartCoroutine(WaitForNextWave());

                AudioManager.Instance.FadeGameTrack(EnumCollection.Track.Track_001_Tree_of_Peace, EnumCollection.Fade.In, 6);
                AudioManager.Instance.FadeGameTrack(EnumCollection.Track.Track_002_Tree_of_War, EnumCollection.Fade.Out, 6);
                break;
            case GameplayStates.WaitForEndOfAttack:
                if (enemiesLeft <= 0)
                {
                    ChangeState(GameplayStates.Peace);  

                    if (OnWaveOvercome != null)
                        OnWaveOvercome.Invoke(currentWave - 1);
                    return;
                }
                break;
            case GameplayStates.Attack:
                if (currentWave >= waves.Length)
                {
                    return;
                }

                AudioManager.Instance.FadeGameTrack(EnumCollection.Track.Track_001_Tree_of_Peace, EnumCollection.Fade.Out, 6);
                AudioManager.Instance.FadeGameTrack(EnumCollection.Track.Track_002_Tree_of_War, EnumCollection.Fade.In, 6);
                StartCoroutine(SpawnWave(waves[currentWave]));
                currentWave++;
                break;
        }

        currentState = newState;

        if (OnStateChange != null)
            OnStateChange.Invoke(newState);
    }

    IEnumerator WaitForNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWavesInSeconds);
        ChangeState(GameplayStates.Attack);
    }

    IEnumerator SpawnWave(EnemyWave enemyWave)
    {
        if (OnWaveStartSpawning != null)
            OnWaveStartSpawning.Invoke(currentWave);

        List<EnemySet> spawnPool = new List<EnemySet>();

        // Initialize spawn list.
        foreach (EnemySet es in enemyWave.enemySets)
        {
            for(int i = 0; i < es.amount; i++)
            {
                spawnPool.Add(new EnemySet(es.enemyType, es.direction));
            }
        }

        while (spawnPool.Count > 0)
        {
            int ran = Random.Range(0, spawnPool.Count);
            EnemySet newEnemyData = spawnPool[ran];
            spawnPool.RemoveAt(ran);

            GameObject spawnPrefab = GetPrefabForType(newEnemyData.enemyType);
            Vector3 spawnPosition = (newEnemyData.direction == AttackDirektion.Left) ? leftSpawnPoint.position : rightSpawnPoint.position;
            if (newEnemyData.enemyType == EnemyType.FireFly)
                spawnPosition.y += 2;
            spawnPosition.y += Random.Range(-0.5f, 0.5f);

            GameObject newEnemy = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);

            PlayEnemyAppearSound();

            enemiesLeft++;
            newEnemy.GetComponent<HealthEntity>().OnDeath.AddListener(EnemyDied);

            yield return new WaitForSeconds(enemyWave.spawnCooldown);
        }

        ChangeState(GameplayStates.WaitForEndOfAttack);
    }

    void PlayEnemyAppearSound()
    {
        if (AudioManager.Instance == null)
            return;

        EnumCollection.SFX soundEffect = enemySpawnSounds[Random.Range(0, enemySpawnSounds.Length)];
         
        AudioManager.Instance.PlaySoundEffect(soundEffect);
    }

    GameObject GetPrefabForType(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Beetle:
                return beetlePrefab;
            case EnemyType.FireFly:
                return fireflyPrefab;
        }
        return beetlePrefab;
    }

    void EnemyDied(HealthEntity healthEntity)
    {
        enemiesLeft--;

        if (enemiesLeft <= 0 && currentState == GameplayStates.WaitForEndOfAttack)
        {
            ChangeState(GameplayStates.Peace);

            if (OnWaveOvercome != null)
                OnWaveOvercome.Invoke(currentWave - 1);
        }
    }
}
