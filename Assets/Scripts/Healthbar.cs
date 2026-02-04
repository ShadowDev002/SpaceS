using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _hpFill;
    
    public void UpdateHealthbar(int currentHP, int maxHP)
    {
        if (_hpFill != null)
        {
            _hpFill.fillAmount = (float)currentHP / maxHP;
        }
    }
}
