
//이번에는 PlqyerInputController로 진행을 할 것이고 TopDownController를 상속받는 클래스이다.
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownController
{
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;// MainCamera태그가 붙어있는 카메라를 가져온다.
    }

    public void OnMove(InputValue value)
    {//이때 WASD로 들어온 값들이 value에 들어오게 된다.
        Vector2 moveInput = value.Get<Vector2>().normalized;
        // normalized로 인해 벡터의 크기를 1로 만들어서 속도를 일정하게 해줄것이다.
        //이 코드를 작성해 줘야하만 WASD로 들어온 값을 value에 가져오게 된다.
        CallMoveEvent(moveInput);// 상속을 받았기 때문에 부모클래스에 있는 메서드를 자유롭게 사용가능
        // 실제 움직이는 처리는 여기서 하지않고 PlayerMovement에서 함
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        //이것 마우스 위치이기때문에 nomalize를 하면 안된다.
        Vector2 worldPos = camera.ScreenToWorldPoint(newAim);
        //worldPos는 마우스 위치가 화면 좌표계에 있는데 그 화면 좌표계로 바꿔주는 것을 해준다.
        //camera.ScreenToWorldPoint(worldPos); 의 의미는 마우스의 위치가 있을 거라고 하면 화면에서
        //카메라가 잡고 있는 화면의 범위가 어디인지를 알 필요가 있다. 이게 월드 좌표로 얼마인지 카메라의 범위를
        //알아야 한다. 이게 월드 좌표로 얼마인지를 변환을 해줘야 한다. 카메라를 mainCamera를 갖고 오는게 필요했던 것이다.
        //카메라가 없으면 여기를 찍고 있을지 저기를 찍고 있을 지 알 수 없기에 여기를 찍고 있어라고 알려줘야 하기에 카메라가
        //필요했던 것이다. ScreenToWorldPoint는 카메라를 기준으로 Screen 마우스 좌표계가 존재하는 Screen 좌표계에서 월드좌표계로바꿔라
        //라는 의미로 넣어준 것이다. 결론적으로 마우스 위치를 월드 기준으로 바꿔준다. 마우스와 캐릭터의 차이가 얼마나 되냐는 의미이다.
        
        newAim = (worldPos - (Vector2)transform.position).normalized;
        //여기서 빼기는 벡터의 빼기를 생각해주면 된다. 즉, worldPos에서 transform.positon으로 간다라고 생각하면 되는데
        //이 의미는 Transfomr.position에서 World가 어떤 방향에 있니 어느위체이 있니 라는 것을 표현한 거다. 그리고 normalized를 통해 
        //벡터의 사이즈를 1로 만들어라 라고 해서 그걸 newAim에다가 넣어줄거다 
        CallLookEvent(newAim);  
        
    }
    //Player는 Input이있기 때문에 이런식으로 Input에 관련되서 처치를 하는 거고
    //Moster같은 경우에는 Input 없기 때문에 OnMove쪽이 달라질 거고 CallMoveEvent하는 부분이 달라질것이다.
    //이렇게 달라진다는 것을 이해하면 된다. 결국에 공통점은 OnMoveEvent를 호출하는 것은 같지만, 어떻게 호출할 것인지 다르다.

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}
