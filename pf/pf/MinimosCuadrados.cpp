#include <iostream>//incluyo la libreria iostream, para poder tener acceso a los dispositivos de entrada y salida
#include <vector>//Es una platilla de clase que proporciona un contenedor de memoria dinamica, puede contener un numero variable de elementos del mismo tipo
#include <iomanip>//biblioteca se usa para ajustar decimales, esta libreria la utilice unicamente para mostrar los 15 decimales de respuesta.
#include <cmath>
#include <fstream>
#include <algorithm>

using namespace std;//importa todo el espacio de nombres std al codigo actual, se utiliza mayormente para utilizar el prefijo std::

// Función para calcular la interpolación
double MMC(const vector<double>& x, const vector<double>& y, double valor) {//creo una funcion tipo double que regresa el valor calculado, pasando los vextores "x" y "y"
    int n = x.size();//creo una variable entera para regresar el numero total de la tabla

    double sumX = 0;
    double sumY = 0;
    double syx = 0;
    double xy = 0;
    double x2 = 0;
    double y2 = 0; //Para calculo de r correlacion relativa
    for (int i = 0; i < n; i++){
        xy += x[i] * y[i];
        x2 += pow(x[i], 2);
        y2 += pow(y[i], 2);
        sumX += x[i];
        sumY += y[i];
    }

    double b = (n*xy - sumX*sumY)/(n*x2 - pow(sumX, 2));
    double a = (sumY/n) - (b*(sumX/n));

    double valorF = a + b*valor;
    cout<<"Ecuacion: Y = ("<<a<<") + ("<<b<<"x)"<<endl;

    double numeradorR = n * xy - sumX * sumY; //Parte superior de la ecuacion
    double denominadorR = sqrt((n * x2 - sumX * sumX) * (n * y2 - sumY * sumY)); //Parte inferior de la ecuacion

    if (denominadorR == 0) { //Validar si el denominador no es 0
        cerr << "División por cero en el cálculo de la correlación." << endl;
        return 0;
    }

    double r = numeradorR / denominadorR;
    cout<< "Correlacion resultante es igual a: r= "<<r<<""<<endl;

    //Calcular el error
    syx = sqrt((y2 - a * sumY - b * xy) / (n - 2));

    cout<< "El error es igual a: "<<syx<<""<< endl;

    // Crea un archivo de datos con los puntos de la recta
    ofstream dataFile("datos_recta.dat");
    for (int i = 0; i < n; ++i) {
        double y = b * x[i] + a; // Ecuación de la recta: y = mx + b
        dataFile << x[i] << " " << y << std::endl;
    }
    dataFile.close();
    // Usa Gnuplot para generar el gráfico
    FILE *gnuplotPipe = popen("gnuplot -persist", "w");
    if (gnuplotPipe) {
        fprintf(gnuplotPipe, "set terminal png\n");
        fprintf(gnuplotPipe, "set output 'recta.png'\n");
        fprintf(gnuplotPipe, "plot 'datos_recta.dat' using 1:2 with points pt 7 lc rgb 'red' title 'Datos', 'datos_recta.dat' with lines lc rgb 'blue' title 'Recta de ajuste'\n");
//        fprintf(gnuplotPipe, "plot 'datos_recta.dat' with lines\n");
        fflush(gnuplotPipe);
        fprintf(gnuplotPipe, "exit\n");
        pclose(gnuplotPipe);
    } else {
        cerr << "Error al abrir la tubería a Gnuplot." << std::endl;
        return 1;
    }
    
    

    return valorF;
}

