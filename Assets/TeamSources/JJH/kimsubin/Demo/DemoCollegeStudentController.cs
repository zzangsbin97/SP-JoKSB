using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClearSky
{
    public class DemoCollegeStudentController : MonoBehaviour
    {
        public float movePower = 10f;
        public float KickBoardMovePower = 15f;

        private Rigidbody2D rb;
        private Animator anim;
        Vector3 movement;
        private int direction = 1;
        private bool alive = true;
        private bool isKickboard = false;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            Restart();
            if (alive)
            {
                Hurt();
                Die();
                Attack();
                KickBoard();
                Run();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }

        void KickBoard()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4) && isKickboard)
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && !isKickboard)
            {
                isKickboard = true;
                anim.SetBool("isKickBoard", true);
            }
        }

        void Run()
        {
            if (!isKickboard)
            {
                Vector3 moveVelocity = Vector3.zero;
                anim.SetBool("isRun", false);

                // 좌우 이동 처리
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    direction = -1;
                    moveVelocity = Vector3.left;
                    transform.localScale = new Vector3(0.5f * direction, 0.5f, 1);
                    anim.SetBool("isRun", true);
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    direction = 1;
                    moveVelocity = Vector3.right;
                    transform.localScale = new Vector3(0.5f * direction, 0.5f, 1);
                    anim.SetBool("isRun", true);
                }

                // 위쪽 이동 처리
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    moveVelocity = Vector3.up;
                    anim.SetBool("isRun", true);
                }
                // 아래쪽 이동 처리
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    moveVelocity = Vector3.down;
                    anim.SetBool("isRun", true);
                }

                // 캐릭터 위치 업데이트
                transform.position += moveVelocity * movePower * Time.deltaTime;
            }

            if (isKickboard)
            {
                Vector3 moveVelocity = Vector3.zero;

                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    direction = -1;
                    moveVelocity = Vector3.left;
                    transform.localScale = new Vector3(0.5f * direction, 0.5f, 1);
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    direction = 1;
                    moveVelocity = Vector3.right;
                    transform.localScale = new Vector3(0.5f * direction, 0.5f, 1);
                }

                // 아래쪽 이동 처리 (킥보드 모드)
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    moveVelocity = Vector3.down;
                }

                transform.position += moveVelocity * KickBoardMovePower * Time.deltaTime;
            }
        }


        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("attack");
            }
        }

        void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }

        void Die()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("die");
                alive = false;
            }
        }

        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("idle");
                alive = true;
            }
        }
    }
}
