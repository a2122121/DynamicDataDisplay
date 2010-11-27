﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Microsoft.Research.DynamicDataDisplay.Charts;
using System.Windows.Shapes;
using Microsoft.Research.DynamicDataDisplay.Charts.Legend_items;

namespace Microsoft.Research.DynamicDataDisplay.Markers2
{
	public abstract class LineChartBase : PointChartBase
	{
		static LineChartBase()
		{
			Type thisType = typeof(LineChartBase);

			Legend.DescriptionProperty.OverrideMetadata(thisType, new FrameworkPropertyMetadata("LineChart"));
			Legend.LegendItemsBuilderProperty.OverrideMetadata(thisType, new FrameworkPropertyMetadata(new LegendItemsBuilder(DefaultLegendItemsBuilder)));
		}

		private static IEnumerable<FrameworkElement> DefaultLegendItemsBuilder(IPlotterElement element)
		{
			LineChartBase lineChart = (LineChart)element;

			Line line = new Line
			{
				X1 = 0,
				Y1 = 10,
				X2 = 20,
				Y2 = 0,
				Stretch = Stretch.Fill,
				DataContext = lineChart
			};
			line.SetBinding(Line.StrokeProperty, "Stroke");
			line.SetBinding(Line.StrokeThicknessProperty, "StrokeThickness");
			line.SetBinding(Line.StrokeDashArrayProperty, "StrokeDashArray");
			Legend.SetVisualContent(lineChart, line);

			var legendItem = LegendItemsHelper.BuildDefaultLegendItem(lineChart);
			yield return legendItem;
		}

		#region Stroke property

		/// <summary>
		/// Gets or sets the stroke of lines. This is a DependencyProperty.
		/// </summary>
		/// <value>The stroke.</value>
		public Brush Stroke
		{
			get { return (Brush)GetValue(StrokeProperty); }
			set { SetValue(StrokeProperty, value); }
		}

		public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
		  "Stroke",
		  typeof(Brush),
		  typeof(LineChartBase),
		  new FrameworkPropertyMetadata(Brushes.Red));

		#endregion

		#region StrokeThickness property

		/// <summary>
		/// Gets or sets the stroke thickness of lines. This is a DependencyProperty.
		/// </summary>
		/// <value>The stroke thickness.</value>
		public double StrokeThickness
		{
			get { return (double)GetValue(StrokeThicknessProperty); }
			set { SetValue(StrokeThicknessProperty, value); }
		}

		public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
		  "StrokeThickness",
		  typeof(double),
		  typeof(LineChartBase),
		  new FrameworkPropertyMetadata(1.0));

		#endregion

		#region StrokeDashArray property

		/// <summary>
		/// Gets or sets the stroke dash array. This is a DependencyProperty.
		/// </summary>
		/// <value>The stroke dash array.</value>
		public DoubleCollection StrokeDashArray
		{
			get { return (DoubleCollection)GetValue(StrokeDashArrayProperty); }
			set { SetValue(StrokeDashArrayProperty, value); }
		}

		public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(
		  "StrokeDashArray",
		  typeof(DoubleCollection),
		  typeof(LineChartBase),
		  new FrameworkPropertyMetadata(null));

		#endregion
	}
}
