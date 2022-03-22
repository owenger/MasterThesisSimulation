using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Randomizers.Utilities;

[Serializable]
[AddRandomizerMenu("Perception/Object Placement Randomizer")]
public class ObjectPlacementRandomizer : Randomizer
{
    public int numberOfPeople;
    public Vector3Parameter placementLocation;
    public Vector3Parameter placementRotation;
    public GameObjectParameter prefabs;
    
    GameObject m_Container;
    GameObjectOneWayCache m_GameObjectOneWayCache;


    protected override void OnAwake()
    {
        m_Container = new GameObject("Foreground Objects");
        m_Container.transform.parent = scenario.transform;
        m_GameObjectOneWayCache = new GameObjectOneWayCache(
            m_Container.transform, prefabs.categories.Select(element => element.Item1).ToArray());
    }
    protected override void OnIterationStart()
    {
        for(int i=0; i<numberOfPeople; i++)
        {
            var currentInstance = m_GameObjectOneWayCache.GetOrInstantiate(prefabs.Sample());
            currentInstance.transform.position = placementLocation.Sample();
            currentInstance.transform.rotation = Quaternion.Euler(placementRotation.Sample());
            currentInstance.transform.localScale = Vector3.one;
        }   
    }
    protected override void OnIterationEnd()
    {
        //GameObject.Destroy(currentInstance);
        m_GameObjectOneWayCache.ResetAllObjects();
    }
}