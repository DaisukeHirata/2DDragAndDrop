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

		moveRight(pointerEventData);

//		Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
//		image.sprite = droppedImage.sprite;
//		image.color = Vector4.one * 0.6f;
	}

    private void moveRight(PointerEventData pointerEventData) 
    {
		GameObject droppedObject = Instantiate(pointerEventData.pointerDrag);
		Vector3 newDropppedObjectPosition = gameObject.transform.position;
		newDropppedObjectPosition.x += 75;
		droppedObject.transform.position = newDropppedObjectPosition;
		droppedObject.transform.SetParent(canvasTran);
		droppedObject.transform.SetAsLastSibling();
		droppedObject.transform.localScale = Vector3.one;
		droppedObject.GetComponent<Image>().color = Vector4.one * 0.6f;

		GameObject newConnector = Instantiate(gameObject);
        Vector3 newConnectorPosition = gameObject.transform.position;
        newConnectorPosition.x += 150;
        newConnector.transform.position = newConnectorPosition;
		newConnector.transform.SetParent(canvasTran);
		newConnector.transform.SetAsLastSibling();
		newConnector.transform.localScale = Vector3.one;
		newConnector.GetComponent<Image>().color = Vector4.one;

        GameObject end = GameObject.Find("end");
		Vector3 newEndPosition = end.transform.position;
        print(newEndPosition);
        newEndPosition.x += 150;
        end.transform.position = newEndPosition;
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
        Vector3 newDropppedObjectPosition = gameObject.transform.position;
        newDropppedObjectPosition.x += 75;
        droppedObject.transform.position = newDropppedObjectPosition;
		droppedObject.transform.SetParent(canvasTran);
		droppedObject.transform.SetAsLastSibling();
		droppedObject.transform.localScale = Vector3.one;
		droppedObject.GetComponent<Image>().color = Vector4.one;

        DragObject drag = droppedObject.GetComponent<DragObject>();
        drag.isDropped = true;

//        Destroy(gameObject);
	}
}
