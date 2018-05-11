using System;

namespace Engine.Upgrade
{
    public enum MainEvent
    {
        GET_REMOTE_VERSIONFILE_OK = 1,         // 获取远程版本列表OK
        DOWNLOAD_UPGRADEFILE_CONFIRM = 2,      // 确认下载更新文件
    }

    public enum MainVote
    {
        DOWNLOAD_UPGRADEFILE = 1,         // 是否下载更新文件
    }
}
