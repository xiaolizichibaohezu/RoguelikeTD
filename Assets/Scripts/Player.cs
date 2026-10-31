using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Character
{
    [SerializeField]private Tower nowTower = null;
    protected override void Die()
    {
        Debug.Log("Die ");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            Tower tower = collision.GetComponent<Tower>();
            if (tower != null)
            {
                nowTower = tower;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            Tower tower = collision.GetComponent<Tower>();
            if (tower != null && tower == nowTower)
            {
                nowTower = null;
            }
        }
    }

    private void FaceToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void GetTowerUp()
    {
        if (nowTower != null && !nowTower.getTowerUp)
        {
            nowTower.PickedUp(this);
        }
        else { Debug.LogWarning("No tower to pick up or already picked up."); }
    }

    public void SetTowerDown()
    {
        if (nowTower != null && nowTower.getTowerUp)
        {
            nowTower.PutDown();
        }
        else { Debug.LogWarning("No tower to put down."); }
    }

    private void Awake()
    {
        ResetCharacter();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FaceToMouse();
    }
}