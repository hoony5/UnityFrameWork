using UnityEngine;

[System.Serializable, CreateAssetMenu (menuName = "ScriptableObject/UI/UISFXSettings")]
public class UISFXSettings : ScriptableObject
{
    [field: SerializeField] public AudioClip ClickSFX { get; private set; }
    [field: SerializeField] public AudioClip OpenSFX { get; private set; }
    [field: SerializeField] public AudioClip CloseSFX { get; private set; }
    [field: SerializeField] public AudioClip AlarmSFX { get; private set; }
    [field: SerializeField] public AudioClip ErrorSFX { get; private set; }
    [field: SerializeField] public AudioClip AllowanceSFX { get; private set; }
}