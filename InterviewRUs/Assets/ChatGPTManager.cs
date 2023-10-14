using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;

public class ChatGPTManager : MonoBehaviour
{


    public OnResponseEvent OnResponse;

    [System.Serializable]
    public class OnResponseEvent : UnityEngine.Events.UnityEvent<string> { }

    private OpenAIApi openAI = new OpenAIApi("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");
    private List<ChatMessage> messages = new List<ChatMessage>();

    public async void AskChatGPT(string newText)
    {
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";

        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);



        if (response.Choices != null && response.Choices.Count > 0)
        {
            // log these 2 lines
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            Debug.Log(chatResponse.Content);

            OnResponse.Invoke(chatResponse.Content);
        }

    }


    // Start called before first frame update
        void Start()
    {

    }


    void update()
    {

    }

}