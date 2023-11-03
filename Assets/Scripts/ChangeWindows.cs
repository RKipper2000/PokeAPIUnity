using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWindows : MonoBehaviour
{
    public void ChangeFromListToView()
    {
        GameObject pokeList = GameObject.Find("PokeList");
        GameObject buttons = GameObject.Find("Buttons");
        GameObject pokeView = FindInActiveObjectByName("PokeView");

        pokeList.SetActive(false);
        buttons.SetActive(false);
        pokeView.SetActive(true);
    }

    public void ChangeFromViewToList()
    {
        GameObject pokeView = GameObject.Find("PokeView");
        GameObject pokeList = FindInActiveObjectByName("PokeList");
        GameObject buttons = FindInActiveObjectByName("Buttons");


        pokeList.SetActive(true);
        buttons.SetActive(true);
        pokeView.SetActive(false);
    }

    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
