using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuidedMissle : EnemyBase
{
    Transform target;
    bool onGuided = true;

    public override void OnInitialize()
    {
        target = GameManager.Inst.Player.transform;
        onGuided = true;
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);

        if (onGuided)
        {
            Vector3 dir = target.position - transform.position;

            // transform의 오른쪽 방향 벡터를 -dir로 설정한다 => transform의 왼쪽 방향 벡터를 dir로 설정한다 
            // => 거기에 맞는 회전을 한다.
            //transform.right = -dir;
            transform.right = -Vector3.Lerp(-transform.right, dir, deltaTime * 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            onGuided = false;
        }        
    }

    // 미사일이 어느 정도 플레이어에게 근접하면 더이상 추적을 하지 않는다.
}
