public class GameController
{
    private static GameController _instance;
    public static GameController Instance => _instance ??= new GameController(); // TODO: initialize in loading state

    public PlayerData PlayerData { get; }
    public CrunchController CrunchController { get; }
    public RebirthController RebirthController { get; }

    // TODO: init OfflineShopProvider and OfflinePlayerStorage here
    public GameController()
    {
        PlayerData = new PlayerData();
        CrunchController = new CrunchController(PlayerData.studioData);
        RebirthController = new RebirthController(PlayerData);
    }
}