using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.PlayerHandling
{
    public class NewRessourceItem : MonoBehaviour
    {
        [SerializeField]
        private float pickUpDistance;
        
        protected bool _isCarried = false;

        private void Start()
        {
            Player.Instance.GetComponent<PlayerController>().playerInteracts.AddListener(OnPlayerUse);
        }


        private void OnDisable()
        {
            if(Player.Instance != null)
                Player.Instance.GetComponent<PlayerController>().playerInteracts.RemoveListener(OnPlayerUse);
        }

        public void OnPlayerUse()
        {
            // If the player is to far away don't do anything.
            if ((transform.position - Player.Instance.transform.position).sqrMagnitude > pickUpDistance * pickUpDistance)
                return;

            // Otherwise attack to player.

            _isCarried = !_isCarried;

            if (_isCarried)
                // Pick Up
                transform.SetParent(Player.Instance.transform, true);
            else
                // Drop
                transform.SetParent(null, true);

            GetComponent<Rigidbody2D>().isKinematic = _isCarried;
            GetComponent<CircleCollider2D>().enabled = !_isCarried;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, pickUpDistance);
        }
    }
}