pair<vector<double>, vector<double>> leerArchivo(string nombreArch) {
    vector<double> numeros1;
    vector<double> numeros2;
    ifstream archivo(nombreArch);

    if(!archivo.is_open()){
        cerr <<"no se pudo abrir el archivo " << nombreArch<<endl;
        return make_pair(numeros1, numeros2); // Devuelve dos vectores vacíos si hay un error al abrir el archivo
    }
    string linea;
    while (getline(archivo, linea)) {
        istringstream ss(linea);
        string valor;

        // Lee los dos valores separados por ';'
        getline(ss, valor, ';'); // Primer valor
        try {
            double numero1 = stod(valor);
            numeros1.push_back(numero1);
        } catch (const invalid_argument& e) {
            cerr << "Error al convertir \"" << valor << "\" a número." << std::endl;
        }

        getline(ss, valor, ';'); // Segundo valor
        try {
            double numero2 = stod(valor);
            numeros2.push_back(numero2);
        } catch (const std::invalid_argument& e) {
            std::cerr << "Error al convertir \"" << valor << "\" a número." << std::endl;
        }
    }

    archivo.close();
    return make_pair(numeros1, numeros2);
}

void menu(){
    cout<<"---------------------------------------MENU-------------------------------------------"<<endl;
    cout<<"1. Ingresar datos a mano."<<endl;
    cout<<"2. Ingresar datos con archivo."<<endl;
    cout<<"3. Salir."<<endl;
}

