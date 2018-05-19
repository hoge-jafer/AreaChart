using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AreaChart
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ChartRenderer _chartRenderer;
        private const float DataStrokeThickness = 1;
        private readonly List<double> _data = new List<double>();
        private readonly Random _rand = new Random();
        private int DataPointsPerFrame =>(int)ValuesPerFrameSlider.Value;
        private double _lastValue = 0.5;
        public MainPage()
        {
            this.InitializeComponent();
            _chartRenderer = new ChartRenderer();
        }

        private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            for (int i = 0; i < DataPointsPerFrame; i++)
            {
                var delta = _rand.NextDouble() * .1 - .05;
                _lastValue = Math.Max(0d, Math.Min(1d, _lastValue + delta));
                _data.Add(_lastValue);
            }
            if (Data.IsChecked == true)
                _chartRenderer.RenderData(canvas, args, Colors.Black, DataStrokeThickness, _data, renderArea: ChartShow.IsChecked == true);
            canvas.Invalidate();
            //_chartRenderer.RenderAveragesAsPieChart(canvas, args, pieValues, new List<Color>(new[] { Colors.DarkSalmon, Colors.Azure, Colors.LemonChiffon, Colors.Honeydew, Colors.Pink }));
        }
    }
}
