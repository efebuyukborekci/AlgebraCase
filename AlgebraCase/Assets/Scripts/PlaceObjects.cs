using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public Camera cam;
    public float depthOffset;
    public float planarOffset;

    public float cameraRatio;
    private Transform[] _childTransforms;

    public enum Directions
    {
        NORTH_WEST,
        NORTH_EAST,
        SOUTH_WEST,
        SOUTH_EAST
    }

    public readonly Vector2 _northWest = Vector2.up;
    public readonly Vector2 _northEast = Vector2.up + Vector2.right;
    public readonly Vector2 _southWest = Vector2.zero;
    public readonly Vector2 _southEast = Vector2.zero + Vector2.right;
    
    private void Start()
    {
        cameraRatio = cam.aspect;

        _childTransforms = CollectChildTransforms(transform);
        SetChildPositions();
    }

    private Transform[] CollectChildTransforms(Transform parent)
    {
        Transform[] transforms = new Transform[parent.childCount];
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i] = parent.GetChild(i);
        }
        return transforms;
    }

    private void SetChildPositions()
    {
        for (int i = 0; i < _childTransforms.Length; i++)
        {
            if (_childTransforms[i].GetComponent<DynamicObject>() == null)
                continue;

            _childTransforms[i].position = GetPositionFromDirections(_childTransforms[i].GetComponent<DynamicObject>().direction);
        }
    }

    private Vector3 GetPositionFromDirections(Directions direction)
    {
        switch (direction)
        {
            case Directions.NORTH_WEST:
                return cam.ViewportToWorldPoint(new Vector3(_northWest.x + planarOffset / cameraRatio, _northWest.y - planarOffset, depthOffset));
            case Directions.NORTH_EAST:
                return cam.ViewportToWorldPoint(new Vector3(_northEast.x - planarOffset / cameraRatio, _northEast.y - planarOffset, depthOffset));
            case Directions.SOUTH_WEST:
                return cam.ViewportToWorldPoint(new Vector3(_southWest.x + planarOffset / cameraRatio, _southWest.y + planarOffset, depthOffset));
            case Directions.SOUTH_EAST:
                return cam.ViewportToWorldPoint(new Vector3(_southEast.x - planarOffset / cameraRatio, _southEast.y + planarOffset, depthOffset));
            default:
                return Vector3.zero;
        }
    }
}
