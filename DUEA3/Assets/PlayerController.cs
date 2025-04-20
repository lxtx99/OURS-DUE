using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Coin Collection")]
    public int coinsCollected = 0;
    public TMP_Text coinCounterText;
    public int coinsRequiredToNextLevel = 10;
    public int nextSceneIndex = 1; // ����������������

    [Header("UI References")]
    public GameObject winPanel; // ʤ�����

    [Header("Audio Settings")]
    public AudioSource coinPickupSound; // ʰȡ�����Ч

    private Rigidbody rb;
    private Vector3 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateCoinUI();
        if (winPanel != null)
        {
            winPanel.SetActive(false); // ��ȫ���
        }
    }

    void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (movementInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (movementInput != Vector3.zero)
        {
            Vector3 moveDirection = movementInput.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveDirection);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinsCollected++;
            Destroy(other.gameObject);
            UpdateCoinUI();

            // ����ʰȡ�����Ч
            if (coinPickupSound != null)
            {
                coinPickupSound.Play();
            }

            if (coinsCollected >= coinsRequiredToNextLevel)
            {
                ShowWinPanel();
            }
        }
    }

    void UpdateCoinUI()
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = "Coins: " + coinsCollected + " / " + coinsRequiredToNextLevel;
        }
    }

    void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // ȷ�����ָ��ɼ�
        }
    }

    // ��ť��������󶨵�UI��ť��
    public void OnNextLevelButton()
    {
        Time.timeScale = 1f;
        // ���ֳ������ط�ʽ���棬����ʹ������
        if (nextSceneIndex > 0)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void OnMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}