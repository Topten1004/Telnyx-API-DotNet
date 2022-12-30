using System.Collections.Generic;
using System;

namespace dotnet_starter.Models
{
  // InboundWebhook myDeserializedClass = JsonConvert.DeserializeObject<InboundWebhook>(myJsonResponse);
  public class From
  {
    public string carrier { get; set; }
    public string line_type { get; set; }
    public string phone_number { get; set; }
  }

  public class MediaItem
  {
    public string content_type { get; set; }
    public string hash_sha256 { get; set; }
    public int size { get; set; }
    public string url { get; set; }
  }

  public class To
  {
    public string carrier { get; set; }
    public string line_type { get; set; }
    public string phone_number { get; set; }
    public string status { get; set; }
  }

  public class Payload
  {
    public List<object> cc { get; set; }
    public object completed_at { get; set; }
    public object cost { get; set; }
    public string direction { get; set; }
    public string encoding { get; set; }
    public List<object> errors { get; set; }
    public From from { get; set; }
    public string id { get; set; }
    public List<MediaItem> media { get; set; }
    public string messaging_profile_id { get; set; }
    public string organization_id { get; set; }
    public int parts { get; set; }
    public DateTime received_at { get; set; }
    public string record_type { get; set; }
    public object sent_at { get; set; }
    public List<object> tags { get; set; }
    public string text { get; set; }
    public List<To> to { get; set; }
    public string type { get; set; }
    public object valid_until { get; set; }
    public object webhook_failover_url { get; set; }
    public string webhook_url { get; set; }
  }

  public class Data
  {
    public string event_type { get; set; }
    public string id { get; set; }
    public DateTime occurred_at { get; set; }
    public Payload payload { get; set; }
    public string record_type { get; set; }
  }

  public class Meta
  {
    public int attempt { get; set; }
    public string delivered_to { get; set; }
  }

  public class InboundWebhook
  {
    public Data data { get; set; }
    public Meta meta { get; set; }
  }

}