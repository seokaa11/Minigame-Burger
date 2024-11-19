using System.Collections.Generic;
using UnityEngine;

public class burgerdorder : MonoBehaviour
{
    // 햄버거 재료(재료의 내용이나 개수는 추후 추가 예정)
    private List<string> availableIngredients = new List<string>
    {
        "피클", "치킨", "새우", "베이컨", "치즈", "양파", "양상추", "패티"
    };

    // 랜덤으로 선택될 햄버거 재료들
    private List<string> selectedIngredients = new List<string>();

    // 햄버거 재료가 될 수 있는 최소 및 최대 설정(2~5)
    public int minIngredients = 2;
    public int maxIngredients = 5;

    void Start()
    {
        // 손님이 햄버거를 주문할 때 호출 받음
        GenerateRandomPizza();
    }

    void GenerateRandomPizza()
    {
        // 초기화
        selectedIngredients.Clear();

        // 재료의 랜덤(개수 설정)
        int ingredientCount = Random.Range(minIngredients, maxIngredients + 1);

        // 재료 랜덤 선택
        List<string> tempIngredients = new List<string>(availableIngredients);
        for (int i = 0; i < ingredientCount; i++)
        {
            if (tempIngredients.Count == 0) break;

            int randomIndex = Random.Range(0, tempIngredients.Count);
            selectedIngredients.Add(tempIngredients[randomIndex]);
            tempIngredients.RemoveAt(randomIndex); // 중복 방지를 위해 제거
        }

        // 결과 출력
        Debug.Log("햄버거 재료: " + string.Join(", ", selectedIngredients));
    }

    // 버튼 클릭 또는 특정 이벤트로 호출 가능
    public void OrderPizza()
    {
        GenerateRandomPizza();
        Debug.Log("새로운 햄버거를 주문했습니다!");
    }
}
