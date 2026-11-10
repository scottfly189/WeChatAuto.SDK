using SqlSugar;

namespace WeChatAuto.Models
{
    /// <summary>
    /// 群成员中间表：成员名单、群名片、邀请人来自原生 chat_room.ext_buffer；
    /// 群主/成员/邀请人的显示名在写入时查 Contact（Remark 优先，空则 NickName）填入。
    /// 查询时不必再 JOIN contact / name2id。支持多微信聚合。
    /// </summary>
    [SugarTable("wechat_chat_room_member")]
    [SugarIndex("uk_chat_room_member",
        nameof(WeChatChatRoomMember.FromWechatWxId), OrderByType.Asc,
        nameof(WeChatChatRoomMember.RoomUsername), OrderByType.Asc,
        nameof(WeChatChatRoomMember.MemberUsername), OrderByType.Asc,
        IsUnique = true)]
    [SugarIndex("index_chat_room_member_room", nameof(WeChatChatRoomMember.RoomUsername), OrderByType.Asc)]
    [SugarIndex("index_chat_room_member_from", nameof(WeChatChatRoomMember.FromWechatWxId), OrderByType.Asc)]
    public class WeChatChatRoomMember
    {
        /// <summary>自定义主键，自增。</summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>此记录属于哪个微信昵称（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此成员记录属于哪个微信昵称")]
        public string FromWechat { get; set; } = string.Empty;

        /// <summary>此记录属于哪个微信 wxid（多号聚合）。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "此成员记录属于哪个微信 wxid")]
        public string FromWechatWxId { get; set; } = string.Empty;

        /// <summary>群 username，如 123@chatroom。</summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "群 username")]
        public string RoomUsername { get; set; } = string.Empty;

        /// <summary>群主 username，对应 chat_room.owner；同一群内各成员行重复存放，便于单表查询。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "群主 username")]
        public string Owner { get; set; } = string.Empty;

        /// <summary>
        /// 群主显示名。ext_buffer 无此字段；写入中间表时按 <see cref="Owner"/> 查 Contact，
        /// Remark 优先，空则 NickName。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "群主显示名")]
        public string OwnerNickName { get; set; } = string.Empty;

        /// <summary>
        /// 成员 username（wxid / 微信号 / @openim），对应 ext_buffer Member field 1。
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false, ColumnDescription = "成员 username")]
        public string MemberUsername { get; set; } = string.Empty;

        /// <summary>
        /// 成员显示名（微信昵称/备注，不是群名片）。ext_buffer 无此字段；
        /// 写入中间表时按 <see cref="MemberUsername"/> 查 Contact，Remark 优先，空则 NickName。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "成员显示名")]
        public string MemberNickName { get; set; } = string.Empty;

        /// <summary>群昵称（群名片），对应 ext_buffer Member field 2；与 <see cref="MemberNickName"/> 不是同一字段。</summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "群昵称（群名片）")]
        public string GroupNickname { get; set; } = string.Empty;

        /// <summary>邀请人 username，对应 ext_buffer Member field 4。</summary>
        [SugarColumn(Length = 200, IsNullable = true, ColumnDescription = "邀请人 username")]
        public string Inviter { get; set; } = string.Empty;

        /// <summary>
        /// 邀请人显示名。ext_buffer 无此字段；写入中间表时按 <see cref="Inviter"/> 查 Contact，
        /// Remark 优先，空则 NickName。
        /// </summary>
        [SugarColumn(ColumnDataType = "TEXT", IsNullable = true, ColumnDescription = "邀请人显示名")]
        public string InviterNickName { get; set; } = string.Empty;

        /// <summary>成员标志，对应 ext_buffer Member field 3；位含义未逐项核实。</summary>
        [SugarColumn(ColumnDescription = "成员 flag")]
        public long Flag { get; set; }
    }
}
