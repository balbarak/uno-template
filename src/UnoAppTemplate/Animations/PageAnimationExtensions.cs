using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;

namespace UnoAppTemplate.Animations;

public static class PageAnimationExtensions
{
    public static async Task AnimatePage(this Page page, PageAnimationType type)
    {
        switch (type)
        {
            case PageAnimationType.None:


                break;
            case PageAnimationType.SlideFromLeft:

                await page.SlideFromLeft();

                break;

            case PageAnimationType.SlideToLeft:

                await page.SlideToLeft();

                break;
            case PageAnimationType.SlideToRight:

                await page.SlideToRight();

                break;
            default:
                break;
        }
    }

    public static async Task SlideFromLeft(this Page page)
    {
        var storyboard = new Storyboard();
        var animation = storyboard.AddSlideFromLeft(250, EasingMode.EaseInOut);

        var transforms = new CompositeTransform();

        page.RenderTransform = transforms;

        Storyboard.SetTarget(animation, transforms);

        Storyboard.SetTargetProperty(animation, nameof(CompositeTransform.TranslateX));

        await storyboard.StartAsync();
    }

    public static async Task SlideToLeft(this Page page)
    {
        var storyboard = new Storyboard();
        var animation = storyboard.AddSlideToLeft(250, EasingMode.EaseInOut);

        var transforms = new CompositeTransform();

        page.RenderTransform = transforms;

        Storyboard.SetTarget(animation, transforms);

        Storyboard.SetTargetProperty(animation, nameof(CompositeTransform.TranslateX));

        await storyboard.StartAsync();
    }

    public static async Task SlideToRight(this Page page)
    {
        var storyboard = new Storyboard();
        var animation = storyboard.AddSlideToRight(250, EasingMode.EaseInOut);

        var transforms = new CompositeTransform();

        page.RenderTransform = transforms;

        Storyboard.SetTarget(animation, transforms);

        Storyboard.SetTargetProperty(animation, nameof(CompositeTransform.TranslateX));

        await storyboard.StartAsync();
    }
}
