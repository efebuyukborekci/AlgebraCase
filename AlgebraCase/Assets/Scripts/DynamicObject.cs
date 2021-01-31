using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObject : MonoBehaviour, IClickable
{
    public PlaceObjects.Directions direction;
    private Animation _anim;

    private void Awake()
    {
        _anim = transform.GetChild(0).GetComponent<Animation>();
    }
    private void Start()
    {
        //Register to input clicks.
        InputManager.instance.AddClickable(this);
    }
    private void OnDestroy()
    {
        //Unregister on destroy
        InputManager.instance.RemoveClickable(this);
    }

    public void Clicked()
    {
        if (!_anim.isPlaying)
        {
            _anim.Play();
        }
        else
        {
            //This is for rewind at stopping .If it doesnt make this way, animation stuck stopped frame.
            _anim.Rewind();
            _anim.Sample();
            _anim.Stop();
        }
    }

    public void Notify()
    {
        Clicked();
    }
}
