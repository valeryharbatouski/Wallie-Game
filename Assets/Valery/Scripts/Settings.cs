
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _screen;


    public void OpenScreen()
    {
        _screen.SetActive(true);
    }

    public void CloseScreen()
    {
        _screen.SetActive(false);
    }
}
