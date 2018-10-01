using System.Windows.Controls;

public class PieceCounter : TextBlock
{

    int counter = 0;

    public PieceCounter(int row, int column)
    {
        Grid.SetColumn(this, column);
        Grid.SetRow(this, row);
        TextAlignment = System.Windows.TextAlignment.Right;
        VerticalAlignment = System.Windows.VerticalAlignment.Top;
        Text = "";
    }

    public void IncreaseCounter()
    {
        counter++;
        if (counter > 0)
        {
            Text = counter.ToString();
        }
        else
        {
            Text = "";
        }
    }

    public void DecreaseCounter()
    {
        counter--;
        if (counter > 0)
        {
            Text = counter.ToString();
        }
        else
        {
            Text = "";
        }
    }

    public void ResetCounter()
    {
        counter = 0;
        Text = "";
    }
}