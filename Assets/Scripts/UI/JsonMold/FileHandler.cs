using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public static class FileHandler
{
    //save to json from a list
    public static void SaveToJSon<T>(List<T> toSave, string filName) //<T> means generic, A.K.A it can be any type of paramater
    {
        Debug.Log(GetPath(filName));
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(GetPath(filName), content);
    }

    //save to json from a Array
    public static void SaveToJSon<T>(T[] toSave, string filName) //<T> means generic, A.K.A it can be any type of paramater
    {
        Debug.Log(GetPath(filName));
        string content = JsonHelper.ToJson<T>(toSave);
        WriteFile(GetPath(filName), content);
    }

    //save a singular object
    public static void SaveToJSon<T>(T toSave, string filName) //<T> means generic, A.K.A it can be any type of paramater
    {
        Debug.Log(GetPath(filName));
        string content = JsonUtility.ToJson(toSave);
        WriteFile(GetPath(filName), content);
    }

    //read list from json
    public static List<T> ReadListFromJSon<T>(string fileName) 
    { 
        string content = ReadFile(GetPath(fileName));
        if (string.IsNullOrEmpty(content)|| content== "{}") 
        {
            return new List<T>();
        }

        List<T> result = JsonHelper.FromJson<T>(content).ToList();
        return result;
    }

    //read object from json
    public static T ReadFromJSon<T>(string fileName) 
    { 
        string content = ReadFile(GetPath(fileName));
        if (string.IsNullOrEmpty(content)|| content== "{}") 
        {
            return default(T); //the default empty value of any generic. if int 0. if bool false, etc.
        }

        T result = JsonUtility.FromJson<T>(content);
        return result;
    }


    //return the full path from the filename
    public static string GetPath(string fileName)
    {
        return Application.persistentDataPath +"/"+ fileName; //return full path to file
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create); //create the file if it doesn't exist, overwrite the file if it does exist
        using(StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content); //write into the file
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path)) //check if a file exists with this path
        {
            using(StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";//return empty if file doesn't exist
    }
}


//Helper class that lets us use an array with JSon utility by putting it under a key instead of being on the top level (aka being unrelateed data rather than one array)
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
