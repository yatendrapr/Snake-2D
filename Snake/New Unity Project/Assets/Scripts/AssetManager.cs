using UnityEngine;

public class AssetManager : MonoBehaviour
{
    #region Fields

    //Game Objects
    public Sprite snakeFood = null;
    public Sprite snakeBody = null;

    #endregion

    #region Constructor

    private void Awake()
    {
        RefrenceManager.getSingleton.assetManagerRefrence = this;
        DontDestroyOnLoad(this);
    }

    #endregion
}
