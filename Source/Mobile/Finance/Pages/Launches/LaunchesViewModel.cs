using Finance.Models;
using Finance.Utilities;

namespace Finance.Pages.Launches;

/// <summary>
/// ViewModel responsável pela lógica da página de lançamentos, incluindo operações de CRUD e navegação.
/// </summary>
internal partial class LaunchesViewModel : ViewModel {

    public ObservableCollection<Notification> Notifications { get; set; }

    public LaunchesViewModel() {
        Notifications = [];
    }

    public override void Update() {
        Notifications.Add(new Notification { });
        Notifications.Add(new Notification { });
        Notifications.Add(new Notification { });
    }
}