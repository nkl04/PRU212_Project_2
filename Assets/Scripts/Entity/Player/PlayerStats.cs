using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Exp { get => exp; set => exp = value; }
    [SerializeField] private float exp;

}
