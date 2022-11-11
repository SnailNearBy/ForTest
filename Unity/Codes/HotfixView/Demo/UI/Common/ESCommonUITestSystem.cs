namespace ET
{
    public static class ESCommonUITestSystem
    {
        public static void SetLabelContent(this ESCommonUITest self , string message)
        {
            self.ELabel_labText.text = message;
        }
    }
}