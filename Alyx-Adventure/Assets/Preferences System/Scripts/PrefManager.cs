using AlyxAdventure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefManager : SingletonMonoBehaviour<PrefManager>
{
    public enum PreferenceKey
    {
        TotalFragments,
        FragmentFromTime,
        TotalCollectables,
        TotalSeconds,
        Highscore
    }

    public void UpdateBoolpref(PreferenceKey Pref, bool value)
    {
        PlayerPrefs.SetInt(Pref.ToString(), (value ? 1 : 0));
    }
    public bool GetBoolPref(PreferenceKey Pref)
    {
        if (PlayerPrefs.HasKey(Pref.ToString()))
            if (PlayerPrefs.GetInt(Pref.ToString()) == 1)
                return true;
        return false;
    }


    public void UpdateIntPref(PreferenceKey Pref, int value)
    {
        PlayerPrefs.SetInt(Pref.ToString(), value);
    }
    public int GetIntPref(PreferenceKey Pref, int Default)
    {
        return PlayerPrefs.GetInt(Pref.ToString(), Default);
    }


    public void UpdateFloatPref(PreferenceKey Pref, float value)
    {
        PlayerPrefs.SetFloat(Pref.ToString(), value);
    }
    public float GetFloatPref(PreferenceKey Pref, float Default)
    {
        return PlayerPrefs.GetFloat(Pref.ToString(), Default);
    }


    public void UpdateStringPref(PreferenceKey pref, string value)
    {
        PlayerPrefs.SetString(pref.ToString(), value);
    }
    public string GetStringPref(PreferenceKey Pref)
    {
        return PlayerPrefs.GetString(Pref.ToString());
    }


    //------------------------Preference methods for storing and retrieving objects----------------------------------------
    public void UpdateCustomPref(PreferenceKey key, object obj)
    {
        string json = JsonUtility.ToJson(obj);
        Debug.Log(json);
        PlayerPrefs.SetString(key.ToString(), json);
        Debug.Log("Custom Preference Updated");
    }

    public object GetCustomPref(PreferenceKey key, Type t)
    {
        string json = PlayerPrefs.GetString(key.ToString());
        return JsonUtility.FromJson(json, t);
    }

    //-------------------------------------Delete Preferences----------------------------------------
    public void ClearAllPrefrences()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ClearKeys(params PreferenceKey[] keys)
    {
        foreach (PreferenceKey key in keys)
            PlayerPrefs.DeleteKey(key.ToString());
    }

}