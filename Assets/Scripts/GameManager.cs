using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Ball _ball;
    private Ball _currentBall;
    [SerializeField] private List<PlayerController> _rightTeam = new List<PlayerController>();    
    [SerializeField] private List<PlayerController> _leftTeam = new List<PlayerController>();
    [SerializeField] private List<Transform> _rightTeamSpawns = new List<Transform>();
    [SerializeField] private List<Transform> _leftTeamSpawns = new List<Transform>();
    [SerializeField] private Transform _startRight;
    [SerializeField] private Transform _startLeft;
    [SerializeField] private GameObject _net;
    [SerializeField] private TouchPanel _touchPanel;
    private int _teamTouchRight;
    private int _teamTouchLeft;
    private bool _rightSide;

    public bool RightSide => _rightSide;

    public Ball CurrentBall => _currentBall;

    public int TeamTouchRight { get => _teamTouchRight; private set => _teamTouchRight = value; }
    public int TeamTouchLeft { get => _teamTouchLeft; private set => _teamTouchLeft = value; }

    private void Start()
    {
        _currentBall = Instantiate(_ball, transform.position, Quaternion.identity);
        _currentBall.Init(this);

        for (int  i = 0; i < 2; i++)
        {
            var leftPlayer = Instantiate(_player, _leftTeamSpawns[i].position, Quaternion.identity);
            var rightPlayer = Instantiate(_player, _rightTeamSpawns[i].position, Quaternion.identity);
            _leftTeam.Add(leftPlayer);
            _rightTeam.Add(rightPlayer);
        }

        foreach (var item in _rightTeam)
        {
            item.Serve.Initialized(_currentBall.GetComponent<Rigidbody2D>(), _startRight, KeyCode.LeftArrow, false);
            item.ButtonInnit(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow);
            item.Setting.ButtonSet(KeyCode.RightArrow, KeyCode.LeftArrow);

        }

        foreach (var item in _leftTeam)
        {
            item.Serve.Initialized(_currentBall.GetComponent<Rigidbody2D>(), _startLeft, KeyCode.D, true);
            item.ButtonInnit(KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.A);
            item.Setting.ButtonSet(KeyCode.D, KeyCode.A);
        }
            
        if(Random.Range(0, 1) == 0)
        {
            _rightTeam[0].Serve.SetUp();
            //_rightTeam[1].ChangeControl();
        }
        else
        {
            _leftTeam[0].Serve.SetUp();
            //_leftTeam[1].ChangeControl();
        }

        _rightTeam[0].SetCommand(true, this);
        _rightTeam[1].SetCommand(true, this);
        _leftTeam[0].SetCommand(false, this);
        _leftTeam[1].SetCommand(false, this);


        _rightTeam[0]._touched += SwapController;
        _rightTeam[1]._touched += SwapController;
        _leftTeam[0]._touched += SwapController;
        _leftTeam[1]._touched += SwapController;

        _rightTeam[0].ChangeControl(true);
        _rightTeam[1].ChangeControl(false);
        _leftTeam[0].ChangeControl(true);
        _leftTeam[1].ChangeControl(false);
    }

    public int GetTeamTouch(bool isRight)
    {
        if (isRight)
            return _teamTouchRight;
        else
            return _teamTouchLeft;
    }

    public void TeamTouch()
    {
        TeamTouchRight = _rightTeam[0].NumTouches + _rightTeam[1].NumTouches;
        if(TeamTouchRight > 3)
        {
            Time.timeScale = 0;
        }
        if(TeamTouchRight >= 1)
        {
            _rightTeam[0].ChangeState(State.Setting);
            _rightTeam[1].ChangeState(State.Setting);
        }

        TeamTouchLeft = _leftTeam[0].NumTouches + _leftTeam[1].NumTouches;
        if(TeamTouchLeft > 3)
        {
            Time.timeScale = 0;
        }
        if (TeamTouchLeft >= 1)
        {
            _leftTeam[0].ChangeState(State.Setting);
            _leftTeam[1].ChangeState(State.Setting);
        }
        
    }

    public void SetSide(bool isSide)
    {
        _rightSide = isSide;
        if (isSide)
        {
            TeamTouchLeft = 0;
            _touchPanel.ResetLights();
            SetupSide(_rightTeam);
            ResetTouches(_leftTeam);
        }
        else
        {
            TeamTouchRight = 0;
            _touchPanel.ResetLights();
            SetupSide(_leftTeam);
            ResetTouches(_leftTeam);
        }
    }

    private void SetupSide(List<PlayerController> controllers)
    {
        foreach (var item in controllers)
        {
            if (item.CurrentState != State.Serving)
                item.ChangeState(State.Recieving);
        }

        controllers[0].ChangeControl(true);
        controllers[1].ChangeControl(false);
    }

    private void ResetTouches(List<PlayerController> controllers)
    {
        foreach (var player in controllers)
            player.ResetTouches();
    }

    private void SwapController(PlayerController currentControl)
    {
        TeamTouch();
        _touchPanel.AddLight();
        if (currentControl.gameObject == _leftTeam[0].gameObject)
        {
            _leftTeam[0].ChangeControl(false);
            _leftTeam[1].ChangeControl(true);
        }
        else if (currentControl.gameObject == _leftTeam[1].gameObject && _leftTeam[0].NumTouches > 0)
        {
            _leftTeam[0].ChangeControl(true);
            _leftTeam[1].ChangeControl(false);
        }


        if (currentControl.gameObject == _rightTeam[0].gameObject)
        {
            _rightTeam[0].ChangeControl(false);
            _rightTeam[1].ChangeControl(true);
        }
        else if (currentControl.gameObject == _rightTeam[1].gameObject && _rightTeam[0].NumTouches > 0)
        {
            _rightTeam[0].ChangeControl(true);
            _rightTeam[1].ChangeControl(false);
        }
    }
}
