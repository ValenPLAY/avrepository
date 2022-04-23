using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject _portal = null;

    private BoxCollider _boxCollider = null;

    private BoxCollider _portalTrigger
    {
        get => _boxCollider = _boxCollider ?? GetComponent<BoxCollider>();
    }
    private void Start()
    {
        GameManager.Instance.WinGame += ActivatePortal;

        _portalTrigger.enabled = false;
    }

    private void OnDestroy()
    {
        GameManager.Instance.WinGame -= ActivatePortal;
    }

    private void ActivatePortal()
    {
        _portal.SetActive(true);
        _portalTrigger.enabled = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Win");
            
            GameManager.Instance.StartLoading();
        }
    }
}
