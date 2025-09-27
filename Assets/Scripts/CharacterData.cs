using UnityEngine;

[CreateAssetMenu(menuName = "Game/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterId;
    public string displayName;
    public RuntimeAnimatorController animatorController;
    public GameObject bulletPrefab;
    public GameObject bulletSidePrefab;
    public float fireRate = 0.4f;
    public float normalSpeed = 6.5f;
    public float lowSpeed = 3.5f;
    public int maxHP = 3;
}