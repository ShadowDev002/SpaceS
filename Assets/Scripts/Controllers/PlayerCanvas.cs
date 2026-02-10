using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private Image _Skill1Fill;
    [SerializeField] private Animator _skill1Animator;

    private float _cooldownTimeSkill_1;
    private float _timer;
    private bool _isOnColldown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if(_Skill1Fill == null) _Skill1Fill = GetComponentInChildren<Image>();
        if(_skill1Animator == null) _skill1Animator = GetComponentInChildren<Animator>();
        _Skill1Fill.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOnColldown)
        {
            _timer -= Time.deltaTime;

            _Skill1Fill.fillAmount = _timer / _cooldownTimeSkill_1;

            if (_timer <= 0)
            {
                EndCooldown();
            }
        }        
    }
    public void StartCooldown(float time)
    {
        _skill1Animator.SetTrigger("Skill1P");
        _cooldownTimeSkill_1 = time;
        _timer = time;
        _isOnColldown = true;
        _Skill1Fill.fillAmount = 1f;
    }
    public void EndCooldown()
    {
        _isOnColldown = false;
        _Skill1Fill.fillAmount = 0;
        _skill1Animator.SetTrigger("Skill1R");
    }
}
