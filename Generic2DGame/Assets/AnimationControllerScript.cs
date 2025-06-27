using Unity.VisualScripting;
using UnityEngine;

public class AnimationControllerScript : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] PlayerControllerScript _player;
    private bool _hasJumped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GetComponent<CharacterController>().isGrounded)
        {
            _animator.SetBool("IsJumping", false);
        }
        if (!_player.GetComponent<CharacterController>().isGrounded)
        {
            _animator.SetBool("IsJumping", true);
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsIdle", false);
        }
        if (_player._moveInput == 0 && _player.GetComponent<CharacterController>().isGrounded && _hasJumped == false)
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsIdle", true);
        }
        if (_player._moveInput < 0 && _player.GetComponent<CharacterController>().isGrounded && _hasJumped == false)
        {
            _animator.SetBool("IsWalking", true);
            _animator.SetBool("IsIdle", false);
            _player.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (_player._moveInput > 0 && _player.GetComponent<CharacterController>().isGrounded && _hasJumped == false)
        {
            _animator.SetBool("IsWalking", true);
            _animator.SetBool("IsIdle", false);
            _player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if(_player._moveInput > 0)
        {
            _player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if(_player._moveInput < 0)
        {
            _player.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

    }

}