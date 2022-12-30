using System;

namespace asp_net_IVR
{
    public class Payload
    {
        public string call_control_id { get; set; }
        public string call_leg_id { get; set; }
        public string call_session_id { get; set; }
        public string client_state { get; set; }
        public string connection_id { get; set; }
        public string from { get; set; }
        public string state { get; set; }
        public string to { get; set; }
        public string direction { get; set; }
        public string digit { get; set; }
        public string digits { get; set; }
        public string hangup_cause { get; set; }
        public string hangup_source { get; set; }
        public string status { get; set; }
    }

    public class Data
    {
        public string event_type { get; set; }
        public string id { get; set; }
        public DateTime occurred_at { get; set; }
        public Payload payload { get; set; }
        public string record_type { get; set; }
    }

    public class CallControlWebhook
    {
        public Data data { get; set; }
    }



}