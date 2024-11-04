using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown; // Time between attacks
    [SerializeField] private Transform firePoint; // Position where projectiles spawn
    [SerializeField] private GameObject[] fireballs; // Array of fireball prefabs

    private Animator anim;
    private PlayerMovement playerMovement; // Reference to the player movement script
    private float cooldownTimer = Mathf.Infinity; // Timer to manage attack cooldown

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Get the Animator component
        playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement component
    }

    private void Update()
    {
        // Check for attack input and cooldown
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
        {
            Attack(); // Call the attack method
        }

        // Increment cooldown timer
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack"); // Trigger attack animation
        cooldownTimer = 0; // Reset the cooldown timer

        int fireballIndex = FindFireball(); // Get an available fireball
        if (fireballIndex >= 0)
        {
            fireballs[fireballIndex].transform.position = firePoint.position; // Set fireball position
            fireballs[fireballIndex].SetActive(true); // Activate the fireball
            fireballs[fireballIndex].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x)); // Set projectile direction
        }
    }

    private int FindFireball()
    {
        // Find an inactive fireball in the array
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i; // Return index of inactive fireball
        }
        return -1; // Return -1 if no fireballs are available
    }
}
