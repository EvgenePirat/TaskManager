﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO.RoleDto.Response
{
    /// <summary>
    /// DTO for response role entity
    /// </summary>
    public class RoleResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
    }
}