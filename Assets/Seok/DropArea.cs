using UnityEngine;

public class DropArea : MonoBehaviour
{
    public static DropArea Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}