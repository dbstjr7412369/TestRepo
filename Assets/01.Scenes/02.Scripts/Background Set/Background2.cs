using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background2 : MonoBehaviour
{
    // 목표 머터리얼의 오프셋을 이용해서 배경스크롤이 되도록하고 싶다
    // 필요 속성
    // 머터리얼
    // 스크롤 속도
    public Material MyMaterial;
    public float ScrollSpeed = 0.2f;
    
    // 구현 순서
    // 0 매 프래임마다
    // 1 방향을 구한다
    // 2 (오프셋을 변경해서) 스크롤한다
    void Update()// 0 매 프래임마다
    {
        //1 방향을 구한다
        Vector2 dir = Vector2.up;

        //2 (오프셋을 변경해서) 스크롤한다
        MyMaterial.mainTextureOffset += dir * ScrollSpeed * Time.deltaTime;
    }
    //위 코드를 자주 사용한다
    //코드만 작성한 다음 유니티의 기능으로 이미지를 반복
}
