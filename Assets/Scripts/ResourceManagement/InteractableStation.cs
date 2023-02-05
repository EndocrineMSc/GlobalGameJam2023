using UnityEngine;
using UnityEngine.Events;
using GameName.PlayerHandling;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InteractableStation : MonoBehaviour
{
    public UnityEvent enoughResourcesAllocated;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private bool showLevelText = true;
    [SerializeField]
    private int[] _targetResourceAmount;

    private bool _resourceMarkedForDestruction;
    private float _suckSpeed = 200f;
    /// <summary>
    /// Marks if all upgrade of a station alreay have been purchased.
    /// </summary>
    private bool allUpgraded = false;


    public int TargetResourceAmount
    {
        get { return _targetResourceAmount[currentTimeUsing]; }
        private set { _targetResourceAmount[currentTimeUsing] = value; }
    }

    private int _currentResourceAmount = 0;
    /// <summary>
    /// How many times this station was already used.
    /// Used for incrementing the cost.
    /// </summary>
    private int currentTimeUsing = 0;

    public int CurrentResourceAmount
    {
        get { return _currentResourceAmount; }
        set { _currentResourceAmount = value; }
    }

    private void Start()
    {
        levelText.text = "Level " + currentTimeUsing.ToString();
        ShowHideText(false);
    }

    private void AddResource()
    {
        _currentResourceAmount++;

        if (_currentResourceAmount >= TargetResourceAmount)
        {
            if (enoughResourcesAllocated != null)
                enoughResourcesAllocated.Invoke();

            currentTimeUsing++;
            levelText.text = "Level " + currentTimeUsing.ToString();

            if (currentTimeUsing >= _targetResourceAmount.Length)
            {
                allUpgraded = true;
                slider.gameObject.SetActive(false);
            }
            else
                ResetStation();
        }
        UpdateSlider();
    }

    private void ResetStation()
    {
        _currentResourceAmount = 0;
        UpdateSlider();
    }

    void UpdateSlider()
    {
        if(!allUpgraded)
            slider.value = _currentResourceAmount / (float)TargetResourceAmount;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (allUpgraded)
            return;

        if (collider.gameObject.CompareTag("Resource"))
        {
            RessourceItem resource = collider.gameObject.GetComponent<RessourceItem>();


            AddResource();
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Player"))
        {
            ShowHideText(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowHideText(false);
        }
    }

    void ShowHideText(bool textIsShown)
    {
        nameText.enabled = textIsShown;
        levelText.enabled = (showLevelText) ? textIsShown : false;
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
