using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static System.Console;
namespace Proyecto11;

class Program
{
    static List<Proyecto> proyectos = new List<Proyecto>();

    static void Main()
    {
        LoadProjects();

        while (true)
        {
            string prompt = @"Bienvenido a notion en C#                                                                                             
         ..       .....:::----...              
        .:+*##%%%%%%%%%##*****%#=.             
        .+@@+:...........   ...-#%*-.          
        .*@@@%+----===+++****###%%@@%=         
        .*@@@@@@%####***++++===----*@*         
        .*@@@@@-.........   ..... .=@*         
        .*@@@@@- .-*##%*-...+%@#=..=@*         
        .*@@@@@- ..-@@@@%-. .*@=...=@*         
        .*@@@@@-  .:@@%@@@=..*@=  .=@*         
        .*@@@@@-  .:@%-+@@@*.*@=  .=@*         
        .*@@@@@- ..:@%-.+@@@#%@=  .=@*         
        .*@@@@@- ..:@%-..=%@@@@=  .=@*         
       ..=%@@@@- ..:@%-...-%@@@=  .=@*         
         .-%@@@- .-***+-....+*+:  .=@*         
           .*@@- ......::::----===+#@*         
            .=%@@@@@@@@@@@@@@%%%###*+:.        
              .----:::.........                
                    .....";
            string[] opciones = { "1. Crear proyecto", "2. Ver lista de proyectos", "3. Agregar tarea a un proyecto",
                                   "4. Listar tareas de un proyecto", "5. Buscar un proyecto", "6. Buscar una tarea", 
                                   "7. Eliminar Proyecto", "8. Eliminar Tarea", "9. Exportar a CSV","10. Salir" };
            Menu menuPrincipal = new Menu(prompt, opciones);
            int seleccionado = menuPrincipal.Run();

            switch (seleccionado)
            {
                case 0:
                    CrearProyecto();
                    break;
                case 1:
                    VerProyectos();
                    break;
                case 2:
                    AgregarTarea();
                    break;
                case 3:
                    ListarTareas();
                    break;
                case 4:
                    BuscarProyecto();
                    break;
                case 5:
                    BuscarTarea();
                    break;
                case 6:
                    EliminarProyecto();
                    break;
                case 7:
                    EliminarTarea();
                    break;
                case 8:
                    ExportarCSV();
                    break;
                case 9:
                return;
            }
        }
    }

    static void CrearProyecto()
    {
        Write("Ingrese el nombre del nuevo proyecto: ");
        string nombreProyecto = ReadLine()!;
        if (string.IsNullOrEmpty(nombreProyecto))
        {
            WriteLine("No ha agregado nada.");
            WriteLine("Presione cualquier tecla para continuar...");
            ReadKey();
            return;
        }
        GuardarProyectos();
        proyectos.Add(new Proyecto(nombreProyecto));
        WriteLine($"Proyecto '{nombreProyecto}' creado exitosamente.");
        WriteLine("Presione cualquier tecla para continuar...");
        ReadKey();
    }

    static void VerProyectos()
    {
        if (proyectos.Count == 0)
        {
            WriteLine("No hay proyectos para mostrar.");
            WriteLine("Presione cualquier tecla para continuar...");
            ReadKey();
            return;
        }

        WriteLine("Lista de Proyectos:");
        for (int i = 0; i < proyectos.Count; i++)
        {
            WriteLine($"{i + 1}. {proyectos[i].Nombre} (Creado el: {proyectos[i].FechaCreacion})");
        }
        WriteLine("Presione cualquier tecla para continuar...");
        ReadKey();
    }

    static void AgregarTarea()
    {
        Clear();
        if (proyectos.Count == 0)
        {
            WriteLine("No hay proyectos. Cree un proyecto primero.");
            WriteLine("Presione cualquier tecla para continuar...");
            ReadKey();
            return;
        }

        VerProyectos();
        Write("Seleccione el número del proyecto para agregar la tarea: ");
        if (int.TryParse(ReadLine(), out int indiceProyecto) && indiceProyecto > 0 && indiceProyecto <= proyectos.Count)
        {
            Clear();
            Write("Ingrese la descripción de la tarea: ");
            string tarea = ReadLine()!;
            proyectos[indiceProyecto - 1].Tareas.Add(tarea);
            GuardarProyectos();
            WriteLine("Tarea agregada exitosamente.");
        }
        else
        {
            WriteLine("Selección inválida.");
        }
        WriteLine("Presione cualquier tecla para continuar...");
        ReadKey();
    }

    static void ListarTareas()
    {
        if (proyectos.Count == 0)
        {
            WriteLine("No hay proyectos para mostrar tareas.");
            WriteLine("Presione cualquier tecla para continuar...");
            ReadKey();
            return;
        }

        VerProyectos();
        Write("Seleccione el número del proyecto para ver sus tareas: ");
        if (int.TryParse(ReadLine(), out int indiceProyecto) && indiceProyecto > 0 && indiceProyecto <= proyectos.Count)
        {
            Clear();
            Proyecto proyecto = proyectos[indiceProyecto - 1];
            WriteLine($"Tareas del proyecto '{proyecto.Nombre}':");
            if (proyecto.Tareas.Count == 0)
            {
                WriteLine("No hay tareas para este proyecto.");
            }
            else
            {
                for (int i = 0; i < proyecto.Tareas.Count; i++)
                {
                    WriteLine($"{i + 1}. {proyecto.Tareas[i]}");
                }
            }
        }
        else
        {
            WriteLine("Selección inválida.");
        }
        WriteLine("Presione cualquier tecla para continuar...");
        ReadKey();
    }

