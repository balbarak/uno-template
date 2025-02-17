using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;

namespace UnoAppTemplate.Animations;

public static class StoryboardExtensions
{
    public static DoubleAnimation AddSlideFromLeft(this Storyboard sb, double duration, EasingMode cubicEasingMode = EasingMode.EaseInOut)
    {
        var width = -App.MainWindow.Bounds.Width;

        var animation = new DoubleAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(duration),
            From = width,
            To = 0,
        };

        animation.EasingFunction = new CubicEase()
        {
            EasingMode = cubicEasingMode
        };

        sb.Children.Add(animation);

        return animation;
    }

    public static DoubleAnimation AddSlideFromRight(this Storyboard sb, double duration, EasingMode cubicEasingMode = EasingMode.EaseInOut)
    {
        var width = App.MainWindow.Bounds.Width;

        var animation = new DoubleAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(duration),
            From = width,
            To = 0,
        };

        animation.EasingFunction = new CubicEase()
        {
            EasingMode = cubicEasingMode
        };

        sb.Children.Add(animation);

        return animation;
    }

    public static DoubleAnimation AddSlideToRight(this Storyboard sb, double duration, EasingMode cubicEasingMode = EasingMode.EaseInOut)
    {
        var width = App.MainWindow.Bounds.Width;

        var animation = new DoubleAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(duration),
            From = 0,
            To = width,
        };

        animation.EasingFunction = new CubicEase()
        {
            EasingMode = cubicEasingMode
        };

        sb.Children.Add(animation);

        return animation;
    }

    public static DoubleAnimation AddSlideToLeft(this Storyboard sb, double duration, EasingMode cubicEasingMode = EasingMode.EaseInOut)
    {
        var width = App.MainWindow.Bounds.Width;

        var animation = new DoubleAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(duration),
            From = 0,
            To = -width,
        };

        animation.EasingFunction = new CubicEase()
        {
            EasingMode = cubicEasingMode
        };

        sb.Children.Add(animation);

        return animation;
    }

    public static DoubleAnimation AddFadeOut(this Storyboard sb, double duration, EasingMode cubicEasingMode = EasingMode.EaseInOut)
    {
        var animation = new DoubleAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(duration),
            From = 1,
            To = 0,
        };

        animation.EasingFunction = new CubicEase()
        {
            EasingMode = cubicEasingMode
        };

        sb.Children.Add(animation);

        return animation;
    }

    public static DoubleAnimation AddFadeIn(this Storyboard sb, double duration, EasingMode cubicEasingMode = EasingMode.EaseInOut)
    {
        var animation = new DoubleAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(duration),
            From = 0,
            To = 1,
        };

        animation.EasingFunction = new CubicEase()
        {
            EasingMode = cubicEasingMode
        };

        sb.Children.Add(animation);

        return animation;
    }

    public static DoubleAnimation AddDoubleAnimation(this Storyboard sb, double value, TimeSpan duration, double? from = null, EasingMode cubicEasingMode = EasingMode.EaseInOut)
    {
        var animation = new DoubleAnimation()
        {
            Duration = duration,
            To = value,
        };

        if (from.HasValue)
            animation.From = from.Value;

        animation.EasingFunction = new CubicEase()
        {
            EasingMode = cubicEasingMode
        };

        sb.Children.Add(animation);

        return animation;
    }

    public static Task StartAsync(this Storyboard storyboard)
    {
        var tcs = new TaskCompletionSource<object>();

        EventHandler<object> completion = null;

        completion = (sender, args) =>
        {
            storyboard.Completed -= completion;
            tcs.SetResult(null);
        };

        storyboard.Completed += completion;

        storyboard.Begin();

        return tcs.Task;
    }
}
