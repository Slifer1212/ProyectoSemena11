
using static System.Console;
namespace Proyecto11
{
    class Menu
    {
        private int Seleccionado;
        private string[] Opciones;
        private string Prompt;


        public Menu(string prompt , string[] opcion)
        {
            Prompt = prompt;
            Opciones = opcion;
            Seleccionado = 0;
        }

        private void verOpciones()
        {
           WriteLine(Prompt);


           for(int i = 0; i < Opciones.Length; i++)
           {
            string opcion_Ahora = Opciones[i];
            string prefijo;


            if (i == Seleccionado)
            {
                prefijo = "*";
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                prefijo = " ";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
            }
             WriteLine($"{prefijo} << {opcion_Ahora} >>");


           }
           ResetColor();
        }
        public int Run()
        {
            ConsoleKey tecla_Presionado;
            do
            {
                Clear();
                verOpciones();


                ConsoleKeyInfo tecla_Info = ReadKey();
                tecla_Presionado = tecla_Info.Key;


                if (tecla_Presionado == ConsoleKey.UpArrow){
                    Seleccionado --;
                    if (Seleccionado == -1)
                    {
                        Seleccionado = Opciones.Length - 1;
                    }
                }
                else if (tecla_Presionado == ConsoleKey.DownArrow)
                {
                    Seleccionado ++;
                    if (Seleccionado == Opciones.Length )
                    {
                        Seleccionado = 0;
                    }
                }
                
            }while(tecla_Presionado != ConsoleKey.Enter);
            return Seleccionado;
        }
    }
   
}
