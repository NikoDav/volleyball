using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperHit : MonoBehaviour
{
    [SerializeField] private Image _superImage;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _hand;
    [SerializeField] private Animator _animator;


    public void SuperHitSetup()
    {
        _player.GameManager.CurrentBall.transform.parent = null;
        _player.GameManager.CurrentBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, -1)) ;
    }

    private IEnumerator Throw()
    {
        yield return new WaitForSeconds(0.1f);
        
    }

    public void SetBall()
    {
        _player.GameManager.CurrentBall.transform.parent = _hand;
       // _player.GameManager.CurrentBall.transform.position = _hand.position;
        _player.GameManager.CurrentBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _player.ChangeState(State.SuperHit);
        _animator.SetTrigger("SuperHit");
     //   SuperHitSetup();
    }
}
