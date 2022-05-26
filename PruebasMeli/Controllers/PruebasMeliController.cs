using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PruebasMeli.Models;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web.Http.Cors;

namespace PruebasMeli.Controllers
{
    public class PruebasMeliController : ApiController
    {
        private PruebasMeliEntities db = new PruebasMeliEntities();
        public JObject response;
        public JObject respuesta;

        [HttpPost]
        [ResponseType(typeof(JObject))]
        [Route("api/guardarAdn")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public JObject GuardarAdn([FromBody] JObject dataCodigoFromBody)
        {
            response = JObject.Parse(@"{response:{},status:'',exception:''}");
            respuesta = JObject.Parse(@"{codigo:'',respuesta:'',mensaje:''}");
            try
            {
                string secuenciaAdn = dataCodigoFromBody["secuenciaAdn"].ToString();
                bool esMutante = Convert.ToBoolean(dataCodigoFromBody["es_mutante"]);
                List<ResultadosPruebasAdn> ResultadosPruebasAdn = db.ResultadosPruebasAdn.Where(x => x.secuencia_adn == secuenciaAdn).ToList();
                if (ResultadosPruebasAdn.Count() > 0)
                {
                    this.respuesta["codigo"] = "0";
                    this.respuesta["respuesta"] = "Ok";
                    this.respuesta["mensaje"] = "El suario ya existe en la bd";

                    this.response["response"] = this.respuesta;
                    this.response["status"] = "200";
                    this.response["exception"] = "";
                }
                else
                {
                    DateTime fecha = DateTime.Now;
                    //List<BiometriaVoz> biometriavozList = new List<BiometriaVoz>();
                    db.ResultadosPruebasAdn.Add(new ResultadosPruebasAdn
                    {
                        secuencia_adn = secuenciaAdn,
                        es_mutante = esMutante,
                        fecha = fecha,
                    });
                    //db.BiometriaVoz.Add(biometriavozList);
                    int datosIngrasados = db.SaveChanges();
                    if (datosIngrasados > 0)
                    {
                        this.respuesta["codigo"] = "1";
                        this.respuesta["respuesta"] = "Ok";
                        this.respuesta["mensaje"] = "Usuario insertado en la bd";

                        this.response["response"] = this.respuesta;
                        this.response["status"] = "200";
                        this.response["exception"] = "";
                    }
                    else
                    {
                        this.respuesta["codigo"] = "0";
                        this.respuesta["respuesta"] = "Nok";
                        this.respuesta["mensaje"] = "No se pudo ingresar en la bd";

                        this.response["response"] = this.respuesta;
                        this.response["status"] = "500";
                        this.response["exception"] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                this.response["status"] = "500";
                this.response["exception"] = ex.ToString();
            }
            return this.response;
        }

        [HttpGet]
        [ResponseType(typeof(JObject))]
        [Route("api/estadisticas")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public JObject Estadisticas()
        {
            respuesta = JObject.Parse(@"{count_mutant_dna:'',cont_human_dna:''}");
            try
            {
                List<ResultadosPruebasAdn> ResultadosPruebasAdn;
                
                ResultadosPruebasAdn = db.ResultadosPruebasAdn.Where(x => x.es_mutante == true).ToList();
                int mutantes = ResultadosPruebasAdn.Count();

                ResultadosPruebasAdn = db.ResultadosPruebasAdn.Where(x => x.es_mutante != true).ToList();
                int humanos = ResultadosPruebasAdn.Count();

                this.respuesta["count_mutant_dna"] = mutantes;
                this.respuesta["cont_human_dna"] = humanos;
                
            }
            catch (Exception ex)
            {
                this.respuesta["status"] = "500";
                this.respuesta["exception"] = ex.ToString();
            }
            return this.respuesta;
        }

        // GET api/PruebasMeli
        public IQueryable<ResultadosPruebasAdn> GetResultadosPruebasAdn()
        {
            return db.ResultadosPruebasAdn;
        }

        // GET api/PruebasMeli/5
        [ResponseType(typeof(ResultadosPruebasAdn))]
        public IHttpActionResult GetResultadosPruebasAdn(int id)
        {
            ResultadosPruebasAdn resultadospruebasadn = db.ResultadosPruebasAdn.Find(id);
            if (resultadospruebasadn == null)
            {
                return NotFound();
            }

            return Ok(resultadospruebasadn);
        }

        // PUT api/PruebasMeli/5
        public IHttpActionResult PutResultadosPruebasAdn(int id, ResultadosPruebasAdn resultadospruebasadn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resultadospruebasadn.id)
            {
                return BadRequest();
            }

            db.Entry(resultadospruebasadn).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultadosPruebasAdnExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/PruebasMeli
        [ResponseType(typeof(ResultadosPruebasAdn))]
        public IHttpActionResult PostResultadosPruebasAdn(ResultadosPruebasAdn resultadospruebasadn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ResultadosPruebasAdn.Add(resultadospruebasadn);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = resultadospruebasadn.id }, resultadospruebasadn);
        }

        // DELETE api/PruebasMeli/5
        [ResponseType(typeof(ResultadosPruebasAdn))]
        public IHttpActionResult DeleteResultadosPruebasAdn(int id)
        {
            ResultadosPruebasAdn resultadospruebasadn = db.ResultadosPruebasAdn.Find(id);
            if (resultadospruebasadn == null)
            {
                return NotFound();
            }

            db.ResultadosPruebasAdn.Remove(resultadospruebasadn);
            db.SaveChanges();

            return Ok(resultadospruebasadn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResultadosPruebasAdnExists(int id)
        {
            return db.ResultadosPruebasAdn.Count(e => e.id == id) > 0;
        }
    }
}