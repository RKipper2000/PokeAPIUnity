using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Originals

[Serializable]
public class pokemonList
{
    public int count;
    public string next;
    public string previous;
    public List<pokeObj> results = new List<pokeObj>();
}

[Serializable]
public class pokeObj
{
    public string name;
    public string url;
}

// For the individual pokemon entry

[Serializable]
public class pokeData
{
    public string id;
    public string name;
    public List<TypeData> types = new List<TypeData>();
}

// for the types in the pokemon entry

[Serializable]
public class TypeData
{
    public int slot;
    public TypeInfo type;
}

[Serializable]
public class TypeInfo
{
    public string name;
    public string url;
}

//[Serializable]
//public class TypeDataArray
//{
//    public List<TypeData> types;
//}