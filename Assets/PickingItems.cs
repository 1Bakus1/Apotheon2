using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingItems : MonoBehaviour
{
    public Vector3 Position = Vector3.zero;
    public Vector3 Rotation;
    public Transform m_NewTransform;
    GameObject weaponslot;
    public bool canPickUp;
    Rigidbody rigidbody;
    GameObject player;
    public float throwPower;
    public GameObject sword;
    public bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        weaponslot = GameObject.FindGameObjectWithTag("WeaponSlot");
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        
        canPickUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K) && canPickUp && inRange)
        {
            Picking();
        }
        if (Input.GetButtonDown("Fire2") && weaponslot.transform.GetChild(0).gameObject != null)
        {
            Throwing();
        }
    }

    void Picking()
    {
        canPickUp = false;
        for (int i = weaponslot.transform.childCount - 1; i == 0; i--)
        {
            GameObject.Destroy(weaponslot.transform.GetChild(i).gameObject);
        }
        transform.parent = weaponslot.transform;
        weaponslot.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(0, 0, 180f);
    }

    void Throwing()
    {
        print("throw");
        GameObject clone = null;
        sword = weaponslot.transform.GetChild(0).gameObject;
        if (clone == null)
        {
            clone = Instantiate(sword, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
        }
        for (int i = weaponslot.transform.childCount - 1; i == 0; i--)
        {
            GameObject.Destroy(weaponslot.transform.GetChild(i).gameObject);
        }
        clone.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePosition;
        clone.GetComponent<Rigidbody>().AddForce(-throwPower, 10f, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            inRange = false;
        }
    }
}
