using System.Collections.Generic;
using System;

namespace dotnet_starter.Models
{
  // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
  public class OutboundWebhookPayload
  {
    public object completed_at { get; set; }
    public object cost { get; set; }
    public string direction { get; set; }
    public string encoding { get; set; }
    public List<object> errors { get; set; }
    public string from { get; set; }
    public string id { get; set; }
    public List<object> media { get; set; }
    public string messaging_profile_id { get; set; }
    public string organization_id { get; set; }
    public int parts { get; set; }
    public object received_at { get; set; }
    public string record_type { get; set; }
    public object sent_at { get; set; }
    public List<object> tags { get; set; }
    public string text { get; set; }
    public List<To> to { get; set; }
    public string type { get; set; }
    public DateTime valid_until { get; set; }
    public string webhook_failover_url { get; set; }
    public string webhook_url { get; set; }
  }

  public class OutboundWebhookData
  {
    public string event_type { get; set; }
    public string id { get; set; }
    public DateTime occurred_at { get; set; }
    public OutboundWebhookPayload payload { get; set; }
    public string record_type { get; set; }
  }

  public class OutboundWebhook
  {
    public OutboundWebhookData data { get; set; }
    public Meta meta { get; set; }
  }

}