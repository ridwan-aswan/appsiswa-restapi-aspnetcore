using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Model
{
    public class Siswa
    {
        public int detail_id { get; set; }

        public string nama { get; set; }
        
        public string alamat { get; set; }

        public string tempat_lahir{ get; set; }

        public int umur { get; set; }
        
        public DateTime tanggal_lahir { get; set; }

        public string jenis_kelamin { get; set; }

        public IFormFile file {get;set;}
        
        public string foto {get;set;}

        public string created_date {get;set;}

        public int return_code { get; set; }

    }
}
