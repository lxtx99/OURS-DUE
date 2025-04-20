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
    public int nextSceneIndex = 1; // 保留场景索引控制

    [Header("UI References")]
    public GameObject winPanel; // 胜利面板

    [Header("Audio Settings")]
    public AudioSource coinPickupSound; // 拾取金币音效

    private Rigidbody rb;
    private Vector3 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateCoinUI();
        if (winPanel != null)
        {
            winPanel.SetActive(false); // 安全检查
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

            // 播放拾取金币音效
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
            Cursor.visible = true; // 确保鼠标指针可见
        }
    }

    // 按钮方法（需绑定到UI按钮）
    public void OnNextLevelButton()
    {
        Time.timeScale = 1f;
        // 两种场景加载方式共存，优先使用索引
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