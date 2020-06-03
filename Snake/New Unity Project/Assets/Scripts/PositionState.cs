using UnityEngine;

public class PositionState 
{

    #region Fields

    private Vector3 position;
    private bool occupied = false;

    public bool occupiedStatus
    {
        get
        {
            return this.occupied;
        }

        set
        {
            this.occupied = value;
        }
    }

    #endregion

    #region Constructor

    public PositionState(Vector3 _position)
    {
        this.position = _position;
    }

    #endregion

    #region Methods

    public Vector3 Return_Position()
    {
        return this.position;
    }

    #endregion

}
