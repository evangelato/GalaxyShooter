using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerUpContainer;
    [SerializeField]
    private GameObject[] _powerUps;
    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine() 
    {
        while(!_stopSpawning) 
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine() 
    {
        // Every 3 -7 seconds, spawn powerup
        while(!_stopSpawning) 
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, _powerUps.Length);
            GameObject newPowerUp = Instantiate(_powerUps[randomPowerUp], posToSpawn, Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    public void StopSpawning() 
    {
        _stopSpawning = true;
    }
}
