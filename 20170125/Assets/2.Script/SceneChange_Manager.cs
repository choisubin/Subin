using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneChange_Manager : MonoBehaviour {

	public void SceneChange_to_Ingame()
    {
        SceneManager.LoadScene(1);
    }
}
