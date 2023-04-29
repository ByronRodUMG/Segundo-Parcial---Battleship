
int[,] tablero = new int[5, 5]; // Tamaño del tablero
int restantes = 4; // Número de barcos restantes
int tiros = 20; // Número de tiros restantes

void crear_tablero() // Llena el tablero con ceros
{
    for (int f = 0; f < tablero.GetLength(0); f++)
    {
        for (int c = 0; c < tablero.GetLength(1); c++)
        {
            tablero[f, c] = 0;
        }
    }
}

void colocar_barcos() // Genera aleatoriamente 4 barcos y los coloca en el tablero
{
    List<(int, int)> valores = new(); // Almacena los barcos generados para evitar duplicados
    Random ran = new();
    int fil, col;
    for (int i = 0; i < 4; i++) // Se repite cuatro veces
    {
        do
        {
            fil = ran.Next(0, 4); // Genera un número entre 0 y 4 para las filas
            col = ran.Next(0, 4); // Genera un número entre 0 y 4 para las columnas
        } while (valores.Contains((fil, col))); // Se repite si el barco generado ya existe en la lista
        valores.Add((fil, col)); // Añade el barco generado a la lista de valores
        tablero[fil, col] = 1; // Cambia el 0 por un 1
    }
}

void colocar_mina() // Genera aleatoriamente una mina y la coloca en el tablero
{
    Random ran = new();
    int fil, col;
    do
    {
        fil = ran.Next(0, 4);
        col = ran.Next(0, 4);
    } while (tablero[fil, col] == 1); // La mina generada no ocupa un espacio ocupado por un barco
    tablero[fil, col] = 3;
}

void imprimir_tablero() // Imprime el tablero en pantalla
{
    String caracter_imprimir;
    for (int f = 0; f < tablero.GetLength(0); f++)
    {
        for (int c = 0; c < tablero.GetLength(1); c++)
        {
            switch (tablero[f, c]) // Imprime un caracter diferente en base al valor del espacio
            {
                case 1: // Caso para los espacios con barcos que aun NO han sido revelados, para realizar pruebas
                    caracter_imprimir = "s";
                    break;
                case 2: // Caso para los espacios con barcos que SI han sido revelados
                    caracter_imprimir = "O";
                    break;
                case -1: // Caso para los espacios sin barcos que SI han sido revelados
                    caracter_imprimir = "X";
                    break;
                case 3:
                    caracter_imprimir = "m";
                    break;
                case 4:
                    caracter_imprimir = "M";
                    break;
                default: // Caso para los espacios sin barcos que aun NO han sido revelados
                    caracter_imprimir = "∼";
                    break;
            }
            Console.Write("\t" + caracter_imprimir);
        }
        Console.WriteLine();
    }
}

void ingresar_coordenadas() // Pregunta las coordenadas al usuario
{
    int fila, columna;
    bool checkf = false; // Para revisar si las filas ingresadas son válidas
    bool checkc = false; // Para revisar si las columnas ingresadas son válidas
    Console.Clear(); // Limpia la pantalla de la consola
    do
    {
        Console.WriteLine("Una X marca un espacio vacío, una O marca un espacio ocupado por un barco.");
        Console.WriteLine("Hay una mina oculta que te hará perder 5 tiros, y será marcada con una M.\n");
        Console.WriteLine($"Aún quedan {restantes} barcos.");
        Console.WriteLine($"Tienes {tiros} tiros.\n");
        imprimir_tablero();
        Console.WriteLine();
        do
        {
            Console.Write("Ingresa la fila (de 1 a 5): "); // De 1 a 5 para mayor comprensión por parte del jugador
            checkf = int.TryParse(Console.ReadLine(), out fila);
        } while (checkf == false || fila == 0 || fila > 5); // Repite la pregunta si no se cumplen las condicíones
        fila--; // Se resta uno al número ingresado para obtener el número correcto del tablero

        do
        {
            Console.Write("Ingresa la columna (de 1 a 5): ");
            checkc = int.TryParse(Console.ReadLine(), out columna);
        } while (checkc == false || columna == 0 || columna > 5);
        columna--;

        if (tablero[fila, columna] == 1) // Si el usuario encuentra un barco
        {
            Console.Beep();
            tablero[fila, columna] = 2; // Cambia el valor del espacio para imprimir otro caracter en pantalla
            restantes--; // Se resta uno al numero de barcos sin encontrar
            tiros--; // Se resta uno al número de tiros restantes
        }
        else if (tablero[fila, columna] == 3 || tablero[fila, columna] == 4)
        {
            tablero[fila, columna] = 4;
            tiros -= 5;
        }
        else // Si el usuario no encuentra un barco
        {
            tablero[fila, columna] = -1;
            tiros--; // Solo se resta uno al número de tiros restantes, no al de los barcos
        }
        Console.Clear();
    } while (restantes > 0 && tiros > 0); // Se repite hasta que el usuario encuentra todos los barcos o se queda sin tiros
}

crear_tablero();
colocar_barcos();
colocar_mina();
Console.WriteLine("*** Battleship contra la computadora ***");
Console.WriteLine("Selecciona una celda ingresando el número de fila\ny columna para verificar si hay un barco en ella.");
Console.WriteLine("Una X marca un espacio vacío, una O marca un espacio\nocupado por un barco. Encuentra los 4 barcos para ganar.");
Console.WriteLine("Presiona cualquier tecla para continuar.");
Console.ReadKey();
ingresar_coordenadas();
if (tiros <= 0)
{
    Console.WriteLine("Tablero final:");
    imprimir_tablero();
    Console.WriteLine("Mala suerte, te has quedado sin intentos.");
}
else
{
    Console.WriteLine("Tablero final:");
    imprimir_tablero();
    Console.WriteLine("Felicidades, has ganado!!");
}
