namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        string operador = "";     // Operador actual
        double num1 = 0;          // Número acumulado
        bool NewNum = true;       // Para saber si empieza un nuevo número

        public MainPage()
        {
            InitializeComponent();
        }

        // Al presionar un número
        void OnNumeroClicked(object sender, EventArgs e)
        {
            var boton = (Button)sender;
            string numero = boton.Text;

            // Si es el primer número o nuevo número después de operador
            if (Resultados.Text == "0" || NewNum)
            {
                Resultados.Text = numero;
                NewNum = false;
            }
            else
            {
                Resultados.Text += numero;
            }

            // Mostrar operación en tiempo real
            if (!string.IsNullOrEmpty(operador))
            {
                Operacion.Text = $"{num1} {OperadorVisual(operador)} {Resultados.Text}";
            }
            else
            {
                Operacion.Text = Resultados.Text;
            }
        }

        // Al presionar un operador
        void OnOperadorClicked(object sender, EventArgs e)
        {
            var boton = (Button)sender;

            double num2 = double.Parse(Resultados.Text);

            // Si ya hay un operador previo, calculamos resultado parcial
            if (!string.IsNullOrEmpty(operador))
            {
                num1 = operador switch
                {
                    "+" => num1 + num2,
                    "-" => num1 - num2,
                    "*" => num1 * num2,
                    "/" => num2 != 0 ? num1 / num2 : 0,
                    _ => num2
                };
            }
            else
            {
                num1 = num2; // Primera vez
            }

            // Guardar operador nuevo
            operador = boton.Text switch
            {
                "×" => "*",
                "÷" => "/",
                "−" => "-",
                _ => boton.Text
            };

            // Mostrar operador visual en la operación
            string operadorVisual = OperadorVisual(operador);
            Operacion.Text = $"{num1} {operadorVisual}";

            Resultados.Text = "0";
            NewNum = true;
        }

        // Al presionar =
        void OnIgualClicked(object sender, EventArgs e)
        {
            double num2 = double.Parse(Resultados.Text);
            double resultado = num1;

            // Calculamos la última operación
            if (!string.IsNullOrEmpty(operador))
            {
                resultado = operador switch
                {
                    "+" => num1 + num2,
                    "-" => num1 - num2,
                    "*" => num1 * num2,
                    "/" => num2 != 0 ? num1 / num2 : 0,
                    _ => num2
                };
            }

            Operacion.Text = $"{Operacion.Text} {Resultados.Text}";
            Resultados.Text = resultado.ToString();

            // Reiniciamos
            num1 = resultado;
            operador = "";
            NewNum = true;
        }

        // Botón .
        void OnPuntoClicked(object sender, EventArgs e)
        {
            if (!Resultados.Text.Contains("."))
                Resultados.Text += ".";
        }

        // Botón AC
        void OnLimpiarClicked(object sender, EventArgs e)
        {
            Resultados.Text = "0";
            Operacion.Text = "";
            num1 = 0;
            operador = "";
            NewNum = true;
        }

        // Botón %
        void OnPorcentajeClicked(object sender, EventArgs e)
        {
            double valor = double.Parse(Resultados.Text);
            valor /= 100;
            Resultados.Text = valor.ToString();
        }

        // Función para mostrar el operador visual
        string OperadorVisual(string op)
        {
            return op switch
            {
                "*" => "×",
                "/" => "÷",
                "-" => "−",
                _ => op
            };
        }
        void OnBorrarClicked(object sender, EventArgs e)
        {
            // Si solo queda un dígito, ponemos 0
            if (Resultados.Text.Length == 1)
            {
                Resultados.Text = "0";
            }
            else
            {
                // Quitamos el último carácter del número
                Resultados.Text = Resultados.Text.Substring(0, Resultados.Text.Length - 1);
            }

            // Actualizamos Operacion.Text si hay un operador
            if (!string.IsNullOrEmpty(operador))
            {
                Operacion.Text = $"{num1} {OperadorVisual(operador)} {Resultados.Text}";
            }
            else
            {
                Operacion.Text = Resultados.Text;
            }
        }

    }
}
