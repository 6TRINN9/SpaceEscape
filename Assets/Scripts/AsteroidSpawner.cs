using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Радиус сферы, на поверхности которой создаются астероиды
    public float radius = 250.0f;
    // Шаблон для создания астероидов
    public Rigidbody asteroidPrefab;
    // Ждать spawnRate ± variance секунд перед созданием нового астероида
    public float spawnRate = 5.0f;
    public float variance = 1.0f;
    // Объект, служащий целью для астероидов
    public Transform target;
    // Значение false запрещает создавать астероиды
    public bool spawnAsteroids = false;
    private void Start () 
    {
    // Запустить сопрограмму, создающую астероиды, немедленно
    StartCoroutine(CreateAsteroids());
    }
    
    IEnumerator CreateAsteroids()
    {
        while(true)
        {
            float nextSpawnTime = spawnRate + Random.Range(-variance, variance);
            yield return new WaitForSeconds(nextSpawnTime);
            yield return new WaitForFixedUpdate();
            CreateNewAsteroid();
        }
    }

    void CreateNewAsteroid()
    {
        //Если запрещено создавать астеройды, выйти
        if(spawnAsteroids == false)
        {
            return;
        }
        //выбор случайной точки
        Vector3 asteroidPosition = Random.onUnitSphere * radius;
        // Масштабировать в соответствии с объектом
        asteroidPosition.Scale(transform.lossyScale);
        // И добавить смещение объекта, порождающего астероиды
        asteroidPosition += transform.position;
        // Создать новый астероид
        Rigidbody newAsteroid = Instantiate(asteroidPrefab);
        // Поместить его в только что вычисленную точку
        newAsteroid.transform.position = asteroidPosition;
        // Направить на цель
        newAsteroid.transform.LookAt(target);
    }
     
    // Вызывается редактором, когда выбирается объект,  порождающий астероиды.
    void OnDrawGizmosSelected() {
        // Установить желтый цвет
        Gizmos.color = Color.yellow;
        // Сообщить визуализатору Gizmos, что тот должен использовать
        // текущие позицию и масштаб
        Gizmos.matrix = transform.localToWorldMatrix;
        // Нарисовать сферу, представляющую собой область создания астероидов
        Gizmos.DrawWireSphere(Vector3.zero, radius);
    }
    public void DestroyAllAsteroids() {
        // Удалить все имеющиеся в игре астероиды
        foreach (var asteroid in
        FindObjectsOfType<Asteroid>()) {
        Destroy (asteroid.gameObject);
        }
    }
}
