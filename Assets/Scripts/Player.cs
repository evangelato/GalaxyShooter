using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    // Public or private reference
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _leftEngineDamage;
    [SerializeField]
    private GameObject _rightEngineDamage;
    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("Player: The Spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("Player: The UI Manager is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Player: Audio Source is NULL");
        } 
        else 
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void Damage()
    {
        if (_shieldVisualizer.activeSelf) {
            _shieldVisualizer.SetActive(false);
            return;
        } else {
            _lives--;
            _uiManager.UpdateLives(_lives);
        }

        if (_lives == 2) 
        {
            _rightEngineDamage.SetActive(true);
        }

        if (_lives == 1)
        {
            _leftEngineDamage.SetActive(true);
        }

        if (_lives < 1)
        {
            _spawnManager.StopSpawning();
            _uiManager.DisplayGameOver();
            Destroy(this.gameObject);
            
        }
    }

    public void ActivateTriplePowerUp()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void ActivateSpeedPowerUp()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedDownRoutine());
    }

    IEnumerator SpeedDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }

    public void ActivateShieldPowerUp()
    {
        if (!_shieldVisualizer.activeSelf) 
        {
            _shieldVisualizer.SetActive(true);
        }
    }

    public void AddScore(int points) 
    {
        _score += points;
        _uiManager.UpdateScore(_score);

    }
}
