using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMove : MonoBehaviour
{
    /*
     목표 플레이어를 이동하고 싶다
      필요 속성:속도
    -이동 속도 
    순서:
    //1. 키보드 입력을 받는다
    //2. 키보드 입력에 따라 이동할 방향을 계산한다.
    //3. 이동할 방향과 이동속도에 따라 플레이어를 이동시킨다.
     */

    private float Speed = 3f;// 이동속도 : 초당 3만큼 이동하겠다. // 캡슐화로 인해  pubilic에서 private으로 변경

    public const float MinX = -3f;// 이동속도 : 초당 3만큼 이동하겠다.
    public const float MaxX = 3f;// 이동속도 : 초당 3만큼 이동하겠다.
    public const float MinY = -6f;// 이동속도 : 초당 3만큼 이동하겠다.
    public const float MaxY = 0f;// 이동속도 : 초당 3만큼 이동하겠다.

    public Animator MyAnimator;

    private void Awake()
    {


        MyAnimator = GetComponent<Animator>();//this.gameObject.는 생략가능
    }

    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()//매 프레임마다 출력되는 함수
    {
        Move();
        SpeedDown();
    }


    private void Move()
    {
        //transform.Translate/*트랜스폼을. 이동시킨다*/(Vector2.up * Speed * Time.deltaTime);
        //(0.1)*3 - (0,3)*Time.deltaTime
        //deltaTime은 프레임 간 시간간격을 반환한다.
        //30fps: d -> 0.03초
        //60fps d -> 0.016ch
        //deltaTime은 컴퓨터 사양에 상관없이 같은 속도를 출력하기 때문에 매번 사용해야한다.
        // 1 유닛은 1m 이다
        //1. 키보드 입력을 받는다
        //float h = Input.GetAxis("Horizontal");// -1.0f ~ 0f ~ +1.0f 출력하고 방향키 좌우를 누르면 왼쪽은 -1.0f 오른쪽은 +1.0f출력
        float h = Input.GetAxisRaw("Horizontal");// 수평입력값 - -1.0f ~ 0f ~ +1.0f
        //Input.GetAxis = 사용자가 키보드나 조이스틱 등의 입력 장치에서 입력 값을 주면 해당 입력 값을 그대로 반환합니다. 이는 입력 값을 부드럽게 조정하지 않고, 사용자가 누른 키의 즉각적인 상태를 반영하고자 할 때 유용

        //Debug.Log (h);
        //float v = Input.GetAxis("Vertical");// 수직입력값을 받아온다(인풋매니저 참고)
        float v = Input.GetAxisRaw("Vertical");// 수직입력값 - -1.0f ~ 0f ~ +1.0f
                                               //Debug.Log($"h: {h}, v: {v}");
                                               //2. 키보드 입력에 따라 이동할 방향을 계산한다.
                                               //Vector2 dir = Vector2.right * h + Vector2.up * v;
                                               //(1.0)*h+(0.1)*v = (h, v)
                                               //방향을 각 성분으로 제작

        // 애니메이터에게 파라미터 값을 넘겨준다
        MyAnimator.SetInteger("h", (int)h);// int 형변환


        Vector2 dir = new Vector2(h, v);
       // Debug.Log($"정규화 전 :{dir.magnitude}");

        //이동방향을 정규화(방향은 같지만 길이를 1로 만들어줌)
        dir = dir.normalized;
       // Debug.Log($"정규화 후:{dir.magnitude}");

        //3. 이동할 방향과 이동속도에 따라 플레이어를 이동시킨다.
        //Debug.Log(Time.deltaTime);
        //transform .Translate (dir * Speed * Time.deltaTime);

        //공식을 이용한 이동
        //새로운 위치 = 현재 위치 + 속도 * 시간 
        Vector2 newPosition = transform.position + (Vector3)/*형변환으로 2에서 3로 바꿈*/ (dir * Speed * Time.deltaTime);
        //새로운 위치를 잘 수정해본다



      //  Debug.Log($"x:{newPosition.x}, y:{newPosition.y}");
        //newPosition.x = 3;

        //if (newPosition.x < MinX)
        //{
        //    newPosition.x = MinX;
        //}
        //else if (newPosition.x > MaxX)
        //{
        //    newPosition.x = MaxX;
        //}

        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        //newPosition.y = Mathf.Max(MinY, newPosition.y);
        //newPosition.y = Mathf.Min(newPosition.y, MaxY);

        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);



        //if(newPosition.y < MinY)
        //{
        //    newPosition.y = MinY;
        //}
        //else if(newPosition.y > MaxY)
        //{
        //    newPosition.y = MaxY;
        //}

        transform.position = newPosition;// 플레이어 위치 = 새로운 위치 


        // 현재위치 출력
      //  Debug.Log(transform.position);
        //transform.position = new Vector2(0, 1);

        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        //Vector2 dir = new Vector2(h, v);
        //Debug.Log($"정규화 전 :{dir.magnitude}");

        //dir = dir.normalized;
        //Debug.Log($"정규화 후:{dir.magnitude}");

        //Vector2 newPosition = transform.position + (Vector3)/*형변환으로 2에서 3로 바꿈*/ (dir * Speed * Time.deltaTime);
        //transform.position = newPosition;

        //Debug.Log(transform.position);
    }

    private void SpeedDown()
    {
        //bool q = Input.GetKeyUp("q");
        //bool e = Input.GetKeyDown("e");

        //if (q)
        //{
        //    Speed += 1;
        //    Debug.Log(Speed);
        //}
        //else if (e) 
        //{
        //    Speed -= 1;
        //    Debug.Log(Speed);
        //}

        // 목표: Q/E 버튼을 누르면 속력을 바꾸고 싶다.
        //속성
        // - 속력(Speed)
        // 순서:

        // 1. Q/E버튼 입력을 판단한다.
        if (Input.GetKeyDown(KeyCode.Q))
        //2. Q버튼이 눌렸다면 스피드 1 다운
        {
            Speed++;
        }
        //3. E버튼이 눌렸다면 스피드 1 업
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Speed--;
        }
    }

    public float GetSpeed()// 캡슐화
    {
        return Speed;
    }

    public void SetSpeed(float speed)// 캡슐화
    {
        Speed = speed;
    }

    public void AddSpeed(float speed)// 캡슐화
    {
        Speed += speed;
    }
}
