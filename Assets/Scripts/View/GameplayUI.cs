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
    [SerializeField] private GameObject workspacePrefab;
    [SerializeField] private GameObject programmerPrefab;
    private int _workspacesAmount;
    private int _programmerAmount;
    private List<WorkplaceUI> workspaces = new List<WorkplaceUI>();
    //private List<WorkplaceUI> programmers = new();

    private PlayerData _data;

    public void Init(PlayerData data)
    {
        _data = data;
        LoadIcons();
        
        data.OnDataUpdated += UpdateCurrencies;
        data.studioData.OnDataUpdated += UpdateWorkspaces;

        UpdateCurrencies();
        UpdateWorkspaces();
    }

    private void UpdateCurrencies()
    {
        gold.text = _data.currencies[CurrencyType.Gold].ToString();
        diamonds.text = _data.currencies[CurrencyType.Gems].ToString();
        rebirth.text = _data.rebirths.ToString();
    }

    private void LoadIcons()
    {
        foreach (var item in _data.studioData.programmers)
        {
            item.icon = iconsCollection.icons.First(x => x.id == item.iconId).icon;
        }
    }

    public void UpdateWorkspaces()
    {
        int diff = _data.studioData.programmersMaxAmount - _workspacesAmount;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                var instance = Instantiate(workspacePrefab, grid.transform);
                var workspaceUI = instance.GetComponent<WorkplaceUI>();

                workspaces.Add(workspaceUI);
                _workspacesAmount++;
            }
        }

        foreach (var programmerData in _data.studioData.programmers)
        {
            var workspace = workspaces.FirstOrDefault(x => x.ProgrammerData == programmerData);
            if (workspace != null) continue;

            var freeWorkspace = workspaces.FirstOrDefault(x => x.IsFree);
            if (freeWorkspace == null) continue;

            freeWorkspace.InitProgrammer(programmerData);
        }
    }

    public void AddProgrammer(ProgrammerItem data)
    {
        var freeWorkspace = workspaces.FirstOrDefault(x => x.IsFree);
        if (freeWorkspace == null) return;

        freeWorkspace.InitProgrammer(data);
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
}
