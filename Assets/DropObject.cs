using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
	private Sprite nowSprite;

	void Start()
	{
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
		Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
		image.sprite = droppedImage.sprite;
		nowSprite = droppedImage.sprite;
		image.color = Vector4.one;
	}
}
