using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_NonPhysics2D : MonoBehaviour
{
    // =====선언

    // Inspector에서 조정하기 위한 속성
    public float speed = 15.0f;
    public Sprite[] run;
    public Sprite[] jump;

    // 내부에서 다루는 변수
    float jumpVy;
    int animIndex;
    bool goalCheck;

    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        jumpVy = 0.0f;
        animIndex = 0;
        goalCheck = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Stage_Gate")
        {
            goalCheck = true;
            return;
        }
        SceneManager.LoadScene("StageA");

    }

    // Update is called once per frame
    void Update()
    {
        if(goalCheck) // 골인되면 처리를 멈춘다.
        {
            return;
        }

        float height = transform.position.y + jumpVy;
        // 달리는 상태(접지) = 높이가 0이다
        if(height <= 0.0f)
        {
            height = 0.0f;
            jumpVy = 0.0f;

            if (Input.GetButtonDown("Fire1"))
            {
                // 점프 처리
                jumpVy = +1.3f;

                GetComponent<SpriteRenderer>().sprite = jump[0];
            }
            else
            {
                // 달리기 처리
                animIndex++;
                if(animIndex >= run.Length)
                {
                    animIndex = 0;
                }

                // 달리기 스프라이트 이미지로 전환
                GetComponent<SpriteRenderer>().sprite = run[animIndex];
            }
        }
        else
        {
            // 점프 후 떨어지는 도중
            jumpVy -= 0.2f;
        }

        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, height, 0.0f);

        GameObject goCam = GameObject.Find("Main Camera");
        goCam.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(10, 10, 300, 60),
            "왼쪽 버튼을 누르면 가속 \n놓으면 점프");

        if(GUI.Button(new Rect(10, 80, 100, 20), "리셋"))
        {
            SceneManager.LoadScene("StageA");
        }
    }
}
