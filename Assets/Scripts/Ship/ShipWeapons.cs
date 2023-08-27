using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipWeapons : MonoBehaviour
{
    private bool isFiring = false;
    private int firePointIndex;
    private PlayerInput playerInput;

    //Префаб для снарядов
    public GameObject shotsPrefans;
    //Список пушек
    public Transform[] firePoints;
    [SerializeField] private float fireRate = 0.2f;



    private void Awake ()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Shooting.started += StartFiring;
        playerInput.Player.Shooting.performed += StopFiring;

    }

    private void OnEnable ()
    {
        playerInput.Enable();
    }

    private void OnDisable ()
    {
        playerInput.Disable();
    }

    private void StartFiring (InputAction.CallbackContext context)
    {
        //Запуск сопрограммы ведения огня
        if (context.started && firePoints.Length != 0)
        {            
            isFiring = true;
            Debug.Log("Firing");
            StartCoroutine(FireWeapons());
            
        }
        else
        {
            return;
        }

    }
    private void StopFiring (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isFiring = false;
            Debug.Log("Firing stoped");
        }
        
    }
    IEnumerator FireWeapons ()
    {
        
        while (isFiring)
        {
            //Если пушек нет, выйти
            
            //Определяет следующую пушку для выстрела
            var firePointToUse = firePoints[firePointIndex];

            Instantiate(shotsPrefans, firePointToUse.position, firePointToUse.rotation);

            // Если пушка имеет компонент источника звука, воспроизвести звуковой эффект
            var audio = firePointToUse.GetComponent<AudioSource>();
            if (audio)
            {
                audio.Play();
            }

            firePointIndex += 1;

            if (firePointIndex >= firePoints.Length)
            {
                firePointIndex = 0;
            }

            // Ждать fireRate секунд перед
            // следующим выстрелом
            yield return new WaitForSeconds(fireRate);
        }
    }
}
