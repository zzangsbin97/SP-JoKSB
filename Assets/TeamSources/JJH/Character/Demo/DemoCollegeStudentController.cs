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
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);

            // 현재 크기 유지
            Vector3 currentScale = transform.localScale;

            // 수평 이동 (왼쪽, 오른쪽)
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // X축만 반전

                anim.SetBool("isRun", true);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z); // X축만 반전

                anim.SetBool("isRun", true);
            }

            // 수직 이동 (위: W, 아래: S)
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveVelocity += Vector3.up;
                anim.SetBool("isRun", true);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveVelocity += Vector3.down;
                anim.SetBool("isRun", true);
            }

            // 이동 속도 적용
            if (isKickboard)
            {
                transform.position += moveVelocity * KickBoardMovePower * Time.deltaTime;
            }
            else
            {
                transform.position += moveVelocity * movePower * Time.deltaTime;
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