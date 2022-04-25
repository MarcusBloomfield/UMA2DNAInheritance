using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace marcus
{
    public static class ListTools
    {
        public static void RemoveNullItems<T>(List<T> gameObjects)
        {
            foreach (var item in gameObjects)
            {
                if (item == null)
                {
                    gameObjects.Remove(item);
                    break;
                }
            }
        }
        public static GameObject FindClosestObject(List<GameObject> gameObjects, GameObject gameObject)
        {
            float closestFoodDistance = float.MaxValue;
            GameObject ClosestFoodObject = null;
            if (gameObjects.Count > 0)
            {
                foreach (GameObject item in gameObjects)
                {
                    if (item != null)
                    {
                        float tempDistance = Vector3.Distance(gameObject.transform.position, item.transform.position);
                        if (closestFoodDistance > tempDistance)
                        {
                            closestFoodDistance = tempDistance;
                            ClosestFoodObject = item;
                        }
                    }
                }
            }
            return ClosestFoodObject;
        }
        public static void DestoryListofGameObjects(List<GameObject> ListofGameObjects)
        {
            if (ListofGameObjects != null)
            {
                if (ListofGameObjects.Count > 0)
                {
                    foreach (GameObject item in ListofGameObjects) Object.Destroy(item);
                    ListofGameObjects.Clear();
                }
            }
        }
        public static void AddOneToList<T>(List<T> AddingTo, T thingToAdd)
        {
            if (thingToAdd != null)
            {
                if (!AddingTo.Contains(thingToAdd)) AddingTo.Add(thingToAdd);
            }
        }
        public static GameObject FindInListByName(string Name, List<GameObject> list)
        {
            GameObject temp = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].name == Name)
                {
                    temp = list[i];
                    break;
                }
            }
            return temp;
        }
        public static void DeActivateAllInList(List<GameObject> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].SetActive(false);
            }
        }
        public static void ActivateAllInList(List<GameObject> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].SetActive(true);
            }
        }
        public static bool AllIsActiveInList(List<GameObject> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].activeInHierarchy == false)
                {
                    return false;
                }
            }
            return true;
        }
        static int SortByName(GameObject name1, GameObject name2)
        {
            return name1.name.CompareTo(name2.name);
        }
        public static List<GameObject> SortByDistance(GameObject origin, List<GameObject> listOfGameObjects)
        {
            return listOfGameObjects.OrderBy(x => Vector3.Distance(origin.transform.position, x.transform.position)).ToList();
        }
    }
}
