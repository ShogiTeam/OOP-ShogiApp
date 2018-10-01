using System.Windows.Controls;
using System.Windows.Media;

namespace Shogi_v_1_0
{
    class Hints
    {
        //public Hints() { }

        static public void HintsOn(Piece activePiece, Field[,] Field)
        {
            //Check all fields in a direction if movement of active Piece is generally allowed and which color the field has
            //if the Field is empty (allegiance value 2), highlight it and give permission on field then check next field in same direction
            //if field has different color than active piece, set hint and give permission, then check next direction            
            //if movement is not allowed or field has same color as active piece, check next direction

            int activePieceRow = Grid.GetRow(activePiece);
            int activePieceColumn = Grid.GetColumn(activePiece);
            int fieldRow = activePieceRow;//keeping activePieceRow for reset purposes
            int fieldColumn = activePieceColumn;//keeping activePiececolumn for reset purposes

            //all fields in straight line downwards
            for (fieldRow = activePieceRow + 1; fieldRow < 10; fieldRow++)
            {
                if (activePiece.IsMoveAllowed(fieldRow, activePieceColumn))//if direction is still allowed
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow, activePieceColumn].SetPermission(true);
                        Field[fieldRow, activePieceColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;//check next direction if the field is occupied
                    }
                    else break;//check next direction if there's a piece of the same color already
                }
                else break;//check next direction if this one is not allowed anymore
            }

