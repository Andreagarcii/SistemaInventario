using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio.Ireposorio
{
    public interface IBodegaRepositorio : Irepositorio<Bodega>
    {
        void Actualizar (Bodega bodega);
        void Remover(Bodega bodegaDb);
    }
}
