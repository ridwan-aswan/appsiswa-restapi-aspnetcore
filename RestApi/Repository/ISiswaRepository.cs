using Microsoft.AspNetCore.Http;
using RestApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Repository
{
    public interface ISiswaRepository
    
    {
        Task<int> saveSiswa(Siswa obj);

        Task<IEnumerable<Siswa>> getAll();

        Task<Siswa> getSiswaByID(long id);

        Task<int> deleteSiswa(long id);

        Task<string> WriteFile(IFormFile file, string fileNames);
    }

}
