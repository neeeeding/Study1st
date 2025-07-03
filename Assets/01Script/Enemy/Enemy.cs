using UnityEngine;

namespace _01Script.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private Vector3 movePos; //이동할 방향
        private int doAction; // 할 행동
        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Invoke("SetMovePos",5);
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = movePos;
        }
        
        private void SetMovePos() //방향 정하기
        {
            movePos = Vector3.zero;
            doAction = Random.Range(0, 5);
            if (doAction == 0)
            {
                SleepEnemy();
            }
            
            MoveEnemy();
            Invoke("SetMovePos",5);
        }
        
        private void SleepEnemy() //잠자기
        {
            print("I'm sleeping");
        }
        
        private void MoveEnemy() //움직이기
        {
            movePos = doAction switch
            {
                1 => Vector3.up,
                2 => Vector3.down,
                3 => Vector3.right,
                4 => Vector3.left,
                _ => Vector3.zero
            };
        }
    }
}
