using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Controls;
using UnoAppTemplate.Helpers;
using UnoAppTemplate.Resx;

namespace UnoAppTemplate;
public class LanguageManager : BindableObjectBase
{
    public static CultureInfo ARABIC_CULTURE = new CultureInfo("ar-EG");
    public static CultureInfo ENGLISH_CULTURE = new CultureInfo("en-US");
    private const string STYLE_RTL = "ms-appx:///Styles/StyleRTL.xaml";

    private ElementFlowDirection _direction;
    private AppLanguage _currentLanguage;

    public AppLanguage CurrentLanguage { get => _currentLanguage; set => SetValue(ref _currentLanguage, value); }
    public ElementFlowDirection Direction { get => _direction; set => SetValue(ref _direction, value); }

    public IList<ResourceDictionary> AppDictionary => App.Current.Resources.MergedDictionaries;

    public LanguageManager()
    {
        _direction = ElementFlowDirection.LeftToRight;
    }

    public async Task ChangeLangauge(AppLanguage lang)
    {
        if (lang == AppLanguage.Arabic)
        {
            SwitchToArabic();
        }
        else
        {
            SwitchToEnglish();
        }
    }

    private void SwitchToArabic()
    {
        CultureInfo.DefaultThreadCurrentCulture = ARABIC_CULTURE;
        CommonText.Culture = ARABIC_CULTURE;
        Direction = ElementFlowDirection.RightToLeft;
        CurrentLanguage = AppLanguage.Arabic;
        AddStyleRtl();
    }

    private void SwitchToEnglish()
    {
        CultureInfo.DefaultThreadCurrentCulture = ENGLISH_CULTURE;
        CommonText.Culture = ENGLISH_CULTURE;
        Direction = ElementFlowDirection.LeftToRight;
        CurrentLanguage = AppLanguage.English;
        RemoveStyleRtl();
    }

    private void AddStyleRtl()
    {
        var found = AppDictionary.Where(a => a.Source == new Uri(STYLE_RTL)).FirstOrDefault();

        if (found != null)
            return;

        AppDictionary.Add(new ResourceDictionary()
        {
            Source = new Uri(STYLE_RTL)
        });
    }

    private void RemoveStyleRtl()
    {
        var rtlResource = AppDictionary.Where(a => a.Source == new Uri(STYLE_RTL)).FirstOrDefault();

        if (rtlResource != null)
            AppDictionary.Remove(rtlResource);
    }
}
