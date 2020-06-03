using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    #region Fields

    public TextMeshProUGUI fpsText = null;

    #endregion

    #region Constructor

    private void Awake()
    {
        RefrenceManager.getSingleton.uiManagerRefrence = this;
        DontDestroyOnLoad(this);
    }

    #endregion
}
