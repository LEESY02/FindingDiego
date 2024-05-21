using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] snowballs;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake() {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        if (playerMovement.canAttack() && Input.GetMouseButton(0) && cooldownTimer > attackCooldown) { // left click to attack
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack() {
        anim.SetTrigger("Attack");
        cooldownTimer = 0;
        //pool Snowball
        snowballs[FindSnowball()].transform.position = firePoint.position;
        snowballs[FindSnowball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindSnowball() {
        for (int i = 0; i < snowballs.Length; i++) {
            if (!snowballs[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }    
}
