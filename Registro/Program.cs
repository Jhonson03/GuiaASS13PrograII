/*Nota: Todo ha sido echo como en la clase lo unico
que las separe en carpetas para un mejor orden 
la parte que dice Codigo es el mismo ID del estudiante*/

using Registro.DAO;
using Registro.Models;

#region Variables y objetos
ClsEstudiante Estu = new ClsEstudiante();
CrdEstudiante db = new CrdEstudiante();

bool Vali1 = true;
int IdWidth = 9;
int NomWidth = 20;
int ApeWidth = 22;
int SexHeight = 4;
int EdadWidth = 6;
#endregion

#region Menu
Console.WriteLine("Registro de estudiantes");

while (Vali1 == true)
{
    Console.Write("\n\tMenu  \n1- Agregar estudiante \n2- Actualizar estudiante \n3- Eliminar \n4- Ver lista de Estudiantes \n5- Salir \n-> ");
    var menu = int.Parse(Console.ReadLine());

    bool Vali2 = true;

    #region Operaciones
    switch (menu)
    {
        #region Agregar
        case 1:
            while (Vali2 == true)
            {
                Console.WriteLine("\n\tAgregar estudiante");

                Console.Write("\nIngrese el nombre: ");
                Estu.Nombres = Console.ReadLine();
                Console.Write("Ingrese el apellido: ");
                Estu.Apellidos = Console.ReadLine();
                Console.Write("Ingrese la edad: ");
                Estu.Edad = int.Parse(Console.ReadLine());
                Console.Write("Ingrese el sexo M // F: ");
                Estu.Sexo = Console.ReadLine().ToLower();
                db.CreateEstu(Estu);

                Console.WriteLine($"\nLos datos de {Estu.Nombres} {Estu.Apellidos} han sido agregados con exito");
                bool Vali3 = false;
                do
                {
                    Console.Write("\nDesea seguir agregando otro Estudiante s/n: ");
                    var menu3 = Console.ReadLine().ToLower().Trim();

                    switch (menu3)
                    {
                        case "s":
                            Vali1 = true;
                            Vali3 = true;
                            break;
                        case "n":
                            Vali2 = false;
                            Vali3 = true;
                            break;
                        default:
                            Console.WriteLine("\nError: Se ingresó una letra diferente de 's' o 'n'");
                            break;
                    }
                } while (!Vali3);
            }
            break;
        #endregion

        #region Update
        case 2:
            while (Vali2 == true)
            {

                Console.WriteLine($"\n{"Codigo".PadRight(IdWidth)} {"Nombres".PadRight(NomWidth)} {"Apellidos".PadRight(ApeWidth)} {"Edad".PadRight(EdadWidth)} {"Sexo".PadRight(SexHeight)}");
                Console.WriteLine(new string('-', IdWidth + NomWidth + ApeWidth + EdadWidth + SexHeight + 8));
                foreach (var i in db.ListaEstudiante())
                {
                    Console.WriteLine($"{i.Id.ToString().PadRight(IdWidth)} {i.Nombres.PadRight(NomWidth)} {i.Apellidos.PadRight(ApeWidth)} {i.Edad.ToString().PadRight(EdadWidth)} {i.Sexo.PadRight(SexHeight)}   |");
                }

                Console.Write("\nIngrese el codigo del Estudiante que desea actualizar: ");
                var buscar = db.EstudianteIndivi(int.Parse(Console.ReadLine()));

                if (buscar == null)
                {
                    Console.WriteLine("El Estudiante no existe");
                }
                else
                {
                    Console.Write(@$"
Ingrese el campo que desea actualizar

1- Nombres {Estu.Nombres}
2- Apellidos {Estu.Apellidos}
3- Edad {Estu.Edad}
4- Sexo {Estu.Sexo}

-> ");
                    var Lector = int.Parse(Console.ReadLine());
                    switch (Lector)
                    {
                        case 1:
                            Console.Write("Ingrese el nombre: ");
                            buscar.Nombres = Console.ReadLine();
                            break;

                        case 2:
                            Console.Write("Ingrese el Apellido: ");
                            buscar.Apellidos = Console.ReadLine();
                            break;

                        case 3:
                            Console.Write("Ingrese la edad: ");
                            buscar.Edad = int.Parse(Console.ReadLine());
                            break;
                        case 4:
                            Console.Write("Ingrese el sexo M // F: ");
                            buscar.Sexo = Console.ReadLine().ToLower();
                            break;
                    }
                    db.UpdateEstu(buscar, Lector);

                    Console.WriteLine("\nSe ha actualizado de manera correcta");

                    bool Vali3 = false;
                    do
                    {
                        Console.Write("\nDesea seguir actualizando s/n: ");
                        var menu3 = Console.ReadLine().ToLower().Trim();

                        switch (menu3)
                        {
                            case "s":
                                Vali1 = true;
                                Vali3 = true;
                                break;
                            case "n":
                                Vali2 = false;
                                Vali3 = true;
                                break;
                            default:
                                Console.WriteLine("\nError: Se ingresó una letra diferente de 's' o 'n'");
                                break;
                        }
                    } while (!Vali3);
                }
            }
            break;
        #endregion

        #region Delete
        case 3:
            do
            {
                Console.WriteLine($"\n{"Codigo".PadRight(IdWidth)} {"Nombres".PadRight(NomWidth)}");
                Console.WriteLine(new string('-', IdWidth + NomWidth + 1));
                foreach (var i in db.ListaEstudiante())
                {
                    Console.WriteLine($"{i.Id.ToString().PadRight(IdWidth)} {i.Nombres.PadRight(NomWidth)}|");
                }
                Console.Write("\nIngrese el código del registro a eliminar (o '0' para volver al menú principal): ");
                if (int.TryParse(Console.ReadLine().ToLower(), out var Estudianteindiv))
                {
                    if (Estudianteindiv == 0)
                    {
                        break; // Salir del ciclo y volver al menú principal
                    }

                    var buscar = db.DeleteEstu(Estudianteindiv);

                    if (buscar.Equals("El estudiante no existe"))
                    {
                        Console.WriteLine(buscar);
                    }
                    else if (buscar.Equals("\nEliminación exitosa"))
                    {
                        Console.WriteLine(buscar);
                    }
                    else
                    {
                        Console.WriteLine("Entrada no válida. Por favor, ingrese un código válido.");
                    }
                }
            } while (true);
            break;
        #endregion

        #region Ver
        case 4:
            var ListarEstudiante = db.ListaEstudiante();

            Console.WriteLine($"\n{"Codigo".PadRight(IdWidth)} {"Nombres".PadRight(NomWidth)} {"Apellidos".PadRight(ApeWidth)} {"Edad".PadRight(EdadWidth)} {"Sexo".PadRight(SexHeight)}");

            Console.WriteLine(new string('-', IdWidth + NomWidth + ApeWidth + EdadWidth + SexHeight + 8));

            foreach (var i in ListarEstudiante)
            {
                Console.WriteLine($"{i.Id.ToString().PadRight(IdWidth)} {i.Nombres.PadRight(NomWidth)} {i.Apellidos.PadRight(ApeWidth)} {i.Edad.ToString().PadRight(EdadWidth)} {i.Sexo.PadRight(SexHeight)}   |");
            }
            Console.WriteLine(new string('-', IdWidth + NomWidth + ApeWidth + EdadWidth + 12));

            break;
        #endregion

        #region Salir

        case 5:
            Vali1 = false;
            break;

            #endregion
    }
    #endregion
}
#endregion

Console.WriteLine("\nCreado por: Jhonson Leiva 愛");