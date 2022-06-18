using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    private SortingGroup parentRenderer;

    private void Awake()
    {
        parentRenderer = GetComponent<SortingGroup>();
    }

    private void LateUpdate()
    {
        float precisionMultiplyer = 5f;
        parentRenderer.sortingOrder = (int)(-transform.position.y * precisionMultiplyer);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
