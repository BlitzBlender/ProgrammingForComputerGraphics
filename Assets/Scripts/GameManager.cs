using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
    public static int WaypointsMet;
    private List<GameObject> Waypoints;
    private TextMeshProUGUI WaypointText;
    private TextMeshProUGUI TitleText;
    private int SpawnLocation;
    private GameObject Car;
   
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Canvas"));
        Waypoints = new List<GameObject>();
        WaypointText = GameObject.Find("Waypoints").GetComponent<TextMeshProUGUI>();
        TitleText = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        TitleText.SetText("City 1");
        WaypointText.SetText("Areas Explored: ");
        
        SpawnCar();
    }

    private void Update()
    {
        WaypointText.SetText("Areas Explored: " + WaypointsMet);
        
        if (WaypointsMet == 3)
        {
            Waypoints.Clear();
            SceneManager.LoadScene("Level2");
            SpawnCar();
            WaypointsMet++;
            WaypointText.SetText("Areas Explored: " + WaypointsMet);
            TitleText.SetText("City 2");
            
        }
        else if (WaypointsMet == 7) 
        {
            Destroy(GameObject.Find("Canvas"));
            SceneManager.LoadScene("Level3");
            Destroy(GameObject.Find("GameManager"));
        }
    }

    private void SpawnCar()
    {
        Car = GameObject.Find("Car");
        SpawnLocation = Random.Range(0, 2);
        Waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));

        switch (SpawnLocation)
        {
            case 0:
                Car.transform.position = Waypoints[0].transform.position;
                break;

            case 1:
                Car.transform.position = Waypoints[1].transform.position;
                break;

            case 2:
                Car.transform.position = Waypoints[2].transform.position;
                break;
        }
    }

    
}
