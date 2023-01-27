using System.Text.RegularExpressions;


class Cell {
    public int x { get; set; }
    public int y { get; set; }

    public CellValue cellValue { get; set; }

    public Cell(int x, int y, CellValue cellValue) {
        this.x = x;
        this.y = y;
        this.cellValue = cellValue;
    }
}

enum CellValue {
    O = 'O',
    X = 'X',
    NONE = '?'
}

class Grid {
    public Cell[,] cells;

    public Grid(int rootLen) {
        //create cell array
        cells = new Cell[rootLen, rootLen];

        //fill array with cells
        for (int x = 0; x < rootLen; x++) {
            for (int y = 0; y < rootLen; y++) {
                cells[x, y] = new Cell(x, y, CellValue.NONE);
            }
        }

        // foreach (Cell cell in cells) {
        //     Console.WriteLine(cell);
        // }
    }

    public void display() {
        for (int x = 0; x < cells.GetLength(0); x++) {
            for (int y = 0; y < cells.GetLength(1); y++) {
                Console.Write($"{(char) cells[x, y].cellValue} ");
            }

            Console.WriteLine();
        }
    }

    public void turn(bool O) {
        //asking for input
        Console.Write("Please enter x and y values for your choice.");
        string choice = Console.ReadLine();

        //regex accepting only valid cell x,y
        Regex regex = new Regex(@$"^[1-{cells.GetLength(0)}],[1-{cells.GetLength(0)}]$", RegexOptions.Compiled);

        //first coords
        int x = 0;
        int y = 0;

        Console.WriteLine("?");

        //user retry loop
        while (!regex.IsMatch(choice) && cells[x, y] != null) {
            Console.Write("Wrong values. Please enter x and y values for your choice.");
            choice = Console.ReadLine();

            //get x,y values
            x = Int32.Parse(choice.Split(",")[0]) - 1;
            y = Int32.Parse(choice.Split(",")[1]) - 1;

            Console.WriteLine(x + "" + y);

            if (cells[x, y].cellValue != CellValue.NONE) {
                choice = "";
                continue;
            };
        }

        //set cellvalue
        cells[x, y].cellValue = O ? CellValue.O : CellValue.X;

        //show grid
        display();
    }
}

class Program {
    static void Main() {
        //create and display grid
        bool O = false;
        Grid grid = new Grid(3);
        grid.display();

        while (true) {
            grid.turn(O);
            O = !O;
        }
    }
}