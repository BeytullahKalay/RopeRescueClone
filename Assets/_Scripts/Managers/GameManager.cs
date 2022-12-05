using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public List<Vector3> MovePath = new List<Vector3>();


    [SerializeField] private float robberFollowPathSpeed = 5f;
    [SerializeField] private float robberFlyForce = 3f;
    public float RobberFollowPathSpeed => robberFollowPathSpeed;
    public float RobberFlyForce => robberFlyForce;
}