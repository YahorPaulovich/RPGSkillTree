using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICustomPointerUpEffect : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pointerUpSoundFX;
    [SerializeField] private AudioClip _pointerUpSoundFXNotAvailable;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (TryGetComponent(out Button button) && button.interactable == false)
        {
            _audioSource.PlayOneShot(_pointerUpSoundFXNotAvailable);
        }
        else
        {
            _audioSource.PlayOneShot(_pointerUpSoundFX);
        }     
    }
}
