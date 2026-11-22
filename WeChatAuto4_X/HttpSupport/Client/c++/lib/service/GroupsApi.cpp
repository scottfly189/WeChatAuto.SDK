#include "GroupsApi.h"

using namespace Tiny;



        Response<
            String
        >
        GroupsApi::
        groupsAddMembers(
            
            std::string clientId
            , 
            
            GroupMemberRequest groupMemberRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/members"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 
            addHeader("Content-Type", "application/json");



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | POST
            // Body     | groupMemberRequest



            payload = groupMemberRequest.toJson().dump();

            int httpCode = sendRequest(url, "POST", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsAddMembersToFriends(
            
            std::string clientId
            , 
            
            AddGroupMemberToFriendsRequest addGroupMemberToFriendsRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/add-members-to-friends"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 
            addHeader("Content-Type", "application/json");



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | POST
            // Body     | addGroupMemberToFriendsRequest



            payload = addGroupMemberToFriendsRequest.toJson().dump();

            int httpCode = sendRequest(url, "POST", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsChangeMemo(
            
            std::string clientId
            , 
            
            ChangeGroupMemoRequest changeGroupMemoRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/memo"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 
            addHeader("Content-Type", "application/json");



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | PUT
            // Body     | changeGroupMemoRequest



            payload = changeGroupMemoRequest.toJson().dump();

            int httpCode = sendRequest(url, "PUT", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsChangeName(
            
            std::string clientId
            , 
            
            RenameGroupRequest renameGroupRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/name"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 
            addHeader("Content-Type", "application/json");



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | PUT
            // Body     | renameGroupRequest



            payload = renameGroupRequest.toJson().dump();

            int httpCode = sendRequest(url, "PUT", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsChangeNickName(
            
            std::string clientId
            , 
            
            ChangeGroupNickNameRequest changeGroupNickNameRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/nickname"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 
            addHeader("Content-Type", "application/json");



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | PUT
            // Body     | changeGroupNickNameRequest



            payload = changeGroupNickNameRequest.toJson().dump();

            int httpCode = sendRequest(url, "PUT", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsCreate(
            
            std::string clientId
            , 
            
            CreateGroupRequest createGroupRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 
            addHeader("Content-Type", "application/json");



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | POST
            // Body     | createGroupRequest



            payload = createGroupRequest.toJson().dump();

            int httpCode = sendRequest(url, "POST", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsGetMembers(
            
            std::string clientId
            , 
            
            std::string groupName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/members"; //clientId 


            // Headers  | 

            // Query    | groupName 
            addQueryParam("groupName",groupName);

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | GET
            // Body     | 
            int httpCode = sendRequest(url, "GET", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsGetOwner(
            
            std::string clientId
            , 
            
            std::string groupName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/{groupName}/owner"; //clientId groupName 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_groupName("{");
                s_groupName.append("groupName");
                s_groupName.append("}");

                int pos = url.find(s_groupName);

                url.erase(pos, s_groupName.length());
                url.insert(pos, stringify(groupName));


            std::string payload = "";
            // Send Request
            // METHOD | GET
            // Body     | 
            int httpCode = sendRequest(url, "GET", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsInvite(
            
            std::string clientId
            , 
            
            InviteGroupMemberRequest inviteGroupMemberRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/invite"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 
            addHeader("Content-Type", "application/json");



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | POST
            // Body     | inviteGroupMemberRequest



            payload = inviteGroupMemberRequest.toJson().dump();

            int httpCode = sendRequest(url, "POST", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsIsOwner(
            
            std::string clientId
            , 
            
            std::string groupName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/{groupName}/is-owner"; //clientId groupName 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_groupName("{");
                s_groupName.append("groupName");
                s_groupName.append("}");

                int pos = url.find(s_groupName);

                url.erase(pos, s_groupName.length());
                url.insert(pos, stringify(groupName));


            std::string payload = "";
            // Send Request
            // METHOD | GET
            // Body     | 
            int httpCode = sendRequest(url, "GET", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsQuit(
            
            std::string clientId
            , 
            
            std::string groupName
            , 
            
            bool clearHistory
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/{groupName}"; //clientId groupName 


            // Headers  | 

            // Query    | clearHistory 
            addQueryParam("clearHistory",clearHistory);

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_groupName("{");
                s_groupName.append("groupName");
                s_groupName.append("}");

                int pos = url.find(s_groupName);

                url.erase(pos, s_groupName.length());
                url.insert(pos, stringify(groupName));


            std::string payload = "";
            // Send Request
            // METHOD | DELETE
            // Body     | 
            int httpCode = sendRequest(url, "DELETE", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsRemoveMembers(
            
            std::string clientId
            , 
            
            GroupMemberRequest groupMemberRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/members"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 
            addHeader("Content-Type", "application/json");



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | DELETE
            // Body     | groupMemberRequest



            payload = groupMemberRequest.toJson().dump();

            int httpCode = sendRequest(url, "DELETE", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }

        Response<
            String
        >
        GroupsApi::
        groupsUpdateNotice(
            
            std::string clientId
            , 
            
            UpdateGroupNoticeRequest updateGroupNoticeRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/groups/notice"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 
            addHeader("Content-Type", "application/json");



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | PUT
            // Body     | updateGroupNoticeRequest



            payload = updateGroupNoticeRequest.toJson().dump();

            int httpCode = sendRequest(url, "PUT", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }



