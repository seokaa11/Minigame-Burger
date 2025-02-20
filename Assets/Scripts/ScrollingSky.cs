using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingSky : MonoBehaviour
{
    [SerializeField] Vector2 speed;
    Material mat;
    Vector2 offset;

    void Start()
    {
        mat = GetComponent<Image>().material;        
    }

    void Update()
    {
        offset = speed * Time.deltaTime;
        mat.mainTextureOffset += offset;
    }
    void OnDisable()
    {
        mat.mainTextureOffset=Vector2.zero;

    }
}
