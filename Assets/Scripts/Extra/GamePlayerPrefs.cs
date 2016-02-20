///
/// The MIT License(MIT)
/// 
/// Copyright(c) 2016 Daniel Lupiañez Casares
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
///

using UnityEngine;
using System.Collections;

/// <summary>
/// A wrapper class to manage all the GamePrefs for the game.
/// </summary>
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
