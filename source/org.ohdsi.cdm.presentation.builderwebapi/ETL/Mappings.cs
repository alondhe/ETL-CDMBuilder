﻿using Microsoft.AspNetCore.Http;

namespace org.ohdsi.cdm.presentation.builderwebapi.ETL
{
    public class Mappings
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}