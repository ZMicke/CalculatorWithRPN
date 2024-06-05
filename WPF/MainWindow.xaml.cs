using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using RPN.Logika; 


namespace MathExpressionApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateAndDrawButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double startRange = double.Parse(StartRangeTextBox.Text);
                double endRange = double.Parse(EndRangeTextBox.Text);
                double step = double.Parse(StepTextBox.Text) / 5;
                double scale = double.Parse(ScaleTextBox.Text);

                GraphCanvas.Children.Clear();

                double centerX = GraphCanvas.ActualWidth / 2;
                double centerY = GraphCanvas.ActualHeight / 2;

                DrawCoordinateGrid(centerX, centerY, scale);

                string selectedFunction = ExpressionTextBox.Text.Trim();

                Polyline polyline = new Polyline
                {
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };

                for (double x = startRange; x <= endRange; x += step)
                {
                    double y = EvaluateExpression(selectedFunction, x);
                    double canvasX = centerX + x * scale;
                    double canvasY = centerY - y * scale;

                    if (canvasX >= 0 && canvasX <= GraphCanvas.ActualWidth && canvasY >= 0 && canvasY <= GraphCanvas.ActualHeight)
                    {
                        polyline.Points.Add(new Point(canvasX, canvasY));
                        DrawPoint(canvasX, canvasY);
                    }
                }

                GraphCanvas.Children.Add(polyline);
            }
            catch (Exception ex)
            {
                ResultLabel.Content = $"Ошибка: {ex.Message}";
            }
        }

        
        private double EvaluateExpression(string expression, double x)
        {
            var rpn = InfixToRPNConverter.Convert(expression);
            
            return RPNCalculator.Calculate(rpn, x);
        }

        private void DrawCoordinateGrid(double centerX, double centerY, double scale)
        {
            DrawArrow(centerX, centerY, GraphCanvas.ActualWidth, centerY, Brushes.Black); // X axis
            DrawLine(centerX, centerY, 0, centerY, Brushes.Black); // X 


            DrawArrow(centerX, centerY, centerX, 0, Brushes.Black); // Y 
            DrawLine(centerX, centerY, centerX, GraphCanvas.ActualHeight, Brushes.Black); // Y axis


            for (double x = -10; x <= 10; x++)
            {
                double canvasX = centerX + x * scale;
                if (canvasX >= 0 && canvasX <= GraphCanvas.ActualWidth)
                {
                    Line tickMark = new Line
                    {
                        Stroke = Brushes.Black,
                        X1 = canvasX,
                        Y1 = centerY - 5,
                        X2 = canvasX,
                        Y2 = centerY + 5,
                        StrokeThickness = 1
                    };
                    GraphCanvas.Children.Add(tickMark);

                    TextBlock label = new TextBlock
                    {
                        Text = x.ToString(),
                        Foreground = Brushes.Black,
                        FontSize = 10
                    };
                    Canvas.SetLeft(label, canvasX - 8);
                    Canvas.SetTop(label, centerY + 8);
                    GraphCanvas.Children.Add(label);
                }
            }


            for (double y = -10; y <= 10; y++)
            {
                double canvasY = centerY - y * scale;
                if (canvasY >= 0 && canvasY <= GraphCanvas.ActualHeight)
                {
                    Line tickMark = new Line
                    {
                        Stroke = Brushes.Black,
                        X1 = centerX - 5,
                        Y1 = canvasY,
                        X2 = centerX + 5,
                        Y2 = canvasY,
                        StrokeThickness = 1
                    };
                    GraphCanvas.Children.Add(tickMark);

                    TextBlock label = new TextBlock
                    {
                        Text = y.ToString(),
                        Foreground = Brushes.Black,
                        FontSize = 10
                    };
                    Canvas.SetLeft(label, centerX - 20);
                    Canvas.SetTop(label, canvasY - 8);
                    GraphCanvas.Children.Add(label);
                }
            }
        }

        private void DrawPoint(double canvasX, double canvasY)
        {
            Ellipse point = new Ellipse
            {
                Width = 4,
                Height = 4,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(point, canvasX - 2);
            Canvas.SetTop(point, canvasY - 2);
            GraphCanvas.Children.Add(point);
        }
        
        private void DrawArrow(double startX, double startY, double endX, double endY, Brush color)
        {
            DrawLine(startX, startY, endX, endY, color);

            double angle = Math.Atan2(endY - startY, endX - startX);


            Point arrowTip1 = new Point(
                endX - 10 * Math.Cos(angle - Math.PI / 6),
                endY - 10 * Math.Sin(angle - Math.PI / 6)
            );
            Line arrowLine1 = new Line
            {
                Stroke = color,
                X1 = endX,
                Y1 = endY,
                X2 = arrowTip1.X,
                Y2 = arrowTip1.Y,
                StrokeThickness = 1
            };
            GraphCanvas.Children.Add(arrowLine1);

            Point arrowTip2 = new Point(
                endX - 10 * Math.Cos(angle + Math.PI / 6),
                endY - 10 * Math.Sin(angle + Math.PI / 6)
            );
            Line arrowLine2 = new Line
            {
                Stroke = color,
                X1 = endX,
                Y1 = endY,
                X2 = arrowTip2.X,
                Y2 = arrowTip2.Y,
                StrokeThickness = 1
            };
            GraphCanvas.Children.Add(arrowLine2);
        }
        private void DrawLine(double startX, double startY, double endX, double endY, Brush color)
        {
            Line line = new Line
            {
                Stroke = color,
                X1 = startX,
                Y1 = startY,
                X2 = endX,
                Y2 = endY,
                StrokeThickness = 1
            };
            GraphCanvas.Children.Add(line);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GraphCanvas.Children.Clear(); 
        }
    }
}
