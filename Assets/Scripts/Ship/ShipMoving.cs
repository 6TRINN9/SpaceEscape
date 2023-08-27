using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMoving : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float acceleration = 10.0f;
    //Скорость поворота корабля
    [SerializeField] private float turmRate = 5.0f;
    //Сила выравнивания корабля
    [SerializeField] private float levelDamping = 0.5f;

    private PlayerInput playerInput;

    

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Acceleration.started += AccelerationShip;
        playerInput.Player.Acceleration.performed += AccelerationShip;
    }

    private void OnEnable ()
    {
        playerInput.Enable();
    }

    private void OnDisable ()
    {
        playerInput.Disable();
    }

    private void FixedUpdate()
    {
        /*Создать новый поворот, умножив вектор напрвления джойстика на turnRate, и ограничить величиной 90% от половины круга
        Получить ввод пользователя*/
        //Vector2 steeringInput = InputManager.instance.steering.delta;
        Vector2 direction = playerInput.Player.Move.ReadValue<Vector2>();
        MoveShip(direction);

    }

    private void MoveShip(Vector2 direction)
    {
        //Перемешает корабль с постоянной скоростью   
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

        Vector3 moveDirection = new Vector3(direction.y, direction.x, 0);

        moveDirection *= turmRate;

        moveDirection.x = Mathf.Clamp(moveDirection.x, -Mathf.PI * 0.9f, Mathf.PI * 0.9f);
        Quaternion newOrientation = Quaternion.Euler(moveDirection);

        transform.rotation *= newOrientation;

        //Минимизация поворота

        Vector3 levelAngles = transform.eulerAngles;
        levelAngles.z = 0.0f;
        Quaternion levelOrientation = Quaternion.Euler(levelAngles);

        /* Объединить текущую ориентацию с небольшой величиной этой ориентации "без вращения" когда это происходит
        на протяжении нескольких кадров, объект медленно  выравнивается над поверхностью*/
        transform.rotation = Quaternion.Slerp(transform.rotation, levelOrientation, levelDamping * Time.deltaTime);

    }

    private void AccelerationShip (InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speed += acceleration;
            Debug.Log("Ship accelerating");
        }
        if (context.performed)
        {
            speed -= acceleration;
        }      
            
    }
}
