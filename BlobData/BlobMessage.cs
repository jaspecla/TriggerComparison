﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlobData
{
  public class BlobMessage
  {
    public string TriggerType { get; set; }
    public string FileName { get; set; }
    public DateTime TimeProcessed { get; set; }
  }
}
