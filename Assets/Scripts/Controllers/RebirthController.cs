public class RebirthController
{
    private readonly PlayerData _playerData;

    public RebirthController(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public void Rebirth()
    {
        _playerData.rebirths++;
        _playerData.inventoryData.programmers.Clear();

        var studio = _playerData.studioData;
        studio.programmers.Clear();
        studio.programmersMaxAmount = 0;
        studio.programmersState = ProgrammersState.Idle;
        studio.secondsLeft = 0f;
        studio.crunchIntensity = 1f;

        _playerData.studioData.NotifyUpdated();
        _playerData.NotifyUpdated();
    }
}