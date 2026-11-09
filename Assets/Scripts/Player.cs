using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField]
    private float _speed = 3.4f;
    [SerializeField]
    private GameObject _Laserprefab;
    [SerializeField]
    private float _Firerate = 0.5f;
    private float _Canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private Spawnmanager spawnmanager;


    public float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = New position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        spawnmanager=GameObject.Find("Spawnmanager").GetComponent<Spawnmanager>();

        if (spawnmanager == null)
        {
            Debug.LogError("THE SPAWN MANAGER IS NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _Canfire)
        {
            FireLaser();
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);

        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);



        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        if (transform.position.x > 11.5)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }

        

    }


    void FireLaser()
    {
        
        
        _Canfire = Time.time + _Firerate;
        Instantiate(_Laserprefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        
    }
    public void Damage()
    {
        _lives--;


        if (_lives < 1)
        {
            spawnmanager.OnplayerDeath();
            Destroy(this.gameObject);
        }

    }
}
