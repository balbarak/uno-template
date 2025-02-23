using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate.Controls;
public partial class LottiePlayer : Control
{
    private AnimatedVisualPlayer _player;

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(nameof(Source), typeof(string), typeof(LottiePlayer), new PropertyMetadata(null));
    public string Source { get => (string)GetValue(SourceProperty); set => SetValue(SourceProperty, value); }

    public LottiePlayer()
    {
        DefaultStyleKey = nameof(LottiePlayer);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _player = GetTemplateChild("PART_Player") as AnimatedVisualPlayer;
    }

    public async Task Play()
    {
        if (_player == null)
            return;

        await _player.PlayAsync(0, 100, false);
    }
}
