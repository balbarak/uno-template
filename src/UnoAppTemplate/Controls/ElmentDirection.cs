using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnoAppTemplate.Controls;

public enum ElementFlowDirection
{
    LeftToRight,
    RightToLeft
}

public partial class ElementDirection : DependencyObject
{
    public static readonly DependencyProperty FlowProperty =
          DependencyProperty.RegisterAttached("Flow", typeof(ElementFlowDirection), typeof(ElementDirection), new PropertyMetadata(ElementFlowDirection.LeftToRight, OnFlowChanged));


    public static void SetFlow(UIElement element, ElementFlowDirection value)
    {
        if (element == null)
            return;

        var currentValue = GetFlow(element);

        if (currentValue == value)
            return;

        element.SetValue(FlowProperty, value);
    }

    public static ElementFlowDirection GetFlow(UIElement element)
    {
        if (element == null)
            return ElementFlowDirection.LeftToRight;

        return (ElementFlowDirection)element.GetValue(FlowProperty);
    }

    private static void OnFlowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var element = d as UIElement;

        if (element == null)
            return;

        var dir = (ElementFlowDirection)e.NewValue;

        ChangeDirection(element, dir);
    }

    private static void ChangeDirection(UIElement element, ElementFlowDirection dir)
    {
        var grids = new List<Grid>();
        var stackPanels = new List<StackPanel>();
        var frameWorkElements = new List<FrameworkElement>();
        var textBlocks = new List<TextBlock>();
        var textBoxes = new List<TextBox>();
        var controls = new List<Control>();

        element = element as Page;

        if (element is DependencyObject pageDp)
        {
            var childs = GetAllChildControls(pageDp);

            grids = childs.Where(a => a is Grid).Cast<Grid>().ToList();
            textBlocks = childs.Where(a => a is TextBlock).Cast<TextBlock>().ToList();
            frameWorkElements = childs.Where(a => a is FrameworkElement).Cast<FrameworkElement>().ToList();
            controls = childs.Where(a => a is Control).Cast<Control>().ToList();
            textBoxes = childs.Where(a => a is TextBox).Cast<TextBox>().ToList();
        }

        grids.ForEach(ReverseGridChilds);
        frameWorkElements.ForEach(ReverseMarginsAndHorizontalAlignment);
        textBlocks.ForEach(ReverseTextBlock);
        controls.ForEach(ReversePadding);
        textBoxes.ForEach(ReverseTextBoxes);

        //stackPanels.ForEach(ReverseStackPanel);
    }

    private static void ReverseGridChilds(Grid grid)
    {
        var childs = grid.Children;

        var colCount = grid.ColumnDefinitions.Count;

        var colDefs = grid.ColumnDefinitions.ToList();

        colDefs = Enumerable.Reverse(colDefs).ToList();

        grid.ColumnDefinitions.Clear();

        foreach (var item in colDefs)
        {
            grid.ColumnDefinitions.Add(item);
        }

        if (colCount <= 0)
            return;

        var array = Enumerable.Range(0, colCount).ToArray();
        array = Enumerable.Reverse(array).ToArray();

        foreach (var item in childs)
        {
            var frame = item as FrameworkElement;

            var colNo = Grid.GetColumn(frame);
            var colSpan = Grid.GetColumnSpan(frame);

            if (colNo > array.Length - 1)
                continue;

            int targetVale = array[colNo];

            if (colSpan > 1)
            {
                Grid.SetColumnSpan(frame, colSpan);

                int targetValue = targetVale / colSpan;

                Grid.SetColumn(frame, targetValue);
            }
            else
            {
                Grid.SetColumn(frame, targetVale);
            }
        }
    }

    private static void ReverseStackPanel(StackPanel panel)
    {
        if (panel.Orientation != Orientation.Horizontal)
            return;

        var childs = panel.Children.ToArray();

        childs = Enumerable.Reverse(childs).ToArray();

        panel.Children.Clear();

        foreach (var item in childs)
        {
            panel.Children.Add(item);
        }
    }

    private static void ReverseMarginsAndHorizontalAlignment(FrameworkElement element)
    {
        var top = element.Margin.Top;
        var bottom = element.Margin.Bottom;
        var left = element.Margin.Left;
        var right = element.Margin.Right;

        element.Margin = new Thickness(right, top, left, bottom);

        if (element.HorizontalAlignment == HorizontalAlignment.Left)
        {
            element.HorizontalAlignment = HorizontalAlignment.Right;
        }
        else if (element.HorizontalAlignment == HorizontalAlignment.Right)
        {
            element.HorizontalAlignment = HorizontalAlignment.Left;
        }
    }

    private static void ReversePadding(Control element)
    {
        var top = element.Padding.Top;
        var bottom = element.Padding.Bottom;
        var left = element.Padding.Left;
        var right = element.Padding.Right;

        element.Padding = new Thickness(right, top, left, bottom);
    }

    private static void ReverseTextBoxes(TextBox textBox)
    {
        var alignment = textBox.TextAlignment;

        var shouldNotChanged = alignment == TextAlignment.Center || alignment == TextAlignment.Justify;

        if (shouldNotChanged)
            return;

        var isLeft = alignment == TextAlignment.Left || alignment == TextAlignment.Start;
        var isRight = alignment == TextAlignment.Right || alignment == TextAlignment.End;


#if ANDROID
        if (isLeft)
        {
            textBox.LayoutDirection = Android.Views.LayoutDirection.Rtl;
            textBox.TextDirection = Android.Views.TextDirection.Rtl;
        }
        else
        {
            textBox.LayoutDirection = Android.Views.LayoutDirection.Ltr;
            textBox.TextDirection = Android.Views.TextDirection.Ltr;
        }

#else

        if (isLeft)
        {
            textBox.TextAlignment = TextAlignment.Right;
        }
        else if (isRight)
        {
            textBox.TextAlignment = TextAlignment.Left;
        }
#endif
    }

    private static void ReverseTextBlock(TextBlock text)
    {
        if (text.HorizontalAlignment == HorizontalAlignment.Center)
            return;

        var textAlignment = text.TextAlignment;

        var isLeft = textAlignment == TextAlignment.Left;
        var isRight = textAlignment == TextAlignment.Right;

#if ANDROID
        //text.TextAlignment = TextAlignment.Start;

        if (isLeft)
        {
            text.TextDirection = Android.Views.TextDirection.Rtl;
            text.HorizontalAlignment = HorizontalAlignment.Right;

        }
        else if (isRight)
        {
            text.TextDirection = Android.Views.TextDirection.Ltr;
            text.HorizontalAlignment = HorizontalAlignment.Left;
        }

#else
        if (isLeft)
        {
            text.TextAlignment = TextAlignment.Right;

        }
        else if (isRight)
        {
            text.TextAlignment = TextAlignment.Left;
        }
#endif

    }

    public static void FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
    {
        int count = VisualTreeHelper.GetChildrenCount(startNode);
        for (int i = 0; i < count; i++)
        {
            DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
            if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
            {
                T asType = (T)current;
                results.Add(asType);
            }
            FindChildren<T>(results, current);
        }
    }

    public static List<UIElement> GetAllChildControls(DependencyObject parent)
    {
        List<UIElement> controls = new List<UIElement>();

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);

            if (child is UIElement)
            {
                controls.Add(child as UIElement);
            }

            controls.AddRange(GetAllChildControls(child));
        }

        return controls;
    }
}
