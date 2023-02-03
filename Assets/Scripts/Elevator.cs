using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameName.PlayerHandling;


namespace GameName.Tree.Traversation
{
    public class Elevator : MonoBehaviour
    {
        #region Fields

        private Player _player;
        private PlayerController _playerController;
        private Rigidbody2D _rigidbody;
        private bool _isMoving;
        private bool _playerIsOnElevator;
        private bool _isOnTopPosition;
        private bool _isOnBottomPosition;
        private Collider2D _currentElevatorStop;
        [SerializeField] private float _elevatorSpeed = 2f;

        #endregion

        #region Functions

        private void Start()
        {
            _player = Player.Instance;
            _playerController = _player.GetComponent<PlayerController>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name.Contains("Player"))
            {
                _playerIsOnElevator = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.name.Contains("Player"))
            {
                _playerIsOnElevator = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("ElevatorBreakPoint"))
            {
                StopElevator();
                CheckIfBottomOrTop(collision);
                DisableElevatorStopCollider(collision);
                _playerController.enabled = true;
            }
        }
        
        private void CheckIfBottomOrTop(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("Top"))
            {
                _isOnTopPosition = true;
            }
            else if (collision.gameObject.name.Contains("Bottom"))
            {
                _isOnBottomPosition = true;
            }
        }

        private void AlignElevator(Collider2D collision)
        {
            if (_rigidbody.velocity.y > 1)
            {
                
            }
        }

        private void StopElevator()
        {
            _rigidbody.velocity = Vector2.zero;
            _isMoving = false;
        }

        private void DisableElevatorStopCollider(Collider2D collision)
        {
                _currentElevatorStop = collision.gameObject.GetComponent<Collider2D>();
                _currentElevatorStop.enabled = false;
        }

        private void Update()
        {
            OnButtonPressMoveElevatorDownwards();
            OnButtonPressMoveElevatorUpwards();
            SetElevatorStopColliderActive();
        }

        private void OnButtonPressMoveElevatorUpwards()
        {
            if (Input.GetKeyDown(KeyCode.W) && _playerIsOnElevator && !_isMoving && !_isOnTopPosition)
            {
                _rigidbody.velocity = Vector2.up * _elevatorSpeed;
                _playerController.enabled = false;
                _isMoving = true;
                ResetTopAndBottomBooleans();
            }
        }

        private void OnButtonPressMoveElevatorDownwards()
        {
            if (Input.GetKeyDown(KeyCode.S) && _playerIsOnElevator && !_isMoving && !_isOnBottomPosition)
            {
                _rigidbody.velocity = Vector2.down * _elevatorSpeed;
                _playerController.enabled = false;
                _isMoving = true;
                ResetTopAndBottomBooleans();
            }
        }

        private void ResetTopAndBottomBooleans()
        {
            _isOnBottomPosition = false;
            _isOnTopPosition = false;
        }

        private void SetElevatorStopColliderActive()
        {
            float elevatorStopYPosition = _currentElevatorStop.gameObject.transform.position.y;
            float deltaPosition = Mathf.Abs(transform.position.y - elevatorStopYPosition);

            if (deltaPosition > 1)
            {
                _currentElevatorStop.enabled = true;
            }
        }
        #endregion
    }
}
