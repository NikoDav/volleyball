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



    public void SuperHitSetup()
    {
        //_player.transform.position = new Vector2(transform.position.x, 0.6f);
        //_gameManager.CurrentBall.transform.position = new Vector2(_player.transform.position.x - 0.5f, _player.transform.position.y);
        _rigidbody2D.AddForce(new Vector2(0, 100), ForceMode2D.Impulse);
        Debug.Log(1234568);
      //  StartCoroutine(Throw());
    }

    private IEnumerator Throw()
    {
        yield return new WaitForSeconds(0.1f);
        _player.GameManager.CurrentBall.transform.parent = null;
        _player.GameManager.CurrentBall.GetComponent<Rigidbody2D>().AddForce(new Vector2()) ;
    }

    public void SetBall()
    {
        _player.GameManager.CurrentBall.transform.parent = _hand;
        _player.GameManager.CurrentBall.transform.position = _hand.position;
        _player.GameManager.CurrentBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _player.ChangeState(State.SuperHit);
        SuperHitSetup();
    }
}
