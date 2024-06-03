using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    GameObject[] weaponInventory;  // 값을 변경 후, 게임 매니져의 같은 값도 변경해야 함(함수 사용).
    int playerDamage; // 값을 변경 후, 게임 매니져의 같은 값도 변경해야 함(함수 사용).

    Rigidbody2D rigid;
    Collider2D col;


    [SerializeField] private Sprite defaultWeaponImage; // 기본 무기 이미지
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        weaponInventory = GameManager.Instance.GetWeaponInventory();
        playerDamage = GameManager.Instance.GetPlayerDamage();

        // 초기 설정 시 기본 무기 이미지를 설정
        GameObject inventoryImage = GameObject.Find("ui").transform.Find("inventory").Find("WeaponImage").gameObject;
        inventoryImage.GetComponent<Image>().sprite = defaultWeaponImage;
        inventoryImage.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon") && Input.GetButtonDown("Interaction"))
        {
            GameManager.Instance.SetWeaponInventory(collision);

            GameObject inventoryImage = GameObject.Find("ui").transform.Find("inventory").Find("WeaponImage").gameObject;
            inventoryImage.GetComponent<Image>().sprite = collision.transform.GetComponent<SpriteRenderer>().sprite;
            inventoryImage.SetActive(true);

            int newBulletIndex = collision.GetComponent<weapon>().weaponId;
            GetComponent<PlayerMove>().ChangeBullet(newBulletIndex);
        }
    }
}
