using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.PlayerHandling
{
    public class Player : MonoBehaviour
    {
        #region Fields

        public static Player Instance { get; private set; }

        #endregion

        #region Functions

        private void Awake()
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
        #endregion
    }

}
