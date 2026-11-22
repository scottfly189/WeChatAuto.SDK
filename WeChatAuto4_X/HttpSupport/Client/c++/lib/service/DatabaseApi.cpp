#include "DatabaseApi.h"

using namespace Tiny;



        Response<
            String
        >
        DatabaseApi::
        databaseGetContact(
            
            std::string clientId
            , 
            
            std::string userName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/contacts/{userName}"; //clientId userName 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_userName("{");
                s_userName.append("userName");
                s_userName.append("}");

                int pos = url.find(s_userName);

                url.erase(pos, s_userName.length());
                url.insert(pos, stringify(userName));


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
        DatabaseApi::
        databaseGetContactByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/contacts/by-show-name/first"; //clientId 


            // Headers  | 

            // Query    | showName 
            addQueryParam("showName",showName);

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
        DatabaseApi::
        databaseGetContacts(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/contacts"; //clientId 


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
        DatabaseApi::
        databaseGetContactsByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/contacts/by-show-name"; //clientId 


            // Headers  | 

            // Query    | showName 
            addQueryParam("showName",showName);

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
        DatabaseApi::
        databaseGetMaxSortSeq(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/messages/max-sort-seq"; //clientId 


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
        DatabaseApi::
        databaseGetMessages(
            
            std::string clientId
            , 
            
            std::string chatUsername
            , 
            
            int count
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/messages"; //clientId 


            // Headers  | 

            // Query    | chatUsername count 
            addQueryParam("chatUsername",chatUsername);
            addQueryParam("count",count);

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
        DatabaseApi::
        databaseGetNewMessages(
            
            std::string clientId
            , 
            
            long afterSortSeq
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/messages/new"; //clientId 


            // Headers  | 

            // Query    | afterSortSeq 
            addQueryParam("afterSortSeq",afterSortSeq);

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
        DatabaseApi::
        databaseGetRecentMessages(
            
            std::string clientId
            , 
            
            std::string chatUsername
            , 
            
            int count
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/messages/recent"; //clientId 


            // Headers  | 

            // Query    | chatUsername count 
            addQueryParam("chatUsername",chatUsername);
            addQueryParam("count",count);

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
        DatabaseApi::
        databaseGetRoomByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/rooms/by-show-name/first"; //clientId 


            // Headers  | 

            // Query    | showName 
            addQueryParam("showName",showName);

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
        DatabaseApi::
        databaseGetRoomMembers(
            
            std::string clientId
            , 
            
            std::string roomUsername
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/rooms/{roomUsername}/members"; //clientId roomUsername 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_roomUsername("{");
                s_roomUsername.append("roomUsername");
                s_roomUsername.append("}");

                int pos = url.find(s_roomUsername);

                url.erase(pos, s_roomUsername.length());
                url.insert(pos, stringify(roomUsername));


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
        DatabaseApi::
        databaseGetRoomMembersByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/rooms/by-show-name/members"; //clientId 


            // Headers  | 

            // Query    | showName 
            addQueryParam("showName",showName);

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
        DatabaseApi::
        databaseGetRooms(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/rooms"; //clientId 


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
        DatabaseApi::
        databaseGetRoomsByLocalType(
            
            std::string clientId
            , 
            
            int localType
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/rooms/by-local-type"; //clientId 


            // Headers  | 

            // Query    | localType 
            addQueryParam("localType",localType);

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
        DatabaseApi::
        databaseGetRoomsByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/rooms/by-show-name"; //clientId 


            // Headers  | 

            // Query    | showName 
            addQueryParam("showName",showName);

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
        DatabaseApi::
        databaseGetSnsPost(
            
            std::string clientId
            , 
            
            long tid
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/sns/posts/{tid}"; //clientId tid 


            // Headers  | 

            // Query    | 

            // Form     | 



                std::string s_clientId("{");
                s_clientId.append("clientId");
                s_clientId.append("}");

                int pos = url.find(s_clientId);

                url.erase(pos, s_clientId.length());
                url.insert(pos, stringify(clientId));
                std::string s_tid("{");
                s_tid.append("tid");
                s_tid.append("}");

                int pos = url.find(s_tid);

                url.erase(pos, s_tid.length());
                url.insert(pos, stringify(tid));


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
        DatabaseApi::
        databaseGetSnsPosts(
            
            std::string clientId
            , 
            
            int limit
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/sns/posts"; //clientId 


            // Headers  | 

            // Query    | limit 
            addQueryParam("limit",limit);

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
        DatabaseApi::
        databaseGetSnsPostsByDate(
            
            std::string clientId
            , 
            
            std::string date
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/sns/posts/by-date"; //clientId 


            // Headers  | 

            // Query    | date 
            addQueryParam("date",date);

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
        DatabaseApi::
        databaseGetSnsPostsByShowName(
            
            std::string clientId
            , 
            
            std::string showName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/sns/posts/by-show-name"; //clientId 


            // Headers  | 

            // Query    | showName 
            addQueryParam("showName",showName);

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
        DatabaseApi::
        databaseGetSnsPostsByUserName(
            
            std::string clientId
            , 
            
            std::string userName
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/sns/posts/by-user"; //clientId 


            // Headers  | 

            // Query    | userName 
            addQueryParam("userName",userName);

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
        DatabaseApi::
        databaseQuery(
            
            std::string clientId
            , 
            
            DatabaseQueryRequest databaseQueryRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/query"; //clientId 


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
            // Body     | databaseQueryRequest



            payload = databaseQueryRequest.toJson().dump();

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
        DatabaseApi::
        databaseSearchAllMessages(
            
            std::string clientId
            , 
            
            std::string keyword
            , 
            
            std::string chatUsername
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/messages/search-all"; //clientId 


            // Headers  | 

            // Query    | keyword chatUsername 
            addQueryParam("keyword",keyword);
            addQueryParam("chatUsername",chatUsername);

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
        DatabaseApi::
        databaseSearchContacts(
            
            std::string clientId
            , 
            
            std::string keyword
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/contacts/search"; //clientId 


            // Headers  | 

            // Query    | keyword 
            addQueryParam("keyword",keyword);

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
        DatabaseApi::
        databaseSearchMessages(
            
            std::string clientId
            , 
            
            std::string keyword
            , 
            
            std::string chatUsername
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/database/messages/search"; //clientId 


            // Headers  | 

            // Query    | keyword chatUsername 
            addQueryParam("keyword",keyword);
            addQueryParam("chatUsername",chatUsername);

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



