using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject subMenu; // 拖入SubMenu对象
    public float buttonSpacing = 80f; // 按钮间距

    void Start()
    {
        // 初始化隐藏子菜单
        if (subMenu != null) subMenu.SetActive(false);
    }

    // 主按钮点击事件
    public void ToggleSubMenu()
    {
        bool shouldShow = !subMenu.activeSelf;
        subMenu.SetActive(shouldShow);

        // 动态排列子按钮（可选）
        if (shouldShow) ArrangeButtons();
    }

    // 子按钮布局（垂直排列）
    void ArrangeButtons()
    {
        for (int i = 0; i < subMenu.transform.childCount; i++)
        {
            RectTransform btn = subMenu.transform.GetChild(i).GetComponent<RectTransform>();
            btn.anchoredPosition = new Vector2(0, -i * buttonSpacing);
        }
    }

    // 场景跳转方法
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadHome() => LoadScene(0); // 主页面
    public void LoadLevel1() => LoadScene(1); // 场景1
    public void LoadLevel2() => LoadScene(2); // 场景2
    public void LoadLevel3() => LoadScene(3); // 场景3
    public void LoadLevel4() => LoadScene(4); // 场景4
}