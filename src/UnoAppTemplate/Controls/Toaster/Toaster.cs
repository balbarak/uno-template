using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;

namespace UnoAppTemplate.Controls;
public partial class Toaster  : Control
{
    private const int TOASTER_TIMER = 3000;
    private SemaphoreSlim _slim;
    private Button _closeButton;
    private Storyboard _closeStoryBoard;
    private TaskCompletionSource _taskCompletionSource;


    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(Toaster), new PropertyMetadata(null));
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

    public Toaster()
    {
        _slim = new SemaphoreSlim(1, 1);
        
        this.DefaultStyleKey = nameof(Toaster);
        
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _closeStoryBoard = GetTemplateChild("PART_CloseStoryboard") as Storyboard;
        _closeButton = GetTemplateChild("PART_CloseButton") as Button;

        if (_closeStoryBoard != null)
        {
            _closeStoryBoard.Completed += OnCloseStoryboardCompleted; ;
        }

        if (_closeButton != null)
        {
            _closeButton.Click += (obj, send) =>
            {
                HideInternal();
            };
        }
    }

    public async Task ShowToaster(string message)
    {
        try
        {
            await _slim.WaitAsync();

            _taskCompletionSource = new TaskCompletionSource();

            Text = message;

            ShowInternal();

            var waitTask = Task.Delay(TOASTER_TIMER);

            var closeTask = _taskCompletionSource.Task;

            await Task.WhenAny(waitTask, closeTask);

            HideInternal();
        }
        finally
        {
            _slim.Release();
        }
       
    }

    private void ShowInternal()
    {
        VisualStateManager.GoToState(this, "ShowState", true);
    }

    private void HideInternal(bool useTransition = true)
    {
        VisualStateManager.GoToState(this, "HideState", useTransition);
    }


    private void OnCloseStoryboardCompleted(object sender, object e)
    {
        _taskCompletionSource?.TrySetResult();
    }

}
