using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    public GameObject hand, pistolPrefab, minigunPrefab, shotgunPrefab, rocketLauncherPrefab;
    public string initialWeapon;
    GameObject pistol, minigun, shotgun, rocketLauncher;
    Weapon weapon;
    void Start()
    {
        pistol = Instantiate(pistolPrefab, hand.transform.position + pistolPrefab.transform.position, Quaternion.identity, hand.transform);
        pistol.SetActive(false);
        minigun = Instantiate(minigunPrefab, hand.transform.position + minigunPrefab.transform.position, Quaternion.identity, hand.transform);
        minigun.SetActive(false);
        shotgun = Instantiate(shotgunPrefab, hand.transform.position + shotgunPrefab.transform.position, Quaternion.identity, hand.transform);
        shotgun.SetActive(false);
        rocketLauncher = Instantiate(rocketLauncherPrefab, hand.transform.position + rocketLauncherPrefab.transform.position, Quaternion.identity, hand.transform);
        rocketLauncher.SetActive(false);
        Switch(initialWeapon);
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.Shoot();
        }
    }
    void Switch(string w)
    {
        switch (w)
        {
            case "Pistol":
                if (weapon != null) weapon.gameObject.SetActive(false);
                weapon = pistol.GetComponent<Pistol>();
                pistol.SetActive(true);
                break;
            case "Minigun":
                if (weapon != null) weapon.gameObject.SetActive(false);
                weapon = minigun.GetComponent<Minigun>();
                minigun.SetActive(true);
                break;
            case "Shotgun":
                if (weapon != null) weapon.gameObject.SetActive(false);
                weapon = shotgun.GetComponent<Shotgun>();
                shotgun.SetActive(true);
                break;
            case "Rocket Launcher":
                if (weapon != null) weapon.gameObject.SetActive(false);
                weapon = rocketLauncher.GetComponent<RocketLauncher>();
                rocketLauncher.SetActive(true);
                break;
            default:
                break;
        }
    }
}
