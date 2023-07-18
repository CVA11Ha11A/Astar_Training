using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ObstacleMap : TileMapControler
{
    private const string OBSTACLE_TILEMAP_OBJ_NAME = "ObstacleTilemap";
    private GameObject[] castleObjs = default;      //! < ��ã�� �˰����� �׽�Ʈ �� ������� �������� ĳ���� ������Ʈ �迭



    //! Awake Ÿ�ӿ� �ʱ�ȭ �� ������ �������Ѵ�.
    public override void InitAwake(MapBoard mapControler_)
    {
        this.tileMapObjName = OBSTACLE_TILEMAP_OBJ_NAME;
        base.InitAwake(mapControler_);
    }   //InitAwake()


    private void Start()
    {
        StartCoroutine(DelayStart(0f));
    }

    private IEnumerator DelayStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        DoStart();

    }   //DelayStart()


    private void DoStart()
    {

        // { ������� �������� �����ؼ� Ÿ���� ��ġ�Ѵ�.
        castleObjs = new GameObject[2];
        TerrainControler[] passableTerrains = new TerrainControler[2];
        List<TerrainControler> searchTerrains = default;
        int searchIdx = 0;
        TerrainControler foundTile = default;

        // ������� �������� �������� y���� ��ġ�ؼ� �� ������ �޾ƿ´�.
        searchIdx = 0;
        foundTile = default;
        while(foundTile == null || foundTile == default)
        {
            // ������ �Ʒ��� ��ġ�Ѵ�.
            searchTerrains = mapControler.GetTerrains_Colum(searchIdx, true);
            foreach(var searchTerrain in searchTerrains)
            {
                if (searchTerrain.IsPassable)
                {
                    foundTile = searchTerrain;
                    break;
                }
                else { /* Do nothing */ }

            }

            if (foundTile != null || foundTile != default) { break; }
            if (mapControler.MapCellSize.x - 1 <= searchIdx) { break; }
            searchIdx++;
        }   // loop: ������� ã�� ����
        passableTerrains[0] = foundTile;

        //�������� �������� �������� y���� ��ġ�ؼ� �� ������ �޾ƿ´�.
        searchIdx = mapControler.MapCellSize.x - 1;
        foundTile = default;
        while (foundTile == null || foundTile == default)
        {
            // �Ʒ����� ���� ��ġ�Ѵ�.
            searchTerrains = mapControler.GetTerrains_Colum(searchIdx);
            foreach(var searchTerrain in searchTerrains)
            {
                if (searchTerrain.IsPassable)
                {
                    foundTile = searchTerrain;
                    break;
                }
                else { /* Do nothing */ }
            }

            if (foundTile != null || foundTile != default) { break; }
            if(searchIdx <= 0) { break; }
            searchIdx--;

        }   // loop : �������� ã�� ����

        passableTerrains[1] = foundTile;

        // } ������� �������� �����ؼ� Ÿ���� ��ġ�Ѵ�.

        // { ������� �������� ������ �߰��Ѵ�.
        
        // } ������� �������� ������ �߰��Ѵ�.
    }   //DoStart()



    //! ������ �߰��Ѵ�.
    public void Add_Obstacle(GameObject obstacle_)
    {
        allTileObjs.Add(obstacle_);
    }   //Add_Obstacle()


}
