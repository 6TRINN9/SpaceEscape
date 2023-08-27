using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        IndicatorManager.instance.AddIndicator(gameObject, Color.green);
    }

}
