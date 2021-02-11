using GameManagers;
using Interaction;
using UnityEngine;

public class GameEvent_ChangeSuit : MonoBehaviour, IGameEvent
{
    [SerializeField] private GameEventType _gameEventType;
    [SerializeField] private GameObject _suitModel;
    
    private SuitChangeController _suitChangeController;
    
    public GameEventType gameEventType
    {
        get
        {
            return _gameEventType;
        }
    }

    private bool _hasRun;
    public bool hasRun 
    {
        get
        {
            return _hasRun;
        }
    }

    private void Awake()
    {
        _suitChangeController = new SuitChangeController();
        _hasRun = false;
    }

    public void RunPermanentEvents()
    {
        _suitModel.SetActive(false);

        GameplayManager.instance.onPlayerSuitChange(PlayerSuitEnum.SUIT1);
    }

    public void RunSingleTimeEvents()
    {
        _suitChangeController.ChangeSuit(PlayerSuitEnum.SUIT1, delegate()
        {
            RunPermanentEvents();
            ChapterManager.instance.GoToNextChapter();
            
            _hasRun = true;
        });
    }
}
