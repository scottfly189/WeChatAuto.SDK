#include "ClientsApi.h"

using namespace Tiny;



        Response<
            String
        >
        ClientsApi::
        clientsClickNotifyIcon(
            
            std::string clientId
            , 
            
            NotifyIconClickRequest notifyIconClickRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/notify-icon/click"; //clientId 


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
            // Body     | notifyIconClickRequest



            payload = notifyIconClickRequest.toJson().dump();

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
        ClientsApi::
        clientsGetAll(
        )
        {
            std::string url = basepath + "/api/v1/clients"; //


            // Headers  | 

            // Query    | 

            // Form     | 





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
        ClientsApi::
        clientsGetInfo(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}"; //clientId 


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
        ClientsApi::
        clientsGetOwnerInfo(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/owner-info"; //clientId 


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
        ClientsApi::
        clientsGetStorageRoot(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/storage-root"; //clientId 


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
        ClientsApi::
        clientsGetVersion(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/version"; //clientId 


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
        ClientsApi::
        clientsSaveAvatar(
            
            std::string clientId
            , 
            
            SaveAvatarRequest saveAvatarRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/avatar"; //clientId 


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
            // Body     | saveAvatarRequest



            payload = saveAvatarRequest.toJson().dump();

            int httpCode = sendRequest(url, "POST", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }



