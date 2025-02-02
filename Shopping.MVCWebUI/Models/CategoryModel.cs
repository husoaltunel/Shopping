﻿using Shopping.MVCWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.MVCWebUI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EnumIsActiveState IsActive { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}