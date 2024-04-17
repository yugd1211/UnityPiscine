using UnityEngine;

public class Turret : MonoBehaviour
{
    public float spawnInterval;
    public GameObject bulletPrefab;
    public float endDistance;
    public float bulletSpeed;

    private float _interval; 

    private void FixedUpdate()
    {
        _interval += Time.deltaTime;
        if (_interval >= spawnInterval)
        {
            GameObject go = Instantiate(bulletPrefab, transform.position + Vector3.up, transform.rotation);
            Bullet bullet = go.GetComponent<Bullet>();
            if (bulletSpeed == 0)
                bullet.Init(endDistance);
            else
                bullet.Init(endDistance, bulletSpeed);
            _interval = 0;
        }
    }

}
