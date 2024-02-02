using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour
{
    public Item item;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject effectObj;

    private void OnEnable()
    {
        if(obj != null) { Destroy(obj); }
        if(effectObj != null) { Destroy(effectObj); }

        obj = Instantiate(item.gameObject, this.transform);
        if (obj.GetComponent<WeaponItem>() != null)
            obj.transform.localPosition = new Vector3(0f, -0.15f, -0.2f);
        else
            obj.transform.localPosition = new Vector3(0f, -0.15f, 0f);
        obj.transform.localRotation = Quaternion.Euler(new Vector3(-40f, 0f, 0f));

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit , 100, LayerMask.GetMask("Ground"));
        effectObj = Instantiate(effect, this.transform);
        effectObj.transform.position = hit.point;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerData>() != null)
        {
            Inventory.Instance.itemList.Add(item);

            Destroy(gameObject);
        }
    }
}
