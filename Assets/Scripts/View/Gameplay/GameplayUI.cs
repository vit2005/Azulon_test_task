using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI diamonds;
    [SerializeField] private TextMeshProUGUI rebirth;
    [SerializeField] private IconsCollection iconsCollection;

    [Space]
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private TextMeshProUGUI cellSizeText;
    private float _cellSize = 100f;

    [Space]
    [SerializeField] private Image crunchIndicator;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color crunchColor;
    [SerializeField] private Color burnoutColor;

    [Space]
    [SerializeField] private GameObject workspacePrefab;
    [SerializeField] private GameObject programmerPrefab;
    private int _programmerAmount;
    private List<WorkplaceUI> workspaces = new List<WorkplaceUI>();
    //private List<WorkplaceUI> programmers = new();

    private PlayerData _data;

    public void Init(PlayerData data)
    {
        _data = data;
        
        data.OnDataUpdated += UpdateCurrencies;
        data.studioData.OnDataUpdated += UpdateWorkspaces;

        UpdateCurrencies();
        UpdateWorkspaces();
    }

    private void UpdateCurrencies()
    {
        gold.text = _data.currencies[CurrencyType.Gold].ToString("F0");
        diamonds.text = _data.currencies[CurrencyType.Gems].ToString();
        rebirth.text = _data.rebirths.ToString();
    }

    public void UpdateWorkspaces()
    {
        int diff = _data.studioData.programmersMaxAmount - workspaces.Count;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                var instance = Instantiate(workspacePrefab, grid.transform);
                var workspaceUI = instance.GetComponent<WorkplaceUI>();

                workspaces.Add(workspaceUI);
            }
        }
        else if (diff < 0) // in case of Rebirth
        {
            foreach (var item in workspaces)
            {
                Destroy(item.gameObject);
            }
            workspaces.Clear();
        }

        foreach (var programmerData in _data.studioData.programmers)
        {
            var workspace = workspaces.FirstOrDefault(x => x.ProgrammerData == programmerData);
            if (workspace != null) continue;

            var freeWorkspace = workspaces.FirstOrDefault(x => x.IsFree);
            if (freeWorkspace == null) continue;

            freeWorkspace.InitProgrammer(programmerData, FreeProgrammer);
        }

        switch (_data.studioData.programmersState)
        {
            case ProgrammersState.Idle:
                crunchIndicator.color = defaultColor;
                break;
            case ProgrammersState.Crunching:
                crunchIndicator.color = crunchColor;
                break;
            case ProgrammersState.Burnout:
                crunchIndicator.color = burnoutColor;
                break;
        }
    }

    public void FreeProgrammer(ProgrammerItem data)
    {
        GameController.Instance.InventoryController.FreeProgrammer(data);
    }

    public void DecreaseCellSize()
    {
        if (_cellSize < 6f) return;
        _cellSize -= 5f;
        _cellSize = Mathf.Round(_cellSize);
        cellSizeText.text = _cellSize.ToString();
        grid.cellSize = new Vector2(_cellSize, _cellSize);
    }

    public void IncreaseCellSize()
    {
        if (_cellSize > 99f) return;
        _cellSize += 5f;
        _cellSize = Mathf.Round(_cellSize);
        cellSizeText.text = _cellSize.ToString();
        grid.cellSize = new Vector2(_cellSize, _cellSize);
    }

    private void OnDestroy()
    {
        _data.OnDataUpdated -= UpdateCurrencies;
        _data.studioData.OnDataUpdated -= UpdateWorkspaces;
    }
}
