using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Helpers;
using UnoAppTemplate.Resx;

namespace UnoAppTemplate.ViewModels;

public class BaseViewModel : BindableObjectBase
{

    private bool _isBusy;
    public bool IsBusy { get => _isBusy; set => SetValue(ref _isBusy, value); }

}
