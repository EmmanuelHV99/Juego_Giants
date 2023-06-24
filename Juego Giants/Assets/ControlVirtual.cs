using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlVirtual : MonoBehaviour,IDragHandler, 
IPointerDownHandler, IPointerUpHandler {
	public static Image imFondo;
	public static Image imControl;
	public static Vector3 inputVector;

	// Use this for initialization
	void Start () {
		imFondo = GetComponent<Image> ();
		imControl = transform.GetChild(0).GetComponent<Image> ();
		inputVector = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag(PointerEventData ped){
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle 
			(imFondo.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = pos.x / imFondo.rectTransform.sizeDelta.x;
			pos.y = pos.y / imFondo.rectTransform.sizeDelta.y;

			inputVector = new Vector3 (pos.x*2 -1, 0, pos.y*2-1);
			inputVector = (inputVector.magnitude > 1) ? 
				inputVector.normalized : inputVector;
			//if (inputVector.magnitude > 1)
			//	inputVector = inputVector.normalized;
			Debug.Log (inputVector);

			//mover imagen control
			imControl.rectTransform.anchoredPosition = 
				new Vector3 (inputVector.x * imFondo.rectTransform.sizeDelta.x / 3, 
					inputVector.z * imFondo.rectTransform.sizeDelta.y / 3);
		}

	}

	public void OnPointerDown(PointerEventData ped){
		OnDrag (ped);
	}

	public void OnPointerUp(PointerEventData ped){
		inputVector= Vector3.zero;
		imControl.rectTransform.anchoredPosition = Vector3.zero;
	}
}
