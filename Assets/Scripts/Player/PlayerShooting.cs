using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject hand;
    [SerializeField] GameObject pistolPrefab;
    [SerializeField] GameObject minigunPrefab;
    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject rocketLauncherPrefab;
    [SerializeField] string initialWeapon;
    [HideInInspector] public bool canOperate;
    GameObject pistol;
    GameObject minigun;
    GameObject shotgun;
    GameObject rocketLauncher;
    Weapon weapon;

    public int CurrentShotgunAmmo { get; private set; }
    public int CurrentRockerlauncherAmmo { get; private set; }
    public int CurrentMinigunAmmo { get; private set; }

    private void InitializeWeapons()
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

    public void Initialize(int currentShotgunAmmo,int currentRockerlauncherAmmo,int currentMinigunAmmo){
        CurrentShotgunAmmo = currentShotgunAmmo;
        CurrentRockerlauncherAmmo = currentRockerlauncherAmmo;
        CurrentMinigunAmmo = currentMinigunAmmo;
        InitializeWeapons();
    }

    void Update()
    {
        if (!canOperate)
            return;
        if (Input.GetButtonDown("Fire1")) weapon.Shoot();
        if (Input.GetKeyDown(KeyCode.Alpha1)) Switch("Pistol");
        if (Input.GetKeyDown(KeyCode.Alpha2)) Switch("Shotgun");
        if (Input.GetKeyDown(KeyCode.Alpha3)) Switch("Minigun");
        if (Input.GetKeyDown(KeyCode.Alpha4)) Switch("Rocket Launcher");

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
