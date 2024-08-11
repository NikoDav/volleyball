using UnityEngine;
using UnityEngine.UI;

public class Serve : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _hand;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Transform _startPoint;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fillBar;
    [SerializeField] private Gradient _gradient;
    private Rigidbody2D _currentBall;
    private float _elapsedHold;
    private KeyCode _serveButton;

    private void Update()
    {
        Service();
    }

    public void Service()
    {
        if (_player.CurrentState != State.Serving)
            return;
        
        if (Input.GetKey(_serveButton))
        {
            _elapsedHold += 3f * Time.deltaTime;
            _slider.value = _elapsedHold;
            _fillBar.color = _gradient.Evaluate(_elapsedHold/5);
        }
        if (Input.GetKeyUp(_serveButton) && _spriteRenderer.flipX == false || _elapsedHold >= 6)
        {
            _currentBall.transform.parent = null;
            _currentBall.bodyType = RigidbodyType2D.Dynamic;
            _currentBall.AddForce(new Vector2(-2f * _elapsedHold, 2f * _elapsedHold), ForceMode2D.Impulse);
            _elapsedHold = 0;
            _player.ChangeState(State.Idle);
        }
    }

    public void SetUp()
    {
        _player.ChangeState(State.Serving);
        transform.position = _startPoint.position;
        _currentBall.bodyType = RigidbodyType2D.Kinematic;
        _currentBall.transform.position = _hand.position;
    }

    public void Initialized(Rigidbody2D ball, Transform startPos, KeyCode ServeButton, bool flip)
    {
        _currentBall = ball;
        _startPoint = startPos;
        _serveButton = ServeButton;
        _spriteRenderer.flipX = flip;
    }
    

    public void SetBall(Rigidbody2D ball)
    {
        _currentBall = ball;
    }

    public void SliderCheck()
    {
        if(_player.CurrentState != State.Serving)
        {
            _slider.enabled = false;
        }
    }
}
