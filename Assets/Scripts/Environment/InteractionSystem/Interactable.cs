namespace Environment.InteractionSystem
{
    public interface IInteractable
    {
        public string InteractionPrompt { get; }

        public InteractionPromptUI InteractionPromptUI { get; set; }

        public bool Interact(Interactor interactor);
    }
}