using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Org.Apache.Http.Conn;
using Uno.UI.Xaml;
using UnoAppTemplate.Animations;
using UnoAppTemplate.Controls;
using UnoAppTemplate.Demo.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;

namespace UnoAppTemplate;

public sealed partial class Shell : Page
{
    private readonly SemaphoreSlim _slim;
    private Grid _contentRoot;
    private IList<NavigationBag> _stack;

    public ICommand MenuCommand { get; }

    public Page CurrentPage { get; private set; }

    public bool CanGoBack { get; private set; }

    public Shell()
    {
        _slim = new SemaphoreSlim(1, 1);

        this.InitializeComponent();

        _stack = new List<NavigationBag>();
        _contentRoot = PART_ContentRoot;

        MenuCommand = new AsyncRelayCommand<object>(OnMenuCommand);

        Navigate(new SecondPage(), PageAnimationType.None);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

    }

    public void ShowMenu()
    {
        PART_SplitView.IsPaneOpen = true;
    }

    public void Navigate(Page page, PageAnimationType animationType = PageAnimationType.None)
    {
        DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, async () =>
        {
            ClearStack();

            InsertPage(page);

            await page.AnimatePage(animationType);
        });
    }

    public void Append(Page page, PageAnimationType animationType = PageAnimationType.None)
    {
        CanGoBack = true;

        DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, async () =>
        {
            //var pageContent = page.Content as Grid;

            //if (pageContent != null)
            //{
            //    pageContent.Shadow = new Shadow();
            //    pageContent.BorderBrush = new SolidColorBrush(Colors.Red);
            //    pageContent.BorderThickness = new Thickness(10);
            //}

            InsertPage(page);

            await page.AnimatePage(animationType);
        });
    }

    public async Task GoBack(PageAnimationType animationType = PageAnimationType.None)
    {
        var topIndex = _stack.Count - 1;

        if (!HasBackStack() || topIndex < 0)
            return;

        DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, async () =>
        {
            var currentPage = _stack[topIndex].Content;
            var container = _stack[topIndex].Container;

            await currentPage.AnimatePage(animationType);

            CurrentPage = _stack[_stack.Count - 2]?.Content;
        });

        await Task.Delay(PageAnimationExtensions.ANIMATION_SPEED);

        await Dispatcher.RunAsync(CoreDispatcherPriority.Idle, async () =>
        {
            await RemovePage(topIndex);
        });
    }

    private void OnMenuButtonClick(object sender, RoutedEventArgs e)
    {
        PART_SplitView.IsPaneOpen = true;
    }

    private async Task OnMenuCommand(object sender)
    {
        var control = sender as MenuItem;

        if (control == null)
            return;


        foreach (MenuItem item in PART_MenuItems.Children)
        {
            item.IsSelected = false;
        }

        control.IsSelected = true;

        PART_SplitView.IsPaneOpen = false;
    }

    private void ClearStack()
    {
        _stack.Clear();
        _contentRoot?.Children.Clear();
    }

    private void InsertPage(Page page)
    {
        CurrentPage = page;

        var container = new ContentPresenter()
        {
            Background = new SolidColorBrush(Colors.Transparent),
            Content = page
        };

        _stack.Add(new NavigationBag(container, page));

        if (_contentRoot == null)
            throw new Exception("navigation content root cannot be found !");

        _contentRoot.Children.Add(container);
    }

    private async Task RemovePage(int index)
    {
        try
        {
            await _slim.WaitAsync();

            if (index + 1 > _stack.Count)
                return;

            System.Diagnostics.Debug.WriteLine($"===== Remove index: {index}");

            _stack.RemoveAt(index);
            _contentRoot.Children.RemoveAt(index);
        }
        catch (Exception)
        {

        }
        finally
        {
            _slim.Release();
        }
    }

    private bool HasBackStack()
    {
        return _stack.Count > 1;
    }

}

public record NavigationBag(UIElement Container, Page Content);
