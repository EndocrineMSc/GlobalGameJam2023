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

        [SerializeField] private Slider _healthFill;
        private 

        #endregion

        #region Functions

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            /*
            _healthFill.value = _player.Health;
            _shieldFill.value = _player.Shield;

            if (_player.Shield > 0)
            {
                _shield.enabled = true;
                _heart.enabled = false;
                _shieldText.enabled = true;

                _shieldText.text = _player.Shield.ToString();
            }
            else
            {
                _shield.enabled = false;
                _heart.enabled = true;
                _shieldText.enabled = false;
            }
            */
        }

        #endregion
    }
}
