using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameName.ResourceHandling
{
    public class ProgressBar : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Slider _resourceFill;
        private InteractableStation _interactableStation;

        #endregion

        #region Functions

        private void Start()
        {
            _interactableStation = GetComponent<InteractableStation>();
            _resourceFill.maxValue = _interactableStation.TargetResourceAmount;
        }

        void Update()
        {
            _resourceFill.value = _interactableStation.CurrentResourceAmount;
        }

        #endregion
    }
}
