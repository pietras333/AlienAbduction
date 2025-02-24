using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform planet;

    [SerializeField]
    private GameObject cowPrefab;

    [SerializeField]
    private GameObject humanPrefab;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject restartCanvas;

    [SerializeField]
    public int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnTenPrefabs();
        InvokeRepeating("SpawnTenPrefabs", 20f, Mathf.Infinity);
        scoreText.text = "Score: " + score;
    }

    public void SpawnTenPrefabs()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnPrefab(cowPrefab);
            SpawnPrefab(humanPrefab);
        }
    }

    public void GameOver()
    {
        restartCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        restartCanvas.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
        SpawnTenPrefabs();
        player.GetComponent<PlayerWeaponController>().Reset();
        FindObjectsOfType<Enemy>().ToList().ForEach(e => Destroy(e.gameObject));
        FindObjectsOfType<Rocket>().ToList().ForEach(e => Destroy(e.gameObject));
        FindObjectOfType<Beam>().GetComponent<Beam>().Reset();
        InvokeRepeating("SpawnTenPrefabs", 20f, Mathf.Infinity);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SpawnPrefab(GameObject prefab)
    {
        Vector3 spawnPoint =
            player.position
            + player.right * Random.Range(-100f, 100f)
            + player.up * Random.Range(-100f, 100f)
            + player.forward * Random.Range(-100f, 100f);

        Debug.DrawRay(spawnPoint, planet.position - spawnPoint, Color.red, 10f);

        Ray ray = new Ray(spawnPoint, planet.position - spawnPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Quaternion rotationOnPlanet = Quaternion.FromToRotation(Vector3.up, hit.normal);
            GameObject spawned = Instantiate(prefab, hit.point, rotationOnPlanet);
            spawned.transform.SetParent(planet);
        }
    }

    public void AddScore(int score)
    {
        Debug.Log("Score: " + score);
        this.score += score;
        scoreText.text = "Score: " + this.score;
    }
}
