using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public Camera cam;
    private List<IClickable> _clickable;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        _clickable = new List<IClickable>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickRay();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ClickRay()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            IClickable currentClickable = hit.collider.transform.parent.GetComponent<IClickable>();
            Click(currentClickable);
        }
    }

    public void AddClickable(IClickable clickable)
    {
        _clickable.Add(clickable);
    }
    public void RemoveClickable(IClickable clickable)
    {
        _clickable.Remove(clickable);
    }

    private void Click(IClickable currentClickable)
    {
        for (int i = 0; i < _clickable.Count; i++)
        {
            //It can be send to all registered or just matched.
            if (_clickable[i] == currentClickable)
            {
                _clickable[i].Notify();
            }
        }
    }
}
