using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerN : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject[] _enemies;
    private int enemiesCount = 7;

    void Start()
    {
        _enemies = new GameObject[enemiesCount];
    }

    void Update()
    {
        for(int i=0; i<_enemies.Length; i++)
        {
            if(_enemies[i] == null)
            {
                _enemies[i] = Instantiate(enemyPrefab, new Vector3(Random.Range(1f, 5f), 1f, Random.Range(1f, 5f)), Quaternion.Euler(0, Random.Range(0, 360f), 0));
                /*_enemies[i] = Instantiate(enemyPrefab) as GameObject;
                _enemies[i].transform.position = new Vector3(Random.Range(1f, 5f), 1f, Random.Range(1f, 5f));
                float angle = Random.Range(0, 360f);
                _enemies[i].transform.Rotate(0, angle, 0);*/
            }
        }
    }
}
