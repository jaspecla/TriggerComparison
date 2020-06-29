using System;
using System.Collections.Generic;
using System.Text;

namespace BlobData
{
  public class BlobEventData
  {
    public string Api { get; set; }
    public string ClientRequestId { get; set; }
    public string RequestId { get; set; }
    public string ETag { get; set; }
    public string ContentType { get; set; }
    public ulong ContentLength { get; set; }
    public string BlobType { get; set; }
    public string Url { get; set; }
    public string Sequencer { get; set; }
    public StorageDiagnosticSettings StorageDiagnostics { get; set; }
  }

  public class StorageDiagnosticSettings
  {
    public string BatchId { get; set; }
  }
}
