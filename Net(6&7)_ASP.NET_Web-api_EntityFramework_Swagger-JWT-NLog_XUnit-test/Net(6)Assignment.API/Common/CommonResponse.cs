using System.Text.Json.Serialization;

namespace Net_6_Assignment.Common
{
    public class CommonResponse<T>
    {
        /*
         *  1. Success: 用于表示操作是否成功。
            2. Message: 操作成功或失败的消息。
            3. Data: 返回的具体数据，可以为空。
            4. Errors: 若操作失败时，返回错误信息列表。
         */
        public bool IsSuccess { get; set; } = true;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; }
    }
}
