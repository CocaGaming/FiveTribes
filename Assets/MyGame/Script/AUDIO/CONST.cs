using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONST
{
    public const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";//lưu giá trị âm lượng của ng chơi lần trước đó, khi mở app sẽ lấy lại gtri cũ
    public const string SE_VOLUME_KEY = "SE_VOLUME_KEY";//nếu get gtri đã lưu mà ko có thì sẽ trả về giá trị default bên dưới
    public const string LONGSE_VOLUME_KEY = "LONGSE_VOLUME_KEY";
    public const float BGM_VOLUME_DEFAULT = 1f;
    public const float SE_VOLUME_DEFAULT = 1f;
    public const float LONGSE_VOLUME_DEFAULT=0.4f;
    public const float BGM_FADE_SPEED_RATE_HIGH = 0.5f;//tốc độ chuyển giao bài nhạc khác
    public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
}
