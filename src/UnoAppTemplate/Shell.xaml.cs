using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using UnoAppTemplate.Animations;
using UnoAppTemplate.Controls;
using UnoAppTemplate.Demo.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace UnoAppTemplate;

public sealed partial class Shell : Page
{
    private Grid _contentRoot;
    private IList<NavigationBag> _stack;

    public ICommand MenuCommand { get;}

    public Page CurrentPage { get; private set; }

    public Shell()
    {
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
            Content = page
        };

        _stack.Add(new NavigationBag(container, page));

        if (_contentRoot == null)
            throw new Exception("navigation content root cannot be found !");

        _contentRoot.Children.Add(container);
    }

    private void RemovePage(int index)
    {
        _stack.RemoveAt(index);
        _contentRoot.Children.RemoveAt(index);
    }

}

public record NavigationBag(ContentPresenter Container, Page Content);
