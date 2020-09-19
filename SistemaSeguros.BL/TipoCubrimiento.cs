using SistemaSeguros.DS;
using SistemaSeguros.EX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSeguros.BL
{
    public class TipoCubrimiento
    {
        BDSistemaSeguros db = new BDSistemaSeguros();
        public List<TipoCubrimiento> ObtenerDatos()
        {
            List<TipoCubrimiento> listaTipoCubrimiento = new List<TipoCubrimiento>();

            try
            {
                db.TipoCubrimiento.ToList().ForEach(cp => listaTipoCubrimiento.Add(new TipoCubrimiento()
                {
                    Id = cp.Id,
                    Descripcion = cp.Descripcion
                }));
                return listaTipoCubrimiento;
            }
            catch (Exception ex)
            {
                throw new ControlErrores(System.Reflection.MethodBase.GetCurrentMethod().Name, vccNomClase, ex.Message);
            }
        }
    }
}
