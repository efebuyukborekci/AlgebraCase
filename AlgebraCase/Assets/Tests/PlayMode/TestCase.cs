using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class TestCase
    {
        private Camera _camera;
        private InputManager _inputManager;
        private PlaceObjects _placeObjects;
        private DynamicObject _dynamicObject;

        [SetUp]
        public void Setup()
        {
            GameObject newCameraObject = new GameObject();
            GameObject newPlaceObject = new GameObject();
            GameObject newDynamicObject = new GameObject();
            GameObject newAnimatedObject = new GameObject();
            GameObject newInputObject = new GameObject();

            newDynamicObject.transform.parent = newPlaceObject.transform;
            newAnimatedObject.transform.parent = newDynamicObject.transform;


            newAnimatedObject.AddComponent<Animation>();
            _dynamicObject = newDynamicObject.AddComponent<DynamicObject>();
            _dynamicObject.direction = PlaceObjects.Directions.NORTH_EAST;

            _camera = newCameraObject.AddComponent<Camera>();
            _placeObjects = newPlaceObject.AddComponent<PlaceObjects>();
            _placeObjects.cam = _camera;
            
            _inputManager = newInputObject.AddComponent<InputManager>();
            
        }
        
        [UnityTest]
        public IEnumerator ClickableTest()
        {
            _inputManager.AddClickable(_dynamicObject);
            Assert.AreSame(_dynamicObject,_inputManager.Click(_dynamicObject));
            yield return null;
        }

        [UnityTest]
        public IEnumerator PositionTest()
        {
            Vector3 tempPosition = _camera.ViewportToWorldPoint(new Vector3(_placeObjects._northWest.x, _placeObjects._northWest.y, 0));
            Assert.AreEqual(tempPosition,_dynamicObject.transform.position);
            yield return null;
        }
    }
}
