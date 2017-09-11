using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Transform canvasTran;
	private GameObject draggingObject;

	void Awake()
	{
		canvasTran = transform.parent;
	}

	public void OnBeginDrag(PointerEventData pointerEventData)
	{
		CreateDragObject();
		draggingObject.transform.position = pointerEventData.position;
	}

	public void OnDrag(PointerEventData pointerEventData)
	{
		draggingObject.transform.position = pointerEventData.position;
	}

	public void OnEndDrag(PointerEventData pointerEventData)
	{
		gameObject.GetComponent<Image>().color = Vector4.one;
		Destroy(draggingObject);
	}

	// ドラッグオブジェクト作成
	private void CreateDragObject()
	{
		draggingObject = new GameObject("Dragging Object");
		draggingObject.transform.SetParent(canvasTran);
		draggingObject.transform.SetAsLastSibling();
		draggingObject.transform.localScale = Vector3.one;

		// レイキャストがブロックされないように
		CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
		canvasGroup.blocksRaycasts = false;

		Image draggingImage = draggingObject.AddComponent<Image>();
		Image sourceImage = GetComponent<Image>();

		draggingImage.sprite = sourceImage.sprite;
		draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
		draggingImage.color = sourceImage.color;
		draggingImage.material = sourceImage.material;

		gameObject.GetComponent<Image>().color = Vector4.one * 0.6f;
	}
}
