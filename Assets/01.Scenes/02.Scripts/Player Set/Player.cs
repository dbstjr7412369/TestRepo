using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Health = 3f;// 과제 35번 캡슐화로 public에서 private으로 변경
    public AudioSource AudioSource;

    public void AddHealth(int healthAmount)// 캡슐화
    {
        if  (healthAmount <= 0) 
        {
            return;
        }

        Health += healthAmount;
    }

    public void DecreaseHealth(int healthAmount)// 캡슐화
    {
        if (healthAmount <= 0)
        {
            return;
        }

        Health -= healthAmount;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        //GetComponent<컴포넌트 타입>(); -> 게임 오브젝트의 컴포넌트를 가져오는 메소드
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
        //sr.color = Color.white;
        //  Transform tr = GetComponent <Transform>();
        // tr.position = new Vector2(0f, -2.7f);
        //transform.position = new Vector2(0f, -2.7f);

        //PlayerMove playerMove = GetComponent <PlayerMove>();
        //Debug.Log(playerMove.Speed);
        //playerMove.Speed = 5f;
        //Debug.Log(playerMove.Speed);
        // 코드로 유니티에 있는 설정을 조절 가능하다
    }

    public float GetHealth()// 캡슐화
    {
        return Health;
    }

    public void SetHealth(float health)// 캡슐화
    {
        Health = health;
    }

    public void AddHealth()
    {
    
    }
  
}
