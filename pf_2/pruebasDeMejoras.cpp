#include <iostream> //incluyo la libreria iostream, para poder tener acceso a los dispositivos de entrada y salida
#include <vector>   //Es una plantilla de clase que proporciona un contenedor de memoria dinamica, puede contener un numeor variable de elemntos del mismo tipo
#include <iomanip>  //biblioteca usada pra ajustar decimales
#include <cmath>
#include <fstream>
#include <sstream>
#include <set>
#include <string>

using namespace std; // importa todo el espacio de nombres std al codigo actual
// funcion para calcular la interpolacion
double MMC(const vector<double> &x, const vector<double> &y, double valor)
{                     // creo una funcion tipo double que regresa el valor calculado
    int n = x.size(); // variable entera para guardar el numero total de filas en la tabla
    double sumX = 0;
    double sumY = 0;
    double syx = 0;
    double xy = 0;
    double x2 = 0;
    double y2 = 0; // para calculo de r correlacion relativa
    for (int i = 0; i < n; i++)
    {
        xy += x[i] * y[i];
        x2 += pow(x[i], 2);
        y2 += pow(y[i], 2);
        sumX += x[i];
        sumY += y[i];
    }

    double b = (n * xy - sumX * sumY) / (n * x2 - pow(sumX, 2));
    double a = (sumY / n) - (b * (sumX / n));

    double valorF = a + b * valor;
    cout << "Ecuacion: Y = (" << a << ") + (" << b << "x)" << endl;

    double numeradorR = n * xy - sumX * sumY;                                    // parte superior de la ecuacion
    double denominadorR = sqrt((n * x2 - sumX * sumX) * (n * y2 - sumY * sumY)); // parte inferior de la ecuacion

    if (denominadorR == 0)
    { // valida si el denominador no es 0
        cerr << "Division por cero en el calculo de la correlacion." << endl;
        return 0;
    }

    double r = numeradorR / denominadorR;
    cout << "El error es igual a: r= " << r << "" << endl;

    // calcular el error
    syx = sqrt((y2 - a * sumY - b * xy) / (n - 2));
    cout << "El error es igual a " << syx << "" << endl;

    // crea un archivo de datos con los puntos de la recta
    ofstream dataFile("datos_recta.dat");
    for (int i = 0; i < n; ++i)
    {
        double y = b * x[i] + a; // ecuacion de la recta: y = mx + b
        dataFile << x[i] << " " << y << std::endl;
    }
    dataFile.close();
    // usa Gnuplot para generar el grafico
    FILE *gnuplotPipe = popen("gnuplot -persist", "w");
    if (gnuplotPipe)
    {
        fprintf(gnuplotPipe, "set terminal png\n");
        fprintf(gnuplotPipe, "set output 'recta.png'\n");
        fprintf(gnuplotPipe, "plot 'datos_recta.dat' with lines\n");
        fflush(gnuplotPipe);
        fprintf(gnuplotPipe, "exit\n");
        pclose(gnuplotPipe);
    }
    else
    {
        cerr << "Error al abrir la tubería a Gnuplot." << std::endl;
        return 1;
    }

    return valorF;
}

pair<vector<double>, vector<double>> leerArchivo(string nombreArch)
{
    vector<double> numeros1;
    vector<double> numeros2;
    ifstream archivo(nombreArch);

    if (!archivo.is_open())
    {
        cerr << "No se pudo abrir el archivo " << nombreArch << endl;
        return make_pair(numeros1, numeros2); // devuelve 2 vectores vacios si hay error al abrir el archivo
    }

    string linea;
    while (getline(archivo, linea))
    {
        istringstream ss(linea);
        string valor1, valor2;

        if (getline(ss, valor1, ';') && getline(ss, valor2, ';'))
        { // lee los valores separados por ';'
            try
            {
                numeros1.push_back(stod(valor1));
                numeros2.push_back(stod(valor2));
            }
            catch (const invalid_argument &e)
            {
                cerr << "Error al convertir los valores en la línea: " << linea << endl;
            }
        }
        else
        {
            cerr << "Formato de línea incorrecto: " << linea << endl;
        }
    }

    archivo.close();
    return make_pair(numeros1, numeros2);
}

void menu()
{
    cout << "---------------------------------------MENU-------------------------------------------" << endl;
    cout << "1. Ingresar datos a mano." << endl;
    cout << "2. Ingresar datos con archivo." << endl;
    cout << "3. Generar diagrama de árbol." << endl;
    cout << "4. Salir." << endl;
}

