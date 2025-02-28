using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate.ViewModels;
public class ButtonViewModel : BaseViewModel
{
    private readonly LanguageManager _langManager;

    public string LoadingButtonTitle { get; set; }
    public string LoadingButtonSubtitle { get; set; }
    public string ButtonTitle { get; set; }
    public string ButtonSubtitle { get; set; }

    public string PrimaryButton { get; set; }

    public string TonalButton { get; set; }

    public string OutlineButton { get; set; }

    public string DisabledButton { get; set; }

    public string LoadingButton { get; set; }

    public ICommand LoadCommand { get; }

    public ButtonViewModel(LanguageManager langManager)
    {
        LoadCommand = new AsyncRelayCommand(OnLoadCommand);
        _langManager = langManager;

        if (_langManager.CurrentLanguage == AppLanguage.Arabic)
        {
            ButtonTitle = "الازرار";
            ButtonSubtitle = "انماط الازرار";
            LoadingButtonTitle = "زر التحميل";
            LoadingButtonSubtitle = "يستخدم لعرض جاري التحميل";
            Title = "الازرار";
            PrimaryButton = "زر اساسي";
            TonalButton = "زر نغمي";
            OutlineButton = "زر عريض";
            DisabledButton = "زر معطل";
            LoadingButton = "زر التحميل";

        }
        else
        {
            ButtonTitle = "Buttons";
            ButtonSubtitle = "Buttons styles";
            LoadingButton = "Loading Button";
            LoadingButtonTitle = "Loading Buttons";
            LoadingButtonSubtitle = "Used to display loading indicators";
            Title = "Buttons";
            PrimaryButton = "Primary Button";
            TonalButton = "Tonal Button";
            OutlineButton = "Outline Button";
            DisabledButton = "Disabled Button";
        }
    }

    private async Task OnLoadCommand()
    {
        IsBusy = true;

        await Task.Delay(2000);

        IsBusy = false;
    }
}
