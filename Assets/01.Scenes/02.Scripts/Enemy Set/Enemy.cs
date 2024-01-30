using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static Unity.VisualScripting.Member;
using static UnityEditor.Progress;
public enum EnemyType// 적 타입 열거형
{
    Basic,
    Target,
    Follow
}
public class Enemy : MonoBehaviour
{


    // 목표 적을 아래로 이동시키고 싶다
    // 속성:
    // - 속력
    public float EnemySpanwer = 1f;
    public float Health_Enemy = 2f;
    public float Speed = 3f;// 이동속도 : 초당 3만큼 이동하겠다.
    [Header("아이템")]
    public GameObject Item_HealthPrefab;
    public GameObject Item_SpeedPrefab;
    private  GameObject PlayerMovePrefab;

    public Animator MyAnimator;//애니메이션 

    // Basic타입 아래로 이동
    // Target 처음태어났을 때 플레이어가 있는 방향으로 이동
    // 속성 
    // EnemyType 타입
    // 구현 순서:
    // 2. 방향을 향해 이동한다

    public EnemyType EnemyType;

    private Vector2 _dir;

    private GameObject _target;// GameObject target = GameObject.Find("Player")는 오브젝트가 많아지면 데이터를 많이 소비되 최적화가 되질 않아 이런식으로 작동하는 것

    public EnemyType ETpe { get; private set; }

    public AudioSource audioManger;

