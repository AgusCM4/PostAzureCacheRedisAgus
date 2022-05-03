using Newtonsoft.Json;
using PostAzureCacheRedisAgus.Helpers;
using PostAzureCacheRedisAgus.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAzureCacheRedisAgus.Services
{
    public class ServiceCacheRedis
    {
        private IDatabase database;

        public ServiceCacheRedis()
        {
            this.database = CacheRedisMultiplexer.GetConnection.GetDatabase();
        }

        public void AlmacenarFavoritoCache(Producto producto)
        {
            string jsonProductos = this.database.StringGet("favoritos2");
            List<Producto> favoritos;
            if (jsonProductos == null)
            {
                favoritos = new List<Producto>();
            }
            else
            {
                favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }
            favoritos.Add(producto);
            jsonProductos = JsonConvert.SerializeObject(favoritos);
            this.database.StringSet("favoritos2", jsonProductos);
        }

        public List<Producto> GetFavoritosCache()
        {
            string jsonProductos = this.database.StringGet("favoritos2");
            if (jsonProductos == null)
            {
                return null;
            }
            else
            {
                List<Producto> favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                return favoritos;
            }
        }

        public void EliminarFavoritoCache(int idproducto)
        {
            string jsonProductos = this.database.StringGet("favoritos2");
            if (jsonProductos != null)
            {
                List<Producto> favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                Producto favorito = favoritos.FirstOrDefault(x => x.IdProducto == idproducto);
                favoritos.Remove(favorito);                
                if (favoritos.Count == 0)
                {
                    this.database.KeyDelete("favoritos2");
                }
                else
                {
                    jsonProductos = JsonConvert.SerializeObject(favoritos);
                    this.database.StringSet("favoritos2", jsonProductos, TimeSpan.FromMinutes(15));
                }
            }
        }
    }
}
