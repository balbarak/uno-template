using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoAppTemplate.Resx;

namespace UnoAppTemplate.ViewModels;
public class TypographyViewModel : BaseViewModel
{
    private readonly LanguageManager _langManager;
    public string DisplayTitle { get; set; }
    public string DisplaySubtitle { get; set; }
    public string DisplayLarge { get; set; }
    public string DisplayMedium { get; set; }
    public string DisplaySmall { get; set; }

    public string HeadlineTitle { get; set; }
    public string HeadlineSubtitle { get; set; }
    public string HeadlineLarge { get; set; }
    public string HeadlineMedium { get; set; }
    public string HeadlineSmall { get; set; }


    public string TitleTitle { get; set; }
    public string TitleSubtitle { get; set; }
    public string TitleLarge { get; set; }
    public string TitleMedium { get; set; }
    public string TitleSmall { get; set; }


    public string BodyTitle { get; set; }
    public string BodySubtitle { get; set; }
    public string BodyLarge { get; set; }
    public string BodyMedium { get; set; }
    public string BodySmall { get; set; }

    public TypographyViewModel(LanguageManager langManager)
    {
        Title = CommonText.Typography;

        _langManager = langManager;

        if (_langManager.CurrentLanguage == AppLanguage.Arabic)
        {
            DisplayTitle = "خط العرض";
            DisplaySubtitle = "يستخدم لعرض النصوص";
            DisplayLarge = "عرض كبير";
            DisplayMedium = "عرض متوسط";
            DisplaySmall = "عرض صغير";

            HeadlineTitle = "عنوان رئيسي";
            HeadlineSubtitle = "يستخدم لعرض العناوين الرئيسية";
            HeadlineLarge = "عنوان رئيسي كبير";
            HeadlineMedium = "عنوان رئيسي متوسط";
            HeadlineSmall = "عنوان رئيسي صغير";

            TitleTitle = "عنوان";
            TitleSubtitle = "يستخدم لعرض العناوين";
            TitleLarge = "عنوان كبير";
            TitleMedium = "عنوان وسط";
            TitleSmall = "عنوان صغير";

            BodyTitle = "نص المحتوى";
            BodySubtitle = "يستخدم لعرض نصوص المحتوى";
            BodyLarge = "نص كبير";
            BodyMedium = "نص متوسط";
            BodySmall = "نص صغير";
        }
        else
        {
            DisplayTitle = "Display Fonts";
            DisplaySubtitle = "Used for display texts";
            DisplayLarge = "Display Large";
            DisplayMedium = "Display Medium";
            DisplaySmall = "Display Small";

            HeadlineTitle = "Headline Typography";
            HeadlineSubtitle = "Used to display headlines";
            HeadlineLarge = "Headline Large";
            HeadlineMedium = "Headline Medium";
            HeadlineSmall = "Headline Small";

            TitleTitle = "Title Typgoraphy";
            TitleSubtitle = "Used to display titles";
            TitleLarge = "Title Large";
            TitleMedium = "Title Medium";
            TitleSmall = "Title Small";

            BodyTitle = "Body Typography";
            BodySubtitle = "Used for body text";
            BodyLarge = "Body Large";
            BodyMedium = "Body Medium";
            BodySmall = "Body Small";
        }
   
    }
}
