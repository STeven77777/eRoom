﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eRoom.Shared.CoreLib.Models.Request
{
    public class WaterUsingListRequest : PagingRequest
    {
        public string RoomID { get; set; }
        public DateTime? CreateDate { get; set; }

    }
}
