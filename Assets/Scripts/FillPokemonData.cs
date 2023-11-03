using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class FillPokemonData : MonoBehaviour
{
    public Image pokemonImg;
    public TextMeshProUGUI pokeName;
    public TextMeshProUGUI number;
    public TextMeshProUGUI types;
    public string pokemonURL;
    private string pokeNum;

    [SerializeField] pokeData pokemonData;

    

    public void LoadPokeData(string url)
    {
        pokemonURL = url;
        string[] partes = pokemonURL.Split('/');
        pokeNum = partes[partes.Length - 2];
        StartCoroutine(FetchData(url));
    }

    IEnumerator FetchData(string url)
    {
        WebRequestHandler webRequest = new WebRequestHandler();
        webRequest.method = "GET";
        StartCoroutine(webRequest.ExecuteRequest(pokeNum));
        yield return new WaitForSeconds(0.5f);

        pokemonData = JsonUtility.FromJson<pokeData>(webRequest.result);

        // cambiar los valores de los game objects
        number.text = pokemonData.id;
        pokeName.text = pokemonData.name;
        types.text = SetPokemonTypes(pokemonData.types);

        StartCoroutine(LoadImage("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + pokemonData.id + ".png"));
    }

    IEnumerator LoadImage(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                pokemonImg.sprite = sprite;
            }
        }
    }

    private string SetPokemonTypes(List<TypeData> typesList)
    {
        string ans = "Types:\n";

        foreach (TypeData typeData in typesList)
        {
            string typeName = typeData.type.name;
            ans += typeName + "\n";
        }

        return ans;
    }
}


