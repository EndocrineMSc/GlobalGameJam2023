using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace GameName.PlayerHandling { 
    public class RessourceItem : MonoBehaviour
    {
        protected bool _nextToPlayer = false;
        protected bool _isCarried = false;

        public bool IsCarried { get { return _isCarried; } }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0;
            rigidbody.mass = 1;
            
            Collider2D collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger");
            Console.Write("Trigger");
            // Check if player is on top of item
            if (collision.gameObject.name == "Player")
            {
                Player.Instance.GetComponent<PlayerController>().playerInteracts.AddListener(OnPlayerUse);
                _nextToPlayer = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Check if player is on top of item
            if (collision.gameObject.name == "Player")
            {
                Player.Instance.GetComponent<PlayerController>().playerInteracts.RemoveListener(OnPlayerUse);
                _nextToPlayer = false;
            }
        }

        public void OnPlayerUse()
        {
            if (_isCarried)
            {
                // Drop
                transform.SetParent(null, true);
                GetComponent<Rigidbody2D>().isKinematic = false;
                _isCarried = false;
            }
            else if (!_isCarried && _nextToPlayer)
            {
                // Pick Up
                transform.SetParent(Player.Instance.transform, true);
                GetComponent<Rigidbody2D>().isKinematic = true;
                _isCarried = true;
            }
        }

    }
}

