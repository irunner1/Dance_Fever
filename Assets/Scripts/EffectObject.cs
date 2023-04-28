using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    //! Время жизни объекта стрелки
    public float lifetime = 1f;
    
    void Start()
    {
        
    }
    ///
    /// @brief Метод удаления объекта с течением времени
    ///
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
