using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    public GameObject hand,wep,pistolPrefab;
    Weapon weapon;
    void Start(){
        Switch("Pistol");
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.Shoot();
        }
    }
    void Switch(string w){
        if (w=="Pistol") {
            wep = Instantiate(pistolPrefab,hand.transform.position+pistolPrefab.transform.position,Quaternion.identity,hand.transform);
            weapon = wep.GetComponent<Pistol>();
        }
    }
}
