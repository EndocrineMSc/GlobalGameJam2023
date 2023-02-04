using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameName.Audio;
public enum GameplayStates {Attack, WaitForEndOfAttack, Peace}

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    public UnityEvent<GameplayStates> OnStateChange;

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
    
    int wave = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }


    private void Start()
    {
        ChangeState(GameplayStates.Peace);
    }


    void ChangeState(GameplayStates newState)
    {
        switch (newState)
        {
            case GameplayStates.Peace:
                AudioManager.Instance.FadeGameTrack(EnumCollection.Track.Track_001_Tree_of_Peace, EnumCollection.Fade.In, 6);
                AudioManager.Instance.FadeGameTrack(EnumCollection.Track.Track_002_Tree_of_War, EnumCollection.Fade.Out, 6);
                StartCoroutine(WaitForNextWave());
                break;
            case GameplayStates.WaitForEndOfAttack:
                if (enemiesLeft <= 0)
                {
                    ChangeState(GameplayStates.Peace);
                    return;
                }
                break;
            case GameplayStates.Attack:
                AudioManager.Instance.FadeGameTrack(EnumCollection.Track.Track_001_Tree_of_Peace, EnumCollection.Fade.Out, 6);
                AudioManager.Instance.FadeGameTrack(EnumCollection.Track.Track_002_Tree_of_War, EnumCollection.Fade.In, 6);
                StartCoroutine(SpawnWave(waves[wave]));
                wave++;
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
            ChangeState(GameplayStates.Peace);
    }
}
