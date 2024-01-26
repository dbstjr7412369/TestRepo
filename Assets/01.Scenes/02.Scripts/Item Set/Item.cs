using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.ContentSizeFitter;

public class Item : MonoBehaviour
{
    private float _timer = 0f;//시간
    public const float STOP_TIME = 1.0f;

    public int MyType = 0; // 0: 체력을 올려준다 1: 스피드를 올려준다
    private GameObject _target;
    public float Speed = 3f;
    private Vector2 _dir;
    public GameObject ExplosionVFXItemHeaithPrepre;
    public GameObject ExplosionVFXItemSpeedPrepre;
    Animator MyAnimator;
    // 다른 콜라이더에 의해 트리거가 발동될 때


    public void Start()
    {



        MyAnimator = GetComponent<Animator>();
        MyAnimator.SetInteger("ItemType", MyType);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collison Enter!");
        //MyAnimator.SetInteger("Itemtype", Itemtype);// int 형변환
        //MyAnimator.SetInteger("i2", (int)i2);// int 형변환
        //MyAnimator.SetInteger("i3", (int)i3);// int 형변환
    }

    //다른 콜라이더에 의해 트리거가 발동할 때
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("트리거 시작");


        // 목적 플레이어의 체력을 올리고 싶다
        // 순서 
        // 
        //GameObject playerGameObjeact = GameObject.Find("Player");
        //Player player = otherCollider.GetComponent<Player>();


        //Player player = otherCollider.gameObject.GetComponent<Player>();//위의 두줄 코드들도 같은 역할이다

        //player.Health++;
        //Debug.Log($"현재 플레이어의 체력 : {player.Health}");



        //if (otherCollider.tag == "Player")
        //{
        //   Player player = otherCollider.GetComponent<Player>();
        //   player.Health += 1.0f;
        //}
        //if (otherCollider.tag == "Player")// 내가 적은 코드
        //{


        //Destroy (this .gameObject);
        //}
    }


    // 다른 콜라이더에 의해 트리거가 발동 중일 때
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        _timer += Time.deltaTime;
        if (_timer >= STOP_TIME)
        {
            if (MyType == 0)
            {
                Player player = otherCollider.gameObject.GetComponent<Player>();
                player.AddHealth(0);
                //ItemSource.Play();
                GameObject vfx = Instantiate(ExplosionVFXItemHeaithPrepre);
                vfx.transform.position = this.transform.position;
            }
            else if (MyType == 1)
            {
                Debug.Log("스피드");
                PlayerMove playermove = otherCollider.gameObject.GetComponent<PlayerMove>();
                //playermove.SetSpeed(+1);
                //ItemSource.Play();
                playermove.AddSpeed(1);// 캡슐화 이용
                GameObject vfx = Instantiate(ExplosionVFXItemSpeedPrepre);
                vfx.transform.position = this.transform.position;
            }
            Destroy(this.gameObject);
        }

        //Debug.Log("트리거 중");
    }
    private void Update()
    {
         transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
        _target = GameObject.Find("Player");
        _timer += Time.deltaTime;
        

        if (_timer >= 3)
        {
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();
        }
    }

    // 다른 콜라이더에 의해 트리거가 끝났을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        _timer = 0f;
        Debug.Log("트리거 종료");
    }
}
