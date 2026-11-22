#include "MessagesApi.h"

using namespace Tiny;



        Response<
            String
        >
        MessagesApi::
        messagesCloneVoice(
            
            std::string clientId
            , 
            
            CloneVoiceRequest cloneVoiceRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/clone-voice"; //clientId 


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
            // Body     | cloneVoiceRequest



            payload = cloneVoiceRequest.toJson().dump();

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
        MessagesApi::
        messagesFocusInput(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/focus-input"; //clientId 


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
        MessagesApi::
        messagesForwardMultiple(
            
            std::string clientId
            , 
            
            ForwardMultipleMessageRequest forwardMultipleMessageRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/forward-multiple"; //clientId 


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
            // Body     | forwardMultipleMessageRequest



            payload = forwardMultipleMessageRequest.toJson().dump();

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
        MessagesApi::
        messagesForwardSingle(
            
            std::string clientId
            , 
            
            ForwardSingleMessageRequest forwardSingleMessageRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/forward-single"; //clientId 


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
            // Body     | forwardSingleMessageRequest



            payload = forwardSingleMessageRequest.toJson().dump();

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
        MessagesApi::
        messagesGetHistory(
            
            std::string clientId
            , 
            
            std::string who
            , 
            
            std::string date
            , 
            
            std::string startDate
            , 
            
            std::string endDate
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/history"; //clientId 


            // Headers  | 

            // Query    | who date startDate endDate 
            addQueryParam("who",who);
            addQueryParam("date",date);
            addQueryParam("start_date",startDate);
            addQueryParam("end_date",endDate);

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
        MessagesApi::
        messagesGetOnlyTitle(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/only-title"; //clientId 


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
        MessagesApi::
        messagesGetTitle(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/title"; //clientId 


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
        MessagesApi::
        messagesPull(
            
            std::string clientId
            , 
            
            PullMessagesRequest pullMessagesRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/pull"; //clientId 


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
            // Body     | pullMessagesRequest



            payload = pullMessagesRequest.toJson().dump();

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
        MessagesApi::
        messagesReference(
            
            std::string clientId
            , 
            
            ReferenceRequest referenceRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/reference"; //clientId 


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
            // Body     | referenceRequest



            payload = referenceRequest.toJson().dump();

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
        MessagesApi::
        messagesReferenceLast(
            
            std::string clientId
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/reference-last"; //clientId 


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
        MessagesApi::
        messagesSend(
            
            std::string clientId
            , 
            
            SendMessageRequest sendMessageRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send"; //clientId 


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
            // Body     | sendMessageRequest



            payload = sendMessageRequest.toJson().dump();

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
        MessagesApi::
        messagesSendAndClose(
            
            std::string clientId
            , 
            
            SendMessageRequest sendMessageRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-and-close"; //clientId 


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
            // Body     | sendMessageRequest



            payload = sendMessageRequest.toJson().dump();

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
        MessagesApi::
        messagesSendByWxid(
            
            std::string clientId
            , 
            
            SendMessageByWxidRequest sendMessageByWxidRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-by-wxid"; //clientId 


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
            // Body     | sendMessageByWxidRequest



            payload = sendMessageByWxidRequest.toJson().dump();

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
        MessagesApi::
        messagesSendEmoji(
            
            std::string clientId
            , 
            
            SendEmojiRequest sendEmojiRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-emoji"; //clientId 


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
            // Body     | sendEmojiRequest



            payload = sendEmojiRequest.toJson().dump();

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
        MessagesApi::
        messagesSendEmojiAndClose(
            
            std::string clientId
            , 
            
            SendEmojiRequest sendEmojiRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-emoji-and-close"; //clientId 


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
            // Body     | sendEmojiRequest



            payload = sendEmojiRequest.toJson().dump();

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
        MessagesApi::
        messagesSendEmojiByWxid(
            
            std::string clientId
            , 
            
            SendEmojiByWxidRequest sendEmojiByWxidRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-emoji-by-wxid"; //clientId 


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
            // Body     | sendEmojiByWxidRequest



            payload = sendEmojiByWxidRequest.toJson().dump();

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
        MessagesApi::
        messagesSendFile(
            
            std::string clientId
            , 
            
            SendFileRequest sendFileRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-file"; //clientId 


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
            // Body     | sendFileRequest



            payload = sendFileRequest.toJson().dump();

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
        MessagesApi::
        messagesSendFileAndClose(
            
            std::string clientId
            , 
            
            SendFileRequest sendFileRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-file-and-close"; //clientId 


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
            // Body     | sendFileRequest



            payload = sendFileRequest.toJson().dump();

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
        MessagesApi::
        messagesSendFileByWxid(
            
            std::string clientId
            , 
            
            SendFileByWxidRequest sendFileByWxidRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-file-by-wxid"; //clientId 


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
            // Body     | sendFileByWxidRequest



            payload = sendFileByWxidRequest.toJson().dump();

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
        MessagesApi::
        messagesSendVideoChat(
            
            std::string clientId
            , 
            
            WhoRequest whoRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-video-chat"; //clientId 


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
            // Body     | whoRequest



            payload = whoRequest.toJson().dump();

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
        MessagesApi::
        messagesSendVoiceChat(
            
            std::string clientId
            , 
            
            WhoRequest whoRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-voice-chat"; //clientId 


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
            // Body     | whoRequest



            payload = whoRequest.toJson().dump();

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
        MessagesApi::
        messagesSendVoiceChats(
            
            std::string clientId
            , 
            
            SendVoiceChatsRequest sendVoiceChatsRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-voice-chats"; //clientId 


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
            // Body     | sendVoiceChatsRequest



            payload = sendVoiceChatsRequest.toJson().dump();

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
        MessagesApi::
        messagesSendVoiceMessage(
            
            std::string clientId
            , 
            
            SendVoiceMessageRequest sendVoiceMessageRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-voice-message"; //clientId 


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
            // Body     | sendVoiceMessageRequest



            payload = sendVoiceMessageRequest.toJson().dump();

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
        MessagesApi::
        messagesSendVoiceMessageAndClose(
            
            std::string clientId
            , 
            
            SendVoiceMessageRequest sendVoiceMessageRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-voice-message-and-close"; //clientId 


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
            // Body     | sendVoiceMessageRequest



            payload = sendVoiceMessageRequest.toJson().dump();

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
        MessagesApi::
        messagesSendVoiceMessageByWxid(
            
            std::string clientId
            , 
            
            SendVoiceMessageByWxidRequest sendVoiceMessageByWxidRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-voice-message-by-wxid"; //clientId 


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
            // Body     | sendVoiceMessageByWxidRequest



            payload = sendVoiceMessageByWxidRequest.toJson().dump();

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
        MessagesApi::
        messagesSendVoiceMessageTts(
            
            std::string clientId
            , 
            
            SendVoiceMessageTtsRequest sendVoiceMessageTtsRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-voice-message-tts"; //clientId 


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
            // Body     | sendVoiceMessageTtsRequest



            payload = sendVoiceMessageTtsRequest.toJson().dump();

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
        MessagesApi::
        messagesSendVoiceMessageTtsAndClose(
            
            std::string clientId
            , 
            
            SendVoiceMessageTtsRequest sendVoiceMessageTtsRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-voice-message-tts-and-close"; //clientId 


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
            // Body     | sendVoiceMessageTtsRequest



            payload = sendVoiceMessageTtsRequest.toJson().dump();

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
        MessagesApi::
        messagesSendVoiceMessageTtsByWxid(
            
            std::string clientId
            , 
            
            SendVoiceMessageTtsByWxidRequest sendVoiceMessageTtsByWxidRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/send-voice-message-tts-by-wxid"; //clientId 


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
            // Body     | sendVoiceMessageTtsByWxidRequest



            payload = sendVoiceMessageTtsByWxidRequest.toJson().dump();

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
        MessagesApi::
        messagesTap(
            
            std::string clientId
            , 
            
            TapRequest tapRequest
            
        )
        {
            std::string url = basepath + "/api/v1/clients/{clientId}/messages/tap"; //clientId 


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
            // Body     | tapRequest



            payload = tapRequest.toJson().dump();

            int httpCode = sendRequest(url, "POST", reinterpret_cast<uint8_t*>(&payload[0]), payload.length());

            // Handle Request
            String output = getResponseBody();
            std::string output_string = output.c_str();


            Response<String> response(output, httpCode);
            return response;
        }



