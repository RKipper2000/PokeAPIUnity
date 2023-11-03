using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateMainView : MonoBehaviour
{
    public GameObject mainView;
    public GameObject pokemonPrefab;

    [SerializeField] pokemonList pokemonList;

    private int limit = 16;
    private int offset = 0;

    private void Awake()
    {
        StartCoroutine(getPokemonList());
    }

    IEnumerator getPokemonList()
    {
        WebRequestHandler webRequest = new WebRequestHandler();
        webRequest.method = "GET";
        StartCoroutine(webRequest.ExecuteRequest("?offset=" + offset.ToString() + "&limit=" + limit.ToString())); //StartCoroutine(webRequest.ExecuteRequest(string.Empty));
        yield return new WaitForSeconds(1.0f);
        Debug.Log(webRequest.result);
        pokemonList = JsonUtility.FromJson<pokemonList>(webRequest.result);

        foreach (pokeObj pokemonData in pokemonList.results)
        {
            // Instantiate the prefab
            GameObject pokemonGO = Instantiate(pokemonPrefab, mainView.transform);
            pokemonGO.transform.SetParent(mainView.transform);

            // Assuming you have some script on your prefab to handle this data setting
            PokeItemHandler displayScript = pokemonGO.GetComponent<PokeItemHandler>();

            if (displayScript != null)
            {
                // Set name and image
                displayScript.SetPokemonData(pokemonData.name);
                displayScript.setPokeURL(pokemonData.url);
            }

            // You might need to get the image url from the API and download the image
            // to set it on your PokemonDisplay script.
        }
    }

    private void DestroyExistingPokemon()
    {
        foreach (Transform child in mainView.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void PrevClick()
    {
        if ((offset - limit) >= 0)
        {
            offset -= limit;
            DestroyExistingPokemon();
            StartCoroutine(getPokemonList());
        }
    }

    public void NextClick()
    {
        if ((offset + limit) < 1292)
        {
            offset += limit;
            DestroyExistingPokemon();
            StartCoroutine(getPokemonList());
            //Debug.Log(offset);
        }
    }
}