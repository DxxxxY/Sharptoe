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

    public Regex regex;

    public Grid(int rootLen) {
        //create cell array
        cells = new Cell[rootLen, rootLen];

        //compile regex
        regex = new Regex(@$"^[1-{cells.GetLength(0)}],[1-{cells.GetLength(0)}]$", RegexOptions.Compiled);

        //fill array with cells
        for (int x = 0; x < rootLen; x++) {
            for (int y = 0; y < rootLen; y++) {
                cells[x, y] = new Cell(x, y, CellValue.NONE);
            }
        }
    }

    public void display() {
        Console.WriteLine();

        for (int x = 0; x < cells.GetLength(0); x++) {
            for (int y = 0; y < cells.GetLength(1); y++) {
                Console.Write($"{(char) cells[x, y].cellValue} ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public void turn(bool O) {
        //ask for input
        Console.WriteLine("Please enter (x,y) values for your choice.");
        string choice = Console.ReadLine();

        //user retry loop
        while (!validChoice(choice, O)) {
            Console.WriteLine("Wrong values. Please enter (x,y) values for your choice.");
            choice = Console.ReadLine();
        }

        //show grid
        display();
    }

    public bool validChoice(string choice, bool O) {
        //avoid invalid input
        if (!regex.IsMatch(choice)) return false;    

        //get x and y values
        int x = Int32.Parse(choice.Split(",")[0]) - 1;
        int y = Int32.Parse(choice.Split(",")[1]) - 1;

        //avoid out of bounds
        if (cells[x, y] == null) return false;

        //avoid replacing a cell
        if (cells[x, y].cellValue != CellValue.NONE) return false;

        //set cellvalue
        cells[x, y].cellValue = O ? CellValue.O : CellValue.X;

        return true; //end loop and then turn
    }

    public bool isOngoing() {
        CellValue cellValue;

        //check for horizontal win
        for (int x = 0; x < cells.GetLength(0); x++) {
            //get first cell value
            cellValue = cells[x, 0].cellValue;

            if (cellValue == CellValue.NONE) continue;

            //check if all cells have the same value as first cell (y0)
            for (int y = 1; y < cells.GetLength(1); y++) {
                if (cells[x, y].cellValue != cellValue) break;
                if (y == cells.GetLength(1) - 1) {
                    Console.WriteLine($"{(char) cellValue}'s player wins!");
                    return false;
                }
            }
        }

        //check for vertical win
        for (int y = 0; y < cells.GetLength(1); y++) {
            //get first cell value
            cellValue = cells[0, y].cellValue;

            if (cellValue == CellValue.NONE) continue;

            //check if all cells have the same value as first cell (x0)
            for (int x = 1; x < cells.GetLength(0); x++) {
                if (cells[x, y].cellValue != cellValue) break;
                if (x == cells.GetLength(0) - 1) {
                    Console.WriteLine($"{(char) cellValue}'s player wins!");
                    return false;
                }
            }
        }

        //get first cell value
        cellValue = cells[0, 0].cellValue;

        if (cellValue != CellValue.NONE) {
            //check if all cells have the same value as first cell (x0, y0)
            for (int i = 1; i < cells.GetLength(0); i++) {
                if (cells[i, i].cellValue != cellValue) break;
                if (i == cells.GetLength(0) - 1) {
                    Console.WriteLine($"{(char) cellValue}'s player wins!");
                    return false;
                }
            }
        }

        return true;
    }
}

class Program {
    public static void Main() {
        //X starts first
        bool O = false;

        //create grid
        Grid grid = new Grid(3);

        //display grid
        grid.display();

        //game loop
        while (grid.isOngoing()) {
            //display whose turn it is
            Console.WriteLine("It is " + (O ? "O" : "X") + "'s turn.");

            //play the turn
            grid.turn(O);

            //switch player
            O = !O;
        }
    }
}