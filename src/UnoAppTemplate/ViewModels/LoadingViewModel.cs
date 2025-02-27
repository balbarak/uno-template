using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Resx;

namespace UnoAppTemplate.ViewModels;
public class LoadingViewModel : BaseViewModel
{
    public LoadingViewModel()
    {
        Title = CommonText.PleaseWait;
    }
}
