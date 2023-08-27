
using UnityEngine;

public class Asteroid : MonoBehaviour
{    
    [SerializeField] private float speed = 10.0f;
    
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        IndicatorManager.instance.AddIndicator(gameObject, Color.red, null, true);
    }
}
