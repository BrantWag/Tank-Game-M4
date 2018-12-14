using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapType : MonoBehaviour
{
    // text to hold seed
    public Text seedSelect;

    // set mapMaker prefab to mapGenerator type selected from UI
    public void selectMapOfDay()
    {
        GameManager.instance.mapMaker.maptype = MapGenerator.mapType.mapOfTheDay;
    }
    // set mapMaker prefab to mapGenerator type selected from UI
    public void selectRandomMap()
    {
        GameManager.instance.mapMaker.maptype = MapGenerator.mapType.randomMap;
    }
    // set mapMaker prefab's seed from UI
    public void setSeed()
    {
        GameManager.instance.mapMaker.seedValue = int.Parse(seedSelect.text.ToString());
    }
    // set mapMaker prefab to mapGenerator type selected from UI
    public void selectSeededMap()
    {
        GameManager.instance.mapMaker.maptype = MapGenerator.mapType.seededMap;
    }
}
