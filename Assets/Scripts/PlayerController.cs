using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.PlayerHandling
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _speed;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        #endregion

        #region Functions

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody= GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            float x = Input.GetAxisRaw("Horizontal");

            if (x > 0)
            {
                _rigidbody.AddForce(Vector2.right * _speed);
                _spriteRenderer.flipX = false;
            }
            else if (x < 0)
            {
                _rigidbody.AddForce(Vector2.left * _speed);
                _spriteRenderer.flipX= true;
            }
        }

        #endregion
    }
}
