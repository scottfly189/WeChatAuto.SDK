#include "FriendsApi.h"

using namespace Tiny;



        Response<
            String
        >
        FriendsApi::
        friendsAdd(
            
            std::string clientId
            , 
            
            AddFriendsRequest addFriendsRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/add"; //clientId 


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
            // Body     | addFriendsRequest



            payload = addFriendsRequest.toJson().dump();

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
        FriendsApi::
        friendsAddOrUpdateFriend(
            
            std::string clientId
            , 
            
            FriendInfo friendInfo
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/cache"; //clientId 


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
            // Body     | friendInfo



            payload = friendInfo.toJson().dump();

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
        FriendsApi::
        friendsCloseAddWindow(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/add-window/close"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | POST
            // Body     | 
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
        FriendsApi::
        friendsGetAllFriendNames(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/names"; //clientId 


            // Headers  | 

            // Query    | 

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
        FriendsApi::
        friendsGetAllFriends(
            
            std::string clientId
            , 
            
            bool fromCache
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends"; //clientId 


            // Headers  | 

            // Query    | fromCache 
            addQueryParam("fromCache",fromCache);

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
        FriendsApi::
        friendsGetFriendByWxidFromCache(
            
            std::string clientId
            , 
            
            std::string wxid
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid}"; //clientId wxid 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_wxid("{");
                s_wxid.append("wxid");
                s_wxid.append("}");

                int pos = url.find(s_wxid);

                url.erase(pos, s_wxid.length());
                url.insert(pos, stringify(wxid));


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
        FriendsApi::
        friendsGetFriendFromCache(
            
            std::string clientId
            , 
            
            std::string who
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/cache/{who}"; //clientId who 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_who("{");
                s_who.append("who");
                s_who.append("}");

                int pos = url.find(s_who);

                url.erase(pos, s_who.length());
                url.insert(pos, stringify(who));


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
        FriendsApi::
        friendsGetFriendListFromCache(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/cache"; //clientId 


            // Headers  | 

            // Query    | 

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
        FriendsApi::
        friendsGetFriendsFromCache(
            
            std::string clientId
            , 
            
            std::string who
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/cache/by-name/{who}"; //clientId who 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_who("{");
                s_who.append("who");
                s_who.append("}");

                int pos = url.find(s_who);

                url.erase(pos, s_who.length());
                url.insert(pos, stringify(who));


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
        FriendsApi::
        friendsOpenAddWindow(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/add-window/open"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | POST
            // Body     | 
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
        FriendsApi::
        friendsPassAll(
            
            std::string clientId
            , 
            
            PassedAllNewFriendRequest passedAllNewFriendRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/pass-all"; //clientId 


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
            // Body     | passedAllNewFriendRequest



            payload = passedAllNewFriendRequest.toJson().dump();

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
        FriendsApi::
        friendsPauseAutoAccept(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/auto-accept/pause"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | POST
            // Body     | 
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
        FriendsApi::
        friendsRemoveFriend(
            
            std::string clientId
            , 
            
            std::string nickName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/{nickName}"; //clientId nickName 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_nickName("{");
                s_nickName.append("nickName");
                s_nickName.append("}");

                int pos = url.find(s_nickName);

                url.erase(pos, s_nickName.length());
                url.insert(pos, stringify(nickName));


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
        FriendsApi::
        friendsRemoveFriendByWxidFromCache(
            
            std::string clientId
            , 
            
            std::string wxid
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/cache/by-wxid/{wxid}"; //clientId wxid 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_wxid("{");
                s_wxid.append("wxid");
                s_wxid.append("}");

                int pos = url.find(s_wxid);

                url.erase(pos, s_wxid.length());
                url.insert(pos, stringify(wxid));


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
        FriendsApi::
        friendsRemoveFriendFromCache(
            
            std::string clientId
            , 
            
            std::string who
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/cache/{who}"; //clientId who 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_who("{");
                s_who.append("who");
                s_who.append("}");

                int pos = url.find(s_who);

                url.erase(pos, s_who.length());
                url.insert(pos, stringify(who));


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
        FriendsApi::
        friendsResumeAutoAccept(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/auto-accept/resume"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | POST
            // Body     | 
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
        FriendsApi::
        friendsStartAutoAccept(
            
            std::string clientId
            , 
            
            PassedAllNewFriendRequest passedAllNewFriendRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/auto-accept/start"; //clientId 


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
            // Body     | passedAllNewFriendRequest



            payload = passedAllNewFriendRequest.toJson().dump();

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
        FriendsApi::
        friendsStopAutoAccept(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/friends/auto-accept/stop"; //clientId 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));


            std::string payload = "";
            // Send Request
            // METHOD | POST
            // Body     | 
            int httpCode = sendRequest(url, "POST", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }



