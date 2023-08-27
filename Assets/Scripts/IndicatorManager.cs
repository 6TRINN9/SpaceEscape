using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IndicatorManager : Singleton<IndicatorManager> 
{
    // Объект, потомками которого будут все индикаторы
    public RectTransform labelContainer;
    // Шаблон для создания индикаторов
    public Indicator indicatorPrefab;
    //
    public GameObject objectDistance;
    // Этот метод будет вызываться другими объектами
    public Indicator AddIndicator(GameObject target, Color color, Sprite sprite = null, bool showDistanceTo = false) 
    {
        // Создать объект индикатора
        Indicator newIndicator = Instantiate(indicatorPrefab);
        // Связать его с целевым объектом
        newIndicator.target = target.transform;
        // Обновить его цвет
        newIndicator.color = color;
        // Если задан спрайт, установить его как изображение для данного индикатора
        if (sprite != null) 
        {
            newIndicator.GetComponent<Image>().sprite = sprite;
        }
        // Добавить индикатор в контейнер.
        newIndicator.transform.SetParent(labelContainer, false);
        //Показывать расстояние
        newIndicator.showDistanceTo = showDistanceTo;
        //Объекто до которого рассчитавется расстояние
        newIndicator.objectDistanceTo = objectDistance.transform;
        
        return newIndicator;
    }
}
