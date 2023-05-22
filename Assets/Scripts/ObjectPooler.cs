using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public List<GameObject> objectsList;
    public int poolSize;

    // dictionary of different object pools
    private Dictionary<string, LinkedList<GameObject>> objectPools;

    // create all the pools and fill them with (poolSize) # of objects
    void Start()
    {
        objectPools = new Dictionary<string, LinkedList<GameObject>>();

        // for each object type, make a linked list of objects
        foreach (GameObject obj in objectsList)
        {
            LinkedList<GameObject> objectList = new LinkedList<GameObject>();

            // create the linked list of objects
            for (int i = 0; i < poolSize; i++)
            {
                // create the object, set inactive, add to end of list
                GameObject pooledObj = Instantiate(obj, transform);
                pooledObj.name = obj.name;
                pooledObj.SetActive(false);
                objectList.AddLast(pooledObj);
            }

            // add the linked list to dictionary
            objectPools.Add(obj.name, objectList);
        }
    }

    // get the requested pool object, create a new one if necessary
    public GameObject GetPooledObject(string objectName)
    {

        // safety check for the string param
        if (!objectPools.ContainsKey(objectName))
        { 
            Debug.LogWarning("ObjectPooler: Object type '" + objectName + "' not found");
            return null;

        }

        // get the pool of this objectName
        LinkedList<GameObject> currentObjectList = objectPools[objectName];

        // if there's nothing in it, create a new object and return it 
        if (currentObjectList.Count == 0)
        {
            // find the object type we're interested in, make a clone, add it to the pool, and return it
            GameObject originalObj = objectsList.Find(x => x.name == objectName);
            GameObject obj = Instantiate(originalObj, transform);
            obj.name = originalObj.name;
            obj.SetActive(false);
            currentObjectList.AddLast(obj);

            return obj;
        }

        // grab the first object in the pool, and remove/return it
        GameObject poolObject = currentObjectList.First.Value;
        currentObjectList.RemoveFirst();
        poolObject.SetActive(true);
        return poolObject;
    }

    // deactivate this object and put it back into the pool
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);

        LinkedList<GameObject> currentObjectList;

        // safety check for this object
        if (!objectPools.TryGetValue(obj.name, out currentObjectList))
        {
            Debug.LogWarning("ObjectPooler: Object type '" + obj.name + "' not found in the pool!");
            return;
        }

        // add it back into the pool
        currentObjectList.AddLast(obj);
    }

}
