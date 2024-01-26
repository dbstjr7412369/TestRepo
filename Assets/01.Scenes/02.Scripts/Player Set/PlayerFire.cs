using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 총알 발사 제작
    // 목표 총알을 만들어서 발사하고 싶다
    // 속성
    // - 총알
    // - 총구위치
    // 구현 순서
    // 1. 발사 버튼을 누르면
    // 2. 프리팹으로부터 총알을 동적으로 만들고
    // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.
    //Prefad 재활용이 가능한 코드

    [Header("총알 프리팹")]
    public GameObject BulletPrefab;// 총알 프리팹
    [Header("서브총알 프리팹")]
    public GameObject BulletPrefab_serve;// 서브 총알 프리팹

    [Header("총구갯수")]
    public GameObject[] Muzzles;      // 배열로 총구
    [Header("서브총구갯수")]
    public GameObject[] Muzzles_serve;      // 배열로 서브총구

    [Header("타이머")]
    public float Timer = 10.0f;
    public const float COOL_TIME = 0.6f;
    public float BoomTimer = 5f;
    public const float BoomCOOL_TIME = 0f;

    [Header("자동모드")]
    public bool AutMode = false;

    //소리
    public AudioSource FireSource;
    //생성할 붐 프리펩
    public GameObject BoomPrepre;


    private void Start()
    {
        Timer = 0;// public float Timer = 10.0f;을 초기화한 값
        AutMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("자동공격모드");
            AutMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("수동공격모드");
            AutMode = false;
        }
        Timer -= Time.deltaTime;// 타이머 제작
        // 1. 타이머가 0보다 작은 상태에서 발사 버튼을 누르면


        // 총알 발사 쿨타임 적용 (0.6초에 한 발 발사 가능)
        // 타이머 계산
        BoomTimer -= Time.deltaTime;// 폭탄 타이머 제작
        if (Input.GetKeyDown(KeyCode.Alpha3) && BoomTimer <= 0f)
        {

            if (BoomTimer <= 0f && Input.GetKeyDown(KeyCode.Alpha3))
            {
                BoomTimer = BoomCOOL_TIME;

                Debug.Log("필살기 폭탄공격");

                GameObject boom = Instantiate(BoomPrepre);
                //boom.transform.position = Vector2.zero;
                //boom.transform.position = new Vector2(0 ,0);
                //boom.transform.position = new Vector2(0 ,1.6f);
            }
        }


        bool ready = (AutMode || Input.GetKeyDown(KeyCode.Space));
        if (Timer <= 0 && ready)
        {
                //GameObject bullet2 = Instantiate(BulletPrefab);
                //bullet2.transform.position = Muzzle2.transform.position;
                //목표 총구만큼 총알을 만들고 만든 총알의 위치를 각 총구의 위치로 바꾼다.
                //1번 키→ 자동 공격 모드 / 2번 키 → 수동 공격 모드
                Timer = COOL_TIME;
                FireSource.Play();
                for (int i = 0; i < Muzzles.Length; i++)
                {
                    // 총알을 만들고 
                    GameObject bullet = Instantiate(BulletPrefab);
                    // 위치를 설정한다
                    bullet.transform.position = Muzzles[i].transform.position;
                }
                //서브총알 
                for (int i = 0; i < Muzzles_serve.Length; i++)
                {
                    // 총알을 만들고 
                    GameObject bullet = Instantiate(BulletPrefab_serve);
                    // 위치를 설정한다
                    bullet.transform.position = Muzzles_serve[i].transform.position;
                }

        }


    }
}