    public GameObject ExplosionVFXPrepre;
    private void Start()
    {
        //캐싱 자주 쓰는 데이터를 더 가까운 장소에 저장해두고 필요할 때 가져다 쓰는 거
        // 시작할 때 플레이어를 찾아서 기억해둔다.


       _target = GameObject.Find("Player");
        MyAnimator = GetComponent<Animator>();
        GameObject i = GameObject.Find("audioManager");
        audioManger = i.GetComponent<AudioSource>();     

            if (EnemyType == EnemyType.Target )
            {
             //1.시작할 때 방향을 구한다(플레이어가 있는 방향)
             //1-1 플레이어를 찾는다
             // GameObject target = GameObject.Find("Player");
             //GameObject.Find= GameObjectWithTag("Player");

             // 1-2 방향을 구한다(target - me)
               _dir = _target.transform.position - this.transform.position;
               _dir.Normalize();

             //1번 각도를 구한다
             //tan@ = y/x -> @ = y/x*atan
             float radian = Mathf.Atan2(_dir.y, _dir.x);
             Debug.Log(radian);//호도법 -> 라디안 값
             float degree =radian *Mathf.Rad2Deg;
             //@ = y / x * atan
             Debug.Log($"{degree}");

             transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90));// 이미지 리소스에 맞게 90도를 더한다.
             //transform.eulerAngles = new Vector3(0, 0, degree + 90);// 위 코드는 이 코드로도 가능하다
            //transform.LookAt(_target.transform); 3d에서 자주사용 2d에서는 사용이 거의 없음
            }
            else
            {
               _dir = Vector2.down;
            }                 
    }
  
    void Update()
    {
        //구현 순서 
        // 1 방향구하기
        // Vector2 dir = new Vector2(0, -1);
        //transform.Translate(Vector2.down * Speed * Time.deltaTime);
        // 2 이동시킨다.
        //transform.Translate(Vector2.down * Speed * Time.deltaTime);

        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
        if (EnemyType == EnemyType.Follow)
        {
           
            //1.시작할 때 방향을 구한다(플레이어가 있는 방향)
            //1-1 플레이어를 찾는다
            // GameObject target = GameObject.Find("Player");
            //GameObject.Find= GameObjectWithTag("Player");

            // 1-2 방향을 구한다(target - me)
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();
        }

        if (EnemyType == EnemyType.Follow)
        {
            
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();


            float radian = Mathf.Atan2(_dir.y, _dir.x);
            Debug.Log(radian);//호도법 -> 라디안 값
            float degree = radian * Mathf.Rad2Deg;

            Debug.Log($"{degree}");

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90));// 이미지 리소스에 맞게 90도를 더한다.


        }
        
    }
    // 목표 충돌하면 적과 플레이어를 삭제하고 싶다
    // 구현 순서 
    // 만약 충돌이 일어나면 
    // 적과 플레이어를 삭제한다
    // 충동이 일어나면 호출되는 이벤트 함수
    // Enter Stay Exit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //MyAnimator.SetInteger("a", (int)a);// int 형변환
        //MyAnimator.SetInteger("b", (int)b);// int 형변환
        //MyAnimator.SetInteger("c", (int)c);// int 형변환

        //충돌을 시작했을 때
        Debug.Log("Enter");
        // 충돌한 게임오브젝트의 태그를 확인
        Debug.Log(collision.collider.tag);//Player or Bullet
                                          //2.충돌한 너와 나를 삭제
                                          // 너죽고(플레이어)
        

        // 나죽자(나 자신)
        //if (collision.collider.tag == "Player")
        if (collision.collider.CompareTag ("Player"))
        {
            Destroy(gameObject);
            //플레이어 스크립트를  가져온다
            Player player = collision.collider.GetComponent<Player>();
            //플레이어 체력을 -= 1
            player.DecreaseHealth(1);

            //플레이어 체력이 적다면
            //if (player.GetHealth() <= 0)
            //{
                // 너죽고
               //Destroy(collision.collider.gameObject);
            //}
                Kill();
            
        }
        //else if (collision.collider.tag == "Bullet")
        else if (collision.collider.CompareTag ("Bullet"))
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            //총알의 체력이 2라 할 때 주총알은 0 보조 총알은 1로 설정하고 적과 충돌해야한다.
            if (bullet.BTye == 0)
            {

                Health_Enemy -= 2;
                Debug.Log(Health_Enemy);
                Destroy(collision.gameObject);
            }
            if (bullet.BTye == 1)
            {
           
                Health_Enemy -= 1;
                Debug.Log(collision.gameObject);
                Destroy(collision.gameObject);
            }
            // 충돌한 너와 나를 삭제한다
            //불릿 스크립트를 가져오고
            if (Health_Enemy <= 0)
            {
               
                Debug.Log(Health_Enemy);
                //Main = 0,  //생략해도 0으로 출력
                //Sub = 1,
                //Pet = 2
                // 나죽자
                Kill();
                //audioManger.Play();

                // 목표 50 확률로 체력 올려주는 아이템 나머지 반은 이동속도 올려주는 아이템
                if (Random.Range(0, 2) == 0)
                {
                    // 아이템 만들고
                    GameObject item = Instantiate(Item_HealthPrefab);

                    // 위치를 나의 위치로 수정
                    item.transform.position = this.transform.position;//this는 자신이라는 뜻이다 즉 Enemy라는 뜻
                }
                else if (Random.Range(0, 2) == 1)
                {

                    GameObject item_speed = Instantiate(Item_SpeedPrefab);
                    item_speed.transform.position = this.transform.position;
                }

            }
            else
            {

                    MyAnimator.Play("Hit");
                
                    //MyAnimator.Play("Hit",-1, 2)

            }
            // 총알 삭제 
            //Destroy(collision.collider.gameObject);
            collision.collider.gameObject.SetActive(false);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collisio)
    {
        //충돌 중일 때 매번
        Debug.Log("Stay");
    }
    private void OnCollisionExit2D(Collision2D collisio)
    {

        // 충돌이 끝났을 때
      
        Debug.Log("Exit");
    }



    public void Kill()
    {

        Destroy(this.gameObject);
        GameObject vfx = Instantiate(ExplosionVFXPrepre);
        vfx.transform.position = this.transform.position;

        // 목표 적을 잡을 때 마다 점수를 올리고, 현재점수를 UI에 표시하고 싶다
        // 구현순서 
        // 만약에 적을 잡으면?
        // 스코어를 증가시킨다
        // 씬에서 SocoeManager 게임 오브젝트를 찾아온다
        //GameObject smgameObject = GameObject.Find("ScoreManager");
        // SocorManager 게임오브젝트에서 SocoeManager 스크렙트 컴포넌트를 얻어온다

        //ScoreManager scoreManager = smgameObject.GetComponent<ScoreManager>();
        // 컴포넌트의Socoe 속성을 증가시킨다


        //캡슐화 놓친 코드 있음 선생님께 물어보기
        // 싱글톤 객체 참조로 변경
        //ScoreManager.Instance.AddScore();
        //int score = scoreManager.GetScore();
        //scoreManager.SetScore(score += 1);
        //Debug.Log(scoreManager.GetScore());


        //int curentScore = ScoreManager.Instance Instance,GetScore();
        //ScoreManager.Instance.SetScore(currentScore +1);

        ScoreManager.Instance. Score +=1;



        //// 목표 스코어를 화면에 표시한다
        //scoreManager.ScoreTesxtUI.text = $"점수: {scoreManager.Score}";

        //// 목표 최고 점수를 갱신하고 UI에 표시한다
        //GameObject.Find("ScoreManager1");
        //ScoreManager scoreManager1 = smgameObject.GetComponent<ScoreManager>();


        ////목표 최고 점수를 갱신하고 UI에 표시

        ////만약 현재점수가 최고 점수보다 높다면
        //if (scoreManager.Score > scoreManager.BastScore)
        //{
        //    //최고점수를 갱신하고
        //    scoreManager.BastScore = scoreManager.Score;

        //    // 목표: 최고점수를 저장
        //    //'PlayerPrefs'클래스를 사용 환경설정이라는 뜻
        //    //-> 값을 키(Key)와 '값'(Value) 형태로 저장하는 클래스입니다
        //    // 저장할 수 있는 데이터 타입: int, float, string
        //    // 타입별로 저장/로그가 가능한 Set/Get 메서드가 있다.

        //    PlayerPrefs.SetInt("BastScore", scoreManager.BastScore);
        //    //단점 데이터가 많을 시 라인으로 저장이 불가능

        //    //UI에 표시한다
        //    scoreManager.BastScoreTesxtUI.text = $"최고 점수: {scoreManager.Score}";
        //}
        // 통째로 ScoreManger로 옮긴 후 수정

    }
}
