using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.ContentSizeFitter;

public class EnemySpawer : MonoBehaviour
{
    //역할: 일정시간마다 적을 생성해서 내 위치에 갖다놓고싶다.

    // 필요 속성:
    // 적 프리팹
    // 일정시간
    // 현재시간 

    // 구현순서
    //1. 시간이 흐르다가
    //2. 만약에 시간이 일정시간이 되면
    //3. 프리팹으로부터 적을 생성한다
    //4. 생성한 적의 위치를 내 위치로 바꾼다

    // 목표 적생성시간을 랜덤하게 하고 싶다
    // 필요속성
    //최소시간
    //최대시간
    public float MinTime = 0.5f;
    public float MaxTime = 1.5f;


    [Header("적 프리팹")]
    public GameObject EnemyPrefab;      // Basic
    public GameObject EnemyPrefabTarget;
    public GameObject EnemyPrefabFollow;

    [Header("적 생성 속도")]
    public float Timer = 10.0f;
    public float COOL_TIME = 1.0f;

    [Header("적 스폰")]
    public GameObject[] EnemySpanwer_1;      

    [Header("자동모드")]
    public bool AutMode = false;

    
    private void Start()
    {
        // 시작할 때 적 생성시간을 랜덤하게 생성한다.
        RandomSpawnTime();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("적 출현");
            AutMode = true;
        }
        Timer -= Time.deltaTime;// 타이머 제작

        // 1. 타이머가 0보다 작은 상태에서 발사 버튼을 누르면
       
       if (Timer <= 0)
       {
           // 타이머 초기화
           Timer = COOL_TIME;
           
           for (int i = 0; i < EnemySpanwer_1.Length; i++)
           {
                // 총알을 만들고 
                //GameObject Enemy = Instantiate(EnemyPrefab);
                // 위치를 설정한다
                GameObject Enemy = null;
                if (Random.Range(0, 10) < 1)
                {
                     Enemy = Instantiate(EnemyPrefabFollow);
                }
                else if (Random.Range(0, 10) < 3)
                {
                    Enemy = Instantiate(EnemyPrefabTarget);
                }
                else
                {
                     Enemy = Instantiate(EnemyPrefab);
                }
                Enemy.transform.position = EnemySpanwer_1[i].transform.position;
               RandomSpawnTime();


           }
       }
        // 10% 확률로 적이 날 따라오는 Follow형 적 생성하기

        // int randomNumber = Random.Range(0, 10);
        // Debug.Log(randomNumber);
        // if(randomNumber < 3)
        // {
        //      Instantiate(EnemyPrefabTarget);
        // }
        // else
        // {
        //    Instantiate(EnemyPrefab);
        // }
    }
    private void RandomSpawnTime()
    {
        Timer = Random.Range(MinTime, MaxTime);
    }
}
