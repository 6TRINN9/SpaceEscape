using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : DamageTaking
{
    private void OnDestroy()
    {
        GameManager.instance.AddScore(300);
    }
    

}
