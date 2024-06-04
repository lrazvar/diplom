using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
    public GameObject spritePrefab;
    public int numPoints = 3;
    public float spawnZ = 60.0f; // Координата Z для точек
    public Vector2 rightSpawnXRange = new Vector2(20f, 80f);
    public Vector2 rightSpawnYRange = new Vector2(-30f, 30f);
    public Vector2 leftSpawnXRange = new Vector2(-80f, -20f); // Диапазон для точек слева
    public Vector2 leftSpawnYRange = new Vector2(-30f, 30f);
    
    private GameObject[] points;
    private LineRenderer lineRenderer;

    void Start()
    {
        points = new GameObject[numPoints * 2]; // Удваиваем количество точек
        lineRenderer = GetComponent<LineRenderer>();

        GeneratePoints();
        DrawLines();
    }

    void GeneratePoints()
    {
        for (int i = 0; i < numPoints; i++)
        {
            // Генерируем точки справа
            float spawnX = Random.Range(rightSpawnXRange.x, rightSpawnXRange.y);
            float spawnY = Random.Range(rightSpawnYRange.x, rightSpawnYRange.y);
            Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
            points[i] = Instantiate(spritePrefab, spawnPos, Quaternion.identity);

            // Генерируем точки слева
            float userSpawnX = Random.Range(leftSpawnXRange.x, leftSpawnXRange.y);
            float userSpawnY = Random.Range(leftSpawnYRange.x, leftSpawnYRange.y);
            Vector3 userSpawnPos = new Vector3(userSpawnX, userSpawnY, spawnZ);
            points[i + numPoints] = Instantiate(spritePrefab, userSpawnPos, Quaternion.identity);
        }
    }

    void DrawLines()
    {
        lineRenderer.positionCount = numPoints + 1;

        for (int i = 0; i < numPoints; i++)
        {
            lineRenderer.SetPosition(i, points[i].transform.position);
        }

        lineRenderer.SetPosition(numPoints, points[0].transform.position);
    }
}