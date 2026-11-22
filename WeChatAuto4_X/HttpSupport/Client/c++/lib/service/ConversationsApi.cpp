#include "ConversationsApi.h"

using namespace Tiny;



        Response<
            String
        >
        ConversationsApi::
        conversationsGetAllConversations(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/conversations"; //clientId 


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
        ConversationsApi::
        conversationsGetVisibleConversations(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/conversations/visible"; //clientId 


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
        ConversationsApi::
        conversationsGetVisibleTitles(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/conversations/visible-titles"; //clientId 


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
        ConversationsApi::
        conversationsLocate(
            
            std::string clientId
            , 
            
            LocateConversationRequest locateConversationRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/conversations/locate"; //clientId 


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
            // Body     | locateConversationRequest



            payload = locateConversationRequest.toJson().dump();

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
        ConversationsApi::
        conversationsSearch(
            
            std::string clientId
            , 
            
            SearchConversationRequest searchConversationRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/conversations/search"; //clientId 


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
            // Body     | searchConversationRequest



            payload = searchConversationRequest.toJson().dump();

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
        ConversationsApi::
        conversationsSetDoNotDisturb(
            
            std::string clientId
            , 
            
            SetDoNotDisturbRequest setDoNotDisturbRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/conversations/set-do-not-disturb"; //clientId 


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
            // Body     | setDoNotDisturbRequest



            payload = setDoNotDisturbRequest.toJson().dump();

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
        ConversationsApi::
        conversationsSetTopMost(
            
            std::string clientId
            , 
            
            SetTopMostRequest setTopMostRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/conversations/set-top-most"; //clientId 


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
            // Body     | setTopMostRequest



            payload = setTopMostRequest.toJson().dump();

            int httpCode = sendRequest(url, "POST", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }



