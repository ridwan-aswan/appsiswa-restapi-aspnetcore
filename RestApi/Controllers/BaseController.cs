using Microsoft.AspNetCore.Mvc;
using RestApi.Model;
using RestApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [Route("api/Siswa")]
    public class BaseController : ControllerBase
    {

        private readonly ISiswaRepository repository;

        public BaseController(ISiswaRepository _repository)

        {
            this.repository = _repository;

        }

        [HttpPost, Route("saveSiswa")]
        public async Task<ActionResult> saveSiswa(Siswa obj)

        {
            string msg;

            var result = await this.repository.saveSiswa(obj);

            if (result == 1)
            {

                msg = "Berhasil TAMBAH data.";

            }
            else if (result == 2)
            {

                msg = "Berhasil EDIT data.";
            }
            else
            {

                msg = "Gagal simpan data.";

            }


            return Ok(new { code = result, message = msg });


        }

        [HttpGet, Route("getAll")]
        public async Task<ActionResult> getAll()
        {

            return Ok(await this.repository.getAll());

        }
        [HttpGet, Route("getSiswaByID/{id}")]
        public async Task<ActionResult> getSiswaByID(long id)
        {

            var result = await this.repository.getSiswaByID(id);

            if (result == null) {

                return Ok(new {detail_id = 0});
            }


            return Ok(result);
        }

        [HttpPost, Route("deleteSiswa")]
        public async Task<ActionResult> DeleteSiswa(long id)
        {

            var result = await this.repository.deleteSiswa(id);

            if (result == 1)
            {

                return Ok(new { code = result, message = "Berhasil Hapus Data !" });

            }
            else
            {

                return Ok(new { code = result, message = "Gagal Hapus Data !" });

            }

        }

    }
}
