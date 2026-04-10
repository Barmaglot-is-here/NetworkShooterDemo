using Assets.Game.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDebugUI : MonoBehaviour
{
    [SerializeField]
    private Slider _healthBar;
    [SerializeField]
    private HealthComponent _healthComponent;

    private void Start()
    {
        _healthComponent.OnChanged += OnHealthChanged;

        OnHealthChanged(0, _healthComponent.Value);
    }

    private void OnHealthChanged(int previousValue, int newValue) 
        => _healthBar.value = newValue;
}
