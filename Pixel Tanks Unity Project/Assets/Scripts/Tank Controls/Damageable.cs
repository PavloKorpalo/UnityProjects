using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private int _maxHealth;
    private Camera _mainCamera;
    private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _matBlock;

    private int _currentHealth;

    private TankEngine _tankEngine;

    void Start()
    {
        _tankEngine = gameObject.GetComponentInParent<TankEngine>();
        _maxHealth = _tankEngine.GetHealthPoints();
        _mainCamera = Camera.main;
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _matBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        _currentHealth = _tankEngine.GetHealthPoints();
        if(_currentHealth < _maxHealth)
        {
            _meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }
    private void UpdateParams()
    {
        _meshRenderer.GetPropertyBlock(_matBlock);
        _matBlock.SetFloat("_Fill", _currentHealth / (float)_maxHealth);
        _meshRenderer.SetPropertyBlock(_matBlock);
    }
    private void AlignCamera() 
    {
        if (_mainCamera != null) 
        {
            var camXform = _mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }

}
