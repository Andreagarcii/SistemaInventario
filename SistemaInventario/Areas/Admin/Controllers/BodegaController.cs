using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.AccesoDatos.Repositorio;
using SistemaInventario.AccesoDatos;
using System.Reflection.Metadata.Ecma335;
using SistemaInventario.Modelos;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public BodegaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Bodega bodega =new Bodega();
            if (id == null) 
            {
                //Crear nueva bodega
                bodega.Estado = true;
                return View(bodega);
            }

            bodega = await _unidadTrabajo.Bodega.obtener(id.GetValueOrDefault());
            if (bodega == null)
            {
                return NotFound();
            }
            return View(bodega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upsert(Bodega bodega)
        {
            if(ModelState.IsValid)
            {
                if(bodega.Id == 0)
                {
                    await _unidadTrabajo.Bodega.Agregar(bodega);

                }
                else
                {
                    _unidadTrabajo.Bodega.Actualizar(bodega);
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(bodega) ;
        }

        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var bodegaDb = await _unidadTrabajo.Bodega.obtener(id);
                if(bodegaDb == null)
                {
                return Json(new {succes = false, message = "Error al borrar "});
                _unidadTrabajo.Bodega.Remover(bodegaDb);
                await _unidadTrabajo.Guardar();
                return Json(new { succes = true, message ="bodega eliminada co exito " });
            }
        }


        #region API
        [HttpGet]
        public async Task<IActionResult>ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Bodega.ObtenerTodos();
            return Json(new { data = todos });
        }
        #endregion
    }
}
