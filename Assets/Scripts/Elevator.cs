using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameName.PlayerHandling;
using GameName.Audio;
using EnumCollection;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;

namespace GameName.Tree.Traversation
{
    public class Elevator : MonoBehaviour
    {
        #region Fields

        private Player _player;
        private PlayerController _playerController;
        private Rigidbody2D _rigidbody;
        private float _yAlignPosition;
        private bool _isMoving;
        private bool _playerIsOnElevator;
        private bool _isOnTopPosition;
        private bool _isOnBottomPosition;
        private bool _elevatorIsAligned;
        private bool _elevatorIsInBreakPoint;
        private Collider2D _currentElevatorStop;
        private SpriteRenderer _elevatorRenderer;
        private AudioManager _audioManager;
        [SerializeField] private float _elevatorSpeed = 2f;

        #endregion

        #region Functions

        private void Start()
        {
            _player = Player.Instance;
            _playerController = _player.GetComponent<PlayerController>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _elevatorRenderer = GetComponent<SpriteRenderer>();
            _audioManager = AudioManager.Instance;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name.Contains("Player") )
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
                _elevatorIsInBreakPoint = true;
                _yAlignPosition = SetAlignPosition(collision);
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

        private float SetAlignPosition(Collider2D collision)
        {
            SpriteRenderer elevatorBreakPointRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            float yBorderOfStop = elevatorBreakPointRenderer.bounds.max.y;
            return yBorderOfStop;
        }

        private void FixedUpdate()
        {
            if (_elevatorIsInBreakPoint && !_elevatorIsAligned)
            {
                float currentElevatorYPosition = _elevatorRenderer.bounds.max.y;
                float deltaPositions = currentElevatorYPosition - _yAlignPosition;
                float absoluteDelta = Mathf.Abs(deltaPositions);

                transform.position = new Vector3(transform.position.x, _yAlignPosition);
                StopElevator();
                _elevatorIsAligned = true;

                if (absoluteDelta < 0.1f)
                {
                    
                }
                else if (_rigidbody.velocity == Vector2.zero)
                {
                    if (deltaPositions > 0)
                    {
                        _rigidbody.AddForce(Vector2.down * _elevatorSpeed);
                    }
                    else if (deltaPositions < 0)
                    {
                        _rigidbody.AddForce(Vector2.up * _elevatorSpeed);
                    }
                }
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
            StartCoroutine(OnButtonPressMoveElevatorDownwards());
            StartCoroutine(OnButtonPressMoveElevatorUpwards());
            SetElevatorStopColliderActive();
        }

        private IEnumerator OnButtonPressMoveElevatorUpwards()
        {
            if (Input.GetKeyDown(KeyCode.W) && _playerIsOnElevator && !_isMoving && !_isOnTopPosition &&
                Physics2D.Raycast(transform.position, new Vector3(0, 1, 0), 100, 1))
            {
                Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.yellow);
                _rigidbody.velocity = Vector2.up * _elevatorSpeed;
                _playerController.enabled = false;
                _isMoving = true;
                PlayElevatorSound();
                yield return new WaitForSeconds(0.05f);
                ResetTopAndBottomBooleans();
                ResetAlignmentBoolean();
            }
        }

        private IEnumerator OnButtonPressMoveElevatorDownwards()
        {
            if (Input.GetKeyDown(KeyCode.S) && _playerIsOnElevator && !_isMoving && !_isOnBottomPosition &&
                Physics2D.Raycast(transform.position, new Vector3(0, 1, 0), 100, 1))
            {
                _rigidbody.velocity = Vector2.down * _elevatorSpeed;
                _playerController.enabled = false;
                _isMoving = true;
                PlayElevatorSound();
                yield return new WaitForSeconds(0.1f);
                ResetTopAndBottomBooleans();
                ResetAlignmentBoolean();
            }
        }

        private void PlayElevatorSound()
        {
            _audioManager.PlaySoundEffect(SFX.SFX_025_Elevator_Move);
        }

        private void ResetTopAndBottomBooleans()
        {
            _isOnBottomPosition = false;
            _isOnTopPosition = false;
        }

        private void ResetAlignmentBoolean()
        {
            _elevatorIsAligned = false;
            _elevatorIsInBreakPoint = false;
        }

        private void SetElevatorStopColliderActive()
        {
            if (_currentElevatorStop != null)
            {
                float elevatorStopYPosition = _currentElevatorStop.gameObject.transform.position.y;
                float deltaPosition = Mathf.Abs(transform.position.y - elevatorStopYPosition);

                if (deltaPosition > 1)
                {
                    _currentElevatorStop.enabled = true;
                }
            }
        }
        #endregion
    }
}
