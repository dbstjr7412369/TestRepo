using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType//총알 타입에 대한 열거형(상수를 기억하기 좋게 하나의 이름으로 그룹화하는 것)
{
    Main = 0,  //생략해도 0으로 출력
    Sub,
    Pet 
}

public class Bullet : MonoBehaviour
{
    public int BTye = 0; //0이면 주총알, 1이면 보조총알 2면 펫이 쏘는 총알
    //public BulletType BType = BulletType.Main;
    // 목표: 총알이 위로 계속 이동하고 싶다.
    // 속성: 속력
    // 구현 순서
    //1. 이동할 방향을 구한다
    //2. 이동한다

    public float Speed;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        
        transform.Translate (Vector2.up * Speed * Time.deltaTime);//Translate을 잘 사용하지 않는 편이 좋다
        
        //1. 이동할 방향을 구한다

        //Vector2 dir = Vector2.up;

        //2. 이동한다
        //transform.Translate(Vector2.up * Speed * Time.deltaTime);

        //새로운 위치 = 현재위치 * 속도 * 시간
        //transform.position += 
    }

}
    
        


