using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour, Idamageble
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject[] _tailCoins;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private Vector3 _cameraPosition;
    [SerializeField] private float _speed = 6.5f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _distanceBetweenCoins = 0.4f;
    [SerializeField] private int _count;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private bool _resetJumping = false;
    [SerializeField] private bool _stop;
    

    private Transform _headTransform;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _headTransform = GetComponent<Transform>();
        _tailCoins = new GameObject[2];
        _stop = true;
    }
    void Start()
    {
        _direction = Vector3.forward;
        _jumpForce = 250.0f;
        _tailCoins[0] = this.gameObject;
        _count = 1;
        // get camera position
        _cameraPosition = new Vector3(2.8f, 4.1f, -4.5f);
    }

    void Update()
    {
        if(_stop == true)
        {
            return;
        }
        // move sneak head and its tail
        Move();
        // resize array if its lenghth is not enough
        EnlargeTail();
        // move camera near snake
        ControlCamera();
    }
    private void FixedUpdate()
    {
        // jumping with RigidBody
        HeadJump();
    }

    private void ControlCamera()
    {
        _mainCamera.transform.position = transform.position + _cameraPosition;
    }
    private void Move()
    {
        //set different directions depended on clicked buttons
        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector3.back)
        {
            // rotate head in the correct way
            if(_direction == Vector3.left)
            {
                StartCoroutine(LeftRightRotation(1));
            }
            
            if(_direction == Vector3.right)
            {
                StartCoroutine(LeftRightRotation(-1));
            }
            _direction = Vector3.forward;

        }
        if (Input.GetKeyDown(KeyCode.A) && _direction != Vector3.right)
        {
            // correct rotation
            if (_direction == Vector3.forward)
            {
                StartCoroutine(LeftRightRotation(-1));
            }

            if (_direction == Vector3.back)
            {
                StartCoroutine(LeftRightRotation(1));
            }
            _direction = Vector3.left;

            
        }
        if (Input.GetKeyDown(KeyCode.D) && _direction != Vector3.left)
        {
            // correct rotation
            if (_direction == Vector3.forward)
            {
                StartCoroutine(LeftRightRotation(1));
            }

            if (_direction == Vector3.back)
            {
                StartCoroutine(LeftRightRotation(-1));
            }
            _direction = Vector3.right;
           
        }
        if (Input.GetKeyDown(KeyCode.S) && _direction != Vector3.forward)
        {
            // correct rotation
            if (_direction == Vector3.left)
            {
                StartCoroutine(LeftRightRotation(-1));
            }

            if (_direction == Vector3.right)
            {
                StartCoroutine(LeftRightRotation(1));
            }
            _direction = Vector3.back;

        }

        // directly moving head
        _headTransform.position += _direction * _speed * Time.deltaTime;
        //move tail
        MoveTail(_direction);
    }

    private void HeadJump()
    {
        // Set private field _isGrounded
        CheckGround();

        if (_resetJumping == true)
        {
            StartCoroutine("ResetJumping");
        }
        // jump code
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && _resetJumping == false){
            _rigidBody.AddForce(Vector3.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
            _resetJumping = true;
        }

        
    }

    private void CheckGround()
    {
        //check ground with Physics
        float _groundDistance = 0.4f;
        _isGrounded = Physics.CheckSphere(transform.position, _groundDistance, _groundMask); 
        
    }

    private void MoveTail(Vector3 _dir)
    {
        // go through the all coins
        for(int i = 1; i < _count; i++)
        {
            // gegt pre-Coin transorm as a target
            var _preTransform = _tailCoins[i - 1].transform;
            if(_tailCoins[i] != null)
            {
                // moveing every coin towards pre-Coin
                if(_dir == Vector3.forward)
                {
                    _tailCoins[i].transform.position = Vector3.MoveTowards(
                        _tailCoins[i].transform.position, _preTransform.position - new Vector3(0,0, _distanceBetweenCoins), _speed * Time.deltaTime);
                }
                if (_dir == Vector3.back)
                {
                    _tailCoins[i].transform.position = Vector3.MoveTowards(
                        _tailCoins[i].transform.position, _preTransform.position + new Vector3(0, 0, _distanceBetweenCoins), _speed * Time.deltaTime);
                }
                if (_dir == Vector3.left)
                {
                    _tailCoins[i].transform.position = Vector3.MoveTowards(
                        _tailCoins[i].transform.position, _preTransform.position + new Vector3(_distanceBetweenCoins, 0, 0), _speed * Time.deltaTime);
                }
                if (_dir == Vector3.right)
                {
                    _tailCoins[i].transform.position = Vector3.MoveTowards(
                        _tailCoins[i].transform.position, _preTransform.position - new Vector3(_distanceBetweenCoins, 0, 0), _speed * Time.deltaTime);
                }
                
            }

        }
    }

    // recreates array with simple copy algo
    private void EnlargeTail()
    {
        if (_count == _tailCoins.Length)
        {
            //copy array of tail coins
            var _tmpTail = new GameObject[_tailCoins.Length];
            for (int i = 0; i < _tailCoins.Length; i++)
            {
                _tmpTail[i] = _tailCoins[i];
            }
            //enlarge space
            _tailCoins = new GameObject[_tailCoins.Length + 10];
            //copy elements back
            for (int i = 0; i < _tmpTail.Length; i++)
            {
                _tailCoins[i] = _tmpTail[i];
            }
        }
    }
    // set coins position dependind on two directions (forward or back going)
    private void SetTailCoinPosition(Transform _coinTransform)
    {
        if(_direction == Vector3.back)
        {
            //coin comes from forward...
            _coinTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (_count - 1) * _distanceBetweenCoins);
            return;
        }
        // ... or coin comes from back
        _coinTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (_count-1) * _distanceBetweenCoins);
    }

    
    //Add coins to tail array...
    public void AddToTail(GameObject _coin)
    {
        _tailCoins[_count] = _coin;
        _count++;
        // ...and set it on position
        SetTailCoinPosition(_coin.transform);
    }
    // Get damage(destroying coin if it is not a head) and die if coins_count == 0
    public void Damage()
    {
        _count--;

        if (_count == 0)
        {
            Die();
            return;
        }

        Destroy(_tailCoins[_count]);
        
    }

    public void Die()
    {
        //stop movements and start death coroutine
        _stop = true;
        StartCoroutine("Death");
    }

    // finds coin inte array
    public bool HasCoin(GameObject _coin)
    {
        for(int i=1; i<_count; i++)
        {
            if (_coin == _tailCoins[i])
            {
                return true;
            }
        }
        return false;
    }

    //start level with wait coroutine
    public void OnButtonStart()
    {
        StartCoroutine("BeforeStartGame");
        
    }

    public float GetCoinsCount()
    {
        return _count;
    }

    public void SetStopValue(bool _val)
    {
        _stop = _val;
    }
    private IEnumerator BeforeStartGame()
    {
        yield return new WaitForSeconds(0.5f);
        // let the head move
        _stop = false;
    }

    private IEnumerator Death()
    {
        // wait a bit and restart game 
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
        Debug.Log("DEAD");
        _gameManager.OnTryAgain();

    }
    private IEnumerator ResetJumping()
    {
        yield return new WaitForSeconds(2.0f);
        _resetJumping = false;
    }

    private IEnumerator LeftRightRotation(int dir) // -1 or 1 => left or right rotation
    {
        float _angle = 0;
        while (_angle < 90)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(Vector3.forward, dir * 10);
            _angle += 10; // simple counter
        }
    }
}
