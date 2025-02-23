using Microsoft.UI;
using Microsoft.UI.Dispatching;
using UnoAppTemplate.Animations;
using UnoAppTemplate.Controls;
using UnoAppTemplate.Services;
using Windows.UI.Core;

namespace UnoAppTemplate;

public sealed partial class Shell : Page
{
    private readonly SemaphoreSlim _slim;
    private Grid _contentRoot;
    private IList<NavigationBag> _stack;

    public Page CurrentPage { get; private set; }

    public bool CanGoBack { get; private set; }

    public ShellViewModel ViewModel => DataContext as ShellViewModel;

    public Shell()
    {
        _slim = new SemaphoreSlim(1, 1);

        DataContext = App.GetService<ShellViewModel>();

        this.InitializeComponent();

        _stack = new List<NavigationBag>();
        _contentRoot = PART_ContentRoot;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        var dir = ElementDirection.GetFlow(this);

        if (dir == ElementFlowDirection.RightToLeft)
        {
            PART_SplitView.PanePlacement = SplitViewPanePlacement.Right;
        }
        else
        {
            PART_SplitView.PanePlacement = SplitViewPanePlacement.Left;
        }
    }

    public void Navigate(Page page, PageAnimationType animationType = PageAnimationType.None)
    {
        CanGoBack = false;

        DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, async () =>
        {
            SetElementDirection(page);

            SetActiveMenu();

            ClearStack();

            InsertPage(page);

            await page.AnimatePage(animationType);
        });
    }

    public void Append(Page page, PageAnimationType animationType = PageAnimationType.None)
    {
        DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, async () =>
        {
            SetElementDirection(page);

            CanGoBack = true;

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

            CurrentPage = _stack[topIndex - 1]?.Content;

            CanGoBack = _stack.Count > 1;
        });

        await Task.Delay(PageAnimationExtensions.ANIMATION_SPEED + 100);

        await Dispatcher.RunAsync(CoreDispatcherPriority.Idle, async () =>
        {
            await RemovePage(topIndex);
        });
    }

    public void ShowMenu()
    {
        PART_SplitView.IsPaneOpen = true;
    }

    public void HideMenu()
    {
        PART_SplitView.IsPaneOpen = false;
    }

    public async Task ShowToaster(string msg)
    {
        await PART_Toaster.ShowToaster(msg);
    }

    public async Task ShowAlert(string title,string msg,AlertType type)
    {
        await PART_Alert.ShowModal(title, msg, type);
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

    private void SetActiveMenu()
    {

        foreach (MenuItem item in PART_MenuItems.Children)
        {
            if (item.CommandParameter?.ToString() == NavigationService.CurrentRoute)
            {
                item.IsSelected = true;
            }
            else
            {
                item.IsSelected = false;
            }
        }
    }

    private void SetElementDirection(Page page)
    {
        var currentFlow = ElementDirection.GetFlow(this);

        ElementDirection.SetFlow(page, currentFlow);
    }
}

public record NavigationBag(UIElement Container, Page Content);
