using Registro.Data;
using Registro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registro.DAO
{
    public class CrdEstudiante
    {
        EstudianteContext db = new EstudianteContext();

        public ClsEstudiante EstudianteIndivi(int Id)
        {
            var buscar = db.Estudiante.FirstOrDefault(x => x.Id == Id);
            return buscar;
        } 

        public void CreateEstu(ClsEstudiante Es)
        {
            ClsEstudiante Estu = new ClsEstudiante();

            Estu.Nombres = Es.Nombres;
            Estu.Apellidos = Es.Apellidos;
            Estu.Edad = Es.Edad;
            Estu.Sexo = Es.Sexo;

            db.Add(Estu);
            db.SaveChanges();
        }

        public void UpdateEstu(ClsEstudiante Estu, int Lector)
        {
            var buscar = EstudianteIndivi(Estu.Id);

            if (buscar == null)
            {
                Console.WriteLine("El id no existe");
            }
            else
            {
                if (Lector == 1)
                {
                    buscar.Nombres = Estu.Nombres;
                }
                else if (Lector == 2)
                {
                    buscar.Apellidos = Estu.Apellidos;
                }
                else if (Lector == 3)
                {
                    buscar.Edad = Estu.Edad;
                }
                else if(Lector == 4)
                {
                    buscar.Sexo = Estu.Sexo;
                }
                db.Update(Estu);
                db.SaveChanges();
            }
        }

        public string DeleteEstu(int Id)
        {
            var buscar = EstudianteIndivi(Id);

            if (buscar == null)
            {
                return "El estudiante no existe";
            }
            else
            {
                string respuesta;
                do
                {
                    Console.WriteLine("¿Desea eliminar este estudiante? (S/N)");
                    respuesta = Console.ReadLine();

                    if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
                    {
                        db.Estudiante.Remove(buscar);
                        db.SaveChanges();
                        return "\nEliminación exitosa";
                    }
                    else if (!respuesta.Equals("N", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Respuesta no válida. Por favor, ingrese 'S' para confirmar o 'N' para cancelar.");
                    }
                } while (!respuesta.Equals("S", StringComparison.OrdinalIgnoreCase) && !respuesta.Equals("N", StringComparison.OrdinalIgnoreCase));
            }
            return string.Empty; // En caso de no retornar otro valor
        }


        public List<ClsEstudiante> ListaEstudiante()
        {
            return db.Estudiante.ToList();
        }
    }
}