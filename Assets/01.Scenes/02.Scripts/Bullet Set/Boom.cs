using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private const float DEATH_TIME = 3f;
    private float _deathTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");//단일 적 갖고오기
        GameObject[] enemyies = GameObject.FindGameObjectsWithTag("Enemy");//다수 적 배열로 갖고오기
        Debug.Log(enemyies.Length);
        for (int i = 0; i < enemyies.Length; i++)
        {
            Enemy enemy = enemyies[i].GetComponent<Enemy>();
            enemy.Kill();
            //enemy.Item();
            //Destroy(enemyies[i]);
        }
    }
    private void Update()
    {
        _deathTimer += Time.deltaTime;
        if (_deathTimer > DEATH_TIME)
        {
            Destroy (this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Kill();
        }
    }
}
