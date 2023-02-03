using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{

    #region Attributes

    [SerializeField] private Grid _rootGrid;
    [SerializeField] private Tilemap _tilemap;

    private Vector2Int _currentRootLocation = new();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Should be set to player start tile
        _currentRootLocation.Set(9, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int targetTile = new();
        switch(Event.current.keyCode)
        {
            case KeyCode.A:
            case KeyCode.LeftArrow:
                targetTile = _currentRootLocation + Vector2Int.left;
                break;
            case KeyCode.S:
            case KeyCode.DownArrow:
                targetTile = _currentRootLocation + Vector2Int.down;
                break;
            case KeyCode.D:
            case KeyCode.RightArrow:
                targetTile = _currentRootLocation + Vector2Int.right;
                break;
            default:
                break;
        }

        TileChangeData tile2Change = new TileChangeData();

        tile2Change.position = new Vector3Int(targetTile.x, targetTile.y, 0);
        tile2Change.tile = _tilemap.GetTile(Vector3Int.zero);

    }
}
