using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    //Цель отслеживания
    public Transform target;
    //Расстояние от 'target' до данного объекта.
    public bool showDistanceTo = false;
    public Transform objectDistanceTo;
    //Текст для отображения расстояния
    public Text distanceLabel;
    //Расстояние от края экрана
    public int margin = 10;
    //Размер изображения относительно расстояния
    private float minSize = 0.25f;
    private float maxSize = 1f;
    public Color color
    { 
        set
        {
            GetComponent<Image>().color = value;
        }
        get
        {
            return GetComponent<Image>().color;
        }
     }
    
    private void Start()
    {
        distanceLabel.enabled = false;
        GetComponent<Image>().enabled = false;
    }

    private void FixedUpdate()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }     
        
        //Определяет расстояние до цели
        var distance = (int)Vector3.Magnitude(objectDistanceTo.position - target.position);

        //Изменнение размера идикатора относительно расстояния
        /*float distance2 = distance/10;
        distance2 = Mathf.Clamp(distance2, minSize, maxSize);
        GetComponent<RectTransform>().localScale = new Vector3(distance2,distance2, transform.lossyScale.z);*/

        if(showDistanceTo)
        {
            
            distanceLabel.enabled = true;            
            distanceLabel.text = distance.ToString() + "pc";
        }   
        else
        {
            distanceLabel.enabled = false;
        }

        GetComponent<Image>().enabled = true;

        //Определить координаты объекта
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(target.position);

        //Объект позади нас
        if(viewportPoint.z < 0)
        {
            viewportPoint.z = 0;
            viewportPoint = viewportPoint.normalized;
            viewportPoint.x *= -Mathf.Infinity;
        }
        //Определить видимые коордынаты для идикатора
        Vector3 screenPoint = Camera.main.ViewportToScreenPoint(viewportPoint);
        //Ограничить краями экрана
        screenPoint.x = Mathf.Clamp(screenPoint.x, margin, Screen.width - margin *2);
        screenPoint.y = Mathf.Clamp(screenPoint.y, margin, Screen.height - margin *2);

        //Определить, где в области холста находится видимая координата

        Vector2 localPosition = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(),
        screenPoint, Camera.main, out localPosition);
        // Обновить позицию индикатора
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = localPosition;
    }
}
