using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaking : MonoBehaviour {
    // Число очков прочности данного объекта
    [SerializeField] private int hitPoints = 10;

    // При разрушении создать этот объект
    // в текущей позиции
    [SerializeField] private GameObject destructionPrefab;

    // Завершить игру при разрушении данного объекта?
    [SerializeField] private bool gameOverOnDestroyed = false;

    // Вызывается другими объектами (например, астероидами и шарами плазмы)
    // для нанесения повреждений
    public virtual void TakeDamage(int amount) 
    {
        // Сообщить о попадании в текущий объект
        Debug.Log(gameObject.name + " damaged!");

        // Вычесть amount из числа очков прочности
        hitPoints -= amount;

        // Очки исчерпаны?
        if (hitPoints <= 0) 
        {
            // Зафиксировать этот факт
            Debug.Log(gameObject.name + " destroyed!");            
            // Задан шаблон для создания объекта в точке разрушения?
            if (destructionPrefab != null) 
            {
                // Создать объект в текущей позиции
                // с текущей ориентацией.
                Instantiate(destructionPrefab, transform.position, transform.rotation);
            }
            // Удалить себя из игры
            Destroy(gameObject);
            if (gameOverOnDestroyed == true)
            {
                GameManager.instance.GameOver();
            }

        }
    }

    public int GetHitsPoints()
    {
        return hitPoints;
    }
}