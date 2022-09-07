using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    private bool _alive;
    private float _currentHealth;
    // Start is called before the first frame update
   
    public virtual void  InicialieStatus()
    {
        _alive = true;
        _currentHealth = maxHealth;
    }
    public virtual void TakeDamage(float damage) 
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die() 
    {
        _alive = false;
    }

    //public virtual void CureLife() 
    //{

    //}

    public virtual void Shoot() 
    {

    }

}
