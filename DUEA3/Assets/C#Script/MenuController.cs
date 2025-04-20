using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject subMenu; // ����SubMenu����
    public float buttonSpacing = 80f; // ��ť���

    void Start()
    {
        // ��ʼ�������Ӳ˵�
        if (subMenu != null) subMenu.SetActive(false);
    }

    // ����ť����¼�
    public void ToggleSubMenu()
    {
        bool shouldShow = !subMenu.activeSelf;
        subMenu.SetActive(shouldShow);

        // ��̬�����Ӱ�ť����ѡ��
        if (shouldShow) ArrangeButtons();
    }

    // �Ӱ�ť���֣���ֱ���У�
    void ArrangeButtons()
    {
        for (int i = 0; i < subMenu.transform.childCount; i++)
        {
            RectTransform btn = subMenu.transform.GetChild(i).GetComponent<RectTransform>();
            btn.anchoredPosition = new Vector2(0, -i * buttonSpacing);
        }
    }

    // ������ת����
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadHome() => LoadScene(0); // ��ҳ��
    public void LoadLevel1() => LoadScene(1); // ����1
    public void LoadLevel2() => LoadScene(2); // ����2
    public void LoadLevel3() => LoadScene(3); // ����3
    public void LoadLevel4() => LoadScene(4); // ����4
}