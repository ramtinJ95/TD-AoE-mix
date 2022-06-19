using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite mousePointerSprite;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> btnTransformDictionary;
    private Transform mousePointerBtn;

    private void Awake()
    {
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        mousePointerBtn = Instantiate(btnTemplate, transform);
        mousePointerBtn.gameObject.SetActive(true);

        float offsetAmountUI = 120f;
        mousePointerBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmountUI * index, 0);

        mousePointerBtn.Find("image").GetComponent<Image>().sprite = mousePointerSprite;
        mousePointerBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        mousePointerBtn.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            if (ignoreBuildingTypeList.Contains(buildingType)) continue;
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            offsetAmountUI = 120f;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmountUI * index, 0);

            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });
            btnTransformDictionary[buildingType] = btnTransform;
            index++; 
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuldingTypeChanged += BuildingManager_OnActiveBuldingTypeChanged;
        UpdateActiveBuildingTypeButton();
    }

    private void BuildingManager_OnActiveBuldingTypeChanged(object sender, BuildingManager.OnActiveBuldingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton()
    {
        mousePointerBtn.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buldingType in btnTransformDictionary.Keys)
        {
            Transform btnTransform = btnTransformDictionary[buldingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if(activeBuildingType == null)
        {
            mousePointerBtn.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}
