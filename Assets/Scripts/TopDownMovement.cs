using System;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
//최종적으로 정리를 하자면 

{//TopDownMovement는 실제로 이동일어날 컴포넌트를 말한다.
    private TopDownController controller;
    private Rigidbody2D movementRigidbody;
    private Vector2 movementDirection = Vector2.zero;
    //이동안하고 있으면 0
    private void Awake()
    {
        //Awake는 주로 내 컴포넌트 안에서 끝나는 거

        // controller랑 TopDownMovement랑 같은 게임오브젝트 안에 있다라는 가정
        controller = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {//Move라는 함수는 OnMoveEvent에 등록이 되어 있으니까 입력이 있을 때 실행이 되는 함수이다.
        controller.OnMoveEvent += Move;
        //OnMoveEvent에 Move라는 함수를 등록
    }

    private void Move(Vector2 direction)
    {//Move는 업데이트 기반으로 코드가 실행 그렇기 때문에 프레임기반
        movementDirection = direction;
        //movementDirection에 걸어둬서 움직일 때 여기로 움직여라라고 함.
    }

    private void FixedUpdate()
    {
        //FixedUpdate는 물리엡데이트 관련
        //rigidbody의 값을 바꾸니까 FixedUpdate
        //FixedUpdate에선 실제움직임을 처리한다. 그것을 처리하는 함수가
        //Applymovement함수이다.
        ApplyMovement(movementDirection);
    }// 그 떄 함수를 등록해 놓고 그거에 대해 FixedUpdate에 적용을 하는 것이다.

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 5;
        movementRigidbody.velocity = direction;
    }
    //Move는 매 프레임마다 누르고 안 누리고 있따는 것을 판단하기 때문에 업데이트 베이스로 돌고 그렇기 때문에 Move 프레임기반이다.
    ///FixedUpdate로 실제로 움직임을 처리 그거에 대한 함수가 ApplyMovement이다. movementDirection은 아까 Move라는 함수에서 Move라는
    ///함수는 OnMoveEvent에 등록이 되어있으니까 입력이 있을 때 실행이 되는 함수고 그 때 함수를 등록을 해두고 그거에 대해서 이제 FixedUpdate에 대해서
    ///적용을 하는거다. 
}


