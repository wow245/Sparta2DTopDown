using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    //���������� ������ ���ڸ� TopDownContoller�� ���Ͷ� ĳ���� �������� ��ɵ��� �־� ���� ���̰�
    //���⼭ invoke �� �� �ֵ��� event�� ���⿡ �޾Ƴ��ٰ� �����ϸ�ȴ�. Action�� ���⼭ ����� �� �� �ֵ���
    // ����� ����. �� Action�� Move��� �Ŵ� ���� �ִ� Vector2�� �޾Ƽ� � �ൿ�� ó���� ���̰� OnLook�� ����������
    // ���콺 ��ġ�� �޾Ƽ� ó���Ѵٴ� �Ű� callMoveEvent ���� ���� �׵��� ��ϵǾ� �ִ� OnMoveEvent�� Invoke ���ִ� �ǵ�
    //CallMoveEvent�� OnMoveEvent���� ������ �Ŵ� �׷��� �� OnMove��� �Լ��� �� ������Ķ�� �ϸ� normalized�ϰ�  �״�����
    //ScreenToWorldPoint ����� ������  ������ ������ְ�  �̷� ��ó�� �۾��� ���ַ��� OnMove, OnLook�� �ɾ��� �Ŵ�.��ó���� ������
    //�װ� �������� CallMoveEvent�� ������Ѽ� OnMoveEvent�� ���� �� ��ü���� �ѷ��ش�. ��� �����ϸ� �ȴ�.

    public event Action<Vector2> OnMoveEvent;
    // ����2�� ���ڷ� �޴� �Լ��� �����Ѵٴ� �ǹ�
    // Action�� ������ void�� ��ȯ�ؾ� �Ѵ� �ƴϸ� Func �� ����ؾ� �Ѵ�.
    public event Action<Vector2> OnLookEvent; //���콺
    public event Action OnAttackEvent;// �޴°� ���� ������ �׳� �׼��̴�. �����ٴ� ��Ǹ� �߿��ϱ⶧���̴�.

    protected bool IsAttacking {  get;  set; }

    private float timeSinceLastAttack = float.MaxValue;

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        //TODO :: �����ѹ� ���� �����ѹ� : 0.2f��� ����
        if(timeSinceLastAttack < 0.2f)
        {
            timeSinceLastAttack += Time.deltaTime;
        }//�ð��� �����Ǿ� 0.2�ʰ� �Ǹ� �׶� ���� �� �� �ְ� �ȴ�. 
        else if(IsAttacking && timeSinceLastAttack >= 0.2f)
        {
            timeSinceLastAttack = 0f;
            CallAttackEvent();
        }
    }


    public void CallMoveEvent(Vector2 direction)
    {//CallMoveEvent�� MoveEvent�� �߻����� �� invoke�� �Ѵ�. 
        OnMoveEvent?.Invoke(direction); // ���⼭ ?�� ������ ���� ������ �����Ѵٴ� �ǹ̸� ���´�.
        //��� �������� ��������� �Ұ��̴�.
    }
    
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    // ���������� ĳ���Ͷ� ���Ͷ� CallMoveMent�ϴ� ����� �ٸ���.
    // �װ� ��ӹ޴� Ŭ�������� ����ٰ��̴�.

    private void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
}



