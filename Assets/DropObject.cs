using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
	private Sprite nowSprite;
	private Transform canvasTran;
	private GameObject droppedObject;
    private GameObject newConnector;

	void Start()
	{
		canvasTran = transform.parent;
		image = gameObject.GetComponent<Image>();
        nowSprite = image.sprite;
	}

	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		if (pointerEventData.pointerDrag == null) return;

		if (pointerEventData.pointerDrag.tag == "block")
		{
			insertBlock(pointerEventData);
		}

		if (pointerEventData.pointerDrag.tag == "if") 
        {
            insertIf(pointerEventData, 1);
        }
	}

    private void insertBlock(PointerEventData pointerEventData) 
    {
		droppedObject = Instantiate(pointerEventData.pointerDrag);

		DragObject drag = droppedObject.GetComponent<DragObject>();
		if (drag.isDropped) return;

		int transformAmount = 75;

		Vector3 newDropppedObjectPosition = gameObject.transform.position;
		newDropppedObjectPosition.x += transformAmount;
		droppedObject.transform.position = newDropppedObjectPosition;
		droppedObject.transform.SetParent(canvasTran);
		droppedObject.transform.SetAsLastSibling();
		droppedObject.transform.localScale = Vector3.one;
		droppedObject.GetComponent<Image>().color = Vector4.one * 0.6f;

		newConnector = Instantiate(gameObject);
		Vector3 newConnectorPosition = gameObject.transform.position;
		newDropppedObjectPosition.x += transformAmount;
        newConnector.transform.position = newDropppedObjectPosition;
		newConnector.transform.SetParent(canvasTran);
		newConnector.transform.SetAsLastSibling();
		newConnector.transform.localScale = Vector3.one;
		newConnector.GetComponent<Image>().color = Vector4.one;

		GameObject end = GameObject.Find("end");
		newDropppedObjectPosition.x += transformAmount;
		end.transform.position = newDropppedObjectPosition;
	}

    private void makeIfPrefab(string prefabName, Vector3 position) {
        GameObject p1 = (GameObject)Resources.Load(prefabName);
		GameObject ifp1 = Instantiate(p1, position, Quaternion.identity);
		ifp1.transform.SetParent(canvasTran);
		ifp1.transform.SetAsLastSibling();
		ifp1.transform.localScale = Vector3.one;
	}

    private void insertIf(PointerEventData pointerEventData, int howLong) {
        int transformAmount = 75;

		droppedObject = Instantiate(pointerEventData.pointerDrag);
		Vector3 ifPosition = gameObject.transform.position;
		ifPosition.x += transformAmount;
		droppedObject.transform.position = ifPosition;
		droppedObject.transform.SetParent(canvasTran);
		droppedObject.transform.SetAsLastSibling();
		droppedObject.transform.localScale = Vector3.one;
		droppedObject.GetComponent<Image>().color = Vector4.one * 0.6f;

		ifPosition.x += transformAmount;
		makeIfPrefab("if-open", ifPosition);

        for (int i = 0; i < howLong; i++) 
        {
			ifPosition.x += transformAmount;
			makeIfPrefab("if-clauses", ifPosition);
		}

		ifPosition.x += transformAmount;
		makeIfPrefab("if-close", ifPosition);

        // this should not be here. move this to other function
		GameObject end = GameObject.Find("end");
		ifPosition.x += transformAmount;
		end.transform.position = ifPosition;
	}

	public void OnPointerExit(PointerEventData pointerEventData)
	{
		DragObject drag = droppedObject.GetComponent<DragObject>();
        if (!drag.isDropped) 
        {
			Destroy(droppedObject);
			Destroy(newConnector);
			GameObject end = GameObject.Find("end");
			Vector3 newEndPosition = end.transform.position;
			newEndPosition.x -= 150;
			end.transform.position = newEndPosition;
		}

		if (pointerEventData.pointerDrag == null) return;
		image.sprite = nowSprite;
		if (nowSprite == null)
			image.color = Vector4.zero;
		else
			image.color = Vector4.one;
	}

	public void OnDrop(PointerEventData pointerEventData)
	{
		droppedObject.GetComponent<Image>().color = Vector4.one;
        DragObject drag = droppedObject.GetComponent<DragObject>();
        drag.isDropped = true;

//        Destroy(gameObject);
	}
}
