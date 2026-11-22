
#include "AddFriendsOptionsTest.cpp"

#include "AddFriendsRequestTest.cpp"

#include "AddGroupMemberToFriendsRequestTest.cpp"

#include "AddMomentsRequestTest.cpp"

#include "ChangeGroupMemoRequestTest.cpp"

#include "ChangeGroupNickNameRequestTest.cpp"

#include "ChatReferTest.cpp"

#include "ChatSimpleMessageTest.cpp"

#include "ChatTypeTest.cpp"

#include "CloneVoiceRequestTest.cpp"

#include "ColorTest.cpp"

#include "ColorPaletteTest.cpp"

#include "CreateGroupRequestTest.cpp"

#include "DatabaseQueryRequestTest.cpp"

#include "ForwardMessageTypeEnumsTest.cpp"

#include "ForwardMultipleMessageRequestTest.cpp"

#include "ForwardSingleMessageRequestTest.cpp"

#include "FriendInfoTest.cpp"

#include "FriendKeywordRuleRequestTest.cpp"

#include "FriendSearchResultEnumsTest.cpp"

#include "GroupMemberRequestTest.cpp"

#include "ImageTest.cpp"

#include "ImageFormatTest.cpp"

#include "InviteGroupMemberRequestTest.cpp"

#include "LocateConversationRequestTest.cpp"

#include "MomentsOptionsTest.cpp"

#include "NavigationTypeTest.cpp"

#include "NotifyIconClickRequestTest.cpp"

#include "PassedAllNewFriendRequestTest.cpp"

#include "PixelFormatTest.cpp"

#include "PropertyItemTest.cpp"

#include "PullMessagesRequestTest.cpp"

#include "ReferenceRequestTest.cpp"

#include "RemoveMomentsRequestTest.cpp"

#include "RenameGroupRequestTest.cpp"

#include "SaveAvatarRequestTest.cpp"

#include "SearchConversationRequestTest.cpp"

#include "SendEmojiByWxidRequestTest.cpp"

#include "SendEmojiRequestTest.cpp"

#include "SendFileByWxidRequestTest.cpp"

#include "SendFileRequestTest.cpp"

#include "SendMessageByWxidRequestTest.cpp"

#include "SendMessageRequestTest.cpp"

#include "SendVoiceChatsRequestTest.cpp"

#include "SendVoiceMessageByWxidRequestTest.cpp"

#include "SendVoiceMessageRequestTest.cpp"

#include "SendVoiceMessageTtsByWxidRequestTest.cpp"

#include "SendVoiceMessageTtsRequestTest.cpp"

#include "SetDoNotDisturbRequestTest.cpp"

#include "SetTopMostRequestTest.cpp"

#include "SizeTest.cpp"

#include "SizeFTest.cpp"

#include "SubWindowRequestTest.cpp"

#include "SwitchNavigationRequestTest.cpp"

#include "TapRequestTest.cpp"

#include "UpdateGroupNoticeRequestTest.cpp"

#include "VoiceOptionsTest.cpp"

#include "WhoRequestTest.cpp"



void setUp(){}

void tearDown(){}

