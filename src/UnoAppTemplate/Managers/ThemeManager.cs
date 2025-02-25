using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate;

public class ThemeManager : BindableObjectBase
{
    private bool _isDark;
    
    public bool IsDark { get => _isDark; set => SetValue(ref _isDark, value); }


    public ThemeManager()
    {
        
    }

    public async Task SwitchTheme(bool isDark)
    {
        var service = App.ContentHost.GetThemeService();

        var targetTheme = isDark ? AppTheme.Dark : AppTheme.Light;

        await service.SetThemeAsync(targetTheme);

        IsDark = isDark;
    }
}
