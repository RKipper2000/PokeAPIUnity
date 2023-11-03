using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PokeItemHandler : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image spriteImage;
    public string pokeUrl;
    public string ImageUrl;

    GameObject manager;
    public GameObject pokeView;

    public void SetPokemonData(string name)
    {
        nameText.text = name;
        //spriteImage.sprite = sprite;// Assuming you have a Sprite object for the image
    }
    public void setPokeURL(string pokemonUrl)
    {
        pokeUrl = pokemonUrl;
        string[] partes = pokemonUrl.Split('/');
        string idPokemon = partes[partes.Length - 2];
        SetPokemonImage("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + idPokemon + ".png");
    }

    public void SetPokemonImage(string imageUrl)
    {
        StartCoroutine(LoadImage(imageUrl));
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
                spriteImage.sprite = sprite;
            }
        }
    }

    public void Click()
    {
        manager = GameObject.Find("Manager");
        manager.GetComponent<FillPokemonData>().LoadPokeData(pokeUrl);

        manager.GetComponent<ChangeWindows>().ChangeFromListToView();
    }
}
