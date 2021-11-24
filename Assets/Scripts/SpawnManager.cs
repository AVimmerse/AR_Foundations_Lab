using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RayCastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    [SerializeField]
    GameObject spawnablePrefab;
    public GameObject spawnedObject;
    void Start()
    {
        spawnedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        return;

        if (m_RayCastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            SpawnPrefab(m_Hits[0].pose.position);
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
        {
            spawnedObject.transform.position = m_Hits[0].pose.position;
        }
        if(Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            spawnedObject = null;
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
    }
}
