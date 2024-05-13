using System;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller;
    [SerializeField] private Transform projectileSpawnPosition;
    //화살이 어디서 생성되는지 설정을 해줄 것이다.
    private Vector2 aimDirection = Vector2.right;

    public GameObject TestPrefab;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        controller.OnAttackEvent += OnShoot;

        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {//Vector2인 이유는 OnLookEvent라는 함수가 Vector2를 받는 함수 이기 때문이다.
        aimDirection = direction;
    }//마우스를 움직일 때마다 Aim Direction으로 바꿔준다고 생각

    private void OnShoot()
    {
        CreatProjectile();
        //투사체의미 (ex. 표창, 화살 총알)
    }

    private void CreatProjectile()
    {
        // TODO :: 날라가질 않기 때문에 날라가게 만들 것임 
        Instantiate(TestPrefab, projectileSpawnPosition.position, Quaternion.identity);
    }

    public void hello()
    {
        Console.WriteLine("안녕");
    }

    public void hello1()
    {
        Console.WriteLine("안녕1");
    }
}



