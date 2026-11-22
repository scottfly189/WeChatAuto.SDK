pub mod add_friends_options;
pub use self::add_friends_options::AddFriendsOptions;
pub mod add_friends_request;
pub use self::add_friends_request::AddFriendsRequest;
pub mod add_group_member_to_friends_request;
pub use self::add_group_member_to_friends_request::AddGroupMemberToFriendsRequest;
pub mod add_moments_request;
pub use self::add_moments_request::AddMomentsRequest;
pub mod change_group_memo_request;
pub use self::change_group_memo_request::ChangeGroupMemoRequest;
pub mod change_group_nick_name_request;
pub use self::change_group_nick_name_request::ChangeGroupNickNameRequest;
pub mod chat_refer;
pub use self::chat_refer::ChatRefer;
pub mod chat_simple_message;
pub use self::chat_simple_message::ChatSimpleMessage;
pub mod chat_type;
pub use self::chat_type::ChatType;
pub mod clone_voice_request;
pub use self::clone_voice_request::CloneVoiceRequest;
pub mod color;
pub use self::color::Color;
pub mod color_palette;
pub use self::color_palette::ColorPalette;
pub mod create_group_request;
pub use self::create_group_request::CreateGroupRequest;
pub mod database_query_request;
pub use self::database_query_request::DatabaseQueryRequest;
pub mod forward_message_type_enums;
pub use self::forward_message_type_enums::ForwardMessageTypeEnums;
pub mod forward_multiple_message_request;
pub use self::forward_multiple_message_request::ForwardMultipleMessageRequest;
pub mod forward_single_message_request;
pub use self::forward_single_message_request::ForwardSingleMessageRequest;
pub mod friend_info;
pub use self::friend_info::FriendInfo;
pub mod friend_keyword_rule_request;
pub use self::friend_keyword_rule_request::FriendKeywordRuleRequest;
pub mod friend_search_result_enums;
pub use self::friend_search_result_enums::FriendSearchResultEnums;
pub mod group_member_request;
pub use self::group_member_request::GroupMemberRequest;
pub mod image;
pub use self::image::Image;
pub mod image_format;
pub use self::image_format::ImageFormat;
pub mod invite_group_member_request;
pub use self::invite_group_member_request::InviteGroupMemberRequest;
pub mod locate_conversation_request;
pub use self::locate_conversation_request::LocateConversationRequest;
pub mod moments_options;
pub use self::moments_options::MomentsOptions;
pub mod navigation_type;
pub use self::navigation_type::NavigationType;
pub mod notify_icon_click_request;
pub use self::notify_icon_click_request::NotifyIconClickRequest;
pub mod passed_all_new_friend_request;
pub use self::passed_all_new_friend_request::PassedAllNewFriendRequest;
pub mod pixel_format;
pub use self::pixel_format::PixelFormat;
pub mod property_item;
pub use self::property_item::PropertyItem;
pub mod pull_messages_request;
pub use self::pull_messages_request::PullMessagesRequest;
pub mod reference_request;
pub use self::reference_request::ReferenceRequest;
pub mod remove_moments_request;
pub use self::remove_moments_request::RemoveMomentsRequest;
pub mod rename_group_request;
pub use self::rename_group_request::RenameGroupRequest;
pub mod save_avatar_request;
pub use self::save_avatar_request::SaveAvatarRequest;
pub mod search_conversation_request;
pub use self::search_conversation_request::SearchConversationRequest;
pub mod send_emoji_by_wxid_request;
pub use self::send_emoji_by_wxid_request::SendEmojiByWxidRequest;
pub mod send_emoji_request;
pub use self::send_emoji_request::SendEmojiRequest;
pub mod send_file_by_wxid_request;
pub use self::send_file_by_wxid_request::SendFileByWxidRequest;
pub mod send_file_request;
pub use self::send_file_request::SendFileRequest;
pub mod send_message_by_wxid_request;
pub use self::send_message_by_wxid_request::SendMessageByWxidRequest;
pub mod send_message_request;
pub use self::send_message_request::SendMessageRequest;
pub mod send_voice_chats_request;
pub use self::send_voice_chats_request::SendVoiceChatsRequest;
pub mod send_voice_message_by_wxid_request;
pub use self::send_voice_message_by_wxid_request::SendVoiceMessageByWxidRequest;
pub mod send_voice_message_request;
pub use self::send_voice_message_request::SendVoiceMessageRequest;
pub mod send_voice_message_tts_by_wxid_request;
pub use self::send_voice_message_tts_by_wxid_request::SendVoiceMessageTtsByWxidRequest;
pub mod send_voice_message_tts_request;
pub use self::send_voice_message_tts_request::SendVoiceMessageTtsRequest;
pub mod set_do_not_disturb_request;
pub use self::set_do_not_disturb_request::SetDoNotDisturbRequest;
pub mod set_top_most_request;
pub use self::set_top_most_request::SetTopMostRequest;
pub mod size;
pub use self::size::Size;
pub mod size_f;
pub use self::size_f::SizeF;
pub mod sub_window_request;
pub use self::sub_window_request::SubWindowRequest;
pub mod switch_navigation_request;
pub use self::switch_navigation_request::SwitchNavigationRequest;
pub mod tap_request;
pub use self::tap_request::TapRequest;
pub mod update_group_notice_request;
pub use self::update_group_notice_request::UpdateGroupNoticeRequest;
pub mod voice_options;
pub use self::voice_options::VoiceOptions;
pub mod who_request;
pub use self::who_request::WhoRequest;
use serde::{Deserialize, Deserializer, Serializer};
use serde_with::{de::DeserializeAsWrap, ser::SerializeAsWrap, DeserializeAs, SerializeAs};
use std::marker::PhantomData;

pub(crate) struct DoubleOption<T>(PhantomData<T>);

impl<T, TAs> SerializeAs<Option<Option<T>>> for DoubleOption<TAs>
where
    TAs: SerializeAs<T>,
{
    fn serialize_as<S>(values: &Option<Option<T>>, serializer: S) -> Result<S::Ok, S::Error>
    where
        S: Serializer,
    {
        match values {
            None => serializer.serialize_unit(),
            Some(None) => serializer.serialize_none(),
            Some(Some(v)) => serializer.serialize_some(&SerializeAsWrap::<T, TAs>::new(v)),
        }
    }
}

impl<'de, T, TAs> DeserializeAs<'de, Option<Option<T>>> for DoubleOption<TAs>
where
    TAs: DeserializeAs<'de, T>,
    T: std::fmt::Debug,
{
    fn deserialize_as<D>(deserializer: D) -> Result<Option<Option<T>>, D::Error>
    where
        D: Deserializer<'de>,
    {
        Ok(Some(
            DeserializeAsWrap::<Option<T>, Option<TAs>>::deserialize(deserializer)?
                .into_inner(),
        ))
    }
}
