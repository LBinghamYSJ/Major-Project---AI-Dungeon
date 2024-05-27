using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OpenAPIController : MonoBehaviour
{
    public UnityEngine.UI.Image DialogueImage;
    public TMP_Text DialogueText;

    private OpenAIAPI api;
    private List<ChatMessage> messages;
    public void GenerateDialogue()
    {
        DialogueImage.enabled = true;
        DialogueText.enabled = true;
        api = new OpenAIAPI("sk-proj-6NYy28EoLxVd1xBQJaIhT3BlbkFJ8049op5cl6iXgDH3y1Rw"); // <<<<<< WILL LIKELY HAVE TO NOT USE A SYSTEM VARIABLE SO THAT OTHERS CAN UTILISE CHATGPT
        StartConversation();
        GetResponse();
    }

    private void StartConversation()
    {
        messages = new List<ChatMessage>
        {
            new ChatMessage(ChatMessageRole.System, "You are an adventurer inside a dungeon and need help being escorted out of the dungeon. You request to be escorted out of the dungeon. You keep your responses short and to the point.")
        };
    }

    private async void GetResponse()
    {

        // Send the entire chat to OpenAI to get the next message
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1, // How creative the responses are (0-1). 0 = more confident answers. 1 = Might be wildy wrong.
            MaxTokens = 50, // Limit so you do not get too much back.
            Messages = messages // Gives the API the role.
        });

        // Get the response message
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;

        // Update the text field with the response
        DialogueText.text = string.Format("Escort: {0}", responseMessage.Content);

        Invoke("CloseDialogue", 5);

    }

    private void CloseDialogue()
    {
        DialogueImage.enabled = false;
        DialogueText.enabled = false;
    }
}
