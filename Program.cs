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

    public override string ToString() {
        return $"Cell={{ x = {x}, y = {y} }}"; 
    }
}

enum CellValue {
    O = 'O',
    X = 'X'
}

class Grid {
    public Cell[,] cells;

    public Grid(int rootLen) {
        //create cell array
        cells = new Cell[rootLen, rootLen];

        //fill array with cells
        for (int x = 0; x < rootLen; x++) {
            for (int y = 0; y < rootLen; y++) {
                cells[x, y] = new Cell(x, y, CellValue.O);
            }
        }

        // foreach (Cell cell in cells) {
        //     Console.WriteLine(cell);
        // }
    }

    public void display() {
        for (int x = 0; x < cells.GetLength(0); x++) {
            for (int y = 0; y < cells.GetLength(1); y++) {
                Console.Write($"{cells[x, y].cellValue} ");
            }

            Console.WriteLine();
        }
    }
}

class Program {
    static void Main() {
        //create and display grid
        Grid grid = new Grid(3);
        grid.display();

        //asking for input
        Console.Write("Please enter x and y values for your choice.");
        string choice = Console.ReadLine();

        //regex accepting only valid cell x,y
        Regex regex = new Regex(@$"^[1-{grid.cells.GetLength(0)}],[1-{grid.cells.GetLength(0)}]$", RegexOptions.Compiled);

        //user retry loop
        while (!regex.IsMatch(choice)) {
            Console.Write("Wrong values. Please enter x and y values for your choice.");
            choice = Console.ReadLine(); 
        }

        //get x,y values
        int x = Int32.Parse(choice.Split(",")[0]);
        int y = Int32.Parse(choice.Split(",")[1]);
    }
}