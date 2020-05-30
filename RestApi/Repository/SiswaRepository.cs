using RestApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RestApi.Repository
{
    public class SiswaRepository : ISiswaRepository
    {

        private IDBContext _context;

        public SiswaRepository(IDBContext context)
        {
            _context = context;
        }

        public async Task<int> saveSiswa(Siswa obj)
        {

            int response = 0;
            System.Guid guid = System.Guid.NewGuid();
            string fotoNames = guid.ToString() + "." + obj.file.FileName.Split('.')[obj.file.FileName.Split('.').Length - 1];
         
            try
            {
              
                var parameters = new DynamicParameters();
              
                parameters.Add("@id", obj.detail_id);
                parameters.Add("@nama", obj.nama);
                parameters.Add("@alamat", obj.alamat);
                parameters.Add("@tempat_lahir", obj.tempat_lahir);
                parameters.Add("@tanggal_lahir", obj.tanggal_lahir);
                parameters.Add("@jenis_kelamin", obj.jenis_kelamin);
                parameters.Add("@foto", fotoNames);
                parameters.Add("@return_code", 0, direction: ParameterDirection.Output);

                await _context.db.ExecuteAsync("spSaveSiswa", parameters, commandType: CommandType.StoredProcedure);

                response = parameters.Get<int>("@return_code");

                if (response > 0)
                {

                      await WriteFile(obj.file, fotoNames);

                }

            }

            catch (Exception ex)

            {


                Debug.WriteLine("Error : " + ex);

            }

            return response;


        }

        public async Task<string> WriteFile(IFormFile file, string fileNames)
        {
             
            try
            {
                 

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"  , fileNames);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileNames;
        }

        public async Task<int> deleteSiswa(long id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@id", id);
            parameters.Add("@return_code", 0, direction: ParameterDirection.Output);

            await _context.db.ExecuteAsync("spDeleteSiswa", parameters, commandType: CommandType.StoredProcedure);

            var response = parameters.Get<int>("@return_code");

            return response;
        }

        public async  Task<IEnumerable<Siswa>> getAll()

        {
            string _sql;

            _sql = @"SELECT *,TIMESTAMPDIFF(YEAR,tanggal_lahir,NOW()) AS umur FROM tbl_mst_siswa";

            var result = await _context.db.QueryAsync<Siswa>(_sql, commandType: CommandType.Text);

            return result.ToList();



        }

        public async Task<Siswa> getSiswaByID(long id)
        {
            string _sql;

            _sql = @"SELECT * FROM tbl_mst_siswa WHERE detail_id=@detail_id";

            var result = await _context.db.QuerySingleOrDefaultAsync<Siswa>(_sql,new { detail_id = id }, commandType: CommandType.Text);

            return result;
        }

       
    }
}
