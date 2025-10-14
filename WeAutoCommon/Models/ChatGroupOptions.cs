using Newtonsoft.Json;
namespace WxAutoCommon.Models
{
    public class ChatGroupOptions
    {
        private string _GroupName;
        private bool __GroupNameChanged = false;

        /// <summary>
        /// 群聊名称
        /// </summary>
        public string GroupName
        {
            get
            {
                return this._GroupName;
            }
            set
            {
                if (this._GroupName != value)
                {
                    this.__GroupNameChanged = true;
                }
                this._GroupName = value;
            }
        }

        public bool GroupNameChanged { get => this.__GroupNameChanged; }
        private bool _ShowGroupNickName;
        private bool __ShowGroupNickNameChanged = false;
        public bool ShowGroupNickNameChanged { get => this.__ShowGroupNickNameChanged; }
        /// <summary>
        /// 是否显示群组昵称
        /// </summary>
        public bool ShowGroupNickName
        {
            get
            {
                return this._ShowGroupNickName;
            }
            set
            {
                if (this._ShowGroupNickName != value)
                {
                    this.__ShowGroupNickNameChanged = true;
                }
                this._ShowGroupNickName = value;
            }
        }

        private bool _NoDisturb;
        private bool __NoDisturbChanged = false;
        public bool NoDisturbChanged { get => this.__NoDisturbChanged; }
        /// <summary>
        /// 是否免打扰
        /// </summary>
        public bool NoDisturb
        {
            get
            {
                return this._NoDisturb;
            }
            set
            {
                if (this._NoDisturb != value)
                {
                    this.__NoDisturbChanged = true;
                }
                this._NoDisturb = value;
            }
        }

        private bool _Top;
        private bool __TopChanged = false;
        public bool TopChanged { get => this.__TopChanged; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool Top
        {
            get
            {
                return this._Top;
            }
            set
            {
                if (this._Top != value)
                {
                    this.__TopChanged = true;
                }
                this._Top = value;
            }
        }

        private bool _SaveToAddressBook;
        private bool __SaveToAddressBookChanged = false;
        public bool SaveToAddressBookChanged { get => this.__SaveToAddressBookChanged; }
        /// <summary>
        /// 是否保存至通讯录
        /// </summary>
        public bool SaveToAddressBook
        {
            get
            {
                return this._SaveToAddressBook;
            }
            set
            {
                if (this._SaveToAddressBook != value)
                {
                    this.__SaveToAddressBookChanged = true;
                }
                this._SaveToAddressBook = value;
            }
        }

        private string _GroupNotice;
        private bool __GroupNoticeChanged = false;
        public bool GroupNoticeChanged { get => this.__GroupNoticeChanged; }
        /// <summary>
        /// 群公告
        /// </summary>
        public string GroupNotice
        {
            get
            {
                return this._GroupNotice;
            }
            set
            {
                if (this._GroupNotice != value)
                {
                    this.__GroupNoticeChanged = true;
                }
                this._GroupNotice = value;
            }
        }

        private string _MyGroupNickName;
        private bool __MyGroupNickNameChanged = false;
        public bool MyGroupNickNameChanged { get => this.__MyGroupNickNameChanged; }
        /// <summary>
        /// 我在群里的昵称
        /// </summary>
        public string MyGroupNickName
        {
            get
            {
                return this._MyGroupNickName;
            }
            set
            {
                if (this._MyGroupNickName != value)
                {
                    this.__MyGroupNickNameChanged = true;
                }
                this._MyGroupNickName = value;
            }
        }

        private string _GroupMemo;
        private bool __GroupMemoChanged = false;
        public bool GroupMemoChanged { get => this.__GroupMemoChanged; }
        /// <summary>
        /// 群备注
        /// </summary>
        public string GroupMemo
        {
            get
            {
                return this._GroupMemo;
            }
            set
            {
                if (this._GroupMemo != value)
                {
                    this.__GroupMemoChanged = true;
                }
                this._GroupMemo = value;
            }
        }
        /// <summary>
        /// 重置所有选项
        /// </summary>
        public void Reset()
        {
            this.__GroupNameChanged = false;
            this.__ShowGroupNickNameChanged = false;
            this.__NoDisturbChanged = false;
            this.__TopChanged = false;
            this.__SaveToAddressBookChanged = false;
            this.__GroupNoticeChanged = false;
            this.__MyGroupNickNameChanged = false;
            this.__GroupMemoChanged = false;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}