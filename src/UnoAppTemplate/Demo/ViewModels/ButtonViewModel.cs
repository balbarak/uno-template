using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate.ViewModels;
public class ButtonViewModel : BaseViewModel
{

    public ICommand LoadCommand { get; }

    public ButtonViewModel()
    {
        LoadCommand = new AsyncRelayCommand(OnLoadCommand);
    }

    private async Task OnLoadCommand()
    {
        IsBusy = true;

        await Task.Delay(2000);

        IsBusy = false;
    }
}
