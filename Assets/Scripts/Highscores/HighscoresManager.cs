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
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HighscoresManager : MonoBehaviour {

    /// <summary>
    /// The filename for the highscores.
    /// </summary>
    private const string Filename = "highscores.bin";

    /// <summary>
    /// The highscores table.
    /// </summary>
    public HighscoresTable table;

    /// <summary>
    /// Adds a highscore and saves.
    /// </summary>
    /// <param name="name">The name of the entry.</param>
    /// <param name="score">The score of the entry.</param>
    public void AddHighscore(string name, int score)
    {
        table.entries.Add(new HighscoresEntry(name, score));
        table.entries.Sort(delegate (HighscoresEntry entry1, HighscoresEntry entry2) {
            return entry2.score.CompareTo(entry1.score);
        });
        table.entries = table.entries.GetRange(0, Mathf.Min(table.entries.Count, 10));
        Save();
    }

    /// <summary>
    /// Loads the highscores table
    /// </summary>
    public void Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(HighscoresManager.Filename))
        {
            FileStream highscoresFile = File.Open(HighscoresManager.Filename, FileMode.Open);
            table = (HighscoresTable)formatter.Deserialize(highscoresFile);
        }
        else
        {
            table.entries = new List<HighscoresEntry>();
            Save();
        }
    }

    /// <summary>
    /// Saves the highscores table
    /// </summary>
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream highscoresFile = File.Create(HighscoresManager.Filename);
        formatter.Serialize(highscoresFile, table);
        highscoresFile.Close();
    }

    void Start()
    {
        Load();
    }

}
