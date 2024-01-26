using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine. UI;




/*디자인 패턴: 옛날부터 소프트웨어 개발 과정에서 발견된 설계 노하우에
               이름을 붙여 재사용 하기 좋은 형태로 묶어 정리한 것

 장점 
-서로 같은 패턴을 알고 있을 때 의사소통이 잘된다.(내용 설계 원칙 특성 조건)
-모범 사례이므로 가독성/ 유지보수/확장성/ 신뢰성 /상승

 단점
-오용과 남용 (써보고 싶은 마음에 억지로 쓴다)
-초기에 학습곡선이 있다 (처음 적용에 시간이 든다)

-알면 좋은 패턴 5개 / 싱글톤/ 오브젝트 풀 /상태 /옵저버/ 팩토리
 


 */




public class ScoreManager : MonoBehaviour
{
    // 목표 적을 잡을 때 마다 점수를 올리고, 현재점수를 UI에 표시하고 싶다
    // 필요 속성 
    // 현재점수를 표시할 UI
    public Text ScoreTesxtUI;
    // 현재 점수를 기억할 변수
    private int _score = 0;
    // 현재점수를 표시할 UI
    public Text BastScoreTesxtUI;
    // 현재 점수를 기억할 변수
    public int BastScore = 0;

    public int Score
    {
        get
        {

            return _score;
        }
        set
        {
            if (value < 0)
            {
                return;

            }
            _score = value;


            // 목표 스코어를 화면에 표시한다
            ScoreTesxtUI.text = $"점수: {_score}";

            // 목표 최고 점수를 갱신하고 UI에 표시한다
            GameObject.Find("ScoreManager1");
            ScoreManager scoreManager1 = GetComponent<ScoreManager>();


            //목표 최고 점수를 갱신하고 UI에 표시

            //만약 현재점수가 최고 점수보다 높다면
            if (_score > BastScore)
            {
                //최고점수를 갱신하고
                BastScore = _score;

                // 목표: 최고점수를 저장
                //'PlayerPrefs'클래스를 사용 환경설정이라는 뜻
                //-> 값을 키(Key)와 '값'(Value) 형태로 저장하는 클래스입니다
                // 저장할 수 있는 데이터 타입: int, float, string
                // 타입별로 저장/로그가 가능한 Set/Get 메서드가 있다.

                PlayerPrefs.SetInt("BastScore", BastScore);
                //단점 데이터가 많을 시 라인으로 저장이 불가능

                //UI에 표시한다
                BastScoreTesxtUI.text = $"최고 점수: {_score}";
            }
        }
    }

    // ScoreManager가 점수를 관리하는 유일한 매니저 (관리자)이므로 싱글톤을 적용하는게 낫다
    public static ScoreManager Instance { get; set; }// ScoreManager 객체 / get; private set으로도 출력가능

    private void Awake()
    {

        Debug.Log("ScoreManager 객체의 Awake호출");
        // 싱글톤 패턴: 오직 한개의 클래스 인스턴스를 갖도록 보장하고, 
        // 전역적인 접근점을 제공한다(아무데서나 쉽게 이 하나의 객체에 접근 가능)

        // 사용이유
        // 게임 개발에서 매니저/ 관리자 클래스의 싱글톤 패턴을 작용하는 것은 일반적인 관행
        //- 전역접근, 코드 단순화 (해당 관리자를 찾기위한 복잡한 로직이 필요 없다.)
        //- 중복 생성방지 (메모리 및 리소스 관리능력 상승)

        if (Instance == null)
        {

            Debug.Log("새로 생성된 것이다!");
            Instance = this;
        }
        else
        {
            Debug.Log("이미 있다");
            Destroy(gameObject);
        }
    }
    // 오브젝트를 늘릴 시 이미 있어 오브젝트가 삭제된다



    // 목표: 게임을 시작할 때 최고 점수를 불러오고 UI에 푝시하고 싶다
    // 구현순서
    // 게임을 시작할 때
    private void Start()
    {
        // 최고 점수를 불러온다
        BastScore = PlayerPrefs.GetInt("BastScore", 0);
        // UI에 표시한다
        BastScoreTesxtUI.text = $"최고 점수:{BastScore}";

    }









    // 목표 적을 잡을 때 마다 점수를 올리고, 현재점수를 UI에 표시하고 싶다
    // 구현순서 
    // 만약에 적을 잡으면?
    // 스코어를 증가시킨다
    // 스코어를 화면에 표시한다
    // 목표 적을 잡을 때 마다 점수를 올리고, 현재점수를 UI에 표시하고 싶다
    // 구현순서 
    // 만약에 적을 잡으면?
    // 스코어를 증가시킨다
    // 씬에서 SocoeManager 게임 오브젝트를 찾아온다
    //GameObject smgameObject = GameObject.Find("ScoreManager");
    //// SocorManager 게임오브젝트에서 SocoeManager 스크렙트 컴포넌트를 얻어온다
    //ScoreManager scoreManager = smgameObject.GetComponent<ScoreManager>();
    //// 컴포넌트의Socoe 속성을 증가시킨다
    //scoreManager.Score += 1;
    //    Debug.Log(scoreManager.Score);

    //    // 목표 스코어를 화면에 표시한다
    //    scoreManager.ScoreTesxtUI.text = $"점수: {scoreManager.Score}";


    // 목표 score 속성에 대한 캡슐화 (get/set)

    public int GetScore()
    {
        return _score;
    }

    public void AddScore()
    {
        SetScore(_score + 1);
    }

    public void SetScore(int score)
    {
        // 유효성 검사
        if (score < 0)
        {
            return;
        }

        _score = score;

        //// 목표 스코어를 화면에 표시한다
        //ScoreTesxtUI.text = $"점수: {_score}";

        //// 목표 최고 점수를 갱신하고 UI에 표시한다
        //GameObject.Find("ScoreManager1");
        //ScoreManager scoreManager1 = GetComponent<ScoreManager>();


        ////목표 최고 점수를 갱신하고 UI에 표시

        ////만약 현재점수가 최고 점수보다 높다면
        //if (_score > BastScore)
        //{
        //    //최고점수를 갱신하고
        //    BastScore = _score;

        //    // 목표: 최고점수를 저장
        //    //'PlayerPrefs'클래스를 사용 환경설정이라는 뜻
        //    //-> 값을 키(Key)와 '값'(Value) 형태로 저장하는 클래스입니다
        //    // 저장할 수 있는 데이터 타입: int, float, string
        //    // 타입별로 저장/로그가 가능한 Set/Get 메서드가 있다.

        //    PlayerPrefs.SetInt("BastScore", BastScore);
        //    //단점 데이터가 많을 시 라인으로 저장이 불가능

        //    //UI에 표시한다
        //    BastScoreTesxtUI.text = $"최고 점수: {_score}";
        //}

    }
}
