using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(IGameEntity))]
public class CubeVisualUpdater : MonoBehaviour
{
    [SerializeField] protected MeshRenderer meshRenderer;
    [SerializeField] private TextMeshProUGUI[] cubeValueTexts;
    [SerializeField] private Color[] colors;

    private IGameEntity gameEntity;

    private void Awake()
    {
        gameEntity = GetComponent<IGameEntity>();
    }

    private void OnInitialized()
    {
        UpdateText();
        UpdateColor();
    }

    private void UpdateText()
    {
        foreach (TextMeshProUGUI text in cubeValueTexts)
        {
            if (text != null)
                text.text = gameEntity.Value.ToString();
        }       
    }

    private void UpdateColor()
    {
        int index = (int)Mathf.Log(gameEntity.Value, 2) - 1;
        index = Mathf.Clamp(index, 0, colors.Length - 1);
        meshRenderer.material.color = colors[index];
    }

    private void OnEnable()
    {
        gameEntity.OnInitialized += OnInitialized;
    }

    private void OnDisable()
    {
        gameEntity.OnInitialized -= OnInitialized;
    }
}
