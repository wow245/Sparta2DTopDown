using System;
using Unity.VisualScripting;
using UnityEngine;



 public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    [SerializeField] private SpriteRenderer characterRanderer;

    private TopDownController controller;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        // 마우스의 위치가 들어오는 OnLookEvent에 등록하는 것
        // 마우스의 위치를 받아서 팔을 돌리는 데 활용할 것임.
        controller.OnLookEvent += OnAim;
    }//OnLookEvent에 OnAim함수 등록

    private void OnAim(Vector2 direction)
    {//OnLookEvent에서 좌표값을 받아온다.
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {//팔의 회전
        
        float rotZ = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;

        //[1. 캐릭터 뒤집기]
        // 이때 각도는 오른쪽(1, 0 방향)이 0도이므로,
        // -90~90도에서는 오른쪽을 바라보는 게 맞지만, -90도 미만 90도 초과라면 왼쪽을 바라보는 것임.
        characterRanderer.flipX = Mathf.Abs(rotZ) > 90f;
        //Abs는 절댓값을 말한다 Abs(10)이면 절대값10을 의미
        //위 코드의 의미는 절대값 90도 크면 회전을 해준다는 의미이다.

        //[2. 팔 돌리기]
        // 팔을 돌릴 때는 나온 각도를 그대로 적용하는데, 이때 유니티 내부에서 사용하는 쿼터니언으로 변환한다.
        // 쿼터니언으로 변현하는 방법 두가지
        // 1) Vector3를 Quaternion으로 변환해서 넣는 방법
        //  Quaternion.Euler(X회전, y회전, z회전) : 오일러 각 기준으로 값을 넣으면 쿼터니언으로 변환됨.
        // 2) eulerAngles를 통해 자동으로 변환되게 하는 방법 - rotation이랑 비슷하게 변환이 되어서 들어간다고 생각
        //  Transform.eulerAngles을 변경
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        //armPivot에 회전을 시켜줄 코드 Quaternion.Euler로 오일러 값을 넣어준다.
        //Z축으로 회전 Mathf.Atan2(direction.y,direction.x)에서 캐릭터에서 마우스를 바라보는 각도를 얻는다.

       
    }
}