int main() {
    int n = 0;//creo una variable entera n para pedir el numero total de valores en la tabla
    // Puntos conocidos para la interpolación
    int opcion;
    while (opcion != 3)
    {
        menu();
        cout<<"Ingrese la opcion deseada: "<<endl;//despliego un mensaje para pedir el valor de filas, utilizando n
        try//abro un try and catch para prevenir ingreso de datos erroneos
        {
            cin>>opcion;//ingreso el valor del usuairio en la variable n
        }
        catch(const invalid_argument& e)//de aber un error la aplicacion no calculara nada y se cerrara
        {
            cout << e.what() << '\n';//despliego en consola que hubo un error en la ejecucion
        }
        if(opcion == 1){
            system("cls");//utilizo system para limpiar la consola de la recopilacion de datos
            cout<<"ingrese el numero de filas: "<<endl;//despliego un mensaje para pedir el valor de filas, utilizando n
            try//abro un try and catch para prevenir ingreso de datos erroneos
            {
                cin>>n;//ingreso el valor del usuairio en la variable n
            }
            catch(const invalid_argument& e)//de aber un error la aplicacion no calculara nada y se cerrara
            {
                cout << e.what() << '\n';//despliego en consola que hubo un error en la ejecucion
            }
            vector<double> x(n);//creo 2 vectores con tamaño variable, les paso de una vez su valor de numero de datos
            vector<double> y(n);

            cout<<"Ingrese los valores Xs, Ys"<<endl;//doy contexto de los valores que pedire a continuacion.
            for(int js = 0; js < 2; js++){//comienzo a pedir los valores para rellenar al tabla de valores
                string col;//creo un string col, para desplegar la columna que estoy pidiendo, si es la de x o la de y
                if(js+1 == 1){//con un condicional valido si estamos en la columna 1 o en la columna 2
                    col = "X";//si estamos en la columna 1 entonces col es igual a X
                    cout<<"Columna " << col << ": "<<endl;//ahora estructuro el mensaje para que se le haga mas falcil al usuiario ingresar los datos
                    for(int iS = 0; iS < n; iS++){//comienzo a pedir los valores con for anidados
                        cout<<"valor "<<iS+1<<": "<<endl;//despliego un mensaje para el valor de la fila
                        try//abro un try and catch para prevenir ingreso de datos erroneos
                        {
                            cin>>x[iS];//pido el valor y lo inserto en la posicion de la matriz correspondiente
                        }
                        catch(const invalid_argument& e)//de aber un error la aplicacion no calculara nada y se cerrara
                        {
                            cout << e.what() << '\n';//despliego en consola que hubo un error en la ejecucion
                        }
                    }
                }else {
                    col = "Y";//sino col es igual a Y
                    cout<<"Columna " << col << ": "<<endl;//ahora estructuro el mensaje para que se le haga mas falcil al usuiario ingresar los datos
                    for(int iS = 0; iS < n; iS++){//comienzo a pedir los valores con for anidados
                        cout<<"valor "<<iS+1<<": "<<endl;//despliego un mensaje para el valor de la fila
                        try//abro un try and catch para prevenir ingreso de datos erroneos
                        {
                            cin>>y[iS];//pido el valor y lo inserto en la posicion de la matriz correspondiente
                        }
                        catch(const invalid_argument& e)//de aber un error la aplicacion no calculara nada y se cerrara
                        {
                            cout << e.what() << '\n';//despliego en consola que hubo un error en la ejecucion
                        }
                    }
                }
            }

            // Valor a interpolar
            double valor_a_interpolar;
            cout<<"ingrese el valor de a Predecir: "<<endl;//pido el valor de z
            try///abro un try and catch para prevenir ingreso de datos erroneos
            {
                cin>>valor_a_interpolar;//ingreso el valor del usuario en mi variable puntoZ
            }
            catch(const invalid_argument& e)//de aber un error la aplicacion no calculara nada y se cerrara
            {
                cout << e.what() << '\n';//despliego en consola que hubo un error en la ejecucion
            }


            system("cls");//utilizo system para limpiar la consola de la recopilacion de datos
            // Realizar la interpolación y mostrar el resultado
            double resF;
            resF = MMC(x, y, valor_a_interpolar);//creo una variable resultado y la igualo a mi funcion para iniciar con todos los calculos, mandando los vectores "x", "y" y el valor a interpolar
            cout << "El valor a predecido es: " << resF <<endl;//despliego el resultado en consola
            }
        if(opcion == 2){
            system("cls");//utilizo system para limpiar la consola de la recopilacion de datos
            cout<<"ingrese el nombre del archivo, junto con su tipo(.txt/.csv): "<<endl;//despliego un mensaje para pedir el valor de filas, utilizando n
            string nombreArchivo;
            try//abro un try and catch para prevenir ingreso de datos erroneos
            {
                cin>>nombreArchivo;//ingreso el valor del usuairio en la variable n
            }
            catch(const invalid_argument& e)//de aber un error la aplicacion no calculara nada y se cerrara
            {
                cout << e.what() << '\n';//despliego en consola que hubo un error en la ejecucion
            }
            cout<<"Ingrese el valor a predecir: "<<endl;//despliego un mensaje para pedir el valor de filas, utilizando n
            double valor_a_interpolar;
            try//abro un try and catch para prevenir ingreso de datos erroneos
            {
                cin>>valor_a_interpolar;//ingreso el valor del usuairio en la variable n
            }
            catch(const invalid_argument& e)//de aber un error la aplicacion no calculara nada y se cerrara
            {
                cout << e.what() << '\n';//despliego en consola que hubo un error en la ejecucion
            }
            // Leer el archivo y guardar los números en dos arrays distintos
            auto [numeros1, numeros2] = leerArchivo(nombreArchivo);
            int n = numeros1.size();
            vector<double> x(n);//creo 2 vectores con tamaño variable, les paso de una vez su valor de numero de datos
            vector<double> y(n);
            //cout << valor_a_interpolar << endl;
            //cout << "Primer array:" << endl;
            int i = 0;
            for (double numero : numeros1) {
                //cout << numero << endl;
                x[i] = numero;
                i++;
            }
            i = 0;
            //cout << "Segundo array:" << endl;
            for (double numero : numeros2) {
                //cout << numero << endl;
                y[i] = numero;
                i++;
            }

            double resF = MMC(x, y, valor_a_interpolar);//creo una variable resultado y la igualo a mi funcion para iniciar con todos los calculos, mandando los vectores "x", "y" y el valor a interpolar
            cout << "El valor a predecido es: " << resF <<endl;//despliego el resultado en consola
        }
        if(opcion > 3 || opcion <1){
            cout<<"Ingrese una opcion valida."<<endl;
        }
    }
    
    return 0;
}