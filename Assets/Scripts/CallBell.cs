using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallBell : MonoBehaviour
{
    // Button과 Text 요소를 연결하기 위한 변수
    public Button callButton;
    public Text displayText;
    public Button displayButton;
    public Button yesButton;
    public Button noButton;
    public GameObject buttonPrefab; // 버튼 프리팹
    public Transform buttonContainer; // 버튼들을 배치할 부모 객체
    public float buttonSpacing = 150f; // 버튼 간 간격
    public int maxButtons = 5; // 최대 버튼 개수

    private List<GameObject> buttons = new List<GameObject>(); // 현재 버튼 리스트
    private string[] ingredients = { "캣츠버거", "더블캣츠버거", "치즈버거", "더블치즈버거", "야채버거","샐러드" };
    void Start()
    {
        // Text를 초기에는 보이지 않도록 설정
        displayText.enabled = false;
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        displayButton.gameObject.SetActive(false);

        // 버튼 클릭 이벤트 등록
        callButton.onClick.AddListener(OnCallButtonClicked);
        yesButton.onClick.AddListener(CreateButton);
    }

    void OnCallButtonClicked()
    {
        // 버튼을 누르면 텍스트 표시
        callButton.onClick.AddListener(GenerateRandomOrder);
        displayText.enabled = true;
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        displayButton.gameObject.SetActive(true);

    }
    void GenerateRandomOrder()
    {
        // 랜덤으로 재료 선택
        int randomIndex = Random.Range(0, ingredients.Length);
        string selectedIngredient = ingredients[randomIndex];

        // 선택된 재료를 텍스트로 표시
        displayText.text = selectedIngredient + " 줄 수 있겠는가?";
    }
    void CreateButton()
    {
        // 새 버튼 생성
        GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
        newButton.GetComponent<Button>().onClick.AddListener(CreateButton);

        // 버튼 위치 설정
        int buttonIndex = buttons.Count;
        RectTransform rect = newButton.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(buttonIndex * buttonSpacing, 0);
    }
}
