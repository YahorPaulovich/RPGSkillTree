using UnityEngine;

public class UIWiggle : MonoBehaviour
{
    [SerializeField] private GameObject _objectToBob;

    [SerializeField] private float _amplitudeX = 1f; // Set to 0 if no x bob.
    [SerializeField] private float _frequencyX = 1f; // Boobing speed in x.
    [SerializeField] private float _amplitudeY = 1f; // Set to 0 if no y bob.
    [SerializeField] private float _frequencyY = 1f; // Bobbing speed in y.
 
    private Vector3 _refPosition;
 
    private void Start()
    {
        if (_objectToBob == null)
        {
            _objectToBob = this.gameObject;
        }
        _refPosition = _objectToBob.transform.position;
    }
 
    private void Update()
    {
        float dx = _amplitudeX * (Mathf.PerlinNoise(Time.time * _frequencyX, 1f) - 0.5f);
        float dy = _amplitudeY * (Mathf.PerlinNoise(1f, Time.time * _frequencyY) - 0.5f);
        Vector3 position = new Vector3(_refPosition.x , _refPosition.y, _refPosition.z);
        position = position + _objectToBob.transform.up * dy;
        position = position + _objectToBob.transform.right * dx;
        _objectToBob.transform.position = position;
    }
}
