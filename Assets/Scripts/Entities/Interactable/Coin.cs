using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible, IDataPersistence
{
    [Header("Collectible params")]
    [SerializeField] int points = 10;
    private bool _isCollected = false;
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    public string interactHint { get; set; }
    public void Awake()
    {
        interactHint = "Pick up";
    }
    public void OnTriggerEnter(Collider other)
    {
        Interact();
    }
    public void Interact()
    {
        InGameManager.Instance.pointCounter.AddPoints(points);
        _isCollected = true;

        gameObject.active = false;
    }

    public void LoadData(GameData data)
    {
        data.coinsCollected.TryGetValue(id, out _isCollected);
        if (_isCollected)
        {
            gameObject.active = false;
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.coinsCollected.ContainsKey(id))
        {
            data.coinsCollected.Remove(id);
        }
        data.coinsCollected.Add(id, _isCollected);
    }

    [Header("Floating object params")]
    public float rotationSpeed = 100f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Spin();
        Float();
    }

    void Spin()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    void Float()
    {
        Vector3 newPos = startPos;
        newPos.y += Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = newPos;
    }
}
