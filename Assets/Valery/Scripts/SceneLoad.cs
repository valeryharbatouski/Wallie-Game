
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour

{
    [SerializeField] private string _sceneName;
    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }


}
