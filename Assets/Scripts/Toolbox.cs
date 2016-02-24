using UnityEngine;
using System.Collections;

public class Toolbox : MonoBehaviour {

    public static GameControl GameControl { get; private set; }
    public static GameObjectPoolManager PoolManager { get; private set; }
    public static GameMenu GameMenu { get; private set; }
    public static EnemySpawnManager EnemySpawnManager { get; private set; }
    public static HighscoresManager HighscoresManager { get; private set; }

	void Start () {
        Toolbox.GameControl = FindObjectOfType<GameControl>();
        Toolbox.PoolManager = FindObjectOfType<GameObjectPoolManager>();
        Toolbox.GameMenu = FindObjectOfType<GameMenu>();
        Toolbox.EnemySpawnManager = FindObjectOfType<EnemySpawnManager>();
        Toolbox.HighscoresManager = FindObjectOfType<HighscoresManager>();
	}

}