    static Proyecto BuscarProyectoPorNombre(string nombre)
    {
        foreach (var proyecto in proyectos)
        {
            if (proyecto.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                return proyecto;
            }
        }
        return null!; 
    }

    static void BuscarProyecto()
    {
        WriteLine("Ingrese el nombre del proyecto a buscar:");
        string nombreABuscar = ReadLine()!;
        Proyecto proyectoEncontrado = BuscarProyectoPorNombre(nombreABuscar);

        if (proyectoEncontrado != null)
        {
            WriteLine($"Proyecto encontrado: {proyectoEncontrado.Nombre}");
        }
        else
        {
            WriteLine($"No se encontró ningún proyecto con el nombre '{nombreABuscar}'.");
        }
        WriteLine("Presione cualquier tecla para continuar...");
        ReadKey();
    }

    static void BuscarTarea()
    {
        WriteLine("Ingrese la descripción de la tarea a buscar:");
        string tareaABuscar = ReadLine()!;

        List<string> proyectosConTarea = new List<string>();

        foreach (var proyecto in proyectos)
        {
            foreach (var tarea in proyecto.Tareas)
            {
                if (tarea.Equals(tareaABuscar, StringComparison.OrdinalIgnoreCase))
                {
                    proyectosConTarea.Add(proyecto.Nombre);
                }
            }
        }

        if (proyectosConTarea.Count > 0)
        {
            WriteLine("Tarea encontrada en los siguientes proyectos:");
            foreach (var proyectoNombre in proyectosConTarea)
            {
                WriteLine(proyectoNombre);
            }
        }
        else
        {
            WriteLine($"No se encontró ninguna tarea con la descripción '{tareaABuscar}'.");
        }
        WriteLine("Presione cualquier tecla para continuar...");
        ReadKey();
    }

    static void EliminarProyecto()
    {
        Clear();
        VerProyectos();
        Write("Seleccione el número del proyecto para eliminar: ");
        if (int.TryParse(ReadLine(), out int indiceProyecto) && indiceProyecto > 0 && indiceProyecto <= proyectos.Count)
        {
            proyectos.RemoveAt(indiceProyecto - 1);
            GuardarProyectos();
            WriteLine("Proyecto eliminado exitosamente.");
        }
        else
        {
            WriteLine("Selección inválida.");
        }
        WriteLine("Presione cualquier tecla para continuar...");
        ReadKey();
    }

    static void EliminarTarea()
    {
        VerProyectos();
        Write("Seleccione el número del proyecto para eliminar una tarea: ");
        if (int.TryParse(ReadLine(), out int indiceProyecto) && indiceProyecto > 0 && indiceProyecto <= proyectos.Count)
        {
            Proyecto proyecto = proyectos[indiceProyecto - 1];
            if (proyecto.Tareas.Count == 0)
            {
                WriteLine("No hay tareas para este proyecto.");
                WriteLine("Presione cualquier tecla para continuar...");
                ReadKey();
                return;
            }

            for (int i = 0; i < proyecto.Tareas.Count; i++)
            {
                WriteLine($"{i + 1}. {proyecto.Tareas[i]}");
            }

            Write("Seleccione el número de la tarea para eliminar: ");
            if (int.TryParse(ReadLine(), out int indiceTarea) && indiceTarea > 0 && indiceTarea <= proyecto.Tareas.Count)
            {
                proyecto.Tareas.RemoveAt(indiceTarea - 1);
                GuardarProyectos();
                WriteLine("Tarea eliminada exitosamente.");
            }
            else
            {
                WriteLine("Selección inválida.");
            }
        }
        else
        {
            WriteLine("Selección inválida.");
        }
        WriteLine("Presione cualquier tecla para continuar...");
        ReadKey();
    }

    static void ExportarCSV()
    {
        var csv = new StringBuilder();
        csv.AppendLine("Nombre del Proyecto,Fecha de Creación,Tareas");

        foreach (var proyecto in proyectos)
        {
            foreach (var tarea in proyecto.Tareas)
            {
                var newLine = $"{proyecto.Nombre},{proyecto.FechaCreacion},{tarea}";
                csv.AppendLine(newLine);
            }

            if (proyecto.Tareas.Count == 0)
            {
                var newLine = $"{proyecto.Nombre},{proyecto.FechaCreacion},";
                csv.AppendLine(newLine);
            }
        }

        string filePath = "proyectos.csv";
        File.WriteAllText(filePath, csv.ToString());
        WriteLine($"Proyectos y tareas exportados exitosamente a {filePath}");
        WriteLine("Presione cualquier tecla para continuar...");
        ReadKey();
    }

    static void GuardarProyectos()
    {
        string json = JsonSerializer.Serialize(proyectos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("proyectos.json", json);
    }

    static void LoadProjects()
    {
        if (File.Exists("proyectos.json"))
        {
            string json = File.ReadAllText("proyectos.json");
            proyectos = JsonSerializer.Deserialize<List<Proyecto>>(json) ?? new List<Proyecto>();
        }
    }
}


