﻿using Microsoft.EntityFrameworkCore;
using ReturninfoPDF.API.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReturninfoPDF.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<File_CV> File_CVs { get; set; }

        public DbSet<Files> Filess { get; set; }
    }
}
