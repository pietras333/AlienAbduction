using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private float health = 100f;

    [SerializeField]
    private float currentHealth;

    [SerializeField]
    private Slider ammoBar;

    [SerializeField]
    private float ammo = 100f;

    [SerializeField]
    private Slider blockBar;

    [SerializeField]
    private float block = 100f;

    [SerializeField]
    private float currentBlock;

    [SerializeField]
    private ParticleSystem blockEffect;

    [SerializeField]
    private float currentAmmo;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private LayerMask rocketLayer;

    [SerializeField]
    private ParticleSystem beamEffect;

    [SerializeField]
    private Beam beam;

    [SerializeField]
    bool isShooting = false;

    private void Start()
    {
        currentHealth = health;
        currentAmmo = ammo;
        healthBar.value = currentHealth / health;
        ammoBar.value = currentAmmo / ammo;
        currentBlock = block;
        blockBar.value = currentBlock / block;
    }

    private void Update()
    {
        // Handle ammo and health UI updates
        currentAmmo = Mathf.Clamp(currentAmmo, 0, ammo);
        ammoBar.value = currentAmmo / ammo;
        healthBar.value = currentHealth / health;
        currentAmmo += 0.05f;
        currentBlock += 0.05f;
        blockBar.value = currentBlock / block;

        if (isShooting && currentAmmo > 0)
        {
            beamEffect.Play();
            beam.isShooting = isShooting && currentAmmo > 0;
            currentAmmo -= 0.1f;
        }
        else
        {
            beam.isShooting = isShooting && currentAmmo > 0;
            beamEffect.Stop();
        }
    }

    public void Shoot(bool isPressed)
    {
        Debug.Log(isPressed);
        if (isPressed)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth / health;
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void Reset()
    {
        currentHealth = health;
        currentAmmo = ammo;
        currentBlock = block;
        healthBar.value = currentHealth / health;
        ammoBar.value = currentAmmo / ammo;
        blockBar.value = currentBlock / block;
    }

    public void Block(bool isPressed)
    {
        if (!isPressed)
        {
            return;
        }
        if (currentBlock - 30 > 0)
        {
            blockEffect.Play();
            currentBlock -= 30;
            blockBar.value = currentBlock / block;
            Collider[] rockets = Physics.OverlapSphere(transform.position, 15f, rocketLayer);
            foreach (Collider rocket in rockets)
            {
                Destroy(rocket.gameObject);
            }
        }
    }
}
