using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	private Transform canvasTran;
    private GameObject droppingObject;

	void Start()
	{
        canvasTran = GameObject.Find("Panel").transform;
	}

	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		if (pointerEventData.pointerDrag == null) return;

		switch (pointerEventData.pointerDrag.tag)
		{
			case "if":
				insertIf(pointerEventData, "if", 1);
				break;
			case "block":
				insertBlock(pointerEventData, "block");
				break;
		}
	}

	public void OnPointerExit(PointerEventData pointerEventData)
	{
		if (pointerEventData.pointerDrag == null) return;
		if (droppingObject == null) return;

		DroppingObject dropping = droppingObject.GetComponent<DroppingObject>();
		if (!dropping.isDropped)
		{
			Destroy(droppingObject);
			GameObject end = GameObject.Find("end");
			Vector3 newEndPosition = end.transform.position;

			switch (pointerEventData.pointerDrag.tag)
			{
				case "if":
					newEndPosition.x -= 300;
					break;
				case "block":
					newEndPosition.x -= 150;
					break;
			}

			end.transform.position = newEndPosition;
		}
	}

	public void OnDrop(PointerEventData pointerEventData)
	{
		CanvasGroup canvasGroup = droppingObject.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1.0f;
		DroppingObject dropping = droppingObject.GetComponent<DroppingObject>();
		dropping.isDropped = true;
        transform.DetachChildren();
	}

    private void insertBlock(PointerEventData pointerEventData, string tagName) 
    {
		int transformAmount = 75;

		Vector3 insertPosition = gameObject.transform.position;
		insertPosition.x += transformAmount;
        Transform parent = gameObject.transform.parent;
        Transform grandParent = parent.transform.parent;
        droppingObject = makeIfPrefab("envelope", insertPosition, grandParent);
		droppingObject.tag = tagName;
        droppingObject.transform.SetSiblingIndex(parent.GetSiblingIndex() + 1);
		CanvasGroup canvasGroup = droppingObject.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0.3f;

		GameObject copiedBlock = Instantiate(pointerEventData.pointerDrag);
		copiedBlock.transform.position = insertPosition;
		copiedBlock.transform.SetParent(droppingObject.transform);
		copiedBlock.transform.SetAsLastSibling();
		copiedBlock.transform.localScale = Vector3.one;
		copiedBlock.GetComponent<Image>().color = Vector4.one;

		switch (gameObject.tag)
		{
			case "if-top-left":
                GameObject next = getNext().gameObject;
                if (next.tag == "space") {
                    Destroy(next);
                }
				break;
			case "placeholder":
				GameObject afterConnector = Instantiate(gameObject);
				insertPosition.x += transformAmount;
				afterConnector.transform.position = insertPosition;
				afterConnector.transform.SetParent(droppingObject.transform);
				afterConnector.transform.SetAsLastSibling();
				afterConnector.transform.localScale = Vector3.one;
				afterConnector.GetComponent<Image>().color = Vector4.one;

				moveFollowingObjects(droppingObject.transform, 150);
				break;
		}
	}

	private void insertIf(PointerEventData pointerEventData, string tagName, int howLong)
	{
		int transformAmount = 75;

		Vector3 insertPosition = gameObject.transform.position;
		insertPosition.x += transformAmount;
		Transform parent = gameObject.transform.parent;
		Transform grandParent = parent.transform.parent;
		droppingObject = makeIfPrefab("envelope", insertPosition, grandParent);
		droppingObject.tag = tagName;
		droppingObject.transform.position = insertPosition;
        droppingObject.transform.SetSiblingIndex(parent.GetSiblingIndex() + 1);
		CanvasGroup canvasGroup = droppingObject.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0.3f;

		makeIfPrefab("if", insertPosition, droppingObject.transform);

		insertPosition.x += transformAmount;
		makeIfPrefab("if-open", insertPosition, droppingObject.transform);

		for (int i = 0; i < howLong; i++)
		{
			insertPosition.x += transformAmount;
			makeIfPrefab("if-blocks", insertPosition, droppingObject.transform);
		}

		insertPosition.x += transformAmount;
		makeIfPrefab("if-close", insertPosition, droppingObject.transform);

		moveFollowingObjects(droppingObject.transform, 300);
	}

	private GameObject makeIfPrefab(string prefabName, Vector3 position, Transform parent) {
        GameObject p1 = (GameObject)Resources.Load(prefabName);
		GameObject ifp1 = Instantiate(p1, position, Quaternion.identity);
		ifp1.transform.SetParent(parent);
        ifp1.transform.SetAsLastSibling();
		ifp1.transform.localScale = Vector3.one;
        return ifp1;
	}

    private Transform getNext()
	{
		var myself = transform;
		var parent = transform.parent;
		var childCount = parent.childCount;
		for (int i = 0; i < childCount - 1; i++)
		{
			if (parent.GetChild(i) == myself)
				return parent.GetChild(i + 1);
		}
		return null;
	}

    private void moveFollowingObjects(Transform origin, int movingAmount) 
    {
        int originIndex = origin.GetSiblingIndex();

		var myself = transform;
		var parent = origin.parent;
		var childCount = parent.childCount;
		for (int i = originIndex + 1; i < childCount; i++)
		{
            var currentPosition = parent.GetChild(i).transform.position;
            currentPosition.x += movingAmount;
            parent.GetChild(i).transform.position = currentPosition;
		}
	}

    private GameObject createFollowingObject(Vector3 insertPosition)
    {
		var followingObject = makeIfPrefab("envelope", insertPosition, canvasTran);
        followingObject.name = "following";

		var myself = transform;
		var parent = transform.parent;
		var childCount = parent.childCount;
        var foundMyself = false;
		for (int i = 0; i < childCount; i++)
		{
            if (foundMyself) {
				var obj = parent.GetChild(i);
                obj.transform.SetParent(followingObject.transform);
            }

            if (parent.GetChild(i) == myself) foundMyself = true;
		}

        return followingObject;
	}
}
