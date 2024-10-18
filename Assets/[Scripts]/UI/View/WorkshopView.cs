using Cube.Controllers;
using Cube.Gameplay;
using Cube.UI.View;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopView : View
{
    [SerializeField]
    Button _backBtn;

    private void OnBack()
    {
        GameHandler.Instance.SetStatus(GameStatus.MainMenu);
    }

    private void OnEnable() 
    {
        _backBtn.onClick.AddListener(OnBack);
    }

    private void OnDisable() 
    {
        _backBtn.onClick.RemoveAllListeners();
    }

#region Override
    public override ViewType ViewType => ViewType.Workshop;
#endregion
}
