using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private Animator animator;
    public GameObject itemUI; // 아이템 정보를 표시하는 UI 오브젝트
    public ParticleSystem openEffect; // 상자 열기 효과 (파티클)
    public AudioClip openSound; // 상자 열 때 재생할 소리
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("박스와 상호작용할 준비가 되었습니다!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("박스와 상호작용 중...");

        // 상자 열기 애니메이션 트리거
        if (animator != null)
        {
            animator.SetTrigger("OpenTrigger");
        }

        // 파티클 효과 재생
        if (openEffect != null)
        {
            openEffect.Play();
        }

        // 상자 열기 소리 재생
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        // 아이템 정보 표시
        ShowItemInfo();
    }

    void ShowItemInfo()
    {
        Debug.Log("아이템을 획득했습니다!");
        // 아이템 UI를 활성화하여 아이템 정보를 표시
        if (itemUI != null)
        {
            itemUI.SetActive(true);
        }
    }
}
