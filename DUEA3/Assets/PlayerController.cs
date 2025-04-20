using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // 新增命名空间

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Coin Collection")]
    public int coinsCollected = 0;
    public TMP_Text coinCounterText;
    public int coinsRequiredToNextLevel = 10; // 需要收集的金币数量
    public int nextSceneIndex = 1; // 下一个场景的索引（在Build Settings中查看）

    private Rigidbody rb;
    private Vector3 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateCoinUI();
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

            // 检查是否达到目标金币数
            if (coinsCollected >= coinsRequiredToNextLevel)
            {
                LoadNextScene();
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

    // 新增方法：加载下一个场景
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
        // 如需过渡效果，可替换为：
        // SceneManager.LoadSceneAsync(nextSceneIndex);
    }
}