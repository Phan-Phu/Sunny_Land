using UnityEngine;

public class Player : MonoBehaviour
{
    SavePoint savePoint;
    GameSession gameSession;

    float myGravityScaleAtStart;
    float speedRunStart;
    bool isAlive = true;
    bool isClimb = false;

    // Start is called before the first frame update
    void Start()
    {

        if (!savePoint) { return; }
        if (!gameSession) { return; }

        if (gameSession.IsSavePos)
        {
            transform.position = savePoint.transform.position;
        }
    }
}