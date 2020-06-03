using UnityEngine;

public class RefrenceManager : MonoBehaviour
{
    #region Fields

    //Singleton
    public static RefrenceManager getSingleton = null;

    //Snake Movement controller Refrence
    public SnakeMovementController snakeMovementControllerRefrence = null;

    //Ui Manager Refrence
    public UiManager uiManagerRefrence = null;

    //Asset Manager Refrence
    public AssetManager assetManagerRefrence = null;

    #endregion

    #region Constructor

    private void Awake()
    {
        if(getSingleton == null)
        {
            getSingleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
