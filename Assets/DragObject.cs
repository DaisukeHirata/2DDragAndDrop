using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class Extensions
{
	public static void SetTransparency(this UnityEngine.UI.Image p_image, float p_transparency)
	{
		if (p_image != null)
		{
			UnityEngine.Color __alpha = p_image.color;
			__alpha.a = p_transparency;
			p_image.color = __alpha;
		}
	}
}

[RequireComponent(typeof(Image))]
public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Transform canvasTran;
	private GameObject draggingObject;
    public bool isDropped;

    void Start()
	{
		canvasTran = transform.parent;
	}

	public void OnBeginDrag(PointerEventData pointerEventData)
	{
		CreateDragObject();
		draggingObject.transform.position = pointerEventData.position;

		if (isDropped)
		{
			CreatePlaceholder();
		}
	}

	public void OnDrag(PointerEventData pointerEventData)
	{
		draggingObject.transform.position = pointerEventData.position;
	}

	public void OnEndDrag(PointerEventData pointerEventData)
	{
		gameObject.GetComponent<Image>().color = Vector4.one;
		Destroy(draggingObject);

		if (isDropped)
		{
			Destroy(gameObject);
		}

        if (gameObject.tag == "if") {
            print("if");
        }
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

    private void CreatePlaceholder() {
		GameObject prefab = (GameObject)Resources.Load("placeholder");
        GameObject ph = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
		ph.transform.SetParent(canvasTran);
		ph.transform.SetAsLastSibling();
		ph.transform.localScale = Vector3.one;
	}
}
