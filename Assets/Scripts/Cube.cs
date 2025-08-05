using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] protected MeshRenderer meshRenderer;

    [SerializeField] private TextMeshProUGUI[] cubeValueTexts;

    [SerializeField] private float minImpulse = 1.5f;

    [SerializeField] private Color[] colors;
 
    private bool isMerging = false;

    public delegate void OnCubeMerge(int newValue, Vector3 position);
    public static event OnCubeMerge onCubeMerge;

    private int _value;

    public int Value
    {
        get => _value;
        private set
        {
            _value = value;
            UpdateUI();
        }
    }


    public void Init(int value = 2, bool isKinematic = true)
    {
        rb.isKinematic = isKinematic;
        Value = value;
    }

    private void UpdateUI()
    {
        foreach (TextMeshProUGUI text in cubeValueTexts)
        {
            if (text != null)
                text.text = Value.ToString();
        }

        int index = (int)Mathf.Log(Value, 2) - 1;
        index = Mathf.Clamp(index, 0, colors.Length - 1);
        meshRenderer.material.color = colors[index];
    }

    public void Throw(float impluse)
    {
        rb.isKinematic = false;
        rb.AddForce(Vector3.forward * impluse, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isMerging) return;

        Cube other = collision.gameObject.GetComponent<Cube>();
        if (other == null || other.isMerging) return;

        float impactForce = collision.impulse.magnitude;
        if (impactForce < minImpulse) return;

        if (Value == other.Value)
        {
            MergeWith(other);
        }
    }

    void MergeWith(Cube other)
    {
        isMerging = true;
        other.isMerging = true;

        int newValue = Value * 2;

        Vector3 mergePosition = (transform.position + other.transform.position) / 2;

        Destroy(other.gameObject);

        onCubeMerge?.Invoke(newValue, mergePosition);

        Destroy(gameObject);
    }
}
