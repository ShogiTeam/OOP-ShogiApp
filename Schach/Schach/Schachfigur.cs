using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    abstract class Schachfigur
    {
        int x;
        int y;
        char symbol;

        public Schachfigur(int x, int y, char symbol)
        {
            this.x = x;
            this.y = y;
            this.symbol = symbol;
        }

        protected int X { get { return x; } }
        protected int Y { get { return y; } }

        public abstract bool IstZugErlaubt(Schachfeld f);

        public override string ToString()
        {
            return symbol.ToString();
        }
        public static void SetzeKoordinaten(Schachfigur f, int x, int y)
        {
            f.x = x;
            f.y = y;
        }
    }

    abstract class Turm : Schachfigur
    {
        public Turm(int x, int y, char symbol) : base(x, y, symbol) { }
        public override bool IstZugErlaubt(Schachfeld f)
        {
            return f.X == X ^ f.Y == Y;
        }
    }

    class SchwarzerTurm : Turm
    {
        public SchwarzerTurm() : base(0, 0, '♜') { }
    }

    class WeißerTurm : Turm
    {
        public WeißerTurm() : base(0, 0, '♖') { }
    }

    abstract class Läufer : Schachfigur
    {
        public Läufer(int x, int y, char symbol) : base(x, y, symbol) { }
        public override bool IstZugErlaubt(Schachfeld f)
        {
            return f.Y == (f.X - X) + Y ^ f.Y == (X - f.X) + Y;
        }
    }

    class SchwarzerLäufer : Läufer
    {
        public SchwarzerLäufer() : base(0, 0, '♝') { }
    }

    class WeißerLäufer : Läufer
    {
        public WeißerLäufer() : base(0, 0, '♗') { }
    }

    abstract class König : Schachfigur
    {
        public König(int x, int y, char symbol) : base(x, y, symbol) { }
        public override bool IstZugErlaubt(Schachfeld f)
        {
            int dx = f.X - X;
            int dy = f.Y - Y;

            return (dx != 0 || dy != 0) && dx * dx + dy * dy <= 2;
        }
    }

    class SchwarzerKönig : König
    {
        public SchwarzerKönig() : base (0, 0, '♛') { }
    }

    class WeißerKönig : König
    {
        public WeißerKönig() : base(0, 0, '♕') { }
    }

    abstract class Springer : Schachfigur
    {
        public Springer(int x, int y, char symbol) : base(x, y, symbol) { }

        public override bool IstZugErlaubt(Schachfeld f)
        {
            int dx = f.X - X;
            int dy = f.Y - Y;

            return f.X != X && f.Y != Y && dx * dx + dy * dy == 5;
        }
    }

    class SchwarzerSpringer : Springer
    {
        public SchwarzerSpringer() : base(0, 0, '♞') { }
    }
    class WeißerSpringer : Springer
    {
        public WeißerSpringer() : base(0, 0, '♘') { }
    }
}
