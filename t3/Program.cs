

// Definirea Sudoku de rezolvat
Console.WriteLine("Introduceti Sudoku-ul initial (utilizati '.' pentru celule goale):");

char[,] sudoku = ReadSudokuFromConsole();
Console.WriteLine("Sudoku initial:");
PrintSudoku(sudoku);

if (SolveSudoku(sudoku))
    {
        Console.WriteLine("Sudoku rezolvat:");
        PrintSudoku(sudoku);
    }
    else
    {
        Console.WriteLine("Nu se poate rezolva Sudoku-ul.");
    }

    Console.ReadLine();
    
static char[,] ReadSudokuFromConsole()
{
    char[,] sudoku = new char[9, 9];

    for (int row = 0; row < 9; row++)
    {
        Console.Write($"Randul {row + 1}: ");
        string input = Console.ReadLine();

        for (int col = 0; col < 9; col++)
        {
            sudoku[row, col] = input[col];
        }
    }

    return sudoku;
}

static bool SolveSudoku(char[,] sudoku)
{
    int row, col;

    // Verificăm dacă mai sunt celule goale
    if (!FindEmptyCell(sudoku, out row, out col))
        return true; // Sudoku este deja rezolvat

    // Încercăm să umplem celula goală cu o cifră
    for (char num = '1'; num <= '9'; num++)
    {
        if (IsSafe(sudoku, row, col, num))
        {
            sudoku[row, col] = num;

            // Recursivitate pentru a rezolva restul Sudoku-ului
            if (SolveSudoku(sudoku))
                return true;

            // Dacă soluția nu este validă, anulăm ultima atribuire
            sudoku[row, col] = '.';
        }
    }

    return false; // Nu s-a putut găsi nicio soluție validă
}

static bool FindEmptyCell(char[,] sudoku, out int row, out int col)
{
    int size = sudoku.GetLength(0);

    for (row = 0; row < size; row++)
    {
        for (col = 0; col < size; col++)
        {
            if (sudoku[row, col] == '.')
                return true;
        }
    }

    row = -1;
    col = -1;
    return false;
}

static bool IsSafe(char[,] sudoku, int row, int col, char num)
{
    return !UsedInRow(sudoku, row, num) &&
           !UsedInColumn(sudoku, col, num) &&
           !UsedInBox(sudoku, row - row % 3, col - col % 3, num);
}

static bool UsedInRow(char[,] sudoku, int row, char num)
{
    int size = sudoku.GetLength(0);

    for (int col = 0; col < size; col++)
    {
        if (sudoku[row, col] == num)
            return true;
    }

    return false;
}

static bool UsedInColumn(char[,] sudoku, int col, char num)
{
    int size = sudoku.GetLength(0);

    for (int row = 0; row < size; row++)
    {
        if (sudoku[row, col] == num)
            return true;
    }

    return false;
}

static bool UsedInBox(char[,] sudoku, int boxStartRow, int boxStartCol, char num)
{
    int boxSize = 3;

    for (int row = 0; row < boxSize; row++)
    {
        for (int col = 0; col < boxSize; col++)
        {
            if (sudoku[row + boxStartRow, col + boxStartCol] == num)
                return true;
        }
    }

    return false;
}

static void PrintSudoku(char[,] sudoku)
{
    int size = sudoku.GetLength(0);

    for (int row = 0; row < size; row++)
    {
        for (int col = 0; col < size; col++)
        {
            Console.Write(sudoku[row, col] + " ");
        }
        Console.WriteLine();
    }
}
    
