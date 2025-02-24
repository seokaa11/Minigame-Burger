using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Droppable : MonoBehaviour
{

    public GameObject burgerPrefab; // Burger �������� �Ҵ��� ����
    private GameObject instantiatedBurger; // ������ Burger ������Ʈ�� ����

    public int stackCount = 0; // ���� ������Ʈ ��
    public List<GameObject> originalIngredients; // ���� ��� ������ ����Ʈ

    // �� ����� �̸�, �ִ� ����, ���� ������ ������ ����Ʈ
    public List<string> ingredientNames;
    public List<int> ingredientMaxAmounts;
    public List<int> ingredientCurrentAmounts;

    // ��ӵ� ������Ʈ���� ������ ����Ʈ
    public List<GameObject> droppedItems = new List<GameObject>();

    // ��Ằ ��ġ�� ������ ����Ʈ
    public List<Vector3> ingredientPositions;

    private void Start()
    {
        // ���� ������ �ʱ�ȭ
        ingredientNames = new List<string> { "Bun", "Patty", "Lettuce", "Tomato", "Cheese", "Pickle", "Onion", "Ketchup2", "Mayo2", "Bulgogi2" };
        ingredientMaxAmounts = new List<int> { 10, 10, 10, 10, 10, 10, 10, 9999, 9999, 9999 };
        ingredientCurrentAmounts = new List<int>(ingredientMaxAmounts);

        // ��Ằ ��ġ �ʱ�ȭ
        ingredientPositions = new List<Vector3>
        {
            new Vector3(-7.54f, -0.76f, 0f), // Bun
            new Vector3(-5.04f, -0.91f, 0f), // Patty
            new Vector3(-2.49f, -0.89f, 0f), // Lettuce
            new Vector3(0.01f, -0.89f, 0f), // Tomato
            new Vector3(2.53f, -0.84f, 0f), // Cheese
            new Vector3(5.02f, -0.88f, 0f), // Pickle
            new Vector3(7.59f, -0.9f, 0f), // Onion
            new Vector3(5.9f, 1.7f, 0f), // Ketchup2
            new Vector3(7.01f, 1.7f, 0f), // Mayo2
            new Vector3(8.09f, 1.7f, 0f)  // Bulgogi2
        };


    }
 

    private void OnMouseDown()
    {
        // Hamburger�� �ڽ� ������Ʈ�� ó��
        if (transform.parent != null && transform.parent.name == "Hamburger")
        {
            Transform parent = transform.parent;

            // Hamburger ����ȭ
            SetTransparencyRecursively(parent, 0f);

            DisableCollidersRecursively(gameObject);


            // Burger ������ ����
            if (burgerPrefab != null)
            {
                if (instantiatedBurger == null) // �ߺ� ���� ����
                {
                    instantiatedBurger = Instantiate(
                        burgerPrefab,
                        parent.position,
                        Quaternion.identity
                    );
                    instantiatedBurger.name = "Burger"; // ������ �̸� ��Ȯ�� ����
                    //Debug.Log("Burger ������ ������: " + instantiatedBurger.name);

                    instantiatedBurger.transform.position = new Vector3(0, -3.2f, 0.2f);

                    // ������ ������ �������� 0.2f, 0.2f, 0.2f�� ����
                    instantiatedBurger.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                }
            }

            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_PACKAGING);

        }
    }

    private void SetTransparencyRecursively(Transform parent, float alpha)
    {
        SetObjectTransparency(parent.gameObject, alpha);

        foreach (Transform child in parent)
        {
            SetTransparencyRecursively(child, alpha);
        }
    }

    private void SetObjectTransparency(GameObject obj, float alpha)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;

            if (material.HasProperty("_Color"))
            {
                Color color = material.color;
                color.a = alpha;
                material.color = color;

                SetMaterialRenderingMode(material, alpha < 1.0f ? "Transparent" : "Opaque");
            }
        }
        else
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = alpha;
            }
        }
    }

    private void SetMaterialRenderingMode(Material material, string mode)
    {
        if (mode == "Transparent")
        {
            material.SetFloat("_Mode", 3);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
        else if (mode == "Opaque")
        {
            material.SetFloat("_Mode", 0);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }
    }

    private void CheckAndDisableDraggableItems()
    {
        // DropArea ���� �ִ� ��� "Bun" ���� Ȯ��
        int bunCount = droppedItems.Count(item => item.name == "Bun");

        if (bunCount >= 3)
        {
            // ���ο� �θ� ������Ʈ ����
            GameObject hamburger = new GameObject("Hamburger");
            hamburger.transform.position = transform.position;

            // ��� ������ ���ο� �θ��� �ڽ����� ����
            transform.SetParent(hamburger.transform);

            // ��� ����� �巡�� ��� ��Ȱ��ȭ
            DisableAllDraggableItems();
            Debug.Log("Bun ������ 2�� �̻��Դϴ�. ��� Draggable�� ��Ȱ��ȭ�մϴ�.");
        }
    }

    public void OnDrop(Draggable draggable)
    {
        Collider2D other = draggable.GetComponent<Collider2D>();
        if (other != null && other.CompareTag("Draggable"))
        {
            //Debug.Log("Draggable entered drop area");


            // Bun���� Ȯ���ϰ� ó�� ���� ��� UnderBun���� ��ȯ
            if (draggable.name.Equals("Bun") && stackCount == 0)
            {
                SpriteRenderer spriteRenderer = draggable.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && draggable.replacementSprite != null)
                {
                    spriteRenderer.sprite = draggable.replacementSprite;
                }
            }
            // Bun�� stackCount�� 0�� �ƴϰ� Bun�� Ŭ���� ��� ���� �̹����� ����
            else if (draggable.name.Equals("Bun") && stackCount != 0)
            {
                SpriteRenderer spriteRenderer = draggable.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && draggable.originalSprite != null)
                {
                    spriteRenderer.sprite = draggable.originalSprite;
                }
            }

            if (new[] { "Ketchup2", "Mayo2", "Bulgogi2" }.Contains(draggable.name))
            {
                SpriteRenderer spriteRenderer = draggable.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && draggable.SauceSprite != null)
                {
                    spriteRenderer.sprite = draggable.SauceSprite;
                }
            }

            // ������Ʈ�� ��� ������ �ڽ����� ����
            other.transform.SetParent(transform);

            // ���̴� ��ġ�� ����
            Vector3 newPosition = transform.position;
            newPosition.y += stackCount * 0.15f; // ���̴� ���� ����
            other.transform.position = newPosition;

            // ���� ������Ʈ �� ����
            stackCount++;

            // ��ӵ� ������Ʈ�� ����Ʈ�� �߰�
            droppedItems.Add(other.gameObject);

            // ���� ��� ���� ����
            string ingredientName = draggable.name.Replace("(Clone)", "").Trim();
            int index = ingredientNames.IndexOf(ingredientName);
            if (index != -1)
            {
                ingredientCurrentAmounts[index]--;
            }

            // �巡�� ��� ��Ȱ��ȭ
            draggable.isDraggable = false;

            // ������Ʈ�� sortingOrder ����
            SpriteRenderer spriteRenderer2 = other.GetComponent<SpriteRenderer>();
            if (spriteRenderer2 != null)
            {
                spriteRenderer2.sortingOrder = stackCount;
            }

            // Instantiate �޼��� ȣ��
            GameObject clone = Instantiate(draggable.gameObject);

            // Ŭ�� ������Ʈ �̸����� (Clone) ����
            clone.name = draggable.name.Replace("(Clone)", "").Trim();

            // Ŭ�� ������Ʈ�� Draggable ������Ʈ �߰� �� �ʱ�ȭ
            Draggable cloneDraggable = clone.GetComponent<Draggable>();
            if (cloneDraggable != null)
            {
                cloneDraggable.isDraggable = true;
                // �߰����� �ʱ�ȭ �۾��� �ʿ��ϸ� ���⿡ �߰�
            }

            // Ŭ�� ��ġ �� ������ ����
            if (index != -1 && index < ingredientPositions.Count)
            {
                clone.transform.position = ingredientPositions[index];
            }
            clone.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            // ����� ������ 0�̸� �巡�� �Ұ����ϰ� ����
            if (index != -1 && ingredientCurrentAmounts[index] <= 0)
            {
                cloneDraggable.isDraggable = false;
                cloneDraggable.GetComponent<Collider2D>().enabled = false;
            }
            else if (index != -1 && ingredientCurrentAmounts[index] > 0)
            {
                cloneDraggable.isDraggable = true;
                cloneDraggable.GetComponent<Collider2D>().enabled = true;
            }

            // ��ӵ� ������Ʈ ����Ʈ�� �߰�
            droppedItems.Add(other.gameObject);

            // �߰�: Bun ���� Ȯ�� �� ��� �巡�� ��Ȱ��ȭ ���� ����
            CheckAndDisableDraggableItems();

            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_PUT);

        }
    }

    public void UpdateIngredientAmount(string ingredientName, int newAmount)
    {
        int index = ingredientNames.IndexOf(ingredientName);
        if (index != -1)
        {
            ingredientCurrentAmounts[index] = newAmount;
        }
    }
    public void MakeClonesDraggable() // Ŭ���� �巡�� �����ϵ��� �����ϴ� �޼���
    {
        // ��� Draggable ��ü�� �˻�
        var allDraggables = GameObject.FindObjectsOfType<Draggable>();

        foreach (var cloneDraggable in allDraggables)
        {
            if (cloneDraggable != null)
            {
                // ��� ������ �ִ� Ŭ�е��� �巡�� �Ұ����ϰ� ����
                if (cloneDraggable.transform.parent == transform)
                {
                    cloneDraggable.isDraggable = false;
                    cloneDraggable.GetComponent<Collider2D>().enabled = false;
                }
                else
                {
                    cloneDraggable.isDraggable = true;
                    cloneDraggable.GetComponent<Collider2D>().enabled = true;
                }
            }
        }
    }

    private void DisableAllDraggableItems()
    {
        // ��� ���� ���� ��� �巡�� ������ ������Ʈ�� ��Ȱ��ȭ
        foreach (var draggable in FindObjectsOfType<Draggable>())
        {
            draggable.isDraggable = false;
        }
    }

    private void AbleAllDraggableItems()
    {
        // ��� ���� ���� ��� �巡�� ������ ������Ʈ�� Ȱ��ȭ
        foreach (var draggable in FindObjectsOfType<Draggable>())
        {
            draggable.isDraggable = true;
        }
    }


    public void DisableCollidersRecursively(GameObject parentObject)
    {
        Collider2D[] colliders2D = parentObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider2D in colliders2D)
        {
            collider2D.enabled = false;
        }
    }

    public void ableCollidersRecursively(GameObject parentObject)
    {
        Collider2D[] colliders2D = parentObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider2D in colliders2D)
        {
            collider2D.enabled = true;
        }
    }

    // DropArea �ȿ� �� ��� ������Ʈ�� �����ϴ� �޼���
    public void ClearAllDroppedItems() // �������� ���� �޼���
    {
        // ��ӵ� ������ ����
        foreach (GameObject item in droppedItems)
        {
            Destroy(item);
        }
        droppedItems.Clear();
        stackCount = 0; // ���� ������Ʈ �� �ʱ�ȭ

        // DropArea�� �θ� Hamburger���� Ȯ���ϰ� ����
        if (transform.parent != null && transform.parent.name == "Hamburger")
        {
            GameObject hamburger = transform.parent.gameObject;
            transform.SetParent(null); // DropArea�� �и�
            Destroy(hamburger); // Hamburger ��ü ����
        }

        if (instantiatedBurger != null)
        {
            Destroy(instantiatedBurger);
            instantiatedBurger = null; // ���� �ʱ�ȭ
        }

        Transform dropArea = FindObjectOfType<Droppable>().transform; // DropArea ����
        SetObjectTransparency(dropArea.gameObject, 1.0f); // DropArea�� ������ ���·� ����

        ableCollidersRecursively(gameObject);

        // ��� Draggable ������ �ٽ� Ȱ��ȭ
        AbleAllDraggableItems();

    }

}