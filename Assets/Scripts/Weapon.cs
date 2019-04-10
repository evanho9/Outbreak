using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class Weapon : MonoBehaviour
{
  //--------------------------------
  // 1 - Designer variables
  //--------------------------------

  /// <summary>
  /// Projectile prefab for shooting
  /// </summary>
  public Transform shotPrefab;

  /// <summary>
  /// Cooldown in seconds between two shots
  /// </summary>
  public float shootingRate = 0.25f;
  public int ammo;
  //--------------------------------
  // 2 - Cooldown
  //--------------------------------

  private float shootCooldown;
  private AudioSource source;

  void Awake() 
  {
    ammo = 1;
    source = GetComponent<AudioSource>();
  }
  
  void Start()
  {
    shootCooldown = 0f;
  }

  void Update()
  {
    if (shootCooldown > 0)
    {
      shootCooldown -= Time.deltaTime;
    }
  }
  
  public void AddAmmo(int amount)
  {
    ammo += amount;
    GameControl.instance.UpdateAmmoCounter();
  }

  //--------------------------------
  // 3 - Shooting from another script
  //--------------------------------

  /// <summary>
  /// Create a new projectile if possible
  /// </summary>
  public void Attack(bool isEnemy)
  {
    if (CanAttack && ammo >= 1)
    {
      ammo--;
      source.Play();
      shootCooldown = shootingRate;

      // Create a new shot
      var shotTransform = Instantiate(shotPrefab) as Transform;

      // Assign position
      var position = transform.position;
      position.x += 1;
      shotTransform.position = position;

      // The is enemy property
      Shot shot = shotTransform.gameObject.GetComponent<Shot>();
      if (shot != null)
      {
        shot.isEnemyShot = isEnemy;
      }
      GameControl.instance.UpdateAmmoCounter();
    }
  }

  /// <summary>
  /// Is the weapon ready to create a new projectile?
  /// </summary>
  public bool CanAttack
  {
    get
    {
      return shootCooldown <= 0f;
    }
  }
}
