using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;

namespace UnoAppTemplate.Controls;

public enum AlertType
{
    Brand,
    Success,
    Warning,
    Error
}

public partial class AlertModal : Control
{
    private Storyboard _closeStoryboard;
    private SemaphoreSlim _slim;
    private TaskCompletionSource _ctk;
    private LottiePlayer _lottiePlayer;

    public static readonly DependencyProperty CloseCommandProperty =
        DependencyProperty.Register(nameof(CloseCommand), typeof(ICommand), typeof(AlertModal), new PropertyMetadata(null));
    public ICommand CloseCommand { get => (ICommand)GetValue(CloseCommandProperty); set => SetValue(CloseCommandProperty, value); }

    public static readonly DependencyProperty IsBackdropProperty =
        DependencyProperty.Register(nameof(IsBackdrop), typeof(bool), typeof(AlertModal), new PropertyMetadata(null));
    public bool IsBackdrop { get => (bool)GetValue(IsBackdropProperty); set => SetValue(IsBackdropProperty, value); }


    public static readonly DependencyProperty CloseTextProperty =
        DependencyProperty.Register(nameof(CloseText), typeof(string), typeof(AlertModal), new PropertyMetadata(null));
    public string CloseText { get => (string)GetValue(CloseTextProperty); set => SetValue(CloseTextProperty, value); }


    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(AlertModal), new PropertyMetadata(null));
    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }


    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(string), typeof(AlertModal), new PropertyMetadata(null));
    public string Message { get => (string)GetValue(MessageProperty); set => SetValue(MessageProperty, value); }


    public AlertModal()
    {
        _slim = new SemaphoreSlim(1, 1);

        DefaultStyleKey = typeof(AlertModal);
        CloseText = "Close";
        CloseCommand = new RelayCommand(Close);
    }


    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _closeStoryboard = GetTemplateChild("PART_CloseStoryboard") as Storyboard;
        _lottiePlayer = GetTemplateChild("PART_LottiePlayer") as LottiePlayer;

        if (_closeStoryboard != null)
        {
            _closeStoryboard.Completed += OnCloseStoryboardCompleted;
        }
    }

    private void OnCloseStoryboardCompleted(object sender, object e)
    {
        _ctk?.TrySetResult();

        _slim.Release();

    }

    public async Task ShowModal(string title, string msg, AlertType type = AlertType.Brand)
    {
        await _slim.WaitAsync();

        SetErrorState(type);

        Title = title;
        Message = msg;

        _ctk = new TaskCompletionSource();

        VisualStateManager.GoToState(this, "ShowState", true);

        await _lottiePlayer.Play();

        await _ctk.Task;
    }

    private void Close()
    {
        VisualStateManager.GoToState(this, "HideState", true);
    }

    private void SetErrorState(AlertType type)
    {
        switch (type)
        {
            case AlertType.Success:

                VisualStateManager.GoToState(this, "SuccessState", false);

                break;
            case AlertType.Error:

                VisualStateManager.GoToState(this, "ErrorState", false);

                break;
            default:

                VisualStateManager.GoToState(this, "NormalState", false);

                break;
        }

    }
}