void runTests(){
    
    
    RUN_TEST(test_AddFriendsOptions_intervalTime_is_assigned_from_json);
    
    
    RUN_TEST(test_AddFriendsOptions_isCloseWin_is_assigned_from_json);
    
    
    RUN_TEST(test_AddFriendsOptions_sayHi_is_assigned_from_json);
    
    
    RUN_TEST(test_AddFriendsOptions_suffix_is_assigned_from_json);
    
    
    RUN_TEST(test_AddFriendsOptions_label_is_assigned_from_json);
    
    
    
    
    
    
    RUN_TEST(test_AddGroupMemberToFriendsRequest_group_name_is_assigned_from_json);
    
    
    
    
    
    
    RUN_TEST(test_AddMomentsRequest_content_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_ChangeGroupMemoRequest_group_name_is_assigned_from_json);
    
    
    RUN_TEST(test_ChangeGroupMemoRequest_new_memo_is_assigned_from_json);
    
    
    
    RUN_TEST(test_ChangeGroupNickNameRequest_group_name_is_assigned_from_json);
    
    
    RUN_TEST(test_ChangeGroupNickNameRequest_nick_name_is_assigned_from_json);
    
    
    
    
    
    RUN_TEST(test_ChatRefer_isCloseSearchWin_is_assigned_from_json);
    
    
    
    RUN_TEST(test_ChatSimpleMessage_who_is_assigned_from_json);
    
    
    RUN_TEST(test_ChatSimpleMessage_message_is_assigned_from_json);
    
    
    RUN_TEST(test_ChatSimpleMessage_sendDateTime_is_assigned_from_json);
    
    
    RUN_TEST(test_ChatSimpleMessage_dateTime_is_assigned_from_json);
    
    
    RUN_TEST(test_ChatSimpleMessage_uniqueString_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_CloneVoiceRequest_api_key_is_assigned_from_json);
    
    
    RUN_TEST(test_CloneVoiceRequest_audio_url_is_assigned_from_json);
    
    
    RUN_TEST(test_CloneVoiceRequest_target_model_is_assigned_from_json);
    
    
    RUN_TEST(test_CloneVoiceRequest_prefix_is_assigned_from_json);
    
    
    
    RUN_TEST(test_Color_r_is_assigned_from_json);
    
    
    RUN_TEST(test_Color_g_is_assigned_from_json);
    
    
    RUN_TEST(test_Color_b_is_assigned_from_json);
    
    
    RUN_TEST(test_Color_a_is_assigned_from_json);
    
    
    RUN_TEST(test_Color_isKnownColor_is_assigned_from_json);
    
    
    RUN_TEST(test_Color_isEmpty_is_assigned_from_json);
    
    
    RUN_TEST(test_Color_isNamedColor_is_assigned_from_json);
    
    
    RUN_TEST(test_Color_isSystemColor_is_assigned_from_json);
    
    
    RUN_TEST(test_Color_name_is_assigned_from_json);
    
    
    
    RUN_TEST(test_ColorPalette_flags_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_CreateGroupRequest_group_name_is_assigned_from_json);
    
    
    RUN_TEST(test_CreateGroupRequest_first_who_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_DatabaseQueryRequest_sql_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_ForwardMultipleMessageRequest_who_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_ForwardMultipleMessageRequest_row_count_is_assigned_from_json);
    
    
    
    RUN_TEST(test_ForwardSingleMessageRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_ForwardSingleMessageRequest_message_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_ForwardSingleMessageRequest_prev_scroll_number_is_assigned_from_json);
    
    
    
    RUN_TEST(test_FriendInfo_nickName_is_assigned_from_json);
    
    
    RUN_TEST(test_FriendInfo_memoName_is_assigned_from_json);
    
    
    RUN_TEST(test_FriendInfo_area_is_assigned_from_json);
    
    
    
    RUN_TEST(test_FriendInfo_sameGroupNumber_is_assigned_from_json);
    
    
    RUN_TEST(test_FriendInfo_signature_is_assigned_from_json);
    
    
    RUN_TEST(test_FriendInfo_source_is_assigned_from_json);
    
    
    RUN_TEST(test_FriendInfo_wxId_is_assigned_from_json);
    
    
    RUN_TEST(test_FriendInfo_avatarPath_is_assigned_from_json);
    
    
    
    RUN_TEST(test_FriendInfo_avatarImageBase64_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_FriendInfo_addDateTime_is_assigned_from_json);
    
    
    RUN_TEST(test_FriendInfo_name_is_assigned_from_json);
    
    
    
    RUN_TEST(test_FriendKeywordRuleRequest_suffix_is_assigned_from_json);
    
    
    RUN_TEST(test_FriendKeywordRuleRequest_label_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_GroupMemberRequest_group_name_is_assigned_from_json);
    
    
    
    
    
    
    
    RUN_TEST(test_Image_width_is_assigned_from_json);
    
    
    RUN_TEST(test_Image_height_is_assigned_from_json);
    
    
    RUN_TEST(test_Image_horizontalResolution_is_assigned_from_json);
    
    
    RUN_TEST(test_Image_verticalResolution_is_assigned_from_json);
    
    
    RUN_TEST(test_Image_flags_is_assigned_from_json);
    
    
    
    
    
    
    
    
    
    RUN_TEST(test_ImageFormat_guid_is_assigned_from_json);
    
    
    
    RUN_TEST(test_InviteGroupMemberRequest_group_name_is_assigned_from_json);
    
    
    
    RUN_TEST(test_InviteGroupMemberRequest_invite_reason_is_assigned_from_json);
    
    
    
    RUN_TEST(test_LocateConversationRequest_title_is_assigned_from_json);
    
    
    
    
    
    RUN_TEST(test_MomentsOptions_isCloseMoments_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_NotifyIconClickRequest_index_is_assigned_from_json);
    
    
    RUN_TEST(test_NotifyIconClickRequest_wechat_name_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_PassedAllNewFriendRequest_suffix_is_assigned_from_json);
    
    
    RUN_TEST(test_PassedAllNewFriendRequest_label_is_assigned_from_json);
    
    
    RUN_TEST(test_PassedAllNewFriendRequest_passed_delete_is_assigned_from_json);
    
    
    
    
    
    RUN_TEST(test_PropertyItem_id_is_assigned_from_json);
    
    
    RUN_TEST(test_PropertyItem_len_is_assigned_from_json);
    
    
    RUN_TEST(test_PropertyItem_type_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_PullMessagesRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_PullMessagesRequest_max_fetch_number_is_assigned_from_json);
    
    
    RUN_TEST(test_PullMessagesRequest_include_system_message_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_ReferenceRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_ReferenceRequest_message_content_is_assigned_from_json);
    
    
    RUN_TEST(test_ReferenceRequest_prev_scroll_number_is_assigned_from_json);
    
    
    
    RUN_TEST(test_RemoveMomentsRequest_content_is_assigned_from_json);
    
    
    
    RUN_TEST(test_RenameGroupRequest_old_group_name_is_assigned_from_json);
    
    
    RUN_TEST(test_RenameGroupRequest_new_group_name_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SaveAvatarRequest_path_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SearchConversationRequest_who_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SendEmojiByWxidRequest_user_name_is_assigned_from_json);
    
    
    RUN_TEST(test_SendEmojiByWxidRequest_emoji_is_assigned_from_json);
    
    
    RUN_TEST(test_SendEmojiByWxidRequest_index_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_SendEmojiRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_SendEmojiRequest_emoji_is_assigned_from_json);
    
    
    RUN_TEST(test_SendEmojiRequest_index_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_SendFileByWxidRequest_user_name_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_SendFileRequest_who_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_SendMessageByWxidRequest_user_name_is_assigned_from_json);
    
    
    RUN_TEST(test_SendMessageByWxidRequest_message_is_assigned_from_json);
    
    
    
    
    
    RUN_TEST(test_SendMessageRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_SendMessageRequest_message_is_assigned_from_json);
    
    
    
    
    
    RUN_TEST(test_SendVoiceChatsRequest_who_is_assigned_from_json);
    
    
    
    
    RUN_TEST(test_SendVoiceMessageByWxidRequest_user_name_is_assigned_from_json);
    
    
    RUN_TEST(test_SendVoiceMessageByWxidRequest_file_path_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_SendVoiceMessageRequest_file_path_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageTtsByWxidRequest_user_name_is_assigned_from_json);
    
    
    RUN_TEST(test_SendVoiceMessageTtsByWxidRequest_api_key_is_assigned_from_json);
    
    
    RUN_TEST(test_SendVoiceMessageTtsByWxidRequest_message_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageTtsByWxidRequest_optimize_with_llm_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageTtsRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_SendVoiceMessageTtsRequest_api_key_is_assigned_from_json);
    
    
    RUN_TEST(test_SendVoiceMessageTtsRequest_message_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageTtsRequest_optimize_with_llm_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SetDoNotDisturbRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_SetDoNotDisturbRequest_setting_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SetTopMostRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_SetTopMostRequest_setting_is_assigned_from_json);
    
    
    
    RUN_TEST(test_Size_isEmpty_is_assigned_from_json);
    
    
    RUN_TEST(test_Size_width_is_assigned_from_json);
    
    
    RUN_TEST(test_Size_height_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SizeF_isEmpty_is_assigned_from_json);
    
    
    RUN_TEST(test_SizeF_width_is_assigned_from_json);
    
    
    RUN_TEST(test_SizeF_height_is_assigned_from_json);
    
    
    
    RUN_TEST(test_SubWindowRequest_who_is_assigned_from_json);
    
    
    
    
    
    RUN_TEST(test_TapRequest_who_is_assigned_from_json);
    
    
    RUN_TEST(test_TapRequest_prev_scroll_number_is_assigned_from_json);
    
    
    
    RUN_TEST(test_UpdateGroupNoticeRequest_group_name_is_assigned_from_json);
    
    
    RUN_TEST(test_UpdateGroupNoticeRequest_group_notice_is_assigned_from_json);
    
    
    
    RUN_TEST(test_VoiceOptions_voice_is_assigned_from_json);
    
    
    RUN_TEST(test_VoiceOptions_model_is_assigned_from_json);
    
    
    RUN_TEST(test_VoiceOptions_languageType_is_assigned_from_json);
    
    
    RUN_TEST(test_VoiceOptions_instructions_is_assigned_from_json);
    
    
    RUN_TEST(test_VoiceOptions_volume_is_assigned_from_json);
    
    
    RUN_TEST(test_VoiceOptions_rate_is_assigned_from_json);
    
    
    RUN_TEST(test_VoiceOptions_pitch_is_assigned_from_json);
    
    
    RUN_TEST(test_VoiceOptions_seed_is_assigned_from_json);
    
    
    RUN_TEST(test_VoiceOptions_optimizeInstructions_is_assigned_from_json);
    
    
    RUN_TEST(test_VoiceOptions_isStream_is_assigned_from_json);
    
    
    
    RUN_TEST(test_WhoRequest_who_is_assigned_from_json);
    
    

    
    
    RUN_TEST(test_AddFriendsOptions_intervalTime_is_converted_to_json);
    
    
    RUN_TEST(test_AddFriendsOptions_isCloseWin_is_converted_to_json);
    
    
    RUN_TEST(test_AddFriendsOptions_sayHi_is_converted_to_json);
    
    
    RUN_TEST(test_AddFriendsOptions_suffix_is_converted_to_json);
    
    
    RUN_TEST(test_AddFriendsOptions_label_is_converted_to_json);
    
    
    
    
    
    
    RUN_TEST(test_AddGroupMemberToFriendsRequest_group_name_is_converted_to_json);
    
    
    
    
    
    
    RUN_TEST(test_AddMomentsRequest_content_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_ChangeGroupMemoRequest_group_name_is_converted_to_json);
    
    
    RUN_TEST(test_ChangeGroupMemoRequest_new_memo_is_converted_to_json);
    
    
    
    RUN_TEST(test_ChangeGroupNickNameRequest_group_name_is_converted_to_json);
    
    
    RUN_TEST(test_ChangeGroupNickNameRequest_nick_name_is_converted_to_json);
    
    
    
    
    
    RUN_TEST(test_ChatRefer_isCloseSearchWin_is_converted_to_json);
    
    
    
    RUN_TEST(test_ChatSimpleMessage_who_is_converted_to_json);
    
    
    RUN_TEST(test_ChatSimpleMessage_message_is_converted_to_json);
    
    
    RUN_TEST(test_ChatSimpleMessage_sendDateTime_is_converted_to_json);
    
    
    RUN_TEST(test_ChatSimpleMessage_dateTime_is_converted_to_json);
    
    
    RUN_TEST(test_ChatSimpleMessage_uniqueString_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_CloneVoiceRequest_api_key_is_converted_to_json);
    
    
    RUN_TEST(test_CloneVoiceRequest_audio_url_is_converted_to_json);
    
    
    RUN_TEST(test_CloneVoiceRequest_target_model_is_converted_to_json);
    
    
    RUN_TEST(test_CloneVoiceRequest_prefix_is_converted_to_json);
    
    
    
    RUN_TEST(test_Color_r_is_converted_to_json);
    
    
    RUN_TEST(test_Color_g_is_converted_to_json);
    
    
    RUN_TEST(test_Color_b_is_converted_to_json);
    
    
    RUN_TEST(test_Color_a_is_converted_to_json);
    
    
    RUN_TEST(test_Color_isKnownColor_is_converted_to_json);
    
    
    RUN_TEST(test_Color_isEmpty_is_converted_to_json);
    
    
    RUN_TEST(test_Color_isNamedColor_is_converted_to_json);
    
    
    RUN_TEST(test_Color_isSystemColor_is_converted_to_json);
    
    
    RUN_TEST(test_Color_name_is_converted_to_json);
    
    
    
    RUN_TEST(test_ColorPalette_flags_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_CreateGroupRequest_group_name_is_converted_to_json);
    
    
    RUN_TEST(test_CreateGroupRequest_first_who_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_DatabaseQueryRequest_sql_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_ForwardMultipleMessageRequest_who_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_ForwardMultipleMessageRequest_row_count_is_converted_to_json);
    
    
    
    RUN_TEST(test_ForwardSingleMessageRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_ForwardSingleMessageRequest_message_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_ForwardSingleMessageRequest_prev_scroll_number_is_converted_to_json);
    
    
    
    RUN_TEST(test_FriendInfo_nickName_is_converted_to_json);
    
    
    RUN_TEST(test_FriendInfo_memoName_is_converted_to_json);
    
    
    RUN_TEST(test_FriendInfo_area_is_converted_to_json);
    
    
    
    RUN_TEST(test_FriendInfo_sameGroupNumber_is_converted_to_json);
    
    
    RUN_TEST(test_FriendInfo_signature_is_converted_to_json);
    
    
    RUN_TEST(test_FriendInfo_source_is_converted_to_json);
    
    
    RUN_TEST(test_FriendInfo_wxId_is_converted_to_json);
    
    
    RUN_TEST(test_FriendInfo_avatarPath_is_converted_to_json);
    
    
    
    RUN_TEST(test_FriendInfo_avatarImageBase64_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_FriendInfo_addDateTime_is_converted_to_json);
    
    
    RUN_TEST(test_FriendInfo_name_is_converted_to_json);
    
    
    
    RUN_TEST(test_FriendKeywordRuleRequest_suffix_is_converted_to_json);
    
    
    RUN_TEST(test_FriendKeywordRuleRequest_label_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_GroupMemberRequest_group_name_is_converted_to_json);
    
    
    
    
    
    
    
    RUN_TEST(test_Image_width_is_converted_to_json);
    
    
    RUN_TEST(test_Image_height_is_converted_to_json);
    
    
    RUN_TEST(test_Image_horizontalResolution_is_converted_to_json);
    
    
    RUN_TEST(test_Image_verticalResolution_is_converted_to_json);
    
    
    RUN_TEST(test_Image_flags_is_converted_to_json);
    
    
    
    
    
    
    
    
    
    RUN_TEST(test_ImageFormat_guid_is_converted_to_json);
    
    
    
    RUN_TEST(test_InviteGroupMemberRequest_group_name_is_converted_to_json);
    
    
    
    RUN_TEST(test_InviteGroupMemberRequest_invite_reason_is_converted_to_json);
    
    
    
    RUN_TEST(test_LocateConversationRequest_title_is_converted_to_json);
    
    
    
    
    
    RUN_TEST(test_MomentsOptions_isCloseMoments_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_NotifyIconClickRequest_index_is_converted_to_json);
    
    
    RUN_TEST(test_NotifyIconClickRequest_wechat_name_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_PassedAllNewFriendRequest_suffix_is_converted_to_json);
    
    
    RUN_TEST(test_PassedAllNewFriendRequest_label_is_converted_to_json);
    
    
    RUN_TEST(test_PassedAllNewFriendRequest_passed_delete_is_converted_to_json);
    
    
    
    
    
    RUN_TEST(test_PropertyItem_id_is_converted_to_json);
    
    
    RUN_TEST(test_PropertyItem_len_is_converted_to_json);
    
    
    RUN_TEST(test_PropertyItem_type_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_PullMessagesRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_PullMessagesRequest_max_fetch_number_is_converted_to_json);
    
    
    RUN_TEST(test_PullMessagesRequest_include_system_message_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_ReferenceRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_ReferenceRequest_message_content_is_converted_to_json);
    
    
    RUN_TEST(test_ReferenceRequest_prev_scroll_number_is_converted_to_json);
    
    
    
    RUN_TEST(test_RemoveMomentsRequest_content_is_converted_to_json);
    
    
    
    RUN_TEST(test_RenameGroupRequest_old_group_name_is_converted_to_json);
    
    
    RUN_TEST(test_RenameGroupRequest_new_group_name_is_converted_to_json);
    
    
    
    RUN_TEST(test_SaveAvatarRequest_path_is_converted_to_json);
    
    
    
    RUN_TEST(test_SearchConversationRequest_who_is_converted_to_json);
    
    
    
    RUN_TEST(test_SendEmojiByWxidRequest_user_name_is_converted_to_json);
    
    
    RUN_TEST(test_SendEmojiByWxidRequest_emoji_is_converted_to_json);
    
    
    RUN_TEST(test_SendEmojiByWxidRequest_index_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_SendEmojiRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_SendEmojiRequest_emoji_is_converted_to_json);
    
    
    RUN_TEST(test_SendEmojiRequest_index_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_SendFileByWxidRequest_user_name_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_SendFileRequest_who_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_SendMessageByWxidRequest_user_name_is_converted_to_json);
    
    
    RUN_TEST(test_SendMessageByWxidRequest_message_is_converted_to_json);
    
    
    
    
    
    RUN_TEST(test_SendMessageRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_SendMessageRequest_message_is_converted_to_json);
    
    
    
    
    
    RUN_TEST(test_SendVoiceChatsRequest_who_is_converted_to_json);
    
    
    
    
    RUN_TEST(test_SendVoiceMessageByWxidRequest_user_name_is_converted_to_json);
    
    
    RUN_TEST(test_SendVoiceMessageByWxidRequest_file_path_is_converted_to_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_SendVoiceMessageRequest_file_path_is_converted_to_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageTtsByWxidRequest_user_name_is_converted_to_json);
    
    
    RUN_TEST(test_SendVoiceMessageTtsByWxidRequest_api_key_is_converted_to_json);
    
    
    RUN_TEST(test_SendVoiceMessageTtsByWxidRequest_message_is_converted_to_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageTtsByWxidRequest_optimize_with_llm_is_converted_to_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageTtsRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_SendVoiceMessageTtsRequest_api_key_is_converted_to_json);
    
    
    RUN_TEST(test_SendVoiceMessageTtsRequest_message_is_converted_to_json);
    
    
    
    RUN_TEST(test_SendVoiceMessageTtsRequest_optimize_with_llm_is_converted_to_json);
    
    
    
    RUN_TEST(test_SetDoNotDisturbRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_SetDoNotDisturbRequest_setting_is_converted_to_json);
    
    
    
    RUN_TEST(test_SetTopMostRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_SetTopMostRequest_setting_is_converted_to_json);
    
    
    
    RUN_TEST(test_Size_isEmpty_is_converted_to_json);
    
    
    RUN_TEST(test_Size_width_is_converted_to_json);
    
    
    RUN_TEST(test_Size_height_is_converted_to_json);
    
    
    
    RUN_TEST(test_SizeF_isEmpty_is_converted_to_json);
    
    
    RUN_TEST(test_SizeF_width_is_converted_to_json);
    
    
    RUN_TEST(test_SizeF_height_is_converted_to_json);
    
    
    
    RUN_TEST(test_SubWindowRequest_who_is_converted_to_json);
    
    
    
    
    
    RUN_TEST(test_TapRequest_who_is_converted_to_json);
    
    
    RUN_TEST(test_TapRequest_prev_scroll_number_is_converted_to_json);
    
    
    
    RUN_TEST(test_UpdateGroupNoticeRequest_group_name_is_converted_to_json);
    
    
    RUN_TEST(test_UpdateGroupNoticeRequest_group_notice_is_converted_to_json);
    
    
    
    RUN_TEST(test_VoiceOptions_voice_is_converted_to_json);
    
    
    RUN_TEST(test_VoiceOptions_model_is_converted_to_json);
    
    
    RUN_TEST(test_VoiceOptions_languageType_is_converted_to_json);
    
    
    RUN_TEST(test_VoiceOptions_instructions_is_converted_to_json);
    
    
    RUN_TEST(test_VoiceOptions_volume_is_converted_to_json);
    
    
    RUN_TEST(test_VoiceOptions_rate_is_converted_to_json);
    
    
    RUN_TEST(test_VoiceOptions_pitch_is_converted_to_json);
    
    
    RUN_TEST(test_VoiceOptions_seed_is_converted_to_json);
    
    
    RUN_TEST(test_VoiceOptions_optimizeInstructions_is_converted_to_json);
    
    
    RUN_TEST(test_VoiceOptions_isStream_is_converted_to_json);
    
    
    
    RUN_TEST(test_WhoRequest_who_is_converted_to_json);
    
    


}

int main(void) {
    UNITY_BEGIN();
    runTests();
    return UNITY_END();
}

void setup() {
    UNITY_BEGIN();
    runTests();
    UNITY_END();
}

void loop() {

}
