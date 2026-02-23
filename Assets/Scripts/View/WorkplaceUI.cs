using UnityEngine;
using UnityEngine.UI;

public class WorkplaceUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Sprite defaultIcon;
    private float income;

    private bool isFree = true;
    public bool IsFree => isFree;

    private ProgrammerItem _programmerData;
    public ProgrammerItem ProgrammerData => _programmerData;

    public void InitProgrammer(ProgrammerItem data)
    {
        _programmerData = data;
        icon.sprite = data.icon;
        income = data.income;
        isFree = false;
    }

    public void FreeWorkspace()
    {
        if (_programmerData == null) return;

        GameController.Instance.FreeProgrammer(_programmerData);
        icon.sprite = defaultIcon;
        _programmerData = null;
        income = 0;
        isFree = true;
    }
}
