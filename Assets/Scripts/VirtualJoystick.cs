using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Спрайт, перемещаемый по экрану
    public RectTransform thumb;

    //Местоположение джойстика и пальца, когда происходит перемещение   
    private Vector2 originalPosition;
    private Vector2 originalThumbPosition;
    
    //Расстояние, на которое сместился палец относительно исходного местоположения
    public Vector2 delta; 

    private void Start()
    {
        //Записывает исходные координаты
        originalPosition = this.GetComponent<RectTransform>().localPosition;
        originalThumbPosition = thumb.localPosition;

        //Выключить площадку, сделав ее невидимой
        thumb.gameObject.SetActive (false);
        //Сброс величину смещения
        delta = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        thumb.gameObject.SetActive(true);
        //Фиксация мировых координат начала перемещения
        Vector3 worldPoint = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, eventData.position, eventData.enterEventCamera, out worldPoint);

        this.GetComponent<RectTransform>().position = worldPoint;

        thumb.localPosition = originalThumbPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //определяет мировые координаты точки соприкосновения пальца с экраном
        Vector3 worldPoint = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, eventData.position, eventData.enterEventCamera, out worldPoint);

        thumb.position = worldPoint;
        //Вычислить смещение от исходной позиции
        var size = GetComponent<RectTransform>().rect.size;
        delta = thumb.localPosition;
        delta.x /= size.x / 2.0f;
        delta.y /= size.y / 2.0f;

        delta.x = Mathf.Clamp(delta.x, -1.0f, 1.0f);
        delta.y = Mathf.Clamp(delta.y, -1.0f, 1.0f);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localPosition = originalPosition;
        delta = Vector2.zero;
        thumb.gameObject.SetActive(false);

    }
}
