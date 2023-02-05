using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private Squirrel leftSquirrel;
    [SerializeField]
    private Squirrel rightSquirrel;
    [SerializeField]
    private Bird leftBird;
    [SerializeField]
    private Bird rightBird;
    [SerializeField]
    Transform slimeLeftPos;
    [SerializeField]
    Transform slimeRightPos;
    [SerializeField]
    GameObject slimePrefab;

    private void Start()
    {
        leftSquirrel.gameObject.SetActive(false);
        rightSquirrel.gameObject.SetActive(false);
        leftBird.gameObject.SetActive(false);
        rightBird.gameObject.SetActive(false);
    }

    public void UpgradeLeftSquirrel()
    {
        if (!leftSquirrel.gameObject.activeSelf)
            leftSquirrel.gameObject.SetActive(true);
        else
            leftSquirrel.Upgrade();
    }

    public void UpgradeRightSquirrel()
    {
        if (!rightSquirrel.gameObject.activeSelf)
            rightSquirrel.gameObject.SetActive(true);
        else
            rightSquirrel.Upgrade();
    }


    public void UpgradeLeftBird()
    {
        if (!leftBird.gameObject.activeSelf)
            leftBird.gameObject.SetActive(true);
        else
            leftBird.Upgrade();
    }


    public void UpgradeRightBird()
    {
        if (!rightBird.gameObject.activeSelf)
            rightBird.gameObject.SetActive(true);
        else
            rightBird.Upgrade();
    }

    public void SpawnSpecialAttack()
    {
        Instantiate(slimePrefab, slimeLeftPos.position, Quaternion.identity);
        Instantiate(slimePrefab, slimeRightPos.position, Quaternion.identity);
    }
}
