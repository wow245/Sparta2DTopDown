using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    //최종적으로 정리를 하자면 TopDownContoller는 몬스터랑 캐릭터 공통적인 기능들을 넣어 놓는 곳이고
    //여기서 invoke 할 수 있도록 event를 여기에 달아놨다고 생각하면된다. Action에 여기서 등록을 할 수 있도록
    // 만들어 놨다. 그 Action은 Move라는 거는 방향 있다 Vector2를 받아서 어떤 행동을 처리할 것이고 OnLook도 마찬가지로
    // 마우스 위치를 받아서 처리한다는 거고 callMoveEvent 같은 경우는 그동안 등록되어 있던 OnMoveEvent를 Invoke 해주는 건데
    //CallMoveEvent는 OnMoveEvent에서 진행할 거다 그런데 왜 OnMove라는 함수를 또 만들었냐라고 하면 normalized하고  그다음에
    //ScreenToWorldPoint 만들어 가지고  방향을 만들어주고  이런 전처리 작업을 해주려고 OnMove, OnLook을 걸어준 거다.전처리한 다음에
    //그걸 바탕으로 CallMoveEvent를 실행시켜서 OnMoveEvent를 전부 다 전체에다 뿌려준다. 라고 생각하면 된다.

    public event Action<Vector2> OnMoveEvent;
    // 벡터2를 인자로 받는 함수를 생성한다는 의미
    // Action은 무조건 void만 반환해야 한다 아니면 Func 를 사용해야 한다.
    public event Action<Vector2> OnLookEvent; //마우스
    public event Action OnAttackEvent;// 받는게 없기 때문에 그냥 액션이다. 눌렀다는 사실만 중요하기때문이다.

    protected bool IsAttacking {  get;  set; }

    private float timeSinceLastAttack = float.MaxValue;

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        //TODO :: 매직넘버 수정 매직넘버 : 0.2f라고 쓴거
        if(timeSinceLastAttack < 0.2f)
        {
            timeSinceLastAttack += Time.deltaTime;
        }//시간이 누적되어 0.2초가 되면 그때 부터 쏠 수 있게 된다. 
        else if(IsAttacking && timeSinceLastAttack >= 0.2f)
        {
            timeSinceLastAttack = 0f;
            CallAttackEvent();
        }
    }


    public void CallMoveEvent(Vector2 direction)
    {//CallMoveEvent는 MoveEvent가 발생했을 때 invoke를 한다. 
        OnMoveEvent?.Invoke(direction); // 여기서 ?는 없으면 말고 있으면 실행한다는 의미를 갖는다.
        //어느 방향으로 움직여라고 할것이다.
    }
    
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    // 실제적으로 캐릭터랑 몬스터랑 CallMoveMent하는 방식이 다르다.
    // 그걸 상속받는 클래스에서 잡아줄것이다.

    private void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
}