            //all fields in straight line upwards
            for (fieldRow = activePieceRow - 1; fieldRow > 0; fieldRow--)
            {
                if (activePiece.IsMoveAllowed(fieldRow, activePieceColumn))//if direction is still allowed
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow, activePieceColumn].SetPermission(true);
                        Field[fieldRow, activePieceColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;//check next direction if the field is occupied
                    }
                    else break;
                }
                else break;
            }

            fieldRow = activePieceRow;//reset row position
                                      //all fields in straight line to the right
            for (fieldColumn = activePieceColumn + 1; fieldColumn < 10; fieldColumn++)
            {
                if (activePiece.IsMoveAllowed(activePieceRow, fieldColumn))//if direction is still allowed
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())//if there is no piece of the same color already
                    {
                        Field[activePieceRow, fieldColumn].SetPermission(true);
                        Field[activePieceRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;//check next direction if the field is occupied
                    }
                    else break;//check next direction if there's a piece of the same color already
                }
                else break;//check next direction if this one is not allowed anymore
            }

            //all fields in straight line to the left
            for (fieldColumn = activePieceColumn - 1; fieldColumn > 0; fieldColumn--)
            {
                if (activePiece.IsMoveAllowed(activePieceRow, fieldColumn))//if direction is still allowed
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())//if there is no piece of the same color already
                    {
                        Field[activePieceRow, fieldColumn].SetPermission(true);
                        Field[activePieceRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;//check next direction if the field is occupied
                    }
                    else break;//check next direction if there's a piece of the same color already
                }
                else break;//check next direction if this one is not allowed anymore
            }

            //all fields diagonally right downwards
            for (fieldColumn = activePieceColumn + 1; fieldColumn < 10; fieldColumn++)
            {
                if (fieldRow < 9) fieldRow++;
                else break;

                if (activePiece.IsMoveAllowed(fieldRow, fieldColumn))//if direction is still allowed
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow, fieldColumn].SetPermission(true);
                        Field[fieldRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;//check next direction if the field is occupied
                    }
                    else break;//check next direction if there's a piece of the same color already
                }
                else break;//check next direction if this one is not allowed anymore
            }

            fieldRow = activePieceRow;//reset row position
                                      //all fields diagonally right upwards
            for (fieldColumn = activePieceColumn + 1; fieldColumn < 10; fieldColumn++)
            {
                if (fieldRow > 1) fieldRow--;
                else break;

                if (activePiece.IsMoveAllowed(fieldRow, fieldColumn))//if direction is still allowed
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow, fieldColumn].SetPermission(true);
                        Field[fieldRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;//check next direction if the field is occupied
                    }
                    else break;//check next direction if there's a piece of the same color already
                }
                else break;//check next direction if this one is not allowed anymore
            }

            fieldRow = activePieceRow;//reset row position
                                      //all fields diagonally left downwards
            for (fieldColumn = activePieceColumn - 1; fieldColumn > 0; fieldColumn--)
            {
                if (fieldRow < 9) fieldRow++;
                else break;

                if (activePiece.IsMoveAllowed(fieldRow, fieldColumn))//if direction is still allowed
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow, fieldColumn].SetPermission(true);
                        Field[fieldRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;//check next direction if the field is occupied
                    }
                    else break;//check next direction if there's a piece of the same color already
                }
                else break;//check next direction if this one is not allowed anymore
            }

            fieldRow = activePieceRow;//reset row position
                                      //all fields diagonally left upwards
            for (fieldColumn = activePieceColumn - 1; fieldColumn > 0; fieldColumn--)
            {
                if (fieldRow > 1) fieldRow--;
                else break;

                if (activePiece.IsMoveAllowed(fieldRow, fieldColumn))//if direction is still allowed
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow, fieldColumn].SetPermission(true);
                        Field[fieldRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;//check next direction if the field is occupied
                    }
                    else break;//check next direction if there's a piece of the same color already
                }
                else break;//check next direction if this one is not allowed anymore
            }

            fieldRow = activePieceRow;//reset row position
            fieldColumn = activePieceColumn;//reset column position
                                            //Knight specific fields
            if (activePiece.GetColor() == 0)//if black
            {
                if (activePiece.IsMoveAllowed(fieldRow - 2, fieldColumn + 1))//Checking 2 up 1 right
                {
                    if (activePiece.GetColor() != Field[fieldRow - 2, fieldColumn + 1].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow - 2, fieldColumn + 1].SetPermission(true);
                        Field[fieldRow - 2, fieldColumn + 1].Background = Brushes.Tomato;
                    }
                }
                if (activePiece.IsMoveAllowed(fieldRow - 2, fieldColumn - 1))//Checking 2 up 1 left
                {
                    if (activePiece.GetColor() != Field[fieldRow - 2, fieldColumn - 1].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow - 2, fieldColumn - 1].SetPermission(true);
                        Field[fieldRow - 2, fieldColumn - 1].Background = Brushes.Tomato;
                    }
                }
            }
            else//if white
            {
                if (activePiece.IsMoveAllowed(fieldRow + 2, fieldColumn + 1))//Checking 2 down 1 right
                {
                    if (activePiece.GetColor() != Field[fieldRow + 2, fieldColumn + 1].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow + 2, fieldColumn + 1].SetPermission(true);
                        Field[fieldRow + 2, fieldColumn + 1].Background = Brushes.Tomato;
                    }
                }
                if (activePiece.IsMoveAllowed(fieldRow + 2, fieldColumn - 1))//Checking 2 down 1 left
                {
                    if (activePiece.GetColor() != Field[fieldRow + 2, fieldColumn - 1].GetColor())//if there is no piece of the same color already
                    {
                        Field[fieldRow + 2, fieldColumn - 1].SetPermission(true);
                        Field[fieldRow + 2, fieldColumn - 1].Background = Brushes.Tomato;
                    }
                }
            }
        }

        static public void HintsCapturedPieceOn(Field[,] Field)
        {
            int Row;
            int Column;

            for (Row = 1; Row < 10; Row++)
            {
                for (Column = 1; Column < 10; Column++)
                {
                    if (Field[Row, Column].GetColor() == 2)
                    {
                        Field[Row, Column].Background = Brushes.Tomato;
                        Field[Row, Column].SetPermission(true);
                    }
                }
            }
        }

        static public void HintsOff(Field[,] Field)
        {
            int Row;
            int Column;

            for (Row = 1; Row < 10; Row++)
            {
                for (Column = 1; Column < 10; Column++)
                {
                    if (Field[Row, Column].GetPermission())
                    {
                        Field[Row, Column].Background = Brushes.LightGray;
                        Field[Row, Column].SetPermission(false);
                    }
                }
            }
        }
    }
}


