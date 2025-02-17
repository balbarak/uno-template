using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;

namespace UnoAppTemplate.Animations;

public static class PageAnimationExtensions
{
    public const int ANIMATION_SPEED = 250;
    public static async Task AnimatePage(this Page page, PageAnimationType type)
    {
        switch (type)
        {
            case PageAnimationType.None:


                break;
            case PageAnimationType.SlideInFromLeft:

                await page.SlideFromLeft();

                break;

            case PageAnimationType.SlideOutToLeft:

                await page.SlideToLeft();

                break;
            case PageAnimationType.SlideOutToRight:

                await page.SlideToRight();

                break;

            case PageAnimationType.SlideInFromRight:

                await page.SlideInFromRight();

                break;
            default:
                break;
        }
    }

    public static async Task SlideFromLeft(this Page page)
    {
        var storyboard = new Storyboard();
        var animation = storyboard.AddSlideFromLeft(ANIMATION_SPEED, EasingMode.EaseInOut);

        var transforms = new CompositeTransform();

        page.RenderTransform = transforms;

        Storyboard.SetTarget(animation, transforms);

        Storyboard.SetTargetProperty(animation, nameof(CompositeTransform.TranslateX));

        await storyboard.StartAsync();
    }

    public static async Task SlideInFromRight(this Page page)
    {
        var storyboard = new Storyboard();
        var animation = storyboard.AddSlideFromRight(ANIMATION_SPEED, EasingMode.EaseInOut);

        var transforms = new CompositeTransform();

        page.RenderTransform = transforms;

        Storyboard.SetTarget(animation, transforms);

        Storyboard.SetTargetProperty(animation, nameof(CompositeTransform.TranslateX));

        await storyboard.StartAsync();
    }

    public static async Task SlideToLeft(this Page page)
    {
        var storyboard = new Storyboard();
        var animation = storyboard.AddSlideToLeft(ANIMATION_SPEED, EasingMode.EaseInOut);

        var transforms = new CompositeTransform();

        page.RenderTransform = transforms;

        Storyboard.SetTarget(animation, transforms);

        Storyboard.SetTargetProperty(animation, nameof(CompositeTransform.TranslateX));

        await storyboard.StartAsync();
    }

    public static async Task SlideToRight(this Page page)
    {
        var storyboard = new Storyboard();
        var animation = storyboard.AddSlideToRight(ANIMATION_SPEED, EasingMode.EaseInOut);

        var transforms = new CompositeTransform();

        page.RenderTransform = transforms;

        Storyboard.SetTarget(animation, transforms);

        Storyboard.SetTargetProperty(animation, nameof(CompositeTransform.TranslateX));

        await storyboard.StartAsync();
    }
}
