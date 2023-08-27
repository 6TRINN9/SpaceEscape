using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollide : MonoBehaviour 
{
    // Объем повреждений, наносимых объекту.
    [SerializeField] private int damage = 1;
    // Объем повреждений, наносимых себе при попадании
    // в какой-то другой объект.
    [SerializeField] private int damageToSelf = 5;
    
    // Объект вошел в область действия данного триггера?
    private void OnTriggerEnter(Collider collider)
    {
        HitObject(collider.gameObject);
    }
    // Другой объект столкнулся с текущим объектом?
    private void OnCollisionEnter(Collision collision)
    {
        HitObject(collision.gameObject);
    }

    private void HitObject (GameObject theObject)
    {
        // Нанести повреждение объекту, в который попал данный объект,
        // если возможно.
        DamageTaking theirDamage = theObject.GetComponentInParent<DamageTaking>();
        if (theirDamage)
        {
            theirDamage.TakeDamage(damage);
        }
        // Нанести повреждение себе, если возможно
        DamageTaking ourDamage = this.GetComponentInParent<DamageTaking>();
        if (ourDamage)
        {
            ourDamage.TakeDamage(damageToSelf);
        }
    }
}
