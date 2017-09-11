using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
	private Sprite nowSprite;
	private Transform canvasTran;

	void Start()
	{
		canvasTran = transform.parent;
		image = gameObject.GetComponent<Image>();
        nowSprite = image.sprite;
	}

	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		if (pointerEventData.pointerDrag == null) return;
		Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
		image.sprite = droppedImage.sprite;
		image.color = Vector4.one * 0.6f;
	}

	public void OnPointerExit(PointerEventData pointerEventData)
	{
		if (pointerEventData.pointerDrag == null) return;
		image.sprite = nowSprite;
		if (nowSprite == null)
			image.color = Vector4.zero;
		else
			image.color = Vector4.one;
	}

	public void OnDrop(PointerEventData pointerEventData)
	{
        GameObject droppedObject = Instantiate(pointerEventData.pointerDrag);
        droppedObject.transform.position = gameObject.transform.position;
		droppedObject.transform.SetParent(canvasTran);
		droppedObject.transform.SetAsLastSibling();
		droppedObject.transform.localScale = Vector3.one;
		droppedObject.GetComponent<Image>().color = Vector4.one;

        DragObject drag = droppedObject.GetComponent<DragObject>();
        drag.isDropped = true;

        Destroy(gameObject);
	}
}
