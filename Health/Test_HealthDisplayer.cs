using TMPro;
using UnityEngine;

[RequireComponent( typeof(HealthComponent))]
public class Test_HealthDisplayer : MonoBehaviour
{
    TextMeshProUGUI _healthText;
    HealthComponent _health;

    private void Awake()
    {
        _healthText = GetComponentInChildren<TextMeshProUGUI>();
        if (_healthText == null)
        {
            _healthText = GetComponent<TextMeshProUGUI>();
            if(_healthText == null)
                Debug.LogError($"Can't find TextMeshProUGUI component.");
        }
        
        
        _health = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        _health.OnHealthChanged += HandleHealthChanged;
        _healthText.text = $"Health : {_health.Health}";
    }

    private void HandleHealthChanged(int health)
    {
        _healthText.text = $"Health : {health}";
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= HandleHealthChanged;
    }
}