void generarDiagramaArbol(int opciones, int intentos, vector<string> &espacioMuestral, string actual = "")
{
    if (intentos == 0)
    {
        espacioMuestral.push_back(actual); // utilizado para añadir un nuevo dayto
        return;
    }
    for (int i = 1; i <= opciones; ++i)
    {
        generarDiagramaArbol(opciones, intentos - 1, espacioMuestral, actual + to_string(i) + " ");
    }
}

void mostrarEspacioMuestral(const vector<string> &espacioMuestral)
{
    cout << "Espacio muestral:" << endl;
    for (const string &evento : espacioMuestral)
    {
        cout << evento << endl;
    }
}

void generarArchivoDot(const vector<string> &espacioMuestral, int opciones, int intentos)
{ // genera un word para luego generar el graphviz
    ofstream file("arbol.dot");
    file << "digraph G {" << endl;
    file << "    rankdir=TB;" << endl; // Orientación de arriba hacia abajo
    file << "    node [shape=circle];" << endl;

    // Agregar nodos y aristas
    for (const string &evento : espacioMuestral)
    {
        istringstream ss(evento);
        string nodoAnterior = "P";
        string nodoActual;
        int paso = 1;
        while (ss >> nodoActual)
        {
            string nombreNodoAnterior = nodoAnterior + to_string(paso - 1);
            string nombreNodoActual = nodoAnterior + nodoActual + to_string(paso);
            file << "    \"" << nombreNodoAnterior << "\" -> \"" << nombreNodoActual << "\";" << endl;
            nodoAnterior = nodoAnterior + nodoActual;
            paso++;
        }
    }

    file << "}" << endl;
    file.close();

    system("dot -Tpng arbol.dot -o arbol.png"); // mando la instruccion para ejecutor en cmd el comando dot para generar el graphviz
}

int main()
{
    int n = 0;
    int opcion = 0;
    while (opcion != 4)
    {
        menu();
        cout << "Ingrese la opción deseada: " << endl;
        cin >> opcion;

        if (opcion == 1)
        {
            system("cls");
            cout << "Ingrese el número de filas: " << endl;
            cin >> n;
            vector<double> x(n);
            vector<double> y(n);

            cout << "Ingrese los valores Xs, Ys" << endl;
            for (int js = 0; js < 2; js++)
            {
                string col = (js == 0) ? "X" : "Y";
                cout << "Columna " << col << ": " << endl;
                for (int iS = 0; iS < n; iS++)
                {
                    cout << "Valor " << iS + 1 << ": " << endl;
                    if (js == 0)
                    {
                        cin >> x[iS];
                    }
                    else
                    {
                        cin >> y[iS];
                    }
                }
            }

            double valor_a_interpolar;
            cout << "Ingrese el valor a predecir: " << endl;
            cin >> valor_a_interpolar;

            system("cls");
            double resF = MMC(x, y, valor_a_interpolar);
            cout << "El valor predicho es: " << resF << endl;
        }
        else if (opcion == 2)
        {
            system("cls");
            cout << "Ingrese el nombre del archivo (incluyendo la extensión .txt/.csv): " << endl;
            string nombreArchivo;
            cin >> nombreArchivo;

            cout << "Ingrese el valor a predecir: " << endl;
            double valor_a_interpolar;
            cin >> valor_a_interpolar;

            auto [numeros1, numeros2] = leerArchivo(nombreArchivo);
            int n = numeros1.size();
            if (n == 0 || numeros1.size() != numeros2.size())
            {
                cerr << "Error en la lectura del archivo o los datos no están balanceados." << endl;
                continue;
            }

            double resF = MMC(numeros1, numeros2, valor_a_interpolar);
            cout << "El valor predicho es: " << resF << endl;
        }
        else if (opcion == 3)
        {
            system("cls");
            cout << "Ingrese el número de opciones del experimento: " << endl;
            int opciones;
            cin >> opciones;

            cout << "Ingrese el número de intentos: " << endl;
            int intentos;
            cin >> intentos;

            vector<string> espacioMuestral;
            generarDiagramaArbol(opciones, intentos, espacioMuestral);

            mostrarEspacioMuestral(espacioMuestral);
            generarArchivoDot(espacioMuestral, opciones, intentos);

            cout << "Diagrama de árbol generado en arbol.png" << endl;
        }
        else if (opcion > 4 || opcion < 1)
        {
            cout << "Ingrese una opción válida." << endl;
        }
    }

    return 0;
}