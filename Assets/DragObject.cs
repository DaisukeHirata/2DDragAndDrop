using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Transform canvasTran;
	private GameObject draggingObject;

    void Start()
	{
		canvasTran = GameObject.Find("Panel").transform;
	}

	public void OnBeginDrag(PointerEventData pointerEventData)
	{
		CreateDragObject();
		draggingObject.transform.position = pointerEventData.position;

        if (isDropped()) removingDroppedEnvelope();
	}

	public void OnDrag(PointerEventData pointerEventData)
	{
		draggingObject.transform.position = pointerEventData.position;
	}

	public void OnEndDrag(PointerEventData pointerEventData)
	{
		Destroy(draggingObject);

		if (gameObject.tag == "if") 
        {
            print("if");
        }

        if (isDropped()) removeDroppedEnvelope();
	}

    private void removingDroppedEnvelope() 
    {
		GameObject parent = transform.parent.gameObject;
		CanvasGroup canvasGroup = parent.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0.3f;
	}

    private void removeDroppedEnvelope() 
    {
		GameObject parent = transform.parent.gameObject;
		GameObject end = GameObject.Find("end");
		Vector3 newEndPosition = end.transform.position;
		switch (parent.tag)
		{
			case "if":
				newEndPosition.x -= 300;
				break;
			case "block":
				newEndPosition.x -= 150;
				break;
		}
		end.transform.position = newEndPosition;
		Destroy(parent);
	}

    private bool isDropped() {
		GameObject parent = transform.parent.gameObject;
		DroppingObject dropping = parent.GetComponent<DroppingObject>();
        if (dropping != null && dropping.isDropped) {
            return true;
        } else {
            return false;
		}
 	}

	private void CreateDragObject()
	{
		draggingObject = new GameObject("Dragging Object");
		draggingObject.transform.SetParent(canvasTran);
		draggingObject.transform.SetAsLastSibling();
		draggingObject.transform.localScale = Vector3.one;

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

    private void CreateSpace() {
		GameObject prefab = (GameObject)Resources.Load("space");
        GameObject ph = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
		ph.transform.SetParent(canvasTran);
		ph.transform.SetAsLastSibling();
		ph.transform.localScale = Vector3.one;
	}
}
