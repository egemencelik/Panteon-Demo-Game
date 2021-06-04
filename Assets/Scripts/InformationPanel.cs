using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    public static InformationPanel instance;

    [SerializeField]
    private GridLayoutGroup layout;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private TextMeshProUGUI elementName;

    [SerializeField]
    private Image elementImage;

    [SerializeField]
    private Button moveButton;

    private BoardElement selectedBoardElement;

    private void Awake()
    {
        instance = this;
        moveButton.onClick.AddListener(Move);
        moveButton.gameObject.SetActive(false);
    }

    private void Move()
    {
        selectedBoardElement.View.SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        var controller = selectedBoardElement.GetComponent<BoardBuildingController>();
        controller.OnMove();
        controller.Initialize();
        GameManager.instance.State = PlayerState.Moving;
        ClearSelectedBoardElement();
    }

    public void SetSelectedBoardElement(BoardElement selected)
    {
        if (selectedBoardElement != null) ClearSelectedBoardElement();
        moveButton.gameObject.SetActive(true);
        selectedBoardElement = selected;
        if (selectedBoardElement.Model is BoardBuildingModel buildingModel)
        {
            foreach (var product in buildingModel.Products)
            {
                var item = Instantiate(prefab, layout.transform);
                var itemScript = item.GetComponent<ProductMenuItem>();
                itemScript.Initialize(product, (BoardBuildingView) buildingModel.boardElement.View);
            }
        }

        elementName.text = selectedBoardElement.Model.Text;
        elementImage.sprite = selectedBoardElement.Model.Image;
        selectedBoardElement.View.SetSelected(true);
    }

    public void ClearSelectedBoardElement()
    {
        if (selectedBoardElement == null) return;
        foreach (Transform tr in layout.transform)
        {
            Destroy(tr.gameObject);
        }
        moveButton.gameObject.SetActive(false);
        selectedBoardElement.View.SetSelected(false);
        selectedBoardElement = null;
        elementName.text = "";
        elementImage.sprite = null;
    }
}