using Microsoft.AspNetCore.Mvc;
using PostAzureCacheRedisAgus.Models;
using PostAzureCacheRedisAgus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAzureCacheRedisAgus.Controllers
{
    public class TarjetasController : Controller
    {
        private ServiceCacheRedis service;
        public List<Producto> productos;

        public TarjetasController(ServiceCacheRedis service)
        {
            this.service = service;
            this.productos = new List<Producto>();
            Producto p1 = new Producto()
            {
                IdProducto = 1,
                Nombre = "Martillo",
                Imagen = "https://m.media-amazon.com/images/I/512KELNIxUL._AC_SX569_.jpg"
            };
            Producto p2 = new Producto()
            {
                IdProducto = 2,
                Nombre = "Destornillador",
                Imagen = "https://pimdatacdn.bahco.com/media/sub1058/16aa0ce3e5fbb23f.png"
            };
            Producto p3 = new Producto()
            {
                IdProducto = 3,
                Nombre = "Sierra",
                Imagen = "https://www.hogarmania.com/archivos/201105/brico-sierra-manual-xl-668x400x80xX.jpg"
            };
            Producto p4 = new Producto()
            {
                IdProducto = 4,
                Nombre = "Tornillos",
                Imagen = "https://tytenlinea.com/wp-content/uploads/2016/09/tornillos.jpg"
            };
            this.productos.Add(p1);
            this.productos.Add(p2);
            this.productos.Add(p3);
            this.productos.Add(p4);
        }

        public IActionResult Index()
        {                        
            return View(productos);
        }

        public IActionResult AddFavorito(int id)
        {            
            Producto p = this.productos.FirstOrDefault(x => x.IdProducto == id);
            service.AlmacenarFavoritoCache(p);
            this.productos.Remove(p);
            return RedirectToAction("Index");
        }

        public IActionResult ConsultarFavoritos()
        {
            List<Producto> productos = this.service.GetFavoritosCache();
            return View(productos);
        }

        public IActionResult EliminarFavoritos(int id)
        {
            this.service.EliminarFavoritoCache(id);
            return RedirectToAction("ConsultarFavoritos");
        }
    }
}
