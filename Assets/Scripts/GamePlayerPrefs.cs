using UnityEngine;
using System.Collections;

public class GamePlayerPrefs {

    /// <summary>
    /// Sets a bool in PlayerPrefs.
    /// </summary>
    /// <param name="name">The PlayerPrefs key of the value.</param>
    /// <param name="value">The boolean value to set. It is translated to an Int (1 or 0).</param>
    private static void SetBool(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value ? 1 : 0);
    }

    /// <summary>
    /// Gets a bool value stored in PlayerPrefs.
    /// </summary>
    /// <param name="name">The PlayerPrefs key of the value.</param>
    /// <returns>The stored PlayerPrefs value for the specified key. If no value is stored, it returns false.</returns>
    private static bool GetBool(string name)
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    /// <summary>
    /// Gets a bool value stored in PlayerPrefs, with a default value in case the value is not stored.
    /// </summary>
    /// <param name="name">The PlayerPrefs key of the value.</param>
    /// <param name="defaultValue">Value to return if the value is not present in PlayerPrefs.</param>
    /// <returns>The stored PlayerPrefs value for the specified key. If no value is stored, it returns the specified defaultValue.</returns>
    private static bool GetBool(string name, bool defaultValue)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return GetBool(name);
        }
        return defaultValue;
    }

    /// <summary>
    /// A PlayerPrefs value indicating if the mouse control is enabled by default.
    /// </summary>
    static public bool IsMouseControlEnabled
    {
        get
        {
            return GetBool("IsMouseControlEnabled", false);
        }
        set
        {
            SetBool("IsMouseControlEnabled", value);
        }
    }
}